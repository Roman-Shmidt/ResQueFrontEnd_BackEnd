using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Common
{
    /// <summary> 
    /// Base custom controller. 
    /// </summary> 
    [ApiController]
    public abstract class CustomControllerBase : ControllerBase
    {
        /// <summary> 
        /// "Item not found" message. 
        /// </summary> 
        protected const string ItemNotFoundMessage = "Item not found";

        /// <summary> 
        /// The item not found error key. 
        /// </summary> 
        protected const string ItemNotFoundErrorKey = "ITEM_NOT_FOUND";

        private const char IncludeSeparator = ',';
        private const char IncludeInnerSeparator = '.';

        /// <summary> 
        /// Checks whether to include specified resource with the Response. 
        /// </summary> 
        /// <param name="includeString">The include string.</param> 
        /// <param name="resourceName">Name of the resource.</param> 
        /// <returns>Result whether to include resource.</returns> 
        protected static bool CheckWhetherToIncludeResource(string includeString, string resourceName)
        {
            if (string.IsNullOrEmpty(includeString))
            {
                return false;
            }

            // Query can be: ?include=giftCards,orders 
            string[] includes = includeString.Split(IncludeSeparator);
            return includes.Any(include =>
            {
                string[] innerIncludes = include.Split(IncludeInnerSeparator);
                return string.Equals(
                    innerIncludes.Length > 1 ? innerIncludes.Last().Trim() : include.Trim(),
                    resourceName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase);
            });
        }

        /// <summary> 
        /// Creates the error action result. 
        /// </summary> 
        /// <param name="errorsDocument">The errors document.</param> 
        /// <returns>Error action result.</returns> 
        protected ObjectResult CreateErrorActionResult(Document errorsDocument)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            return new ObjectResult(errorsDocument);
        }
    }
}
