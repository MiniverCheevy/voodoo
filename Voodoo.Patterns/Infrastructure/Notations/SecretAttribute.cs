using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voodoo.Infrastructure.Notations
{
    /// <summary>
    /// Voodoo based logging and code generation will ignore properties with this attribute
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, Inherited = true)]
    public class SecretAttribute : Attribute
    {
    }
}