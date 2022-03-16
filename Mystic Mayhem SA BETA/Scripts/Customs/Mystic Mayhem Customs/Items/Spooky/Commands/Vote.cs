using System;
using Server.Commands;
using System.Reflection;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class Vote
	{
		public static void Initialize()
		{
			CommandSystem.Register( "VOTE", AccessLevel.Player, new CommandEventHandler( VT_OnCommand ) );
		}   
     
		[Usage( "Vote" )]
		[Description( "Cast Vote for PF" )]

		public static void VT_OnCommand( CommandEventArgs e )
		{
			e.Mobile.LaunchBrowser( "http://mysticmayhem.paradisefounduo.com/" );
		}	
	}
}