using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class DateController : ControllerBase {
        [HttpGet]
        [Route("UnixToString")]
        public IActionResult UnixSecondsToString(long? seconds, long? milliseconds, string format = "M/d/yyyy h:mm tt") {
            DateTime dateTime = new DateTime(1970, 1, 1);
            if (seconds.HasValue) {
                if (seconds >= 253402300800) {
                    UnprocessableEntity(new {
                        error = true,
                        message = "Seconds cannot be greater than or equal to 253402300800"
                    });
                }
                dateTime = dateTime.AddSeconds(seconds.Value);
            } else if (milliseconds.HasValue) {
                if (milliseconds >= 253402300800000) {
                    UnprocessableEntity(new {
                        error = true,
                        message = "Milliseconds cannot be greater than or equal to 253402300800000"
                    });
                }
                dateTime = dateTime.AddMilliseconds(milliseconds.Value);
            } else {
                UnprocessableEntity(new {
                    error = true,
                    message = "Missing seconds or milliseconds parameter"
                });
            }
            return Ok(dateTime.ToString(format));
        }
    }
}
