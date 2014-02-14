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
	exec("./playerBullet/PlayerBullet.cs");
	
	%this.setSceneGroup(5);			//0: Player sceneGroup
	%this.setSceneLayer(5);
	%this.fixedAngle = true;
	
	%this.walkSpeed = 600;			// <-- f*cked (is there a max limit on linearVelocity??)
	%this.health = 100;
	%this.setPosition(0, 25);
	
	%this.setupSprite();
	%this.setupControls();

	%this.setUseMouseEvents(true);
	
    %this.createPolygonBoxCollisionShape(%this.getWidth(), %this.getHeight());
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	%this.setCollisionCallback(true);
}

//-----------------------------------------------------------------------------

function Player::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteAnimation("GameAssets:playerbaseAnim", 0);
	%this.setSpriteSize(159, 210);
}

//-----------------------------------------------------------------------------

function Player::setupControls( %this )
{
	exec("./behaviors/controls/PlayerMovementControls.cs");
	exec("./behaviors/controls/alignToJoystick.cs");
	
 	%controls = PlayerMovementControlsBehavior.createInstance();
	%controls.walkSpeed = %this.walkSpeed;
	%controls.upKey = "keyboard W";
	%controls.leftKey = "keyboard A";
	%controls.downKey = "keyboard S";
	%controls.rightKey = "keyboard D";
	%this.addBehavior(%controls);
	
	/* %controls = AlignToJoystickBehavior.createInstance();
	%controls.xAxis = "gamepad0 thumblx";
	%controls.yAxis = "gamepad0 thumbly";
	%this.addBehavior(%controls); */
}

//-----------------------------------------------------------------------------

function Player::onUpdate( %this )
{
	echo("mousemousemousezzzz");
}

//-----------------------------------------------------------------------------

function Player::destroy( %this )
{
}
