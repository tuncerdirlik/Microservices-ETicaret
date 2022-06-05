using FluentValidation;
using FreeCourse.Web.Models.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(k => k.Code).NotEmpty().WithMessage("İndirim kodu boş olamaz");
        }
    }
}
