using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.Arm;
using System;
using apiRestProva.Models;

namespace apiRestProva.Validators
{


    public class ArticleCartDTOValidator : AbstractValidator<ArticleCartDTO>
    {
        public ArticleCartDTOValidator()
        {
            RuleFor(acart => acart.ArticleCode)
                .NotEmpty().WithMessage("Il codice è obbligatorio");

            RuleFor(acart => acart.Quantity)
                .NotEmpty().LessThan(100).GreaterThan(0).WithMessage("Il codice è obbligatorio");

            RuleFor(acart => acart).Custom((acart, context) =>
             {
                 if (!(acart.Quantity < acart.Price))
                 {
                     context.AddFailure("Quantity", "La quantità deve essere minore al prezzo.");
                 }
             });
        }
    }
}
   