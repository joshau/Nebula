using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Nebula.MvcAddons.Controllers {
    public static class ControllerExtension {

        public static void ValidateViewModel(this Controller controller, object model) {

            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults);
            foreach (var validationResult in validationResults) {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }
    }
}
