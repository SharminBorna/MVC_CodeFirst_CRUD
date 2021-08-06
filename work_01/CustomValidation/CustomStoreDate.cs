using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace work_01.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CustomStoreDate : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime _storeEntry = Convert.ToDateTime(value);
                if (_storeEntry > DateTime.Now)
                {
                    return new ValidationResult("Product entry date can not be greater than current date.");
                }
            }
            return ValidationResult.Success;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule mvr = new ModelClientValidationRule();
            mvr.ErrorMessage = "Product entry date can not be greater than current date";
            mvr.ValidationType = "validstoredate";
            return new[] { mvr };
        }
    }
}