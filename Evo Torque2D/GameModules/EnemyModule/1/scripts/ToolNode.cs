//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function ToolNode::onAdd( %this )
{
	%this.initialize();
}

//-----------------------------------------------------------------------------

function ToolNode::initialize(%this)
{	
	%this.setSceneGroup(10);		//Enemy Unit sceneGroup

	%this.myWidth = 64 * %this.owner.sizeRatio;
	%this.myHeight = 64 * %this.owner.sizeRatio;

	%this.setupBehaviors();
	%this.setupCollisionShape();

	%this.setCollisionCallback(false);
	
}

//-----------------------------------------------------------------------------

function ToolNode::setupCollisionShape( %this )
{
	%offsetX = %this.myWidth/2;
	%offsetY = %this.myHeight/2;
	
	%shapePoints = 
	%this.bodyPosX*%this.myWidth - %offsetX SPC %this.bodyPosY*%this.myHeight - %offsetY SPC 
	(%this.bodyPosX + 1)*%this.myWidth - %offsetX SPC %this.bodyPosY*%this.myHeight - %offsetY SPC 
	(%this.bodyPosX + 1)*%this.myWidth - %offsetX SPC (%this.bodyPosY + 1)*%this.myHeight - %offsetY SPC 
	%this.bodyPosX*%this.myWidth - %offsetX SPC (%this.bodyPosY + 1)*%this.myHeight - %offsetY;
		
	%this.owner.createPolygonCollisionShape(%shapePoints);
}

//-----------------------------------------------------------------------------

function ToolNode::setupSprites( %this )
{
	//echo(%this.getId() SPC %this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
	
	%this.owner.addSprite(%this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
	
	//switch...case
	if(%this.toolType $= "Blob") {
		%this.setupSpriteBlob();
	}
	else if(%this.toolType $= "Armor"){
		%this.setupSpriteArmor();
	}
	else if(%this.toolType $= "Parry"){
		%this.setupSpriteParry();
	}
	else if(%this.toolType $= "Acid"){
		%this.setupSpriteAcid();
	}
	else if(%this.toolType $= "Tar"){
		%this.setupSpriteTar();
	}
	else if(%this.toolType $= "Blade"){
		%this.setupSpriteBlade();
	}
	else if(%this.toolType $= "Shooter"){
		%this.setupSpriteShooter();
	}
	
	%this.owner.setSpriteAngle(%this.orientation);
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteBlob( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_blob1x1_a", 0);
	%this.owner.setSpriteSize(88 * %this.owner.sizeRatio, 88 * %this.owner.sizeRatio);
	%this.sortLevel = 2;
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteArmor( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_armor_a", 0);
	%this.owner.setSpriteSize(144 * %this.owner.sizeRatio, 80 * %this.owner.sizeRatio);
	%this.sortLevel = 1;
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteParry( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_parry_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 5;
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteAcid( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_acid_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 5;
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteTar( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_tar_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 5;
}
//-----------------------------------------------------------------------------

function ToolNode::setupSpriteBlade( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_blade_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 5;
}
//-----------------------------------------------------------------------------

function ToolNode::setupSpriteShooter( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_shooter_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 6;
}


//-----------------------------------------------------------------------------

function ToolNode::setupBehaviors( %this )
{
	exec("./behaviors/movement/Drift.cs");
	%driftMove = DriftBehavior.createInstance();
	%this.addBehavior(%driftMove);
}

//-----------------------------------------------------------------------------

function ToolNode::getOpenSlots( %this )
{
	if(%this.toolType !$= "Blob")
		return "";
		
	%left = (%this.bodyPosX - 1) SPC (%this.bodyPosY);
	%down = (%this.bodyPosX) SPC (%this.bodyPosY - 1);
	%right = (%this.bodyPosX + 1) SPC (%this.bodyPosY);
	%up = (%this.bodyPosX) SPC (%this.bodyPosY + 1);
	
	return %left SPC %down SPC %right SPC %up;
}
//-----------------------------------------------------------------------------

function ToolNode::getBodyPosistion( %this )
{
	return (%this.bodyPosX) SPC (%this.bodyPosY);
}

//-----------------------------------------------------------------------------

function ToolNode::destroy( %this )
{
}
