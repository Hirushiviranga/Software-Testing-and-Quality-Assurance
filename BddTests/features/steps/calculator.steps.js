const { Given, When, Then } = require('@cucumber/cucumber');
const axios = require('axios');
const { expect } = require('chai');

let num1, num2, response;
const BASE_URL = "http://127.0.0.1:5095"; // your ASP.NET backend URL

Given('I have numbers {int} and {int}', function (a, b) {
  num1 = a;
  num2 = b;
});

When('I call the add API', async function () {
  response = await axios.get(`${BASE_URL}/Calculator/Add?a=${num1}&b=${num2}`)
    .catch(err => { response = err.response; });
});

When('I call the subtract API', async function () {
  response = await axios.get(`${BASE_URL}/Calculator/Subtract?a=${num1}&b=${num2}`)
    .catch(err => { response = err.response; });
});

When('I call the multiply API', async function () {
  response = await axios.get(`${BASE_URL}/Calculator/Multiply?a=${num1}&b=${num2}`)
    .catch(err => { response = err.response; });
});
//divide zero test case
When('I call the divide API', async function () {
  try {
    response = await axios.get(`${BASE_URL}/Calculator/Divide?a=${num1}&b=${num2}`);
  } catch (err) {
    if (err.response) {
      response = err.response;
    } else {
      throw err; 
    }
  }
});
//to fail the divide by zero test case

/*When('I call the divide API', async function () {
  response = await axios.get(`${BASE_URL}/Calculator/Divide?a=${num1}&b=${num2}`)
    .catch(err => { response = err.response; });
});*/



Then('I should get the result {int}', function (expected) {
  if (!response) throw new Error("No response from API");
  expect(response.data).to.equal(expected);
});

Then('I should get an error {string}', function (expectedMessage) {
  if (!response) throw new Error("No response from API");
  expect(response.status).to.equal(400);
  expect(response.data).to.equal(expectedMessage);
});
