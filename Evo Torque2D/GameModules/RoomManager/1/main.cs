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
/*
	$UtilityObj = new SimObject()
	{
		class = "Utility";
	};
*/

	//activateDirectInput();
	//enableJoystick();
	//enableXInput();
	//$enableDirectInput=true;
	
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
    exec("./titleScreenGUI.cs");
    exec("./roomCompleteGUI.cs");
	exec("./scripts/behaviors/movement/shooterControls.cs");
	exec("./scripts/behaviors/movement/drift.cs");
	
	%gui_titleScreen = new SceneObject()
	{
		class = "TitleScreenGUI";
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
		currLevel = %this.CurrentLevel;
	};
	
	%arenaScene = new Scene();
	//%arenaScene.setDebugOn("collision");
	%arenaScene.layerSortMode0 = "Newest";
	%arenaScene.add(%gameArena);
	%gameArena.buildArena( );
	
	mainWindow.setScene( %arenaScene );
	
	%this.currentArena = %gameArena;
}

//-----------------------------------------------------------------------------

function RoomManager::endCurrentLevel( %this )
{
	echo("RoomManager.main: Room finished!");
	
	%this.writeRoomSummationFile();

	%this.currentArena.player.clearBehaviors();
	%this.currentArena.getScene().remove(%this.currentArena.player);
	
	%this.currentArena.getScene().schedule(320, "clear");  
	
	%this.goToRoomCompleteScreen();
}

//-----------------------------------------------------------------------------

function RoomManager::writeRoomSummationFile( %this )
{
	%plyrRangeCount = %this.currentArena.player.rangedCount;
	%plyrMeleeCount = %this.currentArena.player.meleeCount;
	%plyrBlockCount = %this.currentArena.player.blockCount;
	%plyrDashCount = %this.currentArena.player.dashCount;
	
	
	echo("RoomManager.main: Results:");
	echo(%plyrRangeCount);
	echo(%plyrMeleeCount);
	echo(%plyrBlockCount);
	echo(%plyrDashCount);
	echo("");
	
	%totalCount = %plyrRangeCount + %plyrMeleeCount + %plyrBlockCount + %plyrDashCount;
					
	if(%totalCount > 0)
	{	
		%plyrRangeCount = %plyrRangeCount/%totalCount;
		%plyrMeleeCount = %plyrMeleeCount/%totalCount;
		%plyrBlockCount = %plyrBlockCount/%totalCount;
		%plyrDashCount = %plyrDashCount/%totalCount;
	}
	
	echo("Result Percents:");
	echo(%plyrRangeCount);
	echo(%plyrMeleeCount);
	echo(%plyrBlockCount);
	echo(%plyrDashCount);
	echo(" total:" SPC %totalCount);
	
	echo("RoomManager.main: chromosome" SPC (%this.currentArena.roomChromosomes));
	
	%file = new FileObject();
	
	
	if(%file.openForWrite("utilities/ga_input.txt"))
	{
		echo("RoomManager.main: write file opened");
	
		if(%this.currentArena.roomShooterShotsFired > 0)
			%averageRangedDamage = (%this.currentArena.roomShooterDamage/%this.currentArena.roomShooterShotsFired);
		else
			%averageRangedDamage = 0;
		
		if(%this.currentArena.roomBladeAttackNums > 0)
			%averageMeleeDamage = (%this.currentArena.roomBladeDamage/%this.currentArena.roomBladeAttackNums);
		else
			%averageMeleeDamage = 0;
			
		echo("Room Avg. Melee:" SPC %averageMeleeDamage);
		echo("Room Avg. Range:" SPC %averageRangedDamage);
		
		%file.writeLine((5 + %this.CurrentLevel*2) @ "");
		%file.writeLine("");
		%file.writeLine(%plyrRangeCount @ "");
		%file.writeLine(%plyrMeleeCount @ "");
		%file.writeLine(%plyrBlockCount @ "");
		%file.writeLine(%plyrDashCount @ "");
		%file.writeLine(%averageMeleeDamage);					//enemy dps melee
		%file.writeLine(%averageRangedDamage);					//enemy dps range
		%file.writeLine("");
		%file.writeLine("");
		%file.writeLine(%this.currentArena.roomChromosomes);	//enemy subChromosomes (1/line)
		
		echo(%this.currentArena.roomChromosomes);
		echo("RoomManager.main: Summation file Written");
	}
	else
	{
		error("RoomManager.main: Summation file is not open for writing");
	}
	%file.close();
	echo("RoomManager.main: write file closed");
	
}   
 
//-----------------------------------------------------------------------------
  
function RoomManager::goToRoomCompleteScreen( %this )
{	
	%completeRoomScene = new Scene();
	%completeRoomScene.layerSortMode0 = "Newest";
	mainWindow.setScene( %completeRoomScene );
	
	%gui_roomCompleteScreen = new SceneObject()
	{
		class = "RoomCompleteGUI";
		myManager = %this;
	};
		 
	%gui_roomCompleteScreen.openScreen(%completeRoomScene);
}   

//-----------------------------------------------------------------------------
  
function RoomManager::endRoomCompleteScreen( %this )
{	
	%this.startNextLevel();
}

//-----------------------------------------------------------------------------

function RoomManager::destroy( %this )
{
}