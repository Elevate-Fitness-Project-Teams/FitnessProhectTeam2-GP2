using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Pagniation
{
    public static class PaginationValidatorExtensions
    {
        public static IRuleBuilderOptions<T, int> MustBeValidPageNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be greater than or equal to 1.");
        }

        public static IRuleBuilderOptions<T, int> MustBeValidPageSize<T>(this IRuleBuilder<T, int> ruleBuilder, int maxPageSize = 50)
        {
            return ruleBuilder
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(maxPageSize)
                .WithMessage($"Page size cannot exceed {maxPageSize} items per page.");
        }
    }
}
