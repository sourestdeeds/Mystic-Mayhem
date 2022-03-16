using System;
using Server.Commands;
using System.Reflection;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class ServerTime
	{
		public static void Initialize()
		{
			CommandSystem.Register( "ST", AccessLevel.Player, new CommandEventHandler( ST_OnCommand ) );
		}   
     
		[Usage( "ServerTime" )]
		[Description( "Displays Server Time" )]

		public static void ST_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage ( "Mystic Time {0} ", DateTime.Now );
		}	
	}
}