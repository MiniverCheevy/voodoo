using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Voodoo.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages.Paging;

namespace Voodoo.Tests.Voodoo.Linq
{
    [TestClass]
    public class QueryableExtensionTests
	{
		private readonly Person person = new Person();

		[TestMethod]
		public void GetName_StringProperty_ReturnsName()
		{
			var result = person.GetName(c => c.UserName);
			Assert.AreEqual("UserName", result);
		}

		[TestMethod]
		public void GetName_IntValue_ReturnsName()
		{
			var result = person.GetName(c => c.Id);
			Assert.AreEqual("Id", result);
		}

		[TestMethod]
		public void GetName_BoolValue_ReturnsName()
		{
			var result = person.GetName(c => c.IsTrue);
			Assert.AreEqual("IsTrue", result);
		}

		[TestMethod]
		public void OrderByDescending_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var result = list.OrderByDescending("Id").ToArray();
			Assert.AreEqual(4, result[0].Id);
			Assert.AreEqual(3, result[1].Id);
			Assert.AreEqual(2, result[2].Id);
			Assert.AreEqual(1, result[3].Id);
		}

		[TestMethod]
		public void PagedResult_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = list.ToPagedResponse(paging);
			var result = pagedResult.Data;
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
			Assert.AreEqual(2, pagedResult.Data.Count);
			Assert.AreEqual(3, result[0].Id);
			Assert.AreEqual(4, result[1].Id);
		}
#if !NET40
        [TestMethod]
		public async Task PagedResultAsync_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = await list.ToPagedResponseAsync(paging);
			var result = pagedResult.Data;
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
			Assert.AreEqual(2, pagedResult.Data.Count);
			Assert.AreEqual(3, result[0].Id);
			Assert.AreEqual(4, result[1].Id);
		}
#endif

		[TestMethod]
		public void PagedResult_ValidValue_ReturnsConvertedListList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = list.ToPagedResponse(paging,
				c => new NameValuePair { Name = c.UserName, Value = c.Id.ToString() });
			var result = pagedResult.Data;
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
			Assert.AreEqual(2, pagedResult.Data.Count);
			Assert.AreEqual("3", result[0].Value);
			Assert.AreEqual("4", result[1].Value);
		}

		[TestMethod]
		public void PagedResult_CustomStateIsReturned_ReturnsConvertedListList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest
			{ PageNumber = 2, PageSize = 2, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);
			var response = new PagedResponse<NameValuePair>();
			response.From(pagedResult,
				c => new NameValuePair { Name = c.UserName, Value = c.Id.ToString() });

			Assert.IsTrue(response.State is PersonPagedRequest);
			Assert.AreEqual(paging.Text, response.State.As<PersonPagedRequest>().Text);
		}

		[TestMethod]
		public void PagedResult_EvenRecordsPerPage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);

			Assert.AreEqual(2, pagedResult.State.TotalPages);
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
		}

		[TestMethod]
		public void PagedResult_OddRecordsPerPage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 3, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);


			Assert.AreEqual(2, pagedResult.State.TotalPages);
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
		}

		[TestMethod]
		public void PagedResult_AllRecordsInOnePage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 1, PageSize = 4, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);

			Assert.AreEqual(1, pagedResult.State.TotalPages);
			Assert.AreEqual(4, pagedResult.State.TotalRecords);
		}

	    [TestMethod]
	    public void PagedResult_RequestIsEmpty_IsOk()
	    {
	        var list = GetTestList().AsQueryable();
	        var pagedResult = list.ToPagedResponse(new PersonPagedRequest());

	        Assert.AreEqual(1, pagedResult.State.TotalPages);
	        Assert.AreEqual(4, pagedResult.State.TotalRecords);
	    }
	    public void PagedResult_PreviousRequestIsEmpty_IsOk()
	    {
	        var list = GetTestList().AsQueryable();
	        var pagedResult = list.ToPagedResponse(new GridState(null));

	        Assert.AreEqual(1, pagedResult.State.TotalPages);
	        Assert.AreEqual(4, pagedResult.State.TotalRecords);
	    }

        [TestMethod]
		public void SortOnNestedProperties_IsOk()
		{
			var list = GetComplexList();
			var sorted = list.AsQueryable().OrderByDynamic("ComplexObject.DateAndTime");
			Assert.IsTrue(sorted.First().ComplexObject.DateAndTime < sorted.Last().ComplexObject.DateAndTime);
			Assert.IsTrue(sorted.Count() == 2);
		}
		[TestMethod]
		public void SortOnInheritedProperties_IsOk()
		{
			var list = GetListWithInheritedObjects();
			var sorted = list.AsQueryable().OrderByDynamic("ComplexObject.DateAndTime");
			Assert.IsTrue(sorted.First().ComplexObject.DateAndTime < sorted.Last().ComplexObject.DateAndTime);
			Assert.IsTrue(sorted.Count() == 2);
		}

		[TestMethod]
		public void SortOnMultipeProperties_IsOk()
		{
			var list = GetTestList();
			var sorted = list.AsQueryable().OrderByDescending("LastName,FirstName");
			Assert.IsTrue(sorted.First().FirstName == "Jill");
			Assert.IsTrue(sorted.First().LastName == "Smith");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void SortOnNonExistantNestedProperties_IsNotOk()
		{
			 GetComplexList().AsQueryable().OrderByDynamic("ComplexObject.DateTime");
		}
	    [TestMethod]
        public void ApplyTokenizedContainsSearch_Zero_Parameter_IsOk()
	    {
	        var list = GetTestList().AsQueryable();
	        var result = list.ToTokenizedContainsSearchQuery(" ", c => c.FirstName, c => c.LastName);
	        result.Count().Should().Be(4);
	    }
        [TestMethod]
        public void ApplyTokenizedContainsSearch_OneParameter_IsOk()
        {
            var list = GetTestList().AsQueryable();
            var result = list.ToTokenizedContainsSearchQuery("Smith", c => c.FirstName, c => c.LastName);
            result.Count().Should().Be(2);
        }
	    [TestMethod]
        public void ApplyTokenizedContainsSearch_OneParameterAndAdditionalQuery_IsOk()
	    {
	        var list = GetTestList().AsQueryable();
	        var result = list.ToTokenizedContainsSearchQuery("Smith", c => c.FirstName, c => c.LastName).Where(c=>c.IsTrue);
	        result.Count().Should().Be(1);
	    }
        [TestMethod]
        public void ApplyTokenizedContainsSearch_TwoParameter_IsOk()
	    {
	        var list = GetTestList().AsQueryable();
	        var result = list.ToTokenizedContainsSearchQuery("Smith Jack", c => c.FirstName, c => c.LastName);
	        result.Count().Should().Be(1);
	    }
        public List<ClassToReflect> GetComplexList()
		{
			return new List<ClassToReflect>()
			{
				new ClassToReflect {ComplexObject = new ClassWithDate {DateAndTime = DateTime.Now.AddDays(2)}},
				new ClassToReflect {ComplexObject = new ClassWithDate {DateAndTime = DateTime.Now.AddDays(1)}},

			};
		}
		public List<ClassWithAncestor> GetListWithInheritedObjects()
		{
			return new List<ClassWithAncestor>()
			{
				new ClassWithAncestor {ComplexObject = new ClassWithDate {DateAndTime = DateTime.Now.AddDays(2)}},
				new ClassWithAncestor {ComplexObject = new ClassWithDate {DateAndTime = DateTime.Now.AddDays(1)}},

			};
		}

		public List<Person> GetTestList()
		{
			return new List<Person>
			{
				new Person {Id = 3,FirstName="Bob",LastName="Robertson", UserName = "Orange", IsTrue = true},
				new Person {Id = 2,FirstName="John",LastName="Johnson", UserName = "Green", IsTrue = true},
				new Person {Id = 1,FirstName="Jack",LastName="Smith", UserName = "Red", IsTrue = false},
				new Person {Id = 4,FirstName="Jill",LastName="Smith", UserName = "Yellow", IsTrue = true}
			};
		}
	}
}