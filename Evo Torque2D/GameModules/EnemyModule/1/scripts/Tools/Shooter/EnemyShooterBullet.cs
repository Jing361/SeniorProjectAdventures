//-----------------------------------------------------------------------------
// PlayerModule: playerBullet class and functions
//-----------------------------------------------------------------------------

function EnemyShooterBullet::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function EnemyShooterBullet::initialize(%this)
{
	%this.setSceneGroup(11);
	%this.setSceneLayer(11);
	%this.fixedAngle = true;
	
	%this.shotDamage = 10;
	%this.shotSpeed = 75;		
	%this.setBullet(true);
	%this.sizeRatio = $pixelToWorldRatio;
	%this.myWidth = 30 * %this.sizeRatio;
	%this.myHeight = 8 * %this.sizeRatio;
	
	%this.setupSprite();
		
	%this.setLinearVelocityPolar(%this.fireAngle+90, %this.shotSpeed);		//0 degrees is down
	
    %this.createPolygonBoxCollisionShape(%this.myWidth, %this.myHeight);
    %this.setCollisionShapeIsSensor(0, true);
	
    //%this.setCollisionGroups($UtilityObj.getCollisionGroup("Player") SPC $UtilityObj.getCollisionGroup("Walls"));
    %this.setCollisionGroups( "5 15" );
	%this.setCollisionCallback(true);
}

//-----------------------------------------------------------------------------

function EnemyShooterBullet::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteImage("GameAssets:shootershot", 0);
	%this.setSpriteSize(%this.myWidth, %this.myHeight);
	%this.setAngle(%this.fireAngle);
}

//-----------------------------------------------------------------------------

function EnemyShooterBullet::onCollision(%this, %object, %collisionDetails)
{
	if(%object.getSceneGroup() == 5)
	{
		%object.takeDamage(%this.shotDamage);
		%this.safeDelete();
	}
	else if(%object.getSceneGroup() == 15)
	{
		%this.safeDelete();
	}
}

//-----------------------------------------------------------------------------

function EnemyShooterBullet::destroy( %this )
{
}
