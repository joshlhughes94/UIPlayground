Feature: Invalid Sauce Demo Tests

Scenario: Negative Login Test
	Given I have navigated to the Sauce Demo login page
	When I have entered invalid credentials
	And I have selected the login button
	Then I am displayed a error message stating username and password do not match