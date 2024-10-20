using System.Collections.Generic;
using System.Linq;
using Voodoo.Helpers;
using Voodoo.Tests.TestClasses;
using Xunit;

namespace Voodoo.Tests.Voodoo.Helpers
{
    
    public class CollectionReconcilerTests
    {
        [Fact]
        public void ctor_0AddedOr0Deleted_0AddedOr0Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};
            var modified = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};
            var helper = new CollectionReconciler<DataObject, DataObject, int>(source, modified, c => c.Id, c => c.Id);

            Assert.Empty(helper.Added);
            Assert.Equal(2, helper.Edited.Count());
            Assert.Empty(helper.Deleted);
        }

        [Fact]
        public void ctor_0Added1Deleted_0Added1Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};
            var modified = new List<DataObject> {new DataObject {Id = 1}};
            var helper = new CollectionReconciler<DataObject, DataObject, int>(source, modified, c => c.Id, c => c.Id);
            Assert.Empty(helper.Added);
            Assert.Single(helper.Edited);
            Assert.Single(helper.Deleted);
        }

        [Fact]
        public void ctor_1Added0Deleted_1Added0Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};

            var modified = new List<DataObject>
            {
                new DataObject {Id = 1},
                new DataObject {Id = 2},
                new DataObject {Id = 0}
            };

            var helper = new CollectionReconciler<DataObject, DataObject, int>(source, modified, c => c.Id, c => c.Id);
            Assert.Single(helper.Added);
            Assert.Equal(2, helper.Edited.Count());
            Assert.Empty(helper.Deleted);
        }

        [Fact]
        public void ctor_2Added1Deleted_2Added1Deleted()
        {
            var source = new List<DataObject> {new DataObject {Id = 1}, new DataObject {Id = 2}};

            var modified = new List<DataObject>
            {
                new DataObject {Id = 1},
                new DataObject {Id = 0},
                new DataObject {Id = 0}
            };
            var helper = new CollectionReconciler<DataObject, DataObject, int>(source, modified, c => c.Id, c => c.Id);
            Assert.Equal(2, helper.Added.Count());
            Assert.Single(helper.Edited);
            Assert.Single(helper.Deleted);
        }
    }
}