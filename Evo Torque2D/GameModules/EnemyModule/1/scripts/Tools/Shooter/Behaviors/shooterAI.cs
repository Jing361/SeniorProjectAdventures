//-----------------------------------------------------------------------------
// Basic player controls & behaviors
//-----------------------------------------------------------------------------

if (!isObject(ShooterToolBehavior))
{
    %template = new BehaviorTemplate(ShooterToolBehavior);

    %template.friendlyName = "Mouse Controls";
    %template.behaviorType = "Input";
    %template.description  = "Shooter style movement control";

    %template.addBehaviorField(walkSpeed, "Speed of travel", float, 0.0);
}

function ShooterToolBehavior::onBehaviorAdd(%this)
{

    //%this.setUpdateCallback(true);
	
	%this.mySchedule = schedule(%this.owner.reloadTime, 0, "ToolShooter::shoot", %this.owner);
}

function ShooterToolBehavior::onBehaviorRemove(%this)
{
}

//-----------------------------------------------------------------------------
/*
function PlayerStrike::onUpdate(%this)
{
	%this.setSpriteBlendAlpha(getEventTimeLeft(%this.mySchedule) / %this.lifeSpan);
}
*/
//------------------------------------------------------------------------------------

