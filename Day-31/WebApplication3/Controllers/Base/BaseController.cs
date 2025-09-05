namespace WebApplication3.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication3.Models;


    public class BaseController : ControllerBase
    {

        protected IActionResult Result<T>(Response<T> response)
        {
            // Set Status based on StatusCode if not already set
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.Accepted)
            {
                response.Status = true;
            }
            else
            {
                response.Status = false;
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                case HttpStatusCode.UnsupportedMediaType:
                    var BadRequestObjectResult = new BadRequestObjectResult(response);
                    BadRequestObjectResult.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    return BadRequestObjectResult;
                default:
                    return new BadRequestObjectResult(response);
            }
        }
    }
