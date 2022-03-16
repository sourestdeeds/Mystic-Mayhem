using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Mobiles; 
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Commands
{
	public class Grab
	{
		ArrayList toDelete = new ArrayList();
			private static Queue m_ToDelete = new Queue();

		public static void Initialize()
		{
			CommandSystem.Register( "Grab", AccessLevel.Player, new CommandEventHandler( Grab_OnCommand ) );
		}

		private class GrabTarget : Target
		{
			public GrabTarget( Mobile m ) : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Corpse )
				{
					Corpse C = (Corpse)o;

					if ( ! from.InRange( C.GetWorldLocation(), 2 ) )
					{
						from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that.
						return;
					}
					if ( !C.CanLoot(from, null) || C.IsCriminalAction(from) )
					{
						from.SendMessage( "You can not Grab Loot from this corpse." );
						return;
					}

					ArrayList toDelete = new ArrayList();
					foreach( Item item in C.Items )
					{
						if( item is Gold )
						{
							int amt = (int)(item.Amount * .9);
							from.AddToBackpack ( new Gold(amt) );
							toDelete.Add( item );
						}
		
						else if ( item is StarSapphire )
						{
							from.AddToBackpack ( new StarSapphire(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Emerald )
						{
							from.AddToBackpack ( new Emerald(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Sapphire )						{
							from.AddToBackpack ( new Sapphire(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Ruby )
						{
							from.AddToBackpack ( new Ruby(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Citrine )
						{
							from.AddToBackpack ( new Citrine(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Amethyst )
						{
							from.AddToBackpack ( new Amethyst(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Tourmaline )
						{
							from.AddToBackpack ( new Tourmaline(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Amber )
						{
							from.AddToBackpack ( new Amber(item.Amount) );
							toDelete.Add( item );
						}
						else if ( item is Diamond )
						{
							from.AddToBackpack ( new Diamond(item.Amount) );
							toDelete.Add( item );
						}
		
					}
					foreach( Item item in toDelete )
						item.Delete();
					from.SendMessage( "You grab the loot!" );
				}
				else
					from.SendMessage( "That is not a Corpse." );

			}
		}
		
		[Usage( "Grab" )]
		[Description( "It Grabs Gold and Gems from any corpse targeted." )]
		private static void Grab_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new GrabTarget( e.Mobile );
		}
		private static void BeginGrab( Mobile from )
		{
			from.Target = new GrabTarget( from );
		}		
	}
}