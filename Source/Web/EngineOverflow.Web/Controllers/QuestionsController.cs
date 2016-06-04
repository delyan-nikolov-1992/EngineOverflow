using EngineOverflow.Web.InputModels.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EngineOverflow.Web.Controllers
{
    public class QuestionsController : Controller
    {
        // /questions/5/difference-between-gasoline-engine-and-diesel-engine
        public ActionResult Display(int id, string url, int page = 1)
        {
            return Content(id + " " + url);
        }

        // /questions/tagged/steam
        public ActionResult GetByTag(string tag)
        {
            return Content(tag);
        }

        [HttpGet]
        public ActionResult Ask()
        {
            return Content("GET");
        }

        [HttpPost]
        public ActionResult Ask(AskInputModel input)
        {
            return Content("POST");
        }
    }
}
