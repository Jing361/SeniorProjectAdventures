//-----------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

if (!isObject(MinDistanceBehavior))
{
  %template = new BehaviorTemplate(MinDistanceBehavior);
   
  %template.friendlyName = "Face Object";
  %template.behaviorType = "AI";
  %template.description  = "Set the object to face another object";

  %template.addBehaviorField(distance, "", int, 15);
}

function MinDistanceBehavior::onBehaviorAdd(%this)
{
  %this.setUpdateCallback(true);
}

function MinDistanceBehavior::onUpdate( %this )
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
