using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace apiRestProva.Models
{
    public class OutputError : ObjectResult
    {
        
        public string ErrorMsg { get; set; }
        public OutputError(int errorCod, string errorMsg) : base(errorMsg)
        {
            
            ErrorMsg = errorMsg;
            StatusCode = errorCod;
        }

   
    }
}
