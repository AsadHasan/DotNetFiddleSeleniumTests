Feature: .Net Fiddle
	In order to fiddle with .Net without a local .Net environment
	As a developer
	I want to be able to use .Net within a browser
	
	
	
Scenario: Run "Hello world" program
	When I run the program
	Then "Hello World" is displayed in output window
	
	
Scenario: Add Nuget package NUnit 3.12 
	Given I search for "NUnit" in Nuget packages
	When I select "NUnit" version: 3.12.0
	Then "NUnit" package is successfully added
