using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
	public class TextAnalyzer : Analyzer
	{
		public TextAnalyzer(string SearchText, bool FilterWords) : base(SearchText, FilterWords)
		{

		}
		public override List<WordsCount> CountTotalWords()
		{
			var words = GetAllWords(_searchText);
			if (_filterWords)
			{
				words = FilterWords(words);
			}

			return words.GroupBy(x => x).Select(x => new WordsCount {Word = x.Key, Count = x.Count() }).ToList();
		}

		public override List<WordsCount> CountTotalExternalLink()
		{
			var validLinks = new List<string>();
			foreach (var word in GetAllLinks(_searchText))
			{
				if (AppLogic.IsUrl(word))
				{
					validLinks.Add(word);
				}
			}

			return validLinks.GroupBy(x => x).Select(x => new WordsCount { Word = x.Key, Count = x.Count() }).ToList(); ;
		}

		public override List<WordsCount> CountTotalMetaTags()
		{
			return new List<WordsCount>();
		}
	}
}
