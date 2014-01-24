//-----------------------------------------------------------------------------
// Basic menu controls and listeners
//-----------------------------------------------------------------------------

if (!isObject(MenuControl))
{
    %template = new BehaviorTemplate(MenuControl);

    %template.friendlyName = "Menu Controls";
    %template.behaviorType = "Input";
    %template.description  = "Menu and option basic control";

    %template.addBehaviorField(enterKey, "Key to bind to next room", keybind, "keyboard enter");
}

function MenuControl::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;
	
    GlobalActionMap.bindObj(getWord(%this.enterKey, 0), getWord(%this.enterKey, 1), "changeRoom", %this);
}

function MenuControl::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    %this.owner.disableUpdateCallback();

    GlobalActionMap.unbindObj(getWord(%this.enterKey, 0), getWord(%this.enterKey, 1), %this);
}



function MenuControl::changeRoom(%this, %val)
{
	new Scene(arenaScene);
	buildArena(arenaScene);
    mainWindow.setScene(arenaScene);
    GlobalActionMap.unbindObj(getWord(%this.enterKey, 0), getWord(%this.enterKey, 1), %this);
}
