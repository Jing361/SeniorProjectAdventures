//-----------------------------------------------------------------------------
// Basic player controls & behaviors
//-----------------------------------------------------------------------------

if (!isObject(PlayerMovementControlsBehavior))
{
    %template = new BehaviorTemplate(PlayerMovementControlsBehavior);

  %template.friendlyName = "Shooter Controls";
  %template.behaviorType = "Input";
  %template.description  = "Shooter style movement control";

  %template.addBehaviorField(upKey, "Key to bind to upward movement", keybind, "keyboard up");
  %template.addBehaviorField(downKey, "Key to bind to downward movement", keybind, "keyboard down");
  %template.addBehaviorField(leftKey, "Key to bind to left movement", keybind, "keyboard left");
  %template.addBehaviorField(rightKey, "Key to bind to right movement", keybind, "keyboard right");

  %template.addBehaviorField(fireKey, "", keybind, "keyboard space");
  %template.addBehaviorField(meleeKey, "", keybind, "keyboard E");
  %template.addBehaviorField(dashKey, "", keybind, "keyboard Q");
  %template.addBehaviorField(blockKey, "", keybind, "keyboard F");
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::onBehaviorAdd(%this)
{
    if (!isObject(GlobalActionMap))
       return;

  GlobalActionMap.bindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), "moveUp", %this);
  GlobalActionMap.bindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), "moveDown", %this);
  GlobalActionMap.bindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), "moveLeft", %this);
  GlobalActionMap.bindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), "moveRight", %this);

  GlobalActionMap.bindObj("keyboard", %this.fireKey, "pressFire", %this);
  GlobalActionMap.bindObj("keyboard", %this.meleeKey, "pressMelee", %this);
  GlobalActionMap.bindObj("keyboard", %this.dashKey, "pressDash", %this);
  GlobalActionMap.bindObj("keyboard", %this.blockKey, "pressBlock", %this);

	%this.up = 0;
	%this.down = 0;
	%this.left = 0;
	%this.right = 0;
	
   // %this.setUpdateCallback(true);
	
	//shot barrel offset (instead of bullet coming out of center of player)	
	%this.barrelXoffset = 55*%this.owner.sizeRatio;
	%this.barrelYoffset = -42*%this.owner.sizeRatio;
	
	//blade offset (instead of strike effect coming out of center of player)	
	%this.bladeXoffset = 100*%this.owner.sizeRatio;
	%this.bladeYoffset = 50*%this.owner.sizeRatio;
	
	%this.wallCheckDist = 2;
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::onBehaviorRemove(%this)
{
    if (!isObject(GlobalActionMap))
       return;

	//%this.owner.setUpdateCallback(true);

  GlobalActionMap.unbindObj(getWord(%this.upKey, 0), getWord(%this.upKey, 1), %this);
  GlobalActionMap.unbindObj(getWord(%this.downKey, 0), getWord(%this.downKey, 1), %this);
  GlobalActionMap.unbindObj(getWord(%this.leftKey, 0), getWord(%this.leftKey, 1), %this);
  GlobalActionMap.unbindObj(getWord(%this.rightKey, 0), getWord(%this.rightKey, 1), %this);

  GlobalActionMap.unbindObj("keyboard", %this.fireKey, %this);
  GlobalActionMap.unbindObj("keyboard", %this.meleeKey, %this);
  GlobalActionMap.unbindObj("keyboard", %this.dashKey, %this);
  GlobalActionMap.unbindObj("keyboard", %this.blockKey, %this);

	%this.up = 0;
	%this.down = 0;
	%this.left = 0;
	%this.right = 0;
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::onCollision(%this, %object, %collisionDetails)
{
}

//-----------------------------------------------------------------------------

function PlayerMovementControlsBehavior::onUpdate(%this)
{
	%this.updateMovement();
}

//------------------------------------------------------------------------------------
  
function PlayerMovementControlsBehavior::updateMovement(%this)
{	 
	if(! %this.owner.isDashing)
	{	
		%this.owner.setLinearVelocityX((%this.right - %this.left) * %this.owner.walkSpeed);
		%this.owner.setLinearVelocityY((%this.up - %this.down) * %this.owner.walkSpeed);
	}
	else
	{
		%this.right = 0;
		%this.left = 0;
		%this.up = 0;
		%this.down = 0;
	}
	
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::checkObjAtPoint(%this, %xPoint, %yPoint, %sceneGroup)
{
	%radius = 4;
	
	%checkPoint = %this.owner.getWorldPoint(%xPoint, %yPoint);

	
	%objList = %this.owner.getScene().pickCircle(%checkPoint, %radius, "-1", "-1", "collision");
	
	for(%i = 0; %i < getWordCount(%objList); %i++)
	{
		%currObjID = getWord(%objList, %i);
		if(%currObjID.getSceneGroup() == %sceneGroup)
		{
			
			return true;
		}
	}
	
	return false;
}

//------------------------------------------------------------------------------------
  
function PlayerMovementControlsBehavior::moveUp(%this, %val)
{
	if(%val == 1)
	{
		if( ! %this.checkObjAtPoint(0, %this.wallCheckDist, 15) )
		{
			%this.up = 1;
		}
		else
		{
			%this.up = 0;
		}
	}
	else
	{
		%this.up = 0;
	}
	
    //%this.updateMovement();
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::moveDown(%this, %val)
{
    if(%val == 1)
	{
		if( ! %this.checkObjAtPoint(0, -%this.wallCheckDist, 15) )
		{
			%this.down = 1;
		}
		else
		{
			%this.down = 0;
		}
	}
	else
	{
		%this.down = 0;
	}
   // %this.updateMovement();
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::moveLeft(%this, %val)
{
    if(%val == 1)
	{
		if( ! %this.checkObjAtPoint(-%this.wallCheckDist, 0, 15) )
		{
			%this.left = 1;
		}
		else
		{
			%this.left = 0;
		}
	}
	else
	{
		%this.left = 0;
	}
    //%this.updateMovement();
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::moveRight(%this, %val)
{
    if(%val == 1)
	{
		if( ! %this.checkObjAtPoint(%this.wallCheckDist, 0, 15) )
		{
			%this.right = 1;
		}
		else
		{
			%this.right = 0;
		}
	}
	else
	{
		%this.right = 0;
	}
	
    //%this.updateMovement();
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::pressFire(%this, %val)
{
	if(%val == 1)
	{
		// add a bullet to the arena
		%newBullet = new CompositeSprite()
		{
			class = "PlayerBullet";
			fireAngle = %this.owner.getAngle();
		};
		
		%this.owner.rangedCount++;
		%this.owner.getScene().add( %newBullet );
		
		
		%newBullet.setPosition(%this.owner.getWorldPoint(%this.barrelXoffset, %this.barrelYoffset) );
	}
} 

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::pressMelee(%this, %val)
{
	if(%val == 1)
	{
		// add a strike effect to the arena
		%newStriker = new CompositeSprite()
		{
			class = "PlayerStrike";
			strikeAngle = %this.owner.getAngle();
		};
			
		%this.owner.meleeCount++;
		%this.owner.getScene().add( %newStriker );
		
		%newStriker.setPosition(%this.owner.getWorldPoint(%this.bladeXoffset, %this.bladeYoffset) );
	}
} 

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::pressDash(%this, %val)
{
	if(%val == 1)
	{
		if((! %this.owner.isDashing) && (! %this.owner.tarred))
		{
			
			%newDashTrail = new CompositeSprite()
			{
				class = "PlayerDash";
				dashAngle = %this.owner.getAngle();
			};
				
			%this.owner.getScene().add( %newDashTrail );
			%newDashTrail.setPosition(%this.owner.getWorldPoint(-20*%this.owner.sizeRatio, 0) );
			
			
			%this.owner.isDashing = true;
			%this.owner.currDashDirection = %this.owner.getAngle();
			%this.owner.setLinearVelocityPolar(%this.owner.getAngle() + 90, %this.owner.dashSpeed);	
			
			%this.dashSchedule = schedule(%this.owner.dashLength, 0, "PlayerMovementControlsBehavior::endDash", %this);
			
			%this.owner.dashCount++;
		
		}
	}
} 

function PlayerMovementControlsBehavior::endDash(%this)
{
	%this.owner.isDashing = false;	
	%this.owner.setLinearVelocityPolar(0, 0);	
}

//------------------------------------------------------------------------------------

function PlayerMovementControlsBehavior::pressBlock(%this, %val)
{
	if(%val == 1)
	{
		%this.owner.blocker = new CompositeSprite()
		{
			class = "PlayerBlock";
			blockAngle = %this.owner.getAngle();
			owner = %this.owner;
		};
		
		if(%this.owner.blockCooldown < %this.owner.blocker.maxDamage - %this.owner.blockCooldownRefresh )
		{
			%this.owner.blockCount++;
			%this.owner.blocker.setPosition(%this.owner.getWorldPoint(0, 0) );
			// add a block effect to the arena
			%this.owner.getScene().add( %this.owner.blocker );
			
			%this.owner.blocker.takeDamage( %this.owner.blockCooldown );
		}
		else
		{
			if(isObject(%this.owner.blocker))
			{
				%this.owner.blocker.takeDamage( %this.owner.blockCooldown );
				%this.owner.blocker.safeDelete();
			}
		}
	}
	else if(%val == 0)
	{
		if(isObject(%this.owner.blocker))
		{
			%this.owner.blocker.safeDelete();
		}
	}
} 
