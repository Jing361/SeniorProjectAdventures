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

/*	
	echo("Mikey man");
	enableXInput();  
	$enableDirectInput = true;  
	activateDirectInput();  
	echoInputState();
	echo("Mikey man");
	*/
	
    // load some scripts and variables
    //exec("./scripts/arena.cs");
    exec("./titleScreen.cs");
	exec("./scripts/behaviors/movement/shooterControls.cs");
	exec("./scripts/behaviors/movement/drift.cs");
	
	%gui_titleScreen = new SceneObject()
	{
		class = "TitleScreen";
		myManager = %this;
	};
		
	%gui_titleScreen.openTitleScreen(mainScene);
}
    
//-----------------------------------------------------------------------------
  
  function RoomManager::changeToArena( %this )
  {
	%gameArena = new SceneObject()
	{
		class = "Arena";
	};
	%arenaScene = new Scene();
	%gameArena.buildArena( %arenaScene );
    mainWindow.setScene( %arenaScene );
  }
  
//-----------------------------------------------------------------------------

function RoomManager::destroy( %this )
{
}