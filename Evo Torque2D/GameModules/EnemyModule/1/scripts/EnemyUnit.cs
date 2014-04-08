//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function EnemyUnit::onAdd( %this )
{
	%this.myArena.EnemyCount++;
}

//-----------------------------------------------------------------------------
///ordering: armor/parry/acid/tar/blade/shooter/blob

function EnemyUnit::initialize(%this)
{
	exec("./EnemyUnitToolConfig.cs");		//separate file for tool config code
	//--Execute Nodes-----------------------
	exec("./Tools/ToolNode.cs");
	exec("./Tools/Armor/ToolArmor.cs");
	exec("./Tools/Parry/ToolParry.cs");
	exec("./Tools/Acid/ToolAcid.cs");
	exec("./Tools/Tar/ToolTar.cs");
	exec("./Tools/Blade/ToolBlade.cs");
	exec("./Tools/Shooter/ToolShooter.cs");
	
	%this.setSceneGroup(10);		//Enemy Unit sceneGroup

	%this.fullHealth = 100;
	%this.health = %this.fullHealth;
	%this.walkSpeed = 10;
	%this.setAngle(90);
	
	%this.sizeRatio = $pixelToWorldRatio;
	
	//%this.setupSprite();
	%this.setupBehaviors();

	%this.setSceneLayer(10);
    %this.setCollisionGroups( "5 15" );
	%this.setCollisionCallback(true);
			
	//Parse local chromsome and build body
	%this.maxBodySize = 0;								//largest (abs) indices for myBody position
	%this.configureTools(%this.myChromosome);		// ordering: shield/parry/acid/tar/blade/shooter/blob (+1)
	
	%this.myHealthbar = new CompositeSprite()
	{
		class = "Healthbar";
		owner = %this;
		xOffset = 0;
		//yOffset = 80*%this.sizeRatio;
		yOffset = (%this.maxBodySize + 1)*%this.myBodyContainer.getObject(0).myHeight;
	};

    %this.getScene().add( %this.myHealthbar );
}

//-----------------------------------------------------------------------------

function EnemyUnit::setupSprite( %this )
{
	%this.addSprite("0 0");
	%this.setSpriteImage("GameAssets:basicenemy", 0);
	%this.setSpriteSize(74, 78);
	
	%obj = new T2dShapeVector()   
    {   
	
        scenegraph = %this;   
    };    
    %obj.setPolyPrimitive( 4 );  
    %obj.setPolyCustom( 4, "0 0 0 1 1 1 1 0" );  
    %obj.setSize( 500, 300 );  
    %obj.setLineColor( "0 0 0 1" );  
    %obj.setFillMode( true );  
    %obj.setFillColor( "1 0 0" );  
    %obj.setFillAlpha( 0.6 );  
    %obj.setLayer( 1 ); 
}

//-----------------------------------------------------------------------------

function EnemyUnit::setupBehaviors( %this )
{
	exec("./behaviors/movement/Drift.cs");
	exec("./behaviors/ai/faceObject.cs");
	%driftMove = DriftBehavior.createInstance();
	%driftMove.speed = %this.walkSpeed;
	%this.addBehavior(%driftMove);
	
	%faceObj = FaceObjectBehavior.createInstance();
	%faceObj.object = %this.mainPlayer;
	%faceObj.rotationOffset = 0;
	%this.addBehavior(%faceObj);
}

//-----------------------------------------------------------------------------

function EnemyUnit::takeDamage( %this, %dmgAmount )
{
	%this.health -= %dmgAmount;
	
	if( %this.health <= 0)
	{
		%this.kill();
		%this.safeDelete();
	}

	%this.myHealthbar.assessDamage();
}



//-----------------------------------------------------------------------------
///concats and returns chromosome code

function EnemyUnit::getChromosome( %this )
{
	%chromosome = %this.toolArmor_count SPC
	%this.toolParry_count SPC
	%this.toolAcid_count SPC
	%this.toolTar_count SPC
	%this.toolBlade_count SPC
	%this.toolShooter_count SPC
	%this.toolBlob_count;
	
	return %chromosome;
}

//-----------------------------------------------------------------------------

function EnemyUnit::kill( %this )
{
	%this.myArena.EnemyCount--;
	
	
	echo("EnemyMod.EnemyUnit:" SPC %this.myBodyContainer.getCount());
	for(%i = 0; %i < %this.myBodyContainer.getCount(); %i++)
	{
		%currTool = %this.myBodyContainer.getObject(%j);
		//%this.getMyScene().remove(%currTool);
		%currTool.safeDelete();
	}
	
	%this.safeDelete();
	
	if(%this.myArena.EnemyCount <= 0)
	{
		%this.myArena.myManager.endCurrentLevel();
	}
}

//-----------------------------------------------------------------------------

function EnemyUnit::getMyScene( %this )
{
	return %this.getScene();
}

//-----------------------------------------------------------------------------

function EnemyUnit::destroy( %this )
{
	echo("EnemyMod.EnemyUnit: Deleted");
}
