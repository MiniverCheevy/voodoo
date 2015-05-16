using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Voodoo.Domain.Models;


namespace Voodoo.Tests
{
    public class TestDataHelper
    {
        public class VoodooContextInitializer : DropCreateDatabaseAlways<VoodooContext>
        {
            protected override void Seed(VoodooContext context)
            {
                base.Seed(context);
            }
        }
        public static void CreateDatabase()
        {
            Database.SetInitializer <VoodooContext>(new VoodooContextInitializer());
            

            
            using (var ctx = new VoodooContext())
            {
                ctx.Database.Delete();
                ctx.Database.Create();
            }
        }
    }
}
