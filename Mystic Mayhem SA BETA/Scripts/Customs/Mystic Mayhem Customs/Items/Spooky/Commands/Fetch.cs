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
	public class Fetch
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Fetch", AccessLevel.GameMaster, new CommandEventHandler( Fetch_OnCommand ) );
		}

		private class FetchTarget : Target
		{
			public FetchTarget( Mobile m ) : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{

				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Corpse )
				{
					Corpse C = (Corpse)o;
					if ( C.Amount != 400 && C.Amount != 401 && C.Amount != 605 && C.Amount != 606 )
						return;
					Bag bag = new Bag();
					bag.Name = C.Owner.Name;
					bag.Hue = 1278;
					Container pack = from.Backpack;
					
					ArrayList stuff = new ArrayList( C.Items );

					foreach( Item item in stuff )
					{
						try{
						bag.DropItem( item );
						}
						catch{}

					}
					pack.DropItem(bag);


				}
				else
					from.SendMessage( "That is not a Corpse." );
			}
		}
		
		[Usage( "PL" )]
		[Description( "It Fetchs Gold from any corpse targeted." )]
		private static void Fetch_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new FetchTarget( e.Mobile );
		}
		private static void BeginFetch( Mobile from )
		{
			from.Target = new FetchTarget( from );
		}		
	}
}