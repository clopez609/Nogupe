namespace Nogupe.Web.Models
{
    public class GenericValidationResult
    {
        public GenericValidationResult(bool result, string message = "", string errorCode = "")
        {
            Result = result;
            Message = message;
            ErrorCode = errorCode;
        }

        public bool Result { get; }
        public string Message { get; }
        public string ErrorCode { get; }
    }
}
