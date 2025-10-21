using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoxAutoIncInterviewTests.Models;

namespace CoxAutoIncInterviewTests.Services
{
    public interface IPageService
    {
        LoginPage LoginPage { get; }
        InventoryPage InventoryPage { get; }
        CheckoutPage CheckoutPage { get; }
    }

    public class PagesService : IPageService
    {
        public PagesService(LoginPage loginPage, InventoryPage inventoryPage, CheckoutPage checkoutPage)
        {
            LoginPage = loginPage ?? throw new ArgumentNullException(nameof(loginPage));
            InventoryPage = inventoryPage ?? throw new ArgumentNullException(nameof(inventoryPage));
            CheckoutPage = checkoutPage ?? throw new ArgumentNullException(nameof(checkoutPage));

        }

        public LoginPage LoginPage { get; }
        public InventoryPage InventoryPage { get; }
        public CheckoutPage CheckoutPage { get; }
    }
}