const { Given, When, Then } = require('@cucumber/cucumber');
const { expect } = require('chai');
const Calculator = require('../../src/Calculator'); // adjust if path differs

let calculator;
let result;
let error;

Given('I have numbers {int} and {int}', function (a, b) {
  calculator = new Calculator();
  this.a = a;
  this.b = b;
});

When('I call the add API', function () {
  result = calculator.add(this.a, this.b);
});

When('I call the subtract API', function () {
  result = calculator.subtract(this.a, this.b);
});

When('I call the multiply API', function () {
  result = calculator.multiply(this.a, this.b);
});

When('I call the divide API', function () {
  try {
    result = calculator.divide(this.a, this.b);
  } catch (err) {
    error = err.message;
  }
});

Then('I should get the result {int}', function (expected) {
  expect(result).to.equal(expected);
});

Then('I should get an error {string}', function (expectedError) {
  expect(error).to.equal(expectedError);
});

