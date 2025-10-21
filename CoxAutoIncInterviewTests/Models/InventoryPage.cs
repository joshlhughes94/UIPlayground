using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoxAutoIncInterviewTests.Services;
using Microsoft.Playwright;

namespace CoxAutoIncInterviewTests.Models
{
    public class InventoryPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        private ILocator _addToCartTShirt => _pageDependencyService.Page.Result.Locator("#add-to-cart-sauce-labs-bolt-t-shirt");
        private ILocator _addToCartBackpack => _pageDependencyService.Page.Result.Locator("#add-to-cart-sauce-labs-backpack");
        private ILocator _cartIcon => _pageDependencyService.Page.Result.Locator("//*[@data-test='shopping-cart-link']");

        public InventoryPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        public async Task<bool> IsProductsTitleVisible()
        {
            var page = await _pageDependencyService.Page;
            var productsTitle = page.Locator("//*[@data-test='secondary-header'][contains(.,'Products')]");
            return await productsTitle.IsVisibleAsync();
        }

        public async Task AddBackpackToCart()
        {
            await _addToCartBackpack.ClickAsync();
        }

        public async Task AddTShirtToCart()
        {
            await _addToCartTShirt.ClickAsync();
        }

        public async Task AddItemsToCart(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                switch (item.Trim().ToLower())
                {
                    case "sauce labs backpack":
                        await AddBackpackToCart();
                        break;
                    case "sauce labs bolt t-shirt":
                        await AddTShirtToCart();
                        break;
                    default:
                        throw new ArgumentException($"Item '{item}' not recognized.");
                }
            }
        }

        public async Task<bool> CartDisplaysTwoItems()
        {
            var page = await _pageDependencyService.Page;
            var cartBasket = page.Locator("//*[@data-test='shopping-cart-link'][contains(.,'2')]");
            return await cartBasket.IsVisibleAsync();
        }

        public async Task ClickCartIcon()
        {
            await _cartIcon.ClickAsync();
        }
        public async Task<bool> WaitForInventoryPageAsync()
        {
            var page = await _pageDependencyService.Page;
            await page.WaitForURLAsync("**/inventory.html", new() { Timeout = 5000 });
            return page.Url.Contains("/inventory.html", StringComparison.OrdinalIgnoreCase);
        }

    }
}
