using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using CoxAutoIncInterviewTests.Services;

namespace CoxAutoIncInterviewTests.Models
{
    public class CheckoutPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        private ILocator _checkoutButton => _pageDependencyService.Page.Result.Locator("#checkout");
        private ILocator _firstNameInput => _pageDependencyService.Page.Result.Locator("#first-name");
        private ILocator _lastNameInput => _pageDependencyService.Page.Result.Locator("#last-name");
        private ILocator _postalCodeInput => _pageDependencyService.Page.Result.Locator("#postal-code");
        private ILocator _continueButton => _pageDependencyService.Page.Result.Locator("#continue");
        private ILocator _itemName => _pageDependencyService.Page.Result.Locator("//*[@data-test='inventory-item-name']");
        private ILocator _totalPrice => _pageDependencyService.Page.Result.Locator("//*[@data-test='total-label']");
        private ILocator _finishButton => _pageDependencyService.Page.Result.Locator("#finish");
        private ILocator _thankYouForYourOrderText => _pageDependencyService.Page.Result.Locator("//*[@data-test='checkout-complete-container'][contains(.,'Thank you for your order!')]");

        public CheckoutPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        public async Task ProceedToCheckout(string firstName, string lastName, string postalCode)
        {
            var page = await _pageDependencyService.Page;

            await _checkoutButton.ClickAsync();
            await _firstNameInput.FillAsync(firstName);
            await _lastNameInput.FillAsync(lastName);
            await _postalCodeInput.FillAsync(postalCode);
            await _continueButton.ClickAsync();
        }

        public async Task<bool> IsItemAndTotalDisplayed(string expectedItemName, string expectedTotal)
        {
            var itemName = await _itemName.InnerTextAsync();
            var totalText = await _totalPrice.InnerTextAsync();

            var actualItem = itemName.Trim();
            var actualTotal = totalText.Replace("Total: ", "").Trim();

            bool isItemCorrect = actualItem.Equals(expectedItemName, StringComparison.OrdinalIgnoreCase);
            bool isTotalCorrect = actualTotal.Equals(expectedTotal, StringComparison.OrdinalIgnoreCase);

            return isItemCorrect && isTotalCorrect;
        }

        public async Task FinishCheckout()
        {
            await _finishButton.ClickAsync();
        }

        public async Task CheckCheckoutConfirmationMessageIsDisplayed()
        {
            bool isVisible = await _thankYouForYourOrderText.IsVisibleAsync();
            if (!isVisible)
            {
                throw new Exception("Checkout confirmation message is not displayed.");
            }
        }
    }
}