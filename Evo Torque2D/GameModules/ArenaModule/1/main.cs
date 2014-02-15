//-----------------------------------------------------------------------------
// Initial setup
//-----------------------------------------------------------------------------

//globals
$roomWidth = 1280;
$roomHeight = 960;


function ArenaModule::create( %this )
{
    exec("./scripts/Arena.cs");
}

//-----------------------------------------------------------------------------

function ArenaModule::destroy( %this )
{
    ArenaModule::cancelPendingEvents();
}
