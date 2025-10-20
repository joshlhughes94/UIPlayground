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
    }
}
