Feature: Valid Sauce Demo Tests

Background: Successful Login
	Given I have navigated to the Sauce Demo login page
	When I have entered valid credentials
	And I have selected the login button
	And I have been redirected to the Inventory Page
	Then I am displayed the products page

Scenario: Add Multiple Items to Cart
	Given I am displayed the products page
	When I add the following items to the cart:
		| Item Name					|
		| Sauce Labs Backpack		|
		| Sauce Labs Bolt T-Shirt	|
	Then the cart should contain 2 items

Scenario: Checkout Flow
	Given I have added a Sauce Labs Backpack to the cart
	When I proceed to checkout with the following information:
		| First Name | Last Name | Postal Code |
		| John       | Doe       | 12345       |
	Then I should see 'Sauce Labs Backpack' and the total price '$32.39' on the checkout overview page
	When I complete the purchase
	Then I should see a confirmation message indicating the order was successful
