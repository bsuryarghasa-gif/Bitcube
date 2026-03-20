using factorial_project.FactorialUI.Hooks;
using Factorial_Project.FactorialUI.Pages;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;

namespace Factorial_Project.FactorialUI.Steps
{
    [Binding]
    public sealed class FactorialSteps
    {
        private readonly BasePage _basePage;
        private readonly FactorialHomePage _factorialHomePage;
        
        // ADDED: Store the hooks driver so we can pull the API data during the 'Then' step
        private readonly Hooks _hooks; 

        public FactorialSteps(Hooks driver)
        {
            _hooks = driver;
            _basePage = new BasePage(driver._page!); // Using ! to tell compiler it won't be null here
            _factorialHomePage = new FactorialHomePage(driver._page!);
        }

        [Given("I navigate to (.*)")]
        public async Task GivenINavigateToAsync(string url)
        {
           await _basePage.GoTo(url);
        }

        [Given("I calculate factorial for an (.*)")]
        public async Task GivenICalculateFactorialForAn(string integer)
        {
            Console.WriteLine(integer);
            await _factorialHomePage.CalculateFactorial(integer);
        }

        [Given("I verify the given values don't calculate factorial (.*)")]
        public async Task GivenIVerifyTheGivenValuesDontCalculateFactorial(string integer)
        {
            await _factorialHomePage.ValidateErrorMsg(integer);
        }

        [Given("I click hyperlinks on the home page")]
        public async Task GivenIClickHyperlinksOnTheHomePage()
        {
            await _factorialHomePage.ClickHyperlinks();
        }

        [Given("I validate the factorial for (.*) and check the answer (.*)")]
        public async Task GivenIValidateTheFactorialFor(string number, string answer)
        {
            await _factorialHomePage.ValidateFactorialAnswer(number, answer);
        }
        
        [Given("I want to check the style for output {string} {string}")]
        public async Task GivenIValidateFactorialOutputStyle(string number,string style)
        {
            await _factorialHomePage.ValidateFactorialOutputStyle(number,style);
        }

        [Given("I want to check the style for input {string} {string}")]
        public async Task GivenIValidateFactorialInputStyle(string number,string style)
        {
            await _factorialHomePage.ValidateFactorialInputStyle(number,style);
        }   

        // ADDED: Missing step for the invalid input scenario
        [When(@"I enter an invalid input ""(.*)""")]
        public async Task WhenIEnterAnInvalidInput(string integer)
        {
            await _factorialHomePage.CalculateFactorial(integer);
        }

        // ADDED: Missing step for the API scenario
        [When(@"I enter an valid input (.*)")]
        public async Task WhenIEnterAnValidInput(string integer)
        {
            await _factorialHomePage.CalculateFactorial(integer);
            // We wait a tiny bit to ensure the network request has time to fire and be caught
            await Task.Delay(500); 
        }
        
        [Then(@"the input field should turn red")] // Matched spelling in feature file
        public async Task ThenTheInputFieldBorderShouldTurnRed() {
            var color = await _factorialHomePage.GetInputFieldBorderColor();
            Assert.IsTrue(color.Contains("red"), $"Expected red border but got {color}");
        }

        [Then(@"the outgoing API request should have correct headers and payload")]
        public void ThenTheAPIRequestIsCorrect() {
            // FIXED: Pull the data from the _hooks variable
            var headers = _hooks.CapturedHeaders;
            var body = _hooks.CapturedBody;
            // Ensure we actually caught something before asserting
            Assert.IsNotNull(headers, "API Request headers were not caught!");
            
            // Note: Playwright makes headers lowercase ("content-type")
            Assert.AreEqual("application/x-www-form-urlencoded; charset=UTF-8", headers["content-type"]);
            Assert.AreEqual("number=10", body);
        }
    }
}