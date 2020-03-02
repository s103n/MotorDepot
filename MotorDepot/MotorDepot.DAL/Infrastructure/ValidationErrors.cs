using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace MotorDepot.DAL.Infrastructure
{
    public class ValidationErrors
    {
        public List<ValidationResult> Errors { get; } = new List<ValidationResult>();

        public ValidationErrors(IEnumerable<DbEntityValidationResult> errors)
        {
            foreach (var error in errors)
            {
                foreach (var err in error.ValidationErrors)
                {
                    Errors.Add(new ValidationResult(
                        error.Entry.Entity.ToString(), 
                        err.ErrorMessage, 
                        err.PropertyName));
                }
            }
        }

        public ValidationErrors() { }
    }
}
