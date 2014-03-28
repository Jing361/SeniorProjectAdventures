//-----------------------------------------------------------------------------
// Room manager, sets up canvas and arena
//-----------------------------------------------------------------------------


//globals
//$roomWidth = 1280;
//$roomHeight = 960;
$roomWidth = 160;
$roomHeight = 120;
$pixelToWorldRatio = $roomWidth/1600;

//---------------------------------------------------------------------

function RoomManager::create( %this )
{   
	activateDirectInput();
	enableJoystick();
	enableXInput();
	$enableDirectInput=true;
	
	setRandomSeed(getRealTime());

    new Scene(mainScene)
	{
		//class="defualtWindow";
	};

    new SceneWindow(mainWindow)
	{
		//useWindowMouseEvents = "1";
	};
	
	/*
	//Broken/delete me
	mainWindow.UseObjectInputEvents = true;
	
	%inputter = new ScriptObject()  
	{  
	   class="ExampleListener";  
	};  

	mainWindow.addInputListener(%inputter);
	*/
	
	
    mainWindow.profile = new GuiControlProfile();
    Canvas.setContent(mainWindow);
	
	new ScriptObject(InputManager);
	mainWindow.addInputListener(InputManager);

    mainWindow.setScene(mainScene);
    mainWindow.setCameraPosition( 0, 0 );
	
	mainScene.layerSortMode0 = "Newest";
	
    mainWindow.setCameraSize( $roomWidth, $roomHeight );
    //mainWindow.setCameraSize( $roomWidth*1.2, $roomHeight*1.2 );	//zoomed out cam

	/*
	mainScene.enableXinput();
	$enableDirectInput=true;
	mainScene.activateDirectInput();
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
	
	//Lasting Variables
	%this.CurrentLevel = 0;
}
    
//-----------------------------------------------------------------------------
  
function RoomManager::startNextLevel( %this )
{
	%this.CurrentLevel++;
	
	%gameArena = new SceneObject()
	{
		class = "Arena";
		myManager = %this;
	};
	
	%arenaScene = new Scene();
	%arenaScene.setDebugOn("collision");
	%arenaScene.layerSortMode0 = "X";
	%arenaScene.add(%gameArena);
	%gameArena.buildArena( );
	
	mainWindow.setScene( %arenaScene );
	
	%this.currentArena = %gameArena;
}

//-----------------------------------------------------------------------------

function RoomManager::endCurrentLevel( %this )
{
	%this.currentArena.player.clearBehaviors();
	%this.currentArena.getScene().remove(%this.currentArena.player);
	%this.startNextLevel();
}

//-----------------------------------------------------------------------------

function RoomManager::destroy( %this )
{
}