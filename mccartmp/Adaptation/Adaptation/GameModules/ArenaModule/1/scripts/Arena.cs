//-----------------------------------------------------------------------------
// Room setup and wall collisions
//-----------------------------------------------------------------------------

function Arena::getAnimationList(%this)
{		
   %list = "GameAssets:playerbaseAnim" @ "," @ "GameAssets:seahorseAnim" ;
}

//-----------------------------------------------------------------------------

function Arena::getAnimationSize(%this, %anim)
{
    switch$(%anim)
    {
        case "GameAssets:seahorseAnim":
        %animInfo = "7.5 15";
        
        case "GameAssets:playerbaseAnim":
        %animInfo = "11.35 15";
        
    }

    return %animInfo;
}

//-----------------------------------------------------------------------------

function Arena::buildArena(%this)
{
    // A pre-built Arena of size 100x75, with background.
    // Triggers will be provide around the edges to let the developer know when objects in the
    // arena have reached the edges.

    // Background
    %background = new Sprite();
    %background.setBodyType( "static" );
    %background.setImage( "GameAssets:background" );
    %background.setSize( $roomWidth, $roomHeight );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(30);
    arenaScene.add( %background );
    
    // Arena Edges
    %roomEdges = new Sprite();
    %roomEdges.setBodyType( "static" );
    %roomEdges.setImage( "GameAssets:backgroundedging" );
    %roomEdges.setSize( $roomWidth, $roomHeight );
    %roomEdges.setCollisionSuppress();
    %roomEdges.setAwake( false );
    %roomEdges.setActive( false );
    %roomEdges.setSceneLayer(2);
    arenaScene.add( %roomEdges ); 
    
    %this.addArenaBoundaries( $roomWidth, $roomHeight );
	
	%this.player = %this.spawnPlayer(-25, 0);
	
	//%enemyList.add(%this.spawnEnemyUnit(%scene, getRandom(-320,320), 200));
	%this.spawnEnemyUnit(getRandom(-$roomWidth/2, $roomWidth/2), $roomHeight/2);
	
	
	
	//for (%i = 0; %i < 2; %i++)
    //  RoomManager.spawnFishFood();
}


//-----------------------------------------------------------------------------

function Arena::addArenaBoundaries(%this, %width, %height)
{
    // add boundaries on all sides of the Arena a bit outside of the border of the screen.
    // The triggers allow for onCollision to be sent to any fish or other object that touches the edges.
    // The triggers are far enough outside the screeen so that objects will most likely be just out of view
    // before they are sent the onCollision callback.  This way will they can adjust "off stage".

    // Calculate a width and height to use for the bounds.
    // They should be bigger than the Arena itself.
    %wrapWidth = %width * 1.0;
    %wrapHeight = %height * 1.0;	//1.1

    arenaScene.add( %this.createOneArenaBoundary( "left",   -%wrapWidth/2 SPC 0,  5 SPC %wrapHeight) );
    arenaScene.add( %this.createOneArenaBoundary( "right",  %wrapWidth/2 SPC 0,   5 SPC %wrapHeight) );
    arenaScene.add( %this.createOneArenaBoundary( "top",    0 SPC -%wrapHeight/2, %wrapWidth SPC 5 ) );
    arenaScene.add( %this.createOneArenaBoundary( "bottom", 0 SPC %wrapHeight/2,  %wrapWidth SPC 5 ) );
}

//-----------------------------------------------------------------------------

function Arena::createOneArenaBoundary(%this, %side, %position, %size)
{
    %boundary = new SceneObject() { class = "ArenaBoundary"; };
    
    %boundary.setSize( %size );
    %boundary.side = %side;
    %boundary.setPosition( %position );
    %boundary.setSceneLayer( 1 );
    %boundary.createPolygonBoxCollisionShape( %size );
    // the objects that collide with us should handle any callbacks.
    // remember to set those scene objects to collide with scene group 15 (which is our group)!
    %boundary.setSceneGroup( 15 );
    %boundary.setCollisionCallback(false);
    %boundary.setBodyType( "static" );
    %boundary.setCollisionShapeIsSensor(0, true);
    return %boundary;
}

//-----------------------------------------------------------------------------

function Arena::spawnPlayer(%this, %xPos, %yPos)
{
    // add a Player object to the Arena
	new CompositeSprite(mainPlayer)
	{
		class = "Player";
	};
	
    arenaScene.add( mainPlayer );
	
	mainPlayer.setPosition(%xPos, %yPos);

	return mainPlayer;
} 

//-----------------------------------------------------------------------------

function Arena::spawnEnemyUnit(%this, %xPos, %yPos)
{
    // add a Player object to the Arena
	%newEnemy = new CompositeSprite()
	{
		class = "EnemyUnit";
	};
	
    arenaScene.add( %newEnemy );
	
	%newEnemy.setPosition(%xPos, %yPos);

	return %newEnemy;
} 
