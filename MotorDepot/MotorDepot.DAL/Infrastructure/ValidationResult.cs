namespace MotorDepot.DAL.Infrastructure
{
    public class ValidationResult
    {
        public string Entity { get; set; }
        public string Error { get; set; }
        public string Property { get; set; }

        public ValidationResult(string entity, string error, string property)
        {
            Entity = entity;
            Error = error;
            Property = property;
        }
    }
}
