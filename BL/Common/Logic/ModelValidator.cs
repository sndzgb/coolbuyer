using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBuyer.Server.BusinessLogic.Common.Logic
{
    public static class ModelValidator
    {
        public static void Validate(object model)
        {
            var validationContext = new ValidationContext(model, serviceProvider: null, items: null);
            Validator.ValidateObject(model, validationContext, true);
        }
    }
}
