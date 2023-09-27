using apiRestProva.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace apiRestProva.Services
{

    internal class OutputException : Exception
    {
        public OutputError error { get; set; }

        public OutputException(OutputError oerr)
        {
            error = oerr;
        }

        
    }
       
}