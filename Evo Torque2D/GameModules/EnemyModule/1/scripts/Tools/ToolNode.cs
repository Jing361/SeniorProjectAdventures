//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function ToolNode::CreateInstance(%emyOwner, %type, %posX, %posY, %toolOrientation)  
{  
    %r = new SceneObject()  
	{  
		class = "ToolNode";  
		owner = %emyOwner;
		toolType = %type;
		bodyPosX = %posX;
		bodyPosY = %posY;
		orientation = %toolOrientation;
		drawSprite = true;
		stackLevel = 1;
	};  
  
    return %r;  
}  

//-----------------------------------------------------------------------------

function ToolNode::onAdd( %this )
{
	//%this.initialize();
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
	
	%this.setupSprite();
	
	%this.addStackSprites();
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

function ToolNode::setupSprite( %this )
{
	if(%this.drawSprite)
	{
		
		%this.owner.addSprite(%this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
		
		if(%this.toolType $= %this.owner.blobToolName) {
			%this.setupSpriteBlob();
		}

		%this.owner.setSpriteAngle(%this.orientation);
		
	}
}

//-----------------------------------------------------------------------------

function ToolNode::addStackSprites( %this )
{
	%perColumn = 4;
	
	for(%i = 0; %i < %this.stackLevel - 1; %i++)
	{
		%x = %this.bodyPosX*%this.myWidth;
		%y = %this.bodyPosY*%this.myHeight;
		
		%offsetter = 2.5;
		%increment = 13 * %this.owner.sizeRatio;
		
		if(%this.orientation == 0)
		{
			%y = %y - ((%this.myHeight/%offsetter) + (mFloor(%i/%perColumn)*1.25));
			
			%x = %x + %increment*(%i % %perColumn);
		}
		else if(%this.orientation == 90)
		{
			%x = %x - ((%this.myWidth/%offsetter) + (mFloor(%i/%perColumn)*1.25));
			
			%y = %y + %increment*(%i % %perColumn);
		}
		else if(%this.orientation == 180)
		{
			%y = %y + ((%this.myHeight/%offsetter) + (mFloor(%i/%perColumn)*1.25));
			
			%x = %x - %increment*(%i % %perColumn);
		}
		else if(%this.orientation == 270)
		{
			%x = %x + ((%this.myWidth/%offsetter) + (mFloor(%i/%perColumn)*1.25));
			
			%y = %y - %increment*(%i % %perColumn);
		}
		
		%this.owner.addSprite(%x SPC %y);
		
		%this.owner.setSpriteImage("GameAssets:tool_toolStacking", 0);
		%this.owner.setSpriteSize(12 * %this.owner.sizeRatio, 12 * %this.owner.sizeRatio);
		
		%this.owner.setSpriteAngle(%this.orientation);
	}
}

//-----------------------------------------------------------------------------

function ToolNode::setupSpriteBlob( %this )
{
	%this.owner.setSpriteImage("GameAssets:tool_blob1x1_a", 0);
	%this.owner.setSpriteSize(88 * %this.owner.sizeRatio, 88 * %this.owner.sizeRatio);
	%this.sortLevel = 2;
}

//-----------------------------------------------------------------------------

function ToolNode::setupBehaviors( %this )
{

}

//-----------------------------------------------------------------------------

function ToolNode::getAdjacentSlots( %this )
{
	if(%this.toolType !$= %this.owner.blobToolName)
		return "";
		
	%left = (%this.bodyPosX - 1) SPC (%this.bodyPosY);
	%down = (%this.bodyPosX) SPC (%this.bodyPosY - 1);
	%right = (%this.bodyPosX + 1) SPC (%this.bodyPosY);
	%up = (%this.bodyPosX) SPC (%this.bodyPosY + 1);
	
	return %left SPC %down SPC %right SPC %up @ " ";
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
