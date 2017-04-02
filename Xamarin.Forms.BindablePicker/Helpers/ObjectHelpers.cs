using System.Reflection;
using Xamarin.Forms.BindablePicker.Exceptions;

namespace Xamarin.Forms.BindablePicker.Helpers
{
	public static class ObjectHelpers
	{
		public static object GetPropertyValue(this object @object, string propertyName)
		{
			var typeInfo = @object.GetType().GetTypeInfo();

			var propertyInfo = typeInfo.GetDeclaredProperty(propertyName);
			if (propertyInfo == null)
				throw new PropertyNotFoundException($"Property {propertyName} not found on {typeInfo.Name} type.");

			return propertyInfo.GetValue(@object);
		}

		public static Tout As<Tout>(this object @object) where Tout : class
		{
			Tout res = default(Tout);
			res = @object as Tout;
			return res;
		}
	}
}
