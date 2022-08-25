Feature: ActorController
Test features for the ActorController API functionality
Link to a feature: [Calculator](Sakila.Testing/Features/ActorController.feature)

@1
Scenario: Get an actors details via id
    Given I am a user interacting with the database api
    When I make a get request to getactorbyid with <actorID>
    Then the response status code is "200"
    And reasonPhrase is "OK"

@2
Scenario: Add a new actor to the database
    Given I am a user interacting with the database api
    When I make a put request to putactor with <firstName> and <lastName>
    Then the response status code is "200"
    And reasonPhrase is "OK"

    Examples: 
    | firstName | lastName |
    | Alex      | Reid     |
    | Taylor    | Bromley  |
    | Adam      | Watson  |


@3
Scenario: Get an actor's details via first name
    Given I am a user interacting with the database api
    When I make a get request to GetActorByFirstName with <firstName>
    Then the response status code is "200"
    And reasonPhrase is "OK"
    And all returned firstName are <firstName>

    Examples: 
    | firstName |
    | Alex      |
    | Taylor    |
    | Adam      |


@4
Scenario: Update an actors details via actorId
    Given I am a user interacting with the database api
    When I make a get request to UpdateActorByID with <actorId> and <firstName> and <lastName>
    Then the response status code is "200"
    And reasonPhrase is "OK"
    When I make a get request to getactorbyid with <actorId>
    Then actor details match <firstName> and <lastName>

    Examples:
    | actorId | firstName | lastName |
    | 201     | Alex      | Reid     |
    | 202     | Taylor    | Bromley  |
    | 203     | Adam      | Watson  |


@5
Scenario: get an actors films using an id and a table join
    Given I am a user interacting with the database api
    When I make a get request to GetActorFilmsById with <1>
    Then the response status code is "200"
    And reasonPhrase is "OK"






