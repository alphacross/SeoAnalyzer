using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
	public class FilterModel
	{
		public string SearchText { get; set; }
		public bool FilterWords { get; set; }
		public bool CountTotalWords { get; set; }
		public bool CountMetaTags { get; set; }
		public bool CountExternalLink { get; set; }
	}
}