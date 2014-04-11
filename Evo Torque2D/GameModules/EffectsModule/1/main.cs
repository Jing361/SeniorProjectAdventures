//-----------------------------------------------------------------------------
// EffectsModule: Effects class and functions
//-----------------------------------------------------------------------------

function EffectsModule::create( %this )
{
    exec("./scripts/Effects.cs");
    exec("./scripts/damageSplashScreen.cs");
	
}

//-----------------------------------------------------------------------------

function EffectsModule::destroy( %this )
{
}
