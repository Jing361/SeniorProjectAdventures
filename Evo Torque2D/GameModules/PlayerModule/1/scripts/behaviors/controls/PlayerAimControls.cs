//-----------------------------------------------------------------------------
// Basic player aim conrols
//-----------------------------------------------------------------------------

if (!isObject(PlayerAimControlsBehavior))
{
    %template = new BehaviorTemplate(PlayerAimControlsBehavior);

    %template.friendlyName = "Shooter Aiming";
    %template.behaviorType = "Input";
    %template.description  = "Shooter style aiming control";
}

function PlayerAimControlsBehavior::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;
	   
	//cardinal
    GlobalActionMap.bindObj("keyboard", "I", "faceUp", %this);
    GlobalActionMap.bindObj("keyboard", "L", "faceRight", %this);
    GlobalActionMap.bindObj("keyboard", "K", "faceDown", %this);
    GlobalActionMap.bindObj("keyboard", "J", "faceLeft", %this);
	
	//diagonal
    GlobalActionMap.bindObj("keyboard", "U", "faceUL", %this);
    GlobalActionMap.bindObj("keyboard", "O", "faceUR", %this);
    GlobalActionMap.bindObj("keyboard", "Comma", "faceDR", %this);
    GlobalActionMap.bindObj("keyboard", "M", "faceDL", %this);
}

function PlayerAimControlsBehavior::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

    GlobalActionMap.unbindObj("keyboard", "I", %this);
    GlobalActionMap.unbindObj("keyboard", "L", %this);
    GlobalActionMap.unbindObj("keyboard", "K", %this);
    GlobalActionMap.unbindObj("keyboard", "J", %this);
	
    GlobalActionMap.unbindObj("keyboard", "U", %this);
    GlobalActionMap.unbindObj("keyboard", "O", %this);
    GlobalActionMap.unbindObj("keyboard", "Comma", %this);
    GlobalActionMap.unbindObj("keyboard", "M", %this);
}

//------------------------------------------------------------------------------------
  
function PlayerAimControlsBehavior::updateFacingDirection(%this, %direction)
{
	%this.owner.setAngle(%direction);
}

function PlayerAimControlsBehavior::faceUp(%this, %val)
{	
	%targetRotation = 90;
	
	if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceDown(%this, %val)
{	
	%targetRotation = 270;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceLeft(%this, %val)
{
	%targetRotation = 180;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceRight(%this, %val)
{
	%targetRotation = 0;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

//------------------------------------------------------------------------------------
function PlayerAimControlsBehavior::faceUR(%this, %val)
{	
	%targetRotation = 45;
	
	if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceDR(%this, %val)
{	
	%targetRotation = 315;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceDL(%this, %val)
{
	%targetRotation = 225;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

function PlayerAimControlsBehavior::faceUL(%this, %val)
{
	%targetRotation = 135;
	
    if(%val == 1)
		%this.updateFacingDirection(%targetRotation);
}

//------------------------------------------------------------------------------------