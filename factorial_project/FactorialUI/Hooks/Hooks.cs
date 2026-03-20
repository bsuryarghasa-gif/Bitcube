using Microsoft.Playwright;
using Reqnroll;

namespace factorial_project.FactorialUI.Hooks;

[Binding]
public class Hooks
{
    public IPlaywright? _playwright;
    public IBrowser? _browser;
    public IBrowserContext? _browserContext;
    public IPage? _page;

    // ADDED: These public properties store the API data so the Steps file can read them
    public IReadOnlyDictionary<string, string>? CapturedHeaders { get; set; }
    public string? CapturedBody { get; set; }

    [BeforeScenario]
    public async Task Setup()
    {
    }

    [BeforeStep]
    public async Task InitiateBrowserIfNeeded()
    {
        var stepText = ScenarioStepContext.Current.StepInfo.Text;
        if (stepText.Contains("I navigate to"))
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, Channel = "chrome", Timeout = 60000});
            _browserContext = await _browser.NewContextAsync();
            _page = await _browser.NewPageAsync();

            
            _page.Request += (_, request) =>
            {
                // Catch the POST request made to the factorial endpoint
                if (request.Url.Contains("/factorial") && request.Method == "POST")
                {
                    CapturedHeaders = request.Headers;
                    CapturedBody = request.PostData;
                }
            };
        }
    }

    [AfterScenario]
    public async Task TearDown()
    {
        if (_browser != null)
            await _browser.CloseAsync();
        _playwright?.Dispose();
    }
}