using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Linq
{
	public class QueryableExtensionTests
	{
		private readonly Person person = new Person();

		[Fact]
		public void GetName_StringProperty_ReturnsName()
		{
			var result = person.GetName(c => c.UserName);
			Assert.Equal("UserName", result);
		}

		[Fact]
		public void GetName_IntValue_ReturnsName()
		{
			var result = person.GetName(c => c.Id);
			Assert.Equal("Id", result);
		}

		[Fact]
		public void GetName_BoolValue_ReturnsName()
		{
			var result = person.GetName(c => c.IsTrue);
			Assert.Equal("IsTrue", result);
		}

		[Fact]
		public void OrderByDescending_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var result = list.OrderByDescending("Id").ToArray();
			Assert.Equal(4, result[0].Id);
			Assert.Equal(3, result[1].Id);
			Assert.Equal(2, result[2].Id);
			Assert.Equal(1, result[3].Id);
		}

		[Fact]
		public void PagedResult_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = list.ToPagedResponse(paging);
			var result = pagedResult.Data;
			Assert.Equal(4, pagedResult.State.TotalRecords);
			Assert.Equal(2, pagedResult.Data.Count);
			Assert.Equal(3, result[0].Id);
			Assert.Equal(4, result[1].Id);
		}

		[Fact]
		public async Task PagedResultAsync_ValidValue_ReturnsSortedList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = await list.ToPagedResponseAsync(paging);
			var result = pagedResult.Data;
			Assert.Equal(4, pagedResult.State.TotalRecords);
			Assert.Equal(2, pagedResult.Data.Count);
			Assert.Equal(3, result[0].Id);
			Assert.Equal(4, result[1].Id);
		}

		[Fact]
		public void PagedResult_ValidValue_ReturnsConvertedListList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

			var pagedResult = list.ToPagedResponse(paging,
				c => new NameValuePair { Name = c.UserName, Value = c.Id.ToString() });
			var result = pagedResult.Data;
			Assert.Equal(4, pagedResult.State.TotalRecords);
			Assert.Equal(2, pagedResult.Data.Count);
			Assert.Equal("3", result[0].Value);
			Assert.Equal("4", result[1].Value);
		}

		[Fact]
		public void PagedResult_CustomStateIsReturned_ReturnsConvertedListList()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest
			{ PageNumber = 2, PageSize = 2, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);
			var response = new PagedResponse<NameValuePair>();
			response.From(pagedResult,
				c => new NameValuePair { Name = c.UserName, Value = c.Id.ToString() });

			Assert.True(response.State is PersonPagedRequest);
			Assert.Equal(paging.Text, response.State.As<PersonPagedRequest>().Text);
		}

		[Fact]
		public void PagedResult_EvenRecordsPerPage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);

			Assert.Equal(2, pagedResult.State.TotalPages);
			Assert.Equal(4, pagedResult.State.TotalRecords);
		}

		[Fact]
		public void PagedResult_OddRecordsPerPage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 3, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);


			Assert.Equal(2, pagedResult.State.TotalPages);
			Assert.Equal(4, pagedResult.State.TotalRecords);
		}

		[Fact]
		public void PagedResult_AllRecordsInOnePage_TotalPagesIsAccurate()
		{
			var list = GetTestList().AsQueryable();
			var paging = new PersonPagedRequest { PageNumber = 1, PageSize = 4, Text = "foo" };

			var pagedResult = list.ToPagedResponse(paging);

			Assert.Equal(1, pagedResult.State.TotalPages);
			Assert.Equal(4, pagedResult.State.TotalRecords);
		}
		[Fact]
		public void SortOnNestedProperties_IsOk()
		{
			var list = GetComplexList();
			var sorted = list.AsQueryable().OrderByDynamic("ComplexObject.DateAndTime");
			Assert.True(sorted.First().ComplexObject.DateAndTime < sorted.Last().ComplexObject.DateAndTime);
			Assert.True(sorted.Count() == 2);
		}
		[Fact]
		public void SortOnInheritedProperties_IsOk()
		{
			var list = GetListWithInheritedObjects();
			var sorted = list.AsQueryable().OrderByDynamic("ComplexObject.DateAndTime");
			Assert.True(sorted.First().ComplexObject.DateAndTime < sorted.Last().ComplexObject.DateAndTime);
			Assert.True(sorted.Count() == 2);
		}

		[Fact]
		public void SortOnMultipeProperties_IsOk()
		{
			var list = GetTestList();
			var sorted = list.AsQueryable().OrderByDescending("LastName,FirstName");
			Assert.True(sorted.First().FirstName == "Jill");
			Assert.True(sorted.First().LastName == "Smith");
		}

		[Fact]
		public void SortOnNonExistantNestedProperties_IsNotOk()
		{
			Exception ex = Assert.Throws<ArgumentException>(() => GetComplexList().AsQueryable().OrderByDynamic("ComplexObject.DateTime"));
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
				new Person {Id = 1,FirstName="Jack",LastName="Smith", UserName = "Red", IsTrue = true},
				new Person {Id = 4,FirstName="Jill",LastName="Smith", UserName = "Yellow", IsTrue = true}
			};
		}
	}
}