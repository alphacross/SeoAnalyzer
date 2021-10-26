using CoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeoAnalyzer.Models
{
	public class ResultModel
	{
		public bool IsCountTotalWords { get; set; }
		public bool IsCountTotalTags { get; set; }
		public bool IsCountTotalLinks { get; set; }
		public List<Analyzer.WordsCount> TotalWords { get; set; }
		public List<Analyzer.WordsCount> TotalTags { get; set; }
		public List<Analyzer.WordsCount> TotalLinks { get; set; }
	}
}