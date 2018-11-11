Battleship Game Tests

The Project implements Battleship Game with following Components

1) Battleship Console application which launches the game, Creates a player and setup the board for the player.
	It also gets input for reading positions, length for placing the ship. Also gets the input of coordinates to fire shots
	
2) Battleship Command Handler assembly handles the various operations on Battleship game 
	i) Add Ship Command Handler - Implements Add Ship Command, Adds the Ship builds the proper message based on the result
	ii) Attack Position Command Handler - Implements Attack Position Command, does input validation and call Attack operation and return result based on the result
	
3) Battleship Contracts project has various common contracts & enums that are shared across the projects

4) Battleship Domain project has the Domain models of Battleship game
	This project follows the Domain Driven Design principles with clear Separation of Concerns & SOLID principles. There're 3 major domain models namely
	
	i) Ship - Ship Domain Model represents each ship in the battleship game 
		This domain model has properties relevant to the ship, methods to create ship and fire shot at the ship etc. All the state management for Ships model and validations are done using this model
	
	2) Board - Board Domain Model represents board for each player with ships collection holding the ships placed by the player on his side
				This model keeps track of ships collection with current status of ships added and shots fired on different coordinates
				It has methods to Add Ship and Attack Position which are publicly available and would be invoked from the command handler
				This domain model takes care of all the operations, validation rules when placing the ship on a particular position etc.
				It also makes sure the ship position doesn't overlap with other ships
				The other private methods facilitate validation rules for ship placement
	
	3) Player - Player Domain Model has player identification (first or second player), their name and the board on their side
				The various operations on the board are performed through the player board property
				
				This Domain model is designed with extensibility in mind when we want to extend the functionality to 2 players, it's already supported with this design.
				
				We would just need to change and extend the consumer and make it 2 players game making the game finish once any one player is Lost.
				
	Technical HighLights of the Project
	------------------------------------
	Domain Driven Design principles 
	CQRS Pattern which is nice combination for developing Microservices, with each command handler responsible for invoking various command related operations and building the result of the operation
	SOLID Principles with Clear Separation of code logic into individual components and each individual class, method is clearly used for it's own specific implementation
	IoC - The project uses Dependency Injection to inject the various command handlers and commands and the Game Organizer makes it easy to maintain and Test each component individually
			mocking other components with Fake implementation or Nock framework.
	Each Component and Domain and Command Handler can be individually unit tested and Integration tested
	
	**Note: I've not implemented any Unit Tests or Component/Integration Tests since it was not a requirement and it's just for the Test
