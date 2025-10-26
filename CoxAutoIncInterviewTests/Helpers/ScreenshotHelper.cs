using Reqnroll;
using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;
using UIPlayground.Services;

namespace UIPlayground.Helpers
{
    [Binding]
    public class ScreenshotHelper
    {
        private readonly IPageDependencyService _pageDependencyService;

        public ScreenshotHelper(IPageDependencyService pageDependencyService)
        {
            _pageDependencyService = pageDependencyService;
        }

        [AfterScenario]
        public async Task TakeScreenshotOnFailure(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                var page = await _pageDependencyService.Page;
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var fileName = $"{scenarioContext.ScenarioInfo.Title}_{timestamp}.png";

                var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var filePath = Path.Combine(screenshotsDir, fileName);
                await page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath, FullPage = true });

                Console.WriteLine($"Screenshot captured: {filePath}");
            }
        }
    }
}