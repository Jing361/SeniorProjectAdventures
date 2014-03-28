//-----------------------------------------------------------------------------
// Room manager, sets up canvas and arena
//-----------------------------------------------------------------------------

function Utility::create( %this )
{
	exec("./scripts/Healthbar.cs");
}
    
//-----------------------------------------------------------------------------

function Utility::destroy( %this )
{
}

//-----------------------------------------------------------------------------

function getAngle(%objectA, %objectB)
{
	%angle = Vector2AngleToPoint (%objectA.getPosition(), %objectB.getPosition());
	return %angle;
}

//-----------------------------------------------------------------------------