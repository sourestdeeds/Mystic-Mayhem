using System;
using System.Reflection;
using Server.Commands;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class SelfRes
	{
		private int m_Price;

		public static void Initialize()
		{
			CommandSystem.Register( "SelfRes", AccessLevel.Player, new CommandEventHandler( Unbless_OnCommand ) );
		}   
     
		[Usage( "SelfRes" )]
		[Description( "Res Self for a fee" )]

		public static void Unbless_OnCommand( CommandEventArgs args )
		{
			Mobile m = args.Mobile;
			PlayerMobile from = m as PlayerMobile;
			if( from != null && !from.Alive)
			{
				from.CloseGump( typeof( SelfResGump ) );
				from.SendGump ( new SelfResGump( from ) );
			}
			else
			{
				from.SendMessage("Hey Bugwit, you are still alive!");
			}
		}
	}
}

namespace Server.Gumps
{	
	public class SelfResGump : Gump
	{		   				
		private int m_Price;
						
		public SelfResGump( PlayerMobile from ) : base( 150, 50 )
		{
			m_Price = (int)(from.SkillsTotal / 5);
			this.Closable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(7, 5, 380, 182, 2620);
			this.AddAlphaRegion(19, 19, 355, 153);
			this.AddItem(310, 80, 7036);

			this.AddLabel(30, 30, 1152, "You can ressurect yourself here for");
			this.AddLabel(30, 52, 1152, "a Fee of ");
			this.AddLabel(100, 52, 1152, m_Price.ToString() );
			this.AddLabel(150, 52, 1152, " Gold taken from your bank");
			this.AddLabel(80, 97, 1152, "Yes,  Ressurect me");
			this.AddButton(32, 91, 10830, 10830, 1, GumpButtonType.Reply, 0);

			this.AddLabel(80, 137, 1152, "No.  Cancel this request.");
			this.AddButton(32, 131, 10850, 10850, 2, GumpButtonType.Reply, 0);

		}

	
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
							
			from.CloseGump( typeof( SelfResGump ) );

			if ( info.ButtonID == 1 || info.ButtonID == 2 )
			{
				if ( from.Map == null ) //|| !from.Map.CanFit( from.Location, 16, false, false ) )
				{
					from.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					return;
				}
				if ( m_Price > 0 )
				{
					if ( info.ButtonID == 1 )
					{
						if ( Banker.Withdraw( from, m_Price ) )
						{
							from.SendLocalizedMessage( 1060398, m_Price.ToString() ); // Amount charged
								//from.SendLocalizedMessage( 1060022, Banker.GetBalance( from ).ToString() ); // Amount left, from bank
						}
						else
						{
							from.SendLocalizedMessage( 1060020 ); // Unfortunately, you do not have enough cash in your bank to cover the cost of the healing.
							return;
						}
					}
					else
					{
						from.SendMessage( "You decide against paying the resurrection fee, and thus remain dead." ); 
						return;
					}
						
				}
				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				from.Resurrect();

				Item corpse=from.Corpse;
				if ( corpse != null )
				{
			 		from.Corpse.Location = new Point3D( from.Location );
					from.Corpse.Map = from.Map;
				}
			}
		}
	}
}

 
 