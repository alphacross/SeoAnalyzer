using CoreLibrary;
using SeoAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeoAnalyzer.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var filter = new FilterModel();
			filter.FilterWords = true;
			filter.CountTotalWords = true;
			filter.CountMetaTags = true;
			filter.CountExternalLink = true;

			return View(filter);
		}

		[HttpPost]
		public JsonResult AnalyzeText(FilterModel mod)
		{
			var res = new AppLogic.StrBool();
			if (string.IsNullOrEmpty(mod.SearchText))
			{
				res.IsValid = false;
				res.Message = "Search is empty!";
				return Json(res);
			}

			try
			{
				Analyzer analyzer;
				if (AppLogic.IsUrl(mod.SearchText))
				{
					analyzer = new UrlAnalyzer(mod.SearchText, mod.FilterWords);
				}
				else
				{
					analyzer = new TextAnalyzer(mod.SearchText, mod.FilterWords);
				}

				var seoResult = new ResultModel();

				if (mod.CountTotalWords)
				{
					seoResult.IsCountTotalWords = mod.CountTotalWords;
					seoResult.TotalWords = analyzer.CountTotalWords();
				}
				if (mod.CountMetaTags)
				{
					seoResult.IsCountTotalTags = mod.CountMetaTags;
					seoResult.TotalTags = analyzer.CountTotalMetaTags();
				}
				if (mod.CountExternalLink)
				{
					seoResult.IsCountTotalLinks = mod.CountExternalLink;
					seoResult.TotalLinks = analyzer.CountTotalExternalLink();
				}

				res.Message = this.RenderRazorViewToString("_Result", seoResult);
			}
			catch (Exception ex)
			{
				Log.AddErrorLog(ex);
				res.IsValid = false;
				res.Message = "An error has occured! Please contact admin";
			}

			return Json(res, JsonRequestBehavior.AllowGet);
		}
	}
}