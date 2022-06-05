using FluentValidation;
using FreeCourse.Web.Models.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Validators
{
    public class CourseCreateInputValidator : AbstractValidator<CourseCreateInput>
    {
        public CourseCreateInputValidator()
        {
            RuleFor(k => k.Name).NotEmpty().WithMessage("İsim boş geçilemez");
            RuleFor(k => k.Description).NotEmpty().WithMessage("Açıklama boş geçilemez");
            RuleFor(k => k.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre alanı boş geçilemez");
            RuleFor(k => k.Price).NotEmpty().WithMessage("Fiyat alanı boş geçilemez").ScalePrecision(2, 6).WithMessage("Hatalı para formatı");
            RuleFor(k => k.CategoryId).NotEmpty().WithMessage("Kategori boş geçilemez");
        }
    }
}
