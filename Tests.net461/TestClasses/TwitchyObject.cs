using System;

namespace Voodoo.Tests.TestClasses
{
    public class TwitchyObject
    {
        public string BrokenProperty
        {
            get { throw new NotImplementedException(); }
        }

        public void MethodThatReturnsNothing()
        {
        }
    }
}