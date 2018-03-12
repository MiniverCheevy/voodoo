using System.Collections.Generic;
using System.Linq;
using Voodoo.Helpers;
using Voodoo.Tests.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Voodoo.Tests.Voodoo.Helpers
{
    [TestClass]
    public class CollectionChangeSetTests
    {
        [TestMethod]
        public void ctor_0AddedOr0Deleted_0AddedOr0Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};

            var modified = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};
            var helper = new CollectionChangeSet(source.Select(c => c.Id), modified.Select(c => c.Id));

            Assert.AreEqual(0, helper.Added.Count());
            Assert.AreEqual(2, helper.Edited.Count());
            Assert.AreEqual(0, helper.Deleted.Count());
            Assert.IsFalse(helper.AreDifferent());
        }

        [TestMethod]
        public void ctor_0Added1Deleted_0Added1Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};
            var modified = new List<DataObject> {new DataObject {Id = 1}};
            var helper = new CollectionChangeSet(source.Select(c => c.Id), modified.Select(c => c.Id));
            Assert.AreEqual(0, helper.Added.Count());
            Assert.AreEqual(1, helper.Edited.Count());
            Assert.AreEqual(1, helper.Deleted.Count());
            Assert.IsTrue(helper.AreDifferent());
        }

        [TestMethod]
        public void ctor_1Added0Deleted_1Added0Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};

            var modified = new List<DataObject>
            {
                new DataObject {Id = 1},
                new DataObject {Id = 2},
                new DataObject {Id = 0}
            };

            var helper = new CollectionChangeSet(source.Select(c => c.Id), modified.Select(c => c.Id));
            Assert.AreEqual(1, helper.Added.Count());
            Assert.AreEqual(2, helper.Edited.Count());
            Assert.AreEqual(0, helper.Deleted.Count());
            Assert.IsTrue(helper.AreDifferent());
        }

        [TestMethod]
        public void ctor_2Added1Deleted_2Added1Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};

            var modified = new List<DataObject>
            {
                new DataObject {Id = 1},
                new DataObject {Id = 0},
                new DataObject {Id = 0}
            };
            var helper = new CollectionChangeSet(source.Select(c => c.Id), modified.Select(c => c.Id));
            Assert.AreEqual(2, helper.Added.Count());
            Assert.AreEqual(1, helper.Edited.Count());
            Assert.AreEqual(1, helper.Deleted.Count());
            Assert.IsTrue(helper.AreDifferent());
        }
    }
}