using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
	public class AppLogic
	{
		public class StrBool
		{
			public bool IsValid { get; set; } = true;
			public string Message { get; set; }
		}

		public static bool IsUrl(string url)
		{
			return Uri.TryCreate(url, UriKind.Absolute, out Uri res) && (res.Scheme == Uri.UriSchemeHttp || res.Scheme == Uri.UriSchemeHttps);
		}
	}
}
