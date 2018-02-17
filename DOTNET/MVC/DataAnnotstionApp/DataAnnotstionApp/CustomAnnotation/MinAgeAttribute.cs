using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAnnotstionApp.CustomAnnotation
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private int _minAge;
        public MinAgeAttribute(int age)
        {
            _minAge = age;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if(value is int)
                {
                    int minAge = (int)value;
                    if(minAge < _minAge)
                    {
                        return new ValidationResult("Minimum age must be "+_minAge);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}