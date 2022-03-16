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
	public class ClaimMulti
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CLM", AccessLevel.Player, new CommandEventHandler( ClaimM_OnCommand ) );
		}

		private class ClaimMTarget : Target
		{
			public ClaimMTarget( Mobile m ) : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				PlayerMobile pm = from as PlayerMobile;

				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Corpse )
				{
					Corpse C = (Corpse)o;

					if ( ! from.InRange( C.GetWorldLocation(), 3 ) )
					{
						from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that.
						return;
					}

					if ( C.Owner is PlayerMobile )
					{
						pm.SendMessage ("Is Player");
						return;
					}
				//	if ( C.Amount == 400 || C.Amount == 401 || C.Amount == 605 || C.Amount == 606 )
				//		return;
					if ( !C.CanLoot(from, null) || C.IsCriminalAction(from) )
					{
						from.SendMessage( "You did not kill this critter!" );
						return;
					}
					else if ( C.Owner is BaseCreature )
					{
						if (!((BaseCreature)C.Owner).NoClaim)
						{
							pm.AddToBackpack ( new Platinum(1) );
							((Corpse)C).Delete();
						}
						else
						{
							((Corpse)C).Delete();
						//	pm.SendMessage("That is not a claimable corpse");
						}
					}
					else
						pm.SendMessage("That is not a claimable corpse");


					BeginClaim( from );
				}
				else
					from.SendMessage( "That is not a Corpse." );
			}
		}
		
		[Usage( "CLM" )]
		[Description( "It Deletes corpse targeted." )]
		private static void ClaimM_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new ClaimMTarget( e.Mobile );
		}
		private static void BeginClaim( Mobile from )
		{
			from.Target = new ClaimMTarget( from );
		}		
	}

	public class Claim
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CL", AccessLevel.Player, new CommandEventHandler( Claim_OnCommand ) );
		}

		private class ClaimTarget : Target
		{
			public ClaimTarget( Mobile m ) : base( 15, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
				PlayerMobile pm = from as PlayerMobile;

				if ( !BaseCommand.IsAccessible( from, o ) )
					from.SendMessage( "That is not accessible." );
				else if ( o is Corpse )
				{
					Corpse C = (Corpse)o;

					if ( ! from.InRange( C.GetWorldLocation(), 3 ) )
					{
						from.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that.
						return;
					}

					if ( C.Owner is PlayerMobile )
					{
						pm.SendMessage ("Is Player");
						return;
					}
				//	if ( C.Amount == 400 || C.Amount == 401 || C.Amount == 605 || C.Amount == 606 )
				//		return;
					if ( !C.CanLoot(from, null) || C.IsCriminalAction(from) )
					{
						from.SendMessage( "You did not kill this critter!" );
						return;
					}
					else if ( C.Owner is BaseCreature )
					{
						if (!((BaseCreature)C.Owner).NoClaim)
						{
							pm.AddToBackpack ( new Platinum(1) );
							((Corpse)C).Delete();
						}
						else
						{
							((Corpse)C).Delete();
						//	pm.SendMessage("That is not a claimable corpse");
						}
					}
					else
						pm.SendMessage("That is not a claimable corpse");


				//	BeginClaim( from );
				}
				else
					from.SendMessage( "That is not a Corpse." );
			}
		}
		
		[Usage( "CL" )]
		[Description( "It Delete corpse targeted." )]
		private static void Claim_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new ClaimTarget( e.Mobile );
		}
	//	private static void BeginClaim( Mobile from )
	//	{
	//		from.Target = new ClaimTarget( from );
	//	}		
	}
}