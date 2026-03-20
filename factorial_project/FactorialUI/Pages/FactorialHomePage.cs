using System.Security.Principal;
using Microsoft.Playwright;

namespace Factorial_Project.FactorialUI.Pages;

public class FactorialHomePage
{
    private readonly IPage _page;
    private readonly BasePage _basePage;
    private readonly ILocator _inputField;
    
    public FactorialHomePage(IPage page)
    {
        _page = page;
        _basePage = new BasePage(page);
        _inputField = page.Locator("input[name='number']");
    }

    public async Task<string> CalculateFactorial(string integer)
    {
        // FIXED: Removed quotes around integer
        await _basePage.SendTextAsync("#number", integer) ;
        await _basePage.ClickAsync("#getFactorial");
        await _basePage.AssertElementToBeVisibleAsync("#resultDiv");
        return await _inputField.EvaluateAsync<string>("el => window.getComputedStyle(el).borderColor");    
    }

    public async Task ValidateErrorMsg(string integer)
    {
        // FIXED: Removed quotes around integer
        await _basePage.SendTextAsync("#number", integer) ;
        await _basePage.ClickAsync("#getFactorial");
        await _basePage.ExpectTextContainsAsync("#resultDiv", "Please enter an integer");
    }

    public async Task ClickHyperlinks()
    {
        await _basePage.ClickByText("About");
        await _basePage.ClickByText("Home");
        await _basePage.ClickByText("Terms and Conditions");
        await _basePage.NavigateBackAsync();
        await _basePage.ClickByText("Privacy");
        await _basePage.NavigateBackAsync();
    }

    public async Task ValidateFactorialAnswer(string integer, string answer)
    {
        await _basePage.SendTextAsync("#number", integer) ;
        await _basePage.ClickAsync("#getFactorial");
        await _basePage.ExpectTextContainsAsync("#resultDiv", "The factorial of "+integer+" is: "+answer); 
    }
    
    public async Task ValidateFactorialOutputStyle(string integer,string style)
    {
        await _basePage.SendTextAsync("#number", integer) ;
        await _basePage.ClickAsync("#getFactorial");
        await _basePage.ExpectStyleContainsAsync("#resultDiv",style); 
    }
    
    public async Task ValidateFactorialInputStyle(string integer,string style)
    {
        await _basePage.SendTextAsync("#number", integer) ;
        await _basePage.ClickAsync("#getFactorial");
        await _basePage.ExpectStyleContainsAsync("#number",style); 
    }

    public async Task<string> GetInputFieldBorderColor()
    {
        return await _inputField.EvaluateAsync<string>("el => window.getComputedStyle(el).border");
    }

    public ILocator GetInputField() => _inputField;
}
