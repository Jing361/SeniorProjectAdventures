//-----------------------------------------------------------------------------
// PlayerModule: player class and functions
//-----------------------------------------------------------------------------

function Player::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function Player::initialize(%this)
{
	%this.verticalSpeed = 60.0;
	%this.horizontalSpeed = 60.0;
	%this.health = 100;
	%this.setPostion(0, 25);
	
	%this.setupSprite();
	%this.setupControls();

	
    %this.createPolygonBoxCollisionShape("74 78");
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	
}

//-----------------------------------------------------------------------------

function Player::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteAnimation("GameAssets:playerbaseAnim", 0);
	%this.setSpriteSize(100, 100);
}

//-----------------------------------------------------------------------------

function Player::setupControls( %this )
{
	exec("./behaviors/controls/PlayerMovementControls.cs");
	%controls = PlayerMovementControlsBehavior.createInstance();
	%controls.upKey = "keyboard W";
	%controls.leftKey = "keyboard A";
	%controls.downKey = "keyboard S";
	%controls.rightKey = "keyboard D";
	%this.addBehavior(%controls);
}

//-----------------------------------------------------------------------------

function Player::destroy( %this )
{
}
