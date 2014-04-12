//-----------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

if (!isObject(MaxDistanceBehavior))
{
  %template = new BehaviorTemplate(MaxDistanceBehavior);
   
  %template.friendlyName = "Face Object";
  %template.behaviorType = "AI";
  %template.description  = "Set the object to face another object";

  %template.addBehaviorField(distance, "", int, 25);
}

function MaxDistanceBehavior::onBehaviorAdd(%this)
{
  %this.setUpdateCallback(true);
}

function MaxDistanceBehavio::onUpdate( %this )
{
	if(!isObject(%this.owner))
	{
		%this.safeDelete();
	}
	else
	{
		%targetRotation = Vector2AngleToPoint(%this.owner.getWorldPosistion(), %this.owner.owner.mainTarget.getPosition()) - 90;
		%this.rotateTo(%targetRotation, %this.turnSpeed);
		%this.setPosition(%this.owner.getWorldPosistion());
	}
}
