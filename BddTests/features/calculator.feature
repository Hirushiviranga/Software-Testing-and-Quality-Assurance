Feature: Calculator API

  Scenario: Add two numbers
    Given I have numbers 5 and 3
    When I call the add API
    Then I should get the result 8

  Scenario: Subtract two numbers
    Given I have numbers 10 and 4
    When I call the subtract API
    Then I should get the result 6

  Scenario: Multiply two numbers
    Given I have numbers 4 and 6
    When I call the multiply API
    Then I should get the result 24

  Scenario: Divide two numbers
    Given I have numbers 10 and 2
    When I call the divide API
    Then I should get the result 5

  Scenario: Divide by zero
    Given I have numbers 10 and 0
    When I call the divide API
    Then I should get an error "Cannot divide by zero"
