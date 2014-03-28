//-----------------------------------------------------------------------------
// Basic player aim conrols
//-----------------------------------------------------------------------------

if (!isObject(PlayerAimControlsBehavior))
{
    %template = new BehaviorTemplate(PlayerAimControlsBehavior);

    %template.friendlyName = "Shooter Controls";
    %template.behaviorType = "Input";
    %template.description  = "Shooter style movement control";

    %template.addBehaviorField(walkSpeed, "Speed of travel", float, 0.0);
    %template.addBehaviorField(upKey, "Key to bind to upward movement", keybind, "keyboard up");
    %template.addBehaviorField(downKey, "Key to bind to downward movement", keybind, "keyboard down");
    %template.addBehaviorField(leftKey, "Key to bind to left movement", keybind, "keyboard left");
    %template.addBehaviorField(rightKey, "Key to bind to right movement", keybind, "keyboard right");
}

function PlayerAimControlsBehavior::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    GlobalActionMap.bindObj("keyboard", "I", "faceUp", %this);
    GlobalActionMap.bindObj("keyboard", "L", "faceRight", %this);
    GlobalActionMap.bindObj("keyboard", "K", "faceDown", %this);
    GlobalActionMap.bindObj("keyboard", "J", "faceLeft", %this);


	%this.up = 0;
	%this.down = 0;
	%this.left = 0;
	%this.right = 0;
}

function PlayerAimControlsBehavior::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    GlobalActionMap.unbindObj("keyboard", "I", %this);
    GlobalActionMap.unbindObj("keyboard", "L", %this);
    GlobalActionMap.unbindObj("keyboard", "K", %this);
    GlobalActionMap.unbindObj("keyboard", "J", %this);

	%this.up = 0;
	%this.down = 0;
	%this.left = 0;
	%this.right = 0;
}

//------------------------------------------------------------------------------------
  
function PlayerAimControlsBehavior::updateFacingDirection(%this)
{	 
	%verti = %this.up - %this.down;
	%horiz = %this.right - %this.left;
	
	%targetRotation = 0;
	
	if(%verti == 1)
	{
		if(%horiz == 0)					//up
		{
			%targetRotation = 90;
		}
		else if(%horiz == 1)			//up-right
		{
			%targetRotation = 45;
		}
		else if(%horiz == -1)			//up-left
		{
			%targetRotation = 135;
		}
	}
	else if(%verti == -1)
	{
		if(%horiz == 0)					//down
		{
			%targetRotation = 270;
		}
		else if(%horiz == 1)			//down-right
		{
			%targetRotation = 315;
		}
		else if(%horiz == -1)			//down-left
		{
			%targetRotation = 225;
		}
	}
	else if(%horiz == 1)				//right
	{
		%targetRotation = 0;
	}
	else if(%horiz == -1)				//left
	{
		%targetRotation = 180;
	}
	else
	{
		return;
	}
	
	%this.owner.setAngle(%targetRotation);
}

function PlayerAimControlsBehavior::faceUp(%this, %val)
{
	%this.up = %val;
	
	if(%val == 1)
		%this.updateFacingDirection();
}

function PlayerAimControlsBehavior::faceDown(%this, %val)
{
	%this.down = %val;
	
    if(%val == 1)
		%this.updateFacingDirection();
}

function PlayerAimControlsBehavior::faceLeft(%this, %val)
{
	%this.left = %val;
	
    if(%val == 1)
		%this.updateFacingDirection();
}

function PlayerAimControlsBehavior::faceRight(%this, %val)
{
	%this.right = %val;
	
    if(%val == 1)
		%this.updateFacingDirection();
}

//------------------------------------------------------------------------------------