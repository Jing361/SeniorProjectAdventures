//-----------------------------------------------------------------------------
// Room setup and wall collisions
//-----------------------------------------------------------------------------

function Arena::buildArena(%this)
{
    // A pre-built Arena of size 100x75, with background.
    // Triggers will be provide around the edges to let the developer know when objects in the
    // arena have reached the edges.

    // Background
    %background = new Sprite() {class="backgroundObj";};
    %background.setBodyType( "static" );
    %background.setImage( "GameAssets:background" );
    %background.setSize( $roomWidth, $roomHeight );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(30);
    %background.setSceneGroup( Utility.getCollisionGroup("") );
    %this.getScene().add( %background );
    
    // Arena Edges
    %roomEdges = new Sprite() {class="backgroundObj";};
    %roomEdges.setBodyType( "static" );
    %roomEdges.setImage( "GameAssets:backgroundedging" );
    %roomEdges.setSize( $roomWidth, $roomHeight );
    %roomEdges.setCollisionSuppress();
    %roomEdges.setAwake( false );
    %roomEdges.setActive( false );
    %roomEdges.setSceneLayer(2);
    %roomEdges.setSceneGroup( Utility.getCollisionGroup("") );
    %this.getScene().add( %roomEdges ); 
    
    %this.addArenaBoundaries( $roomWidth, $roomHeight );
	
    //%this.setUpdateCallback(true);
	
	//Populate room
	%this.player = %this.spawnPlayer(-25, 0);		//add player before Enemies!
	
	%this.roomChromosomes = "";
	
	// Enemy Info
	%this.EnemyCount = 0;
	
	%this.processRoomChromosomes();
	
	
	//-RoomDamageTracking---
	%this.roomShooterDamage = 0;
	%this.roomShooterShotsFired = 0;
	%this.roomBladeDamage = 0;
	%this.roomBladeAttackNums = 0;

	//font 4 times for pseudo boldness
	%this.addRoomFont(-$roomWidth/2 + 1, $roomHeight/2 - 0.2);
}

//-----------------------------------------------------------------------------

function Arena::addRoomFont(%this, %x, %y)
{
	%this.addRoomNumFont(%x, %y);
	%this.addRoomNumFont(%x + 0.1, %y);
	%this.addRoomNumFont(%x + 0.1, %y + 0.1);
	%this.addRoomNumFont(%x, %y + 0.1);
}

//-----------------------------------------------------------------------------

function Arena::addRoomNumFont(%this, %x, %y)
{
	%font = new ImageFont();
	%font.Image = "GameAssets:font";
	%font.Text = "Room:" @ %this.currLevel;
	%font.FontSize = "3 3";
	%font.setBlendColor(1, 1, 0);
	%font.setPosition(%x, %y);
	%font.TextAlignment = "Left";
	%this.getScene().add( %font ); 
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

    %this.getScene().add( %this.createOneArenaBoundary( "left",   -%wrapWidth/2 SPC 0,  5 SPC %wrapHeight) );
    %this.getScene().add( %this.createOneArenaBoundary( "right",  %wrapWidth/2 SPC 0,   5 SPC %wrapHeight) );
    %this.getScene().add( %this.createOneArenaBoundary( "top",    0 SPC -%wrapHeight/2, %wrapWidth SPC 5 ) );
    %this.getScene().add( %this.createOneArenaBoundary( "bottom", 0 SPC %wrapHeight/2,  %wrapWidth SPC 5 ) );
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
    %boundary.setSceneGroup( Utility.getCollisionGroup("Wall") );
    %boundary.setCollisionCallback(false);
    %boundary.setBodyType( "static" );
    return %boundary;
}

//-----------------------------------------------------------------------------

function Arena::spawnPlayer(%this, %xPos, %yPos)
{
    // add a Player object to the Arena
	%mainPlayer = new CompositeSprite()
	{
		class = "Player";
		myArena = %this;
	};
	
    %this.getScene().add( %mainPlayer );
	
	%mainPlayer.initialize();
	
	%mainPlayer.setPosition(%xPos, %yPos);

	return %mainPlayer;
} 

//-----------------------------------------------------------------------------
///ordering: armor/parry/acid/tar/blade/shooter/blob

function Arena::processRoomChromosomes(%this)
{
	%toolVarietyCount = 7;		//number of different tools available, length of local chromosomes
	
	if(%this.currLevel > 1)
	{
		echo("Arena.Arena: Creating GeneticAlgorithm instance");
		$genAlg = new GeneticAlgorithm();
		
		echo("Arena.Arena: GeneticAlgorithm.run()");
		%chromosome = $genAlg.run();
		
		echo("Arena.Arena: GeneticAlgorithm. run successful!");
	}
	else
	{
		%chromosome = "0 0 0 0 1 0 1" SPC "0 0 0 0 0 1 1" SPC "0 0 0 0 1 0 1";
	}
	
	echo("Chromosome:" SPC %chromosome);
	
	for(%i = 0; %i < mFloor(getWordCount(%chromosome)/%toolVarietyCount); %i++)
	{
	
		%subChromosome = getWords(%chromosome, %i*%toolVarietyCount, (%toolVarietyCount - 1) + %i*%toolVarietyCount);
		
		echo("  sub" SPC %subChromosome);
		
		echo("Arena.Arena: spawn enemy unit" SPC %i);

		%this.spawnEnemyUnit(%subChromosome, getRandom(-$roomWidth/3, $roomWidth/3), getRandom(-$roomHeight/3, $roomHeight/3));

		echo("Arena.Arena: spawned enemy unit successfuly" SPC %i);
		
		if(%i >= (getWordCount(%chromosome)/%toolVarietyCount) - 1)
		{
			%this.roomChromosomes = %this.roomChromosomes @ %subChromosome;
		}
		else
		{
			%this.roomChromosomes = %this.roomChromosomes @ %subChromosome NL "";
		}
	}
	
	echo("Chromosome done processing!");
	
	//add health pickup------------------------
	%healthPickup = new CompositeSprite()
	{
		class = "Pickup";
	};
		
	%this.getScene().add( %healthPickup );
}

//-----------------------------------------------------------------------------

function Arena::spawnEnemyUnit(%this, %localChromosome, %xPos, %yPos)
{
    // add a Player object to the Arena
	%newEnemy = new CompositeSprite()
	{
		class = "EnemyUnit";
		myChromosome = %localChromosome;
		myArena = %this;
		mainTarget = %this.player;
	};
	
    %this.getScene().add( %newEnemy );
	
	echo("Arena.Arena: initializing enemy:");
	%newEnemy.initialize();
	
	%newEnemy.setPosition(%xPos, %yPos);

	return %newEnemy;
} 

//-----------------------------------------------------------------------------

function Arena::finishRoom(%this)
{
	%totalButtonCount = %this.player.rangedCount +
		%this.player.meleeCount +
		%this.player.blockCount + 
		%this.player.dashCount;
	
	//sum up everything
	//write file...
	
	
} 
