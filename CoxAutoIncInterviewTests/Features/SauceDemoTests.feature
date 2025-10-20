Feature: SauceDemoTests

Background: Successful Login
	Given I have navigated to the Sauce Demo login page
	When I have entered valid credentials
	And I have selected the login button
	Then I am displayed the products page


Scenario: Add Multiple Items to Cart
	Given I am displayed the products page
	#When I add the following items to the cart:
	#	| Item Name					|
	#	| Sauce Labs Backpack		|
	#	| Sauce Labs Bolt T-Shirt	|
	#Then the cart should contain 2 items

Scenario: Checkout Flow
	Given I have 2 items in my cart
	When I proceed to checkout with the following information:
		| First Name | Last Name | Postal Code |
		| John       | Doe       | 12345       |
	Then I should see the order overview page
	When I complete the purchase
	Then I should see a confirmation message indicating the order was successful
