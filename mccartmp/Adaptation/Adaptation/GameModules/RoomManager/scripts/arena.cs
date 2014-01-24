//-----------------------------------------------------------------------------
// Room setup and wall collisions
//-----------------------------------------------------------------------------

function getAnimationList()
{
   %list = "GameAssets:playerbaseAnim" @ "," @ "GameAssets:seahorseAnim" ;
}

//-----------------------------------------------------------------------------

function getAnimationSize(%anim)
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

function buildArena(%scene)
{
    // A pre-built Arena of size 100x75, with background.
    // Triggers will be provide around the edges to let the developer know when objects in the
    // arena have reached the edges.

    // Background
    %background = new Sprite();
    %background.setBodyType( "static" );
    %background.setImage( "GameAssets:background" );
    %background.setSize( 640, 480 );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(30);
    %scene.add( %background );
    
    // Arena Edges
    %roomEdges = new Sprite();
    %roomEdges.setBodyType( "static" );
    %roomEdges.setImage( "GameAssets:backgroundedging" );
    %roomEdges.setSize( 640, 480 );
    %roomEdges.setCollisionSuppress();
    %roomEdges.setAwake( false );
    %roomEdges.setActive( false );
    %roomEdges.setSceneLayer(10);
    %scene.add( %roomEdges ); 
    
    addArenaBoundaries( %scene, 640, 480 );
	
	RoomManager.spawnPlayer();
	
	for (%i = 0; %i < 2; %i++)
      RoomManager.spawnFishFood();
}

//-----------------------------------------------------------------------------

function addArenaBoundaries(%scene, %width, %height)
{
    // add boundaries on all sides of the Arena a bit outside of the border of the screen.
    // The triggers allow for onCollision to be sent to any fish or other object that touches the edges.
    // The triggers are far enough outside the screeen so that objects will most likely be just out of view
    // before they are sent the onCollision callback.  This way will they can adjust "off stage".

    // Calculate a width and height to use for the bounds.
    // They should be bigger than the Arena itself.
    %wrapWidth = %width * 1.1;
    %wrapHeight = %height * 1.1;

    %scene.add( createOneArenaBoundary( "left",   -%wrapWidth/2 SPC 0,  5 SPC %wrapHeight) );
    %scene.add( createOneArenaBoundary( "right",  %wrapWidth/2 SPC 0,   5 SPC %wrapHeight) );
    %scene.add( createOneArenaBoundary( "top",    0 SPC -%wrapHeight/2, %wrapWidth SPC 5 ) );
    %scene.add( createOneArenaBoundary( "bottom", 0 SPC %wrapHeight/2,  %wrapWidth SPC 5 ) );
}

//-----------------------------------------------------------------------------

function createOneArenaBoundary(%side, %position, %size)
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
