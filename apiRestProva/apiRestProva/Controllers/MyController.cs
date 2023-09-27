using apiRestProva.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace apiRestProva.Controllers
{
    public class MyController : ControllerBase
    {
        [NonAction]
        public virtual ObjectResult CustomError(OutputError outErr)
        {
            return new ObjectResult(outErr.ErrorMsg)
            {
                //StatusCode = outErr.ErrorCod
            };
        }

    }
}
