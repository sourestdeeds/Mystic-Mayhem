using System;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Commands
{
	public class Recover
	{
		public static void Initialize()
		{
			Register();
		}

		public static void Register()
		{
			CommandSystem.Register( "R", AccessLevel.Counselor, new CommandEventHandler( Recover_OnCommand ) );
		}

		private class RecoverTarget : Target
		{
			public RecoverTarget( Mobile m ) : base( -1, true, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				Mobile m;
				
				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Mobile )
				{
					m = (Mobile)o;
					m.Hits = m.HitsMax;
					m.Stam = m.StamMax;
					m.Mana = m.ManaMax;
				}
				else
					from.SendMessage( "That is not a mobile." );
			}
		}
		
		[Usage( "Recover" )]
		[Description( "It recovers Hits, Stam and Mana of the targeted at the maximum level." )]
		private static void Recover_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new RecoverTarget( e.Mobile );
		}		
	}
}