

namespace Talabat.Apis.Errors
{
    public class ApiResponse
    {

        public string? Message { get; set; }
        public int Status { get; set; }

        public ApiResponse(int status, string? Message = null)
        {
            this.Status = status;
            this.Message = Message ?? GetDefaultMessage(status);

        }

        private string? GetDefaultMessage(int status)
        {
            return status switch
            {
                200 => "Request successful.",
                201 => "Resource created successfully.",
                204 => "No content available.",
                400 => "Bad request. Please check your input.",
                401 => "Unauthorized. Please provide valid credentials.",
                403 => "Forbidden. You don't have permission to access this resource.",
                404 => "Not found. The requested resource does not exist.",
                409 => "Conflict. The request could not be completed due to a conflict.",
                422 => "Unprocessable entity. The input data is valid but cannot be processed.",
                500 => "Internal server error. Please try again later.",
                _ => "An unexpected error occurred."
            };
        }
    }
}
