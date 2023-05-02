Feature: Users details

Scenario Outline: Add a user
	Given I input "<name>" of the user
	When I input "<role>" of the user
	When I send create user request
	Then validate user is created
	And I should be displayed with response "<StatusCode>" for CreateAddUserPostRequest

	Examples: 
	          | name     | role   | StatusCode |
	          | morpheus | leader | 201        |
	          | ABCD     | QA     | 200        |
	          | !@#$%    | QQ     | 201        |
	  
	Scenario: List of  users
	Given I send a get users request
	Then I print the users

	Scenario: Single user
	Given I send single user request to get the user details with id "3"
	Then I verify the user details of the user with id "3"


	Scenario Outline: Update a user using put request
	Given I send user update request to update the "<name>" and "<job>"
	Then I verify the updated user details with "<StatusCode>"

	Examples: 
	          | name     | job  | StatusCode |
	          | update01 | QA01 |200    |
	          | update02  | QA02   | 201   |


Scenario Outline: Update a user using patch request
	Given I send user patch request to update the "<name>"
	Then I verify the partially updated user details with "<StatusCode>"

	Examples: 
	          | name     |  StatusCode |
	          | update01 | 200    |
	          | update02  | 202   |


Scenario: Verfiying the Single user from the list of users
    Given I send a get users request
	Then I verfiy the "Lindsay Ferguson" from the list of users
