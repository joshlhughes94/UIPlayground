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
    }

    public class PagesService : IPageService
    {
        public PagesService(LoginPage loginPage, InventoryPage inventoryPage)
        {
            LoginPage = loginPage ?? throw new ArgumentNullException(nameof(loginPage));
            InventoryPage = inventoryPage ?? throw new ArgumentNullException(nameof(inventoryPage));
        }

        public LoginPage LoginPage { get; }
        public InventoryPage InventoryPage { get; }
    }
}