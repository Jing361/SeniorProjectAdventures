
function Player:: onAdd(%this){

    %player = new Sprite()
    {
        Animation = "GameAssets:playerbaseAnim";
       //class = "playerClass";
        position = "0 0";
        size = "74 78";
        SceneLayer = "15";
        SceneGroup = "14";
        CollisionCallback = true;
    };

    %this.createPolygonBoxCollisionShape("74 78");
    %this.setCollisionShapeIsSensor(0, true);
    %this.setCollisionGroups( "10 15" );
	
	
	%playercontrols = ShooterControlsBehavior.createInstance();
	%playercontrols.upKey = "keyboard W";
	%playercontrols.downKey = "keyboard S";
	%playercontrols.leftKey = "keyboard A";
	%playercontrols.rightKey = "keyboard D";
	
	%this.addBehavior(%playercontrols);
}