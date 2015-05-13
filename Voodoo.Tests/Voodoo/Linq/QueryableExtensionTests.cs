using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Linq;
using Voodoo.Messages;
using Voodoo.Tests.TestClasses;

namespace Voodoo.Tests.Voodoo.Linq
{
    [TestClass]
    public class QueryableExtensionTests
    {
        private readonly Person person = new Person();

        [TestMethod]
        public void GetName_StringProperty_ReturnsName()
        {
            var result = person.GetName(c => c.Name);
            Assert.AreEqual("Name", result);
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
            var paging = new PersonPagedRequest {PageNumber = 2, PageSize = 2 };

            var pagedResult = list.ToPagedResponse(paging);
            var result = pagedResult.Data;
            Assert.AreEqual(4, pagedResult.State.TotalRecords);
            Assert.AreEqual(2,pagedResult.Data.Count);
            Assert.AreEqual(3, result[0].Id);
            Assert.AreEqual(4, result[1].Id);
            
        }
        [TestMethod]
        public void PagedResult_ValidValue_ReturnsConvertedListList()
        {
            var list = GetTestList().AsQueryable();
            var paging = new PersonPagedRequest { PageNumber = 2, PageSize = 2 };

            var pagedResult = list.ToPagedResponse(paging, 
                c=> new NameValuePair(){Name=c.Name, Value = c.Id.ToString()});
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
            { PageNumber = 2, PageSize = 2 , Text="foo"};

            var pagedResult = list.ToPagedResponse(paging);
            var response = new PagedResponse<NameValuePair>();
                response.From(pagedResult,
                c => new NameValuePair() { Name = c.Name, Value = c.Id.ToString() });

            Assert.IsTrue(response.State is PersonPagedRequest);
            Assert.AreEqual(paging.Text , response.State.As<PersonPagedRequest>().Text);
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

        public List<Person> GetTestList()
        {
            return new List<Person>()
                {
                new Person{Id=3, Name = "Orange", IsTrue =true},
                new Person{Id=2, Name = "Green", IsTrue =true},                
                new Person{Id=1, Name = "Red", IsTrue =true},
                new Person{Id=4, Name = "Yellow", IsTrue =true},
                }
                ;
        }
    }
}