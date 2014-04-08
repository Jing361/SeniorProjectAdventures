//-----------------------------------------------------------------------------
// PlayerModule: EnemyUnit class and functions
//-----------------------------------------------------------------------------

function ToolArmor::CreateInstance(%emyOwner, %type, %posX, %posY, %toolOrientation)  
{  
    %r = new SceneObject()  
	{  
		superclass = "ToolNode";
		class = "ToolArmor";   
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

function ToolArmor::initialize(%this)
{	
	Parent::initialize(%this);
	
	%this.healthBonus = 10 * %this.stackLevel;
	
	//add to enemy's total health
	%this.owner.fullHealth = %this.owner.fullHealth + %this.healthBonus;
	%this.owner.health = %this.owner.fullHealth;
}

//-----------------------------------------------------------------------------

function ToolArmor::setupSprite( %this )
{
	%this.owner.addSprite(%this.bodyPosX*%this.myWidth SPC %this.bodyPosY*%this.myHeight);
	
	%this.owner.setSpriteImage("GameAssets:tool_armor_a", 0);
	%this.owner.setSpriteSize(144 * %this.owner.sizeRatio, 80 * %this.owner.sizeRatio);
	%this.sortLevel = 1;
	
	%this.owner.setSpriteAngle(%this.orientation);
}

//-----------------------------------------------------------------------------

function ToolArmor::setupBehaviors( %this )
{

}
