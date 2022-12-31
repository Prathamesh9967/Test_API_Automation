Feature: CreateUser

Create new user

@tag1
Scenario: create new user with valid inputs
	Given user with name "peter"
	And user with job "SDET"
	When send req to create user
	Then validate user is created
