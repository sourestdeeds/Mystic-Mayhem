using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Items;
using Server.Mobiles; 
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Commands
{
	public class Plunder
	{
		public static void Initialize()
		{
			CommandSystem.Register( "PL", AccessLevel.Counselor, new CommandEventHandler( Plunder_OnCommand ) );
		}

		private class PlunderTarget : Target
		{
			public PlunderTarget( Mobile m ) : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{

				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Corpse )
				{
					Corpse C = (Corpse)o;
					if ( C.Amount == 400 || C.Amount == 401 || C.Amount == 605 || C.Amount == 606 )
						return;
					foreach( Item item in C.Items )
					{
						if( item is Gold )
						{
						//from.SendMessage( "got Gold" );
						from.AddToBackpack ( new Gold(item.Amount) );

						}
					}

					((Corpse)C).Delete();
					BeginPlunder( from );
				}
				else
					from.SendMessage( "That is not a Corpse." );
			}
		}
		
		[Usage( "PL" )]
		[Description( "It Plunders Gold from any corpse targeted." )]
		private static void Plunder_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new PlunderTarget( e.Mobile );
		}
		private static void BeginPlunder( Mobile from )
		{
			from.Target = new PlunderTarget( from );
		}		
	}
}