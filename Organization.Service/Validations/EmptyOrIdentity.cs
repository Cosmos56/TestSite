using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Organization.Service.Services;

namespace Organization.Service.Validations
{
    public class EmptyOrIdentity : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IRepository) validationContext.GetService(typeof(IRepository));
            var error = "";
            
            if (value == null)
            {
                error = validationContext.DisplayName + " не может быть пустым";
                return new ValidationResult(error);
            }

            if (service.CheckIdentity(validationContext.MemberName, value.ToString()).Result) return ValidationResult.Success;
            var name =  validationContext.MemberName == "OrganizationName" ? "названием" : validationContext.DisplayName;
            error = "Организация с таким " + name + "уже зарегистрирована";
            return new ValidationResult(error);
        }
    }
}
