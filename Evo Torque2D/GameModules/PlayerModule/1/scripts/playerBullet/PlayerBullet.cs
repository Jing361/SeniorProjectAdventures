//-----------------------------------------------------------------------------
// PlayerModule: playerBullet class and functions
//-----------------------------------------------------------------------------

function PlayerBullet::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function PlayerBullet::initialize(%this)
{
	%this.setSceneGroup(6);
	%this.setSceneLayer(6);
	%this.fixedAngle = true;
	
	%this.shotSpeed = 75;		
	%this.setBullet(true);
	%this.sizeRatio = $pixelToWorldRatio;
	%this.myWidth = 39 * %this.sizeRatio;
	%this.myHeight = 24 * %this.sizeRatio;
	
	%this.setupSprite();
		
	%this.setLinearVelocityPolar(%this.fireAngle+90, %this.shotSpeed);		//0 degrees is down
	
    %this.createPolygonBoxCollisionShape(%this.myWidth, %this.myHeight);
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	%this.setCollisionCallback(true);
}

//-----------------------------------------------------------------------------

function PlayerBullet::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteImage("GameAssets:playershot", 0);
	%this.setSpriteSize(%this.myWidth, %this.myHeight);
	%this.setAngle(%this.fireAngle);
}

//-----------------------------------------------------------------------------

function PlayerBullet::onCollision(%this, %object, %collisionDetails)
{
	if(%object.class $= "EnemyUnit")
	{
		%object.recycle(%object.side);
		%this.safeDelete();
	}
}

//-----------------------------------------------------------------------------

function PlayerBullet::destroy( %this )
{
}
