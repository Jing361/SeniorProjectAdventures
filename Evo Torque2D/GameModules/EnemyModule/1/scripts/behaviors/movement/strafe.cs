//-----------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

if (!isObject(WanderAroundBehavior))
{
  %template = new BehaviorTemplate(WanderAroundBehavior);
   
  %template.friendlyName = "Face Object";
  %template.behaviorType = "AI";
  %template.description  = "Set the object to face another object";

  %template.addBehaviorField(turnDelay, "When to change direction. (s)", int, 1);
  %template.addBehaviorField(numDires, "Number of directions allowed.", int, 4);
  %template.addBehaviorField(moveSpeed, "Mob move speed.", int, 10);
  %template.addBehaviorField(turnSpeed, "Mob turn speed.", int, 0);
}

function WanderAroundBehavior::onBehaviorAdd(%this)
{
  %this.changeDir();
}

function WanderAroundBehavior::changeDir(%this)
{
  %targetRotation = (getRandom(0, %this.numDires) * (360/%this.numDires));
/*
  if (%this.turnSpeed == 0)
    %this.owner.setAngle(%targetRotation);
  else
    %this.owner.rotateTo(%targetRotation, %this.turnSpeed);
*/
  %this.owner.setLinearVelocityPolar(%targetRotation + 90, %this.moveSpeed);
    
  %this.schedule(%this.turnDelay * 1000, "changeDir");
}
