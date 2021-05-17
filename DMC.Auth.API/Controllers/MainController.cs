using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace DMC.Auth.API.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected IList<string> _errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ActionIsValid()) return Ok(new { success = true, data = result, errors = GetErrors() });
            else return BadRequest(new { success = false, data = result, errors = GetErrors() });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierError(modelState);
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validateResult)
        {
            foreach (var error in validateResult.Errors)
            {
                NotifierError(error.ErrorMessage);
            }
            return CustomResponse();
        }

        protected void NotifierError(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid)
            {
                var errors = modelState.Values.SelectMany(error => error.Errors);
                foreach (var error in errors)
                {
                    var msgError = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                    NotifierError(msgError);
                }
            }
        }
        protected void NotifierError(string message) => _errors.Add(message);
        protected bool ActionIsValid() => !_errors.Any();
        protected void ClearErrors() => _errors.Clear();

        private IList<string> GetErrors() => _errors;
    }
}