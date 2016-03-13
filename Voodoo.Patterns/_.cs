using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Logging;

namespace Voodoo
{
	public static class _
	{
		public static string ToDebugString(object @object)
		{
			return @object.ToDebugString();
		}
		public static string ToCode(object @object)
		{
			return @object.ToCode();
		}
		public static T To<T>(object @object)
		{
			return @object.To<T>();
		}
		public static T As<T>(object @object)
		{
			return @object.As<T>();
		}

		public static void Log(string @string)
		{
			LogManager.Log(@string);
		}
	}
}
