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
	
	mainScene.layerSortMode0 = "Newest";
	
    mainScene.setDebugOn( "aabb" );			//bound boxes visible
	
    mainWindow.setCameraSize( $roomWidth, $roomHeight );
    //mainWindow.setCameraSize( $roomWidth*1.2, $roomHeight*1.2 );	//zoomed out cam

	enableXinput();
	$enableDirectInput=true;
	activateDirectInput();
	
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
	new Scene(arenaScene);
	arenaScene.setDebugOn( "aabb" );	
	arenaScene.layerSortMode0 = "X";
	%gameArena.buildArena( );
    mainWindow.setScene( arenaScene );
  }
  
//-----------------------------------------------------------------------------

function RoomManager::destroy( %this )
{
}