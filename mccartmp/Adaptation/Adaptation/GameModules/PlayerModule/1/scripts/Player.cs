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
	%this.setSceneGroup(5);			//0: Player sceneGroup
	%this.setSceneLayer(5);
	%this.fixedAngle = true;
	
	%this.walkSpeed = 80.0;
	%this.health = 100;
	%this.setPostion(0, 25);
	
	%this.setupSprite();
	%this.setupControls();

	
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
	%this.setSpriteSize(53, 70);
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
