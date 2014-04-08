//-----------------------------------------------------------------------------
// PickupModule: player class and functions
//-----------------------------------------------------------------------------

function Pickup::onAdd( %this )
{

}

//-----------------------------------------------------------------------------

function Pickup::initialize(%this)
{
	//exec("./x/x.cs");
	
	%this.setSceneGroup(9);	
	%this.setSceneLayer(9);
	%this.fixedAngle = true;
	
	//Stats
	%this.sizeRatio = $pixelToWorldRatio;
	%this.myWidth = 75 * %this.sizeRatio;
	%this.myHeight = 75 * %this.sizeRatio;
	
	%this.setPosition(50, 30);
	
	%this.setupSprite();
	
	%this.setupCollisionShape();

}

//-----------------------------------------------------------------------------

function Pickup::addMyHealthbar( %this )
{
}

//-----------------------------------------------------------------------------

function Pickup::setupCollisionShape( %this )
{	
	%boxSizeRatio = 0.75;
	
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "5" );
	%this.setCollisionCallback(true);
}
//-----------------------------------------------------------------------------

function Pickup::setupSprite( %this )
{
	/*
	%this.addSprite("0 0");
	%this.setSpriteAnimation("GameAssets:playerbaseAnim", 0);
	%this.setSpriteName("BodyAnim");
	%this.setSpriteSize(%this.myWidth, %this.myHeight);
	*/
}

//-----------------------------------------------------------------------------

function Pickup::destroy( %this )
{
}
