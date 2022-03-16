using System;
using Server.Commands;
using System.Reflection;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class ChampTime
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CRT", AccessLevel.Player, new CommandEventHandler( CRT_OnCommand ) );
		}   
     
		[Usage( "ChampTime" )]
		[Description( "Displays Champ Restriction Time Remaining" )]

		public static void CRT_OnCommand( CommandEventArgs e )
		{
			try{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				Item p = pm.Backpack.FindItemByType(typeof(Decay1day));
				if (p == null)
				{
					pm.SendMessage("You have no champ restriction.");
					return;
				}
				else if ( p.Name != "ChampDelay" )
				{
					pm.SendMessage("You have no champ restriction.");
					return;
				}
				Decay1day dc = p as Decay1day;

				TimeSpan ts = dc.TimeEnd - DateTime.Now;
				string gt = String.Format( "{0:D2}:{1:D2}", ts.Hours % 24, ts.Minutes % 60 );
				pm.SendMessage ( "Champ restriction time remaining {0} ", gt );
				}
			catch{}

		}	
	}
}