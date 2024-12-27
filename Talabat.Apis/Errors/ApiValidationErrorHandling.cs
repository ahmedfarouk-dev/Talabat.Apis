namespace Talabat.Apis.Errors
{
    public class ApiValidationErrorHandling : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorHandling() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
