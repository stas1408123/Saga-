using FluentValidation;
using WareHouse.OrderService.API.ViewModels;

namespace WareHouse.OrderService.API.Validators
{
    public class ChangeOrderStatusViewModelValidator : AbstractValidator<ChangeOrderStatusViewModel>
    {
        public ChangeOrderStatusViewModelValidator()
        {
            RuleFor(x => x.Status).NotEmpty().IsInEnum();
        }
    }
}
