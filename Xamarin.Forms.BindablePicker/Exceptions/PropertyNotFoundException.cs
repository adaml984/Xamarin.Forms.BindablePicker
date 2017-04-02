using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.BindablePicker.Exceptions
{
	public class PropertyNotFoundException : Exception
	{
		public PropertyNotFoundException()
		{
		}

		public PropertyNotFoundException(string message) : base(message)
		{
		}

		public PropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
