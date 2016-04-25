using System;

namespace Voodoo.Infrastructure.Notations
{

	/// <summary>
	/// Adds target type to generrated mapping file
	/// </summary>
	public class MapsToAttribute : Attribute
	{
		public MapsToAttribute(Type type)
		{
			this.Type = type;
		}

		public Type Type { get; set; }
	}
}