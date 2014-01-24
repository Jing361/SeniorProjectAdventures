
function openTitleScreen(%scene){
	// Background
    %background = new Sprite();
    %background.setBodyType( "static" );
    %background.setImage( "GameAssets:menu_titleScreenBkgrd" );
    %background.setSize( 640, 480 );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(30);
    %scene.add( %background );
	
	exec("./scripts/behaviors/menus/menuControls.cs");

	%menuController = new Sprite();
	
	%controls = MenuControl.createInstance();
	%controls.enterKey = "keyboard enter";
	%menuController.addBehavior(%controls);
	
    mainScene.add( %menuController ); 
}