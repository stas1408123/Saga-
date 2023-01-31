using FluentValidation;
using WareHouse.OrderService.API.ViewModels;

namespace WareHouse.OrderService.API.Validators
{
    public class PostOrderViewModelValidator : AbstractValidator<PostOrderViewModel>
    {
        public PostOrderViewModelValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);
            RuleFor(x => x.ProductAmount)
                .GreaterThan(0);
        }
    }
}
