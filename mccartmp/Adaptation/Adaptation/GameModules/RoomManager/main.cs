//-----------------------------------------------------------------------------
// Room manager, sets up canvas and arena
//-----------------------------------------------------------------------------

function RoomManager::create( %this )
{
    new Scene(mainScene);

    new SceneWindow(mainWindow);
    mainWindow.profile = new GuiControlProfile();
    Canvas.setContent(mainWindow);

    mainWindow.setScene(mainScene);
    mainWindow.setCameraPosition( 0, 0 );
	
    mainScene.setDebugOn( "aabb" );			//bound boxes visible
	
    mainWindow.setCameraSize( 640, 480 );	//zoomed out...
    //mainWindow.setCameraSize( 100, 75 );

    // load some scripts and variables
    exec("./scripts/arena.cs");
    exec("./titleScreen.cs");
	exec("./scripts/behaviors/movement/shooterControls.cs");
	exec("./scripts/behaviors/movement/drift.cs");
	
	
	openTitleScreen(mainScene);
	
	//game room 101...
   // buildArena(mainScene);
	


}

//-----------------------------------------------------------------------------

function RoomManager::destroy( %this )
{
}

//-----------------------------------------------------------------------------

function RoomManager::spawnPlayer(%this)
{
  
	exec("./scripts/behaviors/movement/shooterControls.cs");
	
	%animSize = "74 78";
    %player = new Sprite()
    {
        Animation = "GameAssets:playerbaseAnim";
       //class = "playerClass";
        position = "0 0";
        size = "74 78";
        SceneLayer = "15";
        SceneGroup = "14";
        CollisionCallback = true;
    };

    %player.createPolygonBoxCollisionShape(%animSize);
    %player.setCollisionShapeIsSensor(0, true);
    %player.setCollisionGroups( "10 15" );
	
	%playercontrols = ShooterControlsBehavior.createInstance();
	%playercontrols.upKey = "keyboard W";
	%playercontrols.downKey = "keyboard S";
	%playercontrols.leftKey = "keyboard A";
	%playercontrols.rightKey = "keyboard D";
	
	%player.addBehavior(%playercontrols);
	
	//%player = new ScriptObject(Player);
	
	//%player.create();

    arenaScene.add( %player ); 
	
}
function RoomManager::spawnFishFood()			//example class, delete eventually
  {
      %food = new Sprite()
      {
          Image = "GameAssets:basicenemy";
          class = "FishFoodClass";
          position = "20 20";
          size = "74 78";
          SceneLayer = "15";
          SceneGroup = "10";
          CollisionCallback = true;
      };
  
      %food.createPolygonBoxCollisionShape(5, 5);
      %food.setCollisionShapeIsSensor(0, true);
      // fish food need only collide with walls
      %food.setCollisionGroups( 15 );

	  %move = DriftBehavior.createInstance();
	  %food.addBehavior(%move);
  
      arenaScene.add( %food ); 
  }
  
  function Player::create()
  {
	RoomManager.spawnFishFood();
	  
    
  }