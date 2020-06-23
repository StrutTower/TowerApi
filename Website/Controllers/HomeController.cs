using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Website.Controllers {
    public class HomeController : Controller {
        public HomeController() {

        }

        public IActionResult Index() {
            DateTime max = DateTime.MaxValue;
            DateTime epoch = new DateTime(1970, 1, 1);

            var test = max.Subtract(epoch).TotalSeconds;
            var test2 = max.Subtract(epoch).TotalMilliseconds;

            return View();
        }

        public IActionResult ReleaseNotes() {
            return View();
        }
    }
}
