using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoxAutoIncInterviewTests.Services;
using Microsoft.Playwright;

namespace CoxAutoIncInterviewTests.Models
{
    public class LoginPage
    {
        private readonly IPageDependencyService _pageDependencyService;
        private ILocator _username => _pageDependencyService.Page.Result.Locator("#user-name");
        private ILocator _password => _pageDependencyService.Page.Result.Locator("#password");
        private ILocator _loginButton => _pageDependencyService.Page.Result.Locator("#login-button");
        public LoginPage(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService ?? throw new ArgumentNullException(nameof(pageDependencyService));
        }

        public async Task GoToLoginPage()
        {
            await _pageDependencyService.Page.Result.GotoAsync(_pageDependencyService.AppSettings.Value.SauceDemoURL);
        }

        public async Task EnterValidUserNamePassword(string username, string password)
        {
            await _username.FillAsync(username);
            await _password.FillAsync(password);
        }

        public async Task ClickLoginButton()
        {
            await _loginButton.ClickAsync();
        }
    }
}