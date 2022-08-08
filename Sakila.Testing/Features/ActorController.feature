Feature: ActorController
Test features for the ActorController API functionality
Link to a feature: [Calculator](Sakila.Testing/Features/ActorController.feature)

@mytag
Scenario: Get an actors details via id
    Given I am a user
    When I make a post request to "/getActorById" when id is 2
    Then the response status code is "200"