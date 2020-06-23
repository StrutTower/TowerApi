using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TowerApiLib.Logic;
using TowerApiLib.Models;

namespace Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class SteamController : ControllerBase {
        private readonly LogicService _logicService;

        public SteamController(LogicService logicService) {
            _logicService = logicService;
        }

        /// <summary>
        /// Returns the details about a steam user
        /// </summary>
        /// <remarks>
        /// Remarks here
        /// </remarks>
        /// <param name="id">Steam ID of the user</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public SteamUserDetails Get(long id) {
            return _logicService.SteamLogic.GetByUserID(id);
        }
    }
}
