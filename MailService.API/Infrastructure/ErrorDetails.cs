using Newtonsoft.Json;

namespace MailService.API.Infrastructure
{
    /// <summary>
    /// Details of occured error
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// HTTP status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Serializes object state to json format
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
