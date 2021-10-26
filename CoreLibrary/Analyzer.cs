using CoreLibrary.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreLibrary
{
	public abstract class Analyzer
	{
		public class WordsCount
		{
			public string Word { get; set; }
			public int Count { get; set; }
		}

		protected string _searchText;
		protected bool _filterWords;

		protected Analyzer(string SearchText, bool FilterWords)
		{
			_searchText = SearchText;
			_filterWords = FilterWords;
		}

		public abstract List<WordsCount> CountTotalWords();
		public abstract List<WordsCount> CountTotalMetaTags();
		public abstract List<WordsCount> CountTotalExternalLink();

		public List<string> GetAllWords(string text)
		{
			var list = new List<string>();

			list.AddRange(text.ToLowerInvariant().Split(new string[] { ".", ",", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => CleanUpWord(x)).Where(x => !string.IsNullOrEmpty(x)));

			return list;
		}

		public List<string> GetAllLinks(string text)
		{
			var list = new List<string>();

			list.AddRange(text.ToLowerInvariant().Split(new string[] { ",", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));

			return list;
		}

		public string CleanUpWord(string word)
		{
			return Regex.Replace(word, @"([^a-zA-Z. ]+)", "").Trim();
			//return Regex.Replace(word, @"([^a-zA-Z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF. ]+)", "").Trim();
		}

		public List<string> FilterWords(List<string> words)
		{
			var stopWords = new List<string>(StopWords.StopWordList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
			return words.Where(x => !stopWords.Contains(x)).ToList();
		}
	}
}
