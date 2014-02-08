//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function EnemyUnit::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function EnemyUnit::initialize(%this)
{
	%this.setSceneGroup(10);		//Enemy Unit sceneGroup

	%this.health = 100;
	
	%this.setupSprite();
	%this.setupBehaviors();

	%this.setSceneLayer(10);
	
    %this.createPolygonBoxCollisionShape(74, 78);
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "5 15" );
	//%this.CollisionCallback = true;
	%this.setCollisionCallback(true);
	
	echo(%this.getCollisionShapeArea(0));
	echo("hhh");
}

//-----------------------------------------------------------------------------

function EnemyUnit::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteImage("GameAssets:basicenemy", 0);
	%this.setSpriteSize(74, 78);
}

//-----------------------------------------------------------------------------

function EnemyUnit::setupBehaviors( %this )
{
	exec("./behaviors/movement/Drift.cs");
	%driftMove = DriftBehavior.createInstance();
	%this.addBehavior(%driftMove);
}

//-----------------------------------------------------------------------------

function EnemyUnit::destroy( %this )
{
}
