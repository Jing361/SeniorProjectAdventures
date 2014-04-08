//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function ToolTar::CreateInstance(%emyOwner, %type, %posX, %posY, %toolOrientation)  
{  
    %r = new SceneObject()  
	{  
		superclass = "ToolNode";
		class = "ToolTar";   
		owner = %emyOwner;
		toolType = %type;
		bodyPosX = %posX;
		bodyPosY = %posY;
		orientation = %toolOrientation;
		stackLevel = 1;
	};  
  
    return %r;  
}  

//-----------------------------------------------------------------------------

function ToolTar::setupSprite( %this )
{
	%this.owner.addSprite(%this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
	
	%this.owner.setSpriteImage("GameAssets:tool_tar_a", 0);
	%this.owner.setSpriteSize(64 * %this.owner.sizeRatio, 64 * %this.owner.sizeRatio);
	%this.sortLevel = 6;
	
	%this.owner.setSpriteAngle(%this.orientation);
}

//-----------------------------------------------------------------------------

function ToolTar::setupBehaviors( %this )
{

}
