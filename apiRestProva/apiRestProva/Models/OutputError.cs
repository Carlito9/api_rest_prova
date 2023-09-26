using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace apiRestProva.Models
{
    public class OutputError 
    {
        public int ErrorCod {get; set; }
        public string ErrorMsg { get; set; }
        public OutputError(int errorCod, string errorMsg)
        {
            ErrorCod = errorCod;
            ErrorMsg = errorMsg;
        }

      
    }
}
