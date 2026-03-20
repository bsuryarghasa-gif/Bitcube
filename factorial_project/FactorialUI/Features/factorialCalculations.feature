# language: en
Feature: As a user I want calculate factorial of integers

    Scenario: Calculate factorial in the home page
        Given I navigate to <url>
        And I calculate factorial for an <integer>         
       
    Examples:
    | url                                     | integer |
    | https://qainterview.pythonanywhere.com/ | 5       |
    | https://qainterview.pythonanywhere.com/ | 0       | 
    | https://qainterview.pythonanywhere.com/ | 1       |
    | https://qainterview.pythonanywhere.com/ | 50      | 
    | https://qainterview.pythonanywhere.com/ | 170     |
    | https://qainterview.pythonanywhere.com/ | 171     |
    | https://qainterview.pythonanywhere.com/ | 991     |
    
     Scenario: Check hyperlinks in the home page
        Given I navigate to <url> 
        And I click hyperlinks on the home page

Scenario: To verify that certain values throw an error message on the UI
    Given I navigate to <url>
    And I verify the given values don't calculate factorial <integer> 
    
Examples:
  | url                                     | integer | 
  | https://qainterview.pythonanywhere.com/ | -5      |
  | https://qainterview.pythonanywhere.com/ | abc     |
  | https://qainterview.pythonanywhere.com/ | 1.5     |
  | https://qainterview.pythonanywhere.com/ | &       |
  
Scenario Outline: To validate the expected answer for integer 12
    Given I navigate to <url>
    And I validate the factorial for <number> and check the answer <answer>
    
Examples:
  | url                                     |  number | answer    |
  | https://qainterview.pythonanywhere.com/ |    12     | 479001600 |
  
Scenario Outline: To validate the form style in the output field
    Given I navigate to <url>
    And I want to check the style for output "<number>" "<style>"
    
Examples:
  | url                                     | number | style             |
  | https://qainterview.pythonanywhere.com/ | and    | rgb(255, 0, 0)    |
  | https://qainterview.pythonanywhere.com/ | 12     | rgb(51, 51, 51)   |
  
Scenario Outline: To validate the form style in the input field
    Given I navigate to <url>
    And I want to check the style for input "<number>" "<style>"
    
Examples:
  | url                                     | number | style          |
  | https://qainterview.pythonanywhere.com/ | and    | 2px solid red; |
  
Scenario: Verify input field turns red on invalid input
    Given I navigate to <url>
    When I enter an invalid input "<integer>"
    Then the input field should turn red
    
Examples:
  | url                                     | integer | 
  | https://qainterview.pythonanywhere.com/ | -5      |
  
Scenario: Verify API request headers and parameters
    Given I navigate to <url>
    When I enter an valid input <integer>
    Then the outgoing API request should have correct headers and payload
        
Examples:
  | url                                     | integer | 
  | https://qainterview.pythonanywhere.com/ | 10      |