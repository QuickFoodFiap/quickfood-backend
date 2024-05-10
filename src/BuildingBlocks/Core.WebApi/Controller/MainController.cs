using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Core.WebApi.Controller
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Produces("application/json")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", null)]
    [SwaggerResponse((int)HttpStatusCode.Created, "Created", null)]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "No Content", null)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request", null)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized", null)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, "Forbidden", null)]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Not Found", null)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal Server Error", null)]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable", null)]
    public abstract class MainController : ControllerBase
    {
        protected IActionResult SuccessOk(object data) =>
            Ok(new BaseApiResponse { Success = true, Data = data });

        protected IActionResult SuccessCreated(string url, object data) =>
            Created(url, new BaseApiResponse { Success = true, Data = data });

        protected IActionResult SuccessNoContent() => NoContent();

        protected IActionResult ErrorBadRequestPutId()
        {
            var errors = new List<string> { "O ID informado não é o mesmo que o informado na request." };
            return BadRequest(new BaseApiResponse { Success = false, Errors = errors });
        }

        protected IActionResult ErrorBadRequestModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            return BadRequest(new BaseApiResponse
            {
                Success = false,
                Errors = errors
            });
        }

        protected IActionResult ErrorUnauthorized(object data) =>
            StatusCode((int)HttpStatusCode.Unauthorized, new BaseApiResponse { Success = false, Data = data });

        protected IActionResult ErrorForbidden(object data) =>
            StatusCode((int)HttpStatusCode.Forbidden, new BaseApiResponse { Success = false, Data = data });

        protected IActionResult ErrorNotFound(string error)
        {
            var errors = new List<string> { error };
            return NotFound(new BaseApiResponse { Success = false, Errors = errors });
        }

        protected IActionResult ErrorInternalServerError(object data) =>
            StatusCode((int)HttpStatusCode.InternalServerError, new BaseApiResponse { Success = false, Data = data });

        protected IActionResult ErrorUnavailable(object data) =>
            StatusCode((int)HttpStatusCode.ServiceUnavailable, new BaseApiResponse { Success = false, Data = data });
    }
}