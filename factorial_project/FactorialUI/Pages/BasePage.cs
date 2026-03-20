using Microsoft.Playwright;
using NUnit.Framework;

namespace Factorial_Project.FactorialUI.Pages;

public class BasePage
{
    private readonly IPage _page;

    public BasePage(IPage page)
    {
        _page = page;
    }

    public async Task GoTo(string url)
    {
        await _page.GotoAsync(url);
    }

    public async Task ClickByText(string buttonText)
    {
        // Clicks the first element (button, span, etc.) with the given visible text
        await _page.ClickAsync($"text={buttonText}");
    }

    public async Task ClickAsync(string selector)
    {
        await _page.ClickAsync(selector);
    }

    public async Task SendTextAsync(string selector, string text)
    {
        await _page.FillAsync(selector, text);
    }

    public async Task ClickByContainingTextAsync(string partialText)
    {
        // Clicks the first element that contains the given partial text
        await _page.ClickAsync($"xpath=//*[contains(text(), '{partialText}')]");
    }

    public async Task ClickByPlaceholderAsync(string placeholder)
    {
        // Clicks the first element with the given placeholder attribute value
        await _page.ClickAsync($"[placeholder='{placeholder}']");
    }

    public async Task EnterTextByPlaceholderAsync(string placeholder, string text)
    {
        // Fills the first element with the given placeholder attribute value
        await _page.FillAsync($"[placeholder='{placeholder}']", text);
    }

    public async Task ClickDropdownByTextAsync(string dropdownText)
    {
        // Clicks a dropdown (select, div, etc.) by its visible text
        await _page.ClickAsync($"text={dropdownText}");
    }

    public async Task AssertElementToBeVisibleAsync(string selector)
    {
        var element = await _page.QuerySelectorAsync(selector);
        if (element == null)
        {
            throw new Exception($"Element with selector '{selector}' was not found on the page.");
        }
    }

    public async Task ExpectTextContainsAsync(string selector, string expectedText)
    {
        await Assertions.Expect(_page.Locator(selector))
            .ToContainTextAsync(expectedText);
    }
    
    public async Task ExpectStyleContainsAsync(string selector,string style)
    {
        
        var border = await _page.Locator(selector)
            .EvaluateAsync<string>("el => window.getComputedStyle(el).border");
        Console.WriteLine(border);
        Assert.AreEqual(style, border);
    }

    public async Task NavigateBackAsync()
    {
        await _page.GoBackAsync();
    }
}
