//-----------------------------------------------------------------------------
// PlayerModule: player class and functions
//-----------------------------------------------------------------------------

function Player::onAdd( %this )
{

}

//-----------------------------------------------------------------------------

function Player::initialize(%this)
{
	exec("./playerBullet/PlayerBullet.cs");
	exec("./playerStrike/PlayerStrike.cs");
	
	%this.setSceneGroup(5);			//0: Player sceneGroup
	%this.setSceneLayer(5);
	%this.fixedAngle = true;
	
	//Stats
	%this.fullHealth = 150;
	%this.health = %this.fullHealth;
	%this.sizeRatio = $pixelToWorldRatio;
	%this.myWidth = 210 * %this.sizeRatio;
	%this.myHeight = 169 * %this.sizeRatio;
	
	%this.walkSpeed = 40;
	%this.health = 100;
	%this.setPosition(0, 25);
	
	//Algorithm Counters
	%this.rangedCount = 0;
	%this.meleeCount = 0;
	%this.blockCount = 0;
	%this.dashCount = 0;
	
	
	%this.setupSprite();
	%this.setupControls();
	
	%this.setupCollisionShape();
	
	%this.myHealthbar = new CompositeSprite()
	{
		class = "Healthbar";
		owner = %this;
		xOffset = 0;
		yOffset = 150*%this.sizeRatio;
		curved = true;
	};
	
	%this.getScene().add( %this.myHealthbar );
}

//-----------------------------------------------------------------------------

function Player::addMyHealthbar( %this )
{
}

//-----------------------------------------------------------------------------

function Player::setupCollisionShape( %this )
{
	%offsetX = %this.myWidth*(5/12);
	%offsetY = %this.myHeight*(5/12);
	
	%boxSizeRatio = 0.75;
	%shapePoints = 
	0 SPC %offsetY*%boxSizeRatio SPC 
	%offsetX*%boxSizeRatio - %this.myWidth*(1/10) SPC 0 SPC 
	0 SPC - %offsetY*%boxSizeRatio SPC 
	-%offsetX*%boxSizeRatio SPC 0;	
	
	%this.createPolygonCollisionShape(%shapePoints);
	
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	%this.setCollisionCallback(true);
}
//-----------------------------------------------------------------------------

function Player::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteAnimation("GameAssets:playerbaseAnim", 0);
	%this.setSpriteName("BodyAnim");
	%this.setSpriteSize(%this.myWidth, %this.myHeight);
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
	
/* 	%xBoxControl = AlignToJoystickBehavior.createInstance();
	%xBoxControl.xAxis = "joystick0 xaxis";
	%xBoxControl.yAxis = "joystick0 yaxis";
	%xBoxControl.rotationOffset = 90;
	%this.addBehavior(%xBoxControl); */
	
	
	exec("./behaviors/controls/faceMouse.cs");
	
	%faceMse = FaceMouseBehavior.createInstance();
	%faceMse.object = mainPlayer;
	%faceMse.rotationOffset = -90;
	%this.addBehavior(%faceMse);
	
	/*
	exec("./behaviors/controls/mouseInput.cs");
	
	%mouseInput = PlayerMouseInputBehavior.createInstance();
	%this.addBehavior(%mouseInput);
	*/
}

//-----------------------------------------------------------------------------

function Player::takeDamage( %this, %dmgAmount )
{
	%this.health -= %dmgAmount;
	
	if( %this.health <= 0)
	{
		%this.safeDelete();
	}

	%this.myHealthbar.assessDamage();
}

//-----------------------------------------------------------------------------

function Player::destroy( %this )
{
	%this.clearBehaviors();
}
