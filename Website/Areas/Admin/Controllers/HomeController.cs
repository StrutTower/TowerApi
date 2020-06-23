using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers {
    [Area("Admin")]
    //[Route("Admin/[controller]/[action]")]
    //[Role(RoleTypes.Admin)]
    public class HomeController : CustomController {
        public IActionResult Index() {
            return View();
        }
    }
}
