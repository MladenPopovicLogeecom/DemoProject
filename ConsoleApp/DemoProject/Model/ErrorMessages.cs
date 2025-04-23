using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1.model
{
    public static class ErrorMessages
    {
        
        
        public static string GetModelStateErrors(ModelStateDictionary modelState)
        {
            var errorMessages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    errorMessages.Add($"{entry.Key}: {error.ErrorMessage}");
                }
            }

            return string.Join("; ", errorMessages);
        }
    }
}