using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoxAutoIncInterviewTests.Services;
using Microsoft.Playwright;
using Reqnroll;

namespace CoxAutoIncInterviewTests
{
    [Binding]
    public class StepDefinitions
    {
        private readonly IPageService _pageService;
        private readonly IPageDependencyService _pageDependencyService;
        private IBrowser _browser;
        private IPage _page;

        public StepDefinitions(IPageService pageService, IPageDependencyService pageDependencyService)
        {
            _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }
        private async Task<IPage> CreateChromePage()
        {
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, 
                SlowMo = 100      
            });

            var page = await browser.NewPageAsync();
            _pageDependencyService.SetPage(page);
            return page;
        }

        [Given("I have navigated to the Sauce Demo login page")]
        public async Task GivenIHaveNavigatedToTheSauceDemoLoginPage()
        {
            _page = await CreateChromePage();
            var loginPage = _pageService.LoginPage;
            await loginPage.GoToLoginPage();
        }

        [When("I have entered valid credentials")]
        public async Task WhenIHaveEnteredValidCredentials()
        {
            var username = _pageDependencyService.AppSettings.Value.ValidUsername;
            var password = _pageDependencyService.AppSettings.Value.ValidPassword;

            await _pageService.LoginPage.EnterValidUserNamePassword(username, password);
        }

        [When("I have selected the login button")]
        public async Task WhenIHaveSelectedTheLoginButton()
        {
            var loginPage = _pageService.LoginPage;
            await loginPage.ClickLoginButton();
        }
        
        [Given("I am displayed the products page")]
        [Then("I am displayed the products page")]
        public async Task ThenIAmDisplayedTheProductsPage()
        {
            var inventoryPage = _pageService.InventoryPage;
            bool isVisible = await inventoryPage.IsProductsTitleVisible();
            Assert.That(isVisible, Is.True, "Products page title is not visible");
        }

        [When("I add the following items to the cart:")]
        public async Task WhenIAddTheFollowingItemsToTheCart(Table table)
        {
            var inventoryPage = _pageService.InventoryPage;
            var items = table.Rows.Select(r => r["Item Name"]);
            await inventoryPage.AddItemsToCart(items);
        }

        [Then("the cart should contain {int} items")]
        public async Task ThenTheCartShouldContainItems(int p0)
        {
            var inventoryPage = _pageService.InventoryPage;
            bool isVisible = await inventoryPage.CartDisplaysTwoItems();
            Assert.That(isVisible, Is.True, "Cart Does Not Display Two Items");
        }

    }
}
