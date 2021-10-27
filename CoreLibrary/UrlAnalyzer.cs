using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace CoreLibrary
{
	public class UrlAnalyzer : Analyzer
	{
		private string _htmlCode;

		public UrlAnalyzer(string SearchText, bool FilterWords) : base(SearchText, FilterWords)
		{
			using (var client = new WebClient())
			{
				_htmlCode = client.DownloadString(_searchText);
			}
		}
		public override List<WordsCount> CountTotalExternalLink()
		{
			try
			{
				var words = new List<string>();
				var doc = new HtmlDocument();
				doc.LoadHtml(_htmlCode);

				var list = doc.DocumentNode.SelectNodes("//a[@href]");
				if (list?.Count > 0)
				{
					foreach (var link in list)
					{
						var url = link.GetAttributeValue("href", "");
						if (AppLogic.IsUrl(url))
						{
							words.Add(url);
						}
					}

					return words.GroupBy(x => x).Select(x => new WordsCount { Word = x.Key, Count = x.Count() }).ToList();
				}
				return new List<WordsCount>();
			}
			catch (Exception ex)
			{
				Log.AddErrorLog(ex);
				throw ex;
			}
			
		}

		public override List<WordsCount> CountTotalMetaTags()
		{
			try
			{
				var acceptableMetas = new List<string> { "description", "title", "og:description", "og:title" };

				var words = new List<string>();
				var doc = new HtmlDocument();
				doc.LoadHtml(_htmlCode);

				var list = doc.DocumentNode.SelectNodes("//meta");
				if (list?.Count > 0)
				{
					foreach (var node in list)
					{
						var name = node.GetAttributeValue("name", "");
						var property = node.GetAttributeValue("property", "");
						if (acceptableMetas.Contains(name.ToLowerInvariant()) || acceptableMetas.Contains(property.ToLowerInvariant()))
						{
							words.AddRange(GetAllWords(node.GetAttributeValue("content", "")));
						}
					}

					if (_filterWords)
					{
						words = FilterWords(words);
					}

					return words.GroupBy(x => x).Select(x => new WordsCount { Word = x.Key, Count = x.Count() }).ToList();
				}
				return new List<WordsCount>();
			}
			catch (Exception ex)
			{
				Log.AddErrorLog(ex);
				throw ex;
			}
		}

		public override List<WordsCount> CountTotalWords()
		{
			try
			{
				var words = new List<string>();
				var doc = new HtmlDocument();
				doc.LoadHtml(_htmlCode);

				//clean up not displayed words
				foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//comment() | //script | //style | //head"))
				{
					node.ParentNode.RemoveChild(node);
				}

				foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
				{
					var splitWords = GetAllWords(HttpUtility.HtmlDecode(node.InnerText));
					foreach (var word in splitWords)
					{
						if (!string.IsNullOrEmpty(word))
						{
							words.Add(word);
						}
					}
				}
				if (words.Count > 0)
				{
					if (_filterWords)
					{
						words = FilterWords(words);
					}
					return words.GroupBy(x => x).Select(x => new WordsCount { Word = x.Key, Count = x.Count() }).ToList();
				}

				return new List<WordsCount>();
			}
			catch (Exception ex)
			{
				Log.AddErrorLog(ex);
				throw ex;
			}
			
		}
	}
}
