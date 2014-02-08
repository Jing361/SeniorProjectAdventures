//-----------------------------------------------------------------------------
// Basic player controls & behaviors
//-----------------------------------------------------------------------------

if (!isObject(PlayerMovementControlsBehavior))
{
    %template = new BehaviorTemplate(PlayerMovementControlsBehavior);

    %template.friendlyName = "Shooter Controls";
    %template.behaviorType = "Input";
    %template.description  = "Shooter style movement control";

    %template.addBehaviorField(upKey, "Key to bind to upward movement", keybind, "keyboard up");
    %template.addBehaviorField(downKey, "Key to bind to downward movement", keybind, "keyboard down");
    %template.addBehaviorField(leftKey, "Key to bind to left movement", keybind, "keyboard left");
    %template.addBehaviorField(rightKey, "Key to bind to right movement", keybind, "keyboard right");
}

function PlayerMovementControlsBehavior::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;

	//new ActionMap(playerControls);
	
    GlobalActionMap.bindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), "moveUp", %this);
    GlobalActionMap.bindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), "moveDown", %this);
    GlobalActionMap.bindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), "moveLeft", %this);
    GlobalActionMap.bindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), "moveRight", %this);

    %this.up = 0;
    %this.down = 0;
    %this.left = 0;
    %this.right = 0;
}

function PlayerMovementControlsBehavior::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    %this.owner.disableUpdateCallback();

    GlobalActionMap.unbindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), %this);
    GlobalActionMap.unbindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), %this);

    %this.up = 0;
    %this.down = 0;
    %this.left = 0;
    %this.right = 0;
}

function PlayerMovementControlsBehavior::onCollision(%this, %object, %collisionDetails)
{
	if(%object.class $= "EnemyUnit")
	{
		%object.recycle(%object.side);
	}
	else
	{
		%this.up = 0;
		%this.down = 0;
		%this.left = 0;
		%this.right = 0;
		%this.updateMovement();
	}
}
  
function PlayerMovementControlsBehavior::updateMovement(%this)
{	 
    %this.owner.setLinearVelocityX((%this.right - %this.left) * %this.owner.walkSpeed);
    %this.owner.setLinearVelocityY((%this.up - %this.down) * %this.owner.walkSpeed);
}

function PlayerMovementControlsBehavior::moveUp(%this, %val)
{
    %this.up = %val;
    %this.updateMovement();
}

function PlayerMovementControlsBehavior::moveDown(%this, %val)
{
    %this.down = %val;
    %this.updateMovement();
}

function PlayerMovementControlsBehavior::moveLeft(%this, %val)
{
    %this.left = %val;
    %this.updateMovement();
}

function PlayerMovementControlsBehavior::moveRight(%this, %val)
{
    %this.right = %val;
    %this.updateMovement();
}

