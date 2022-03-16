using System;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	
	public class CorpseRetriever : Item
	{
		private int m_Price;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Price
		{
			get{ return m_Price; }
			set{ m_Price = value; }
		}

		[Constructable]
		public CorpseRetriever() : this( 500 )
		{
		}

		[Constructable]
		public CorpseRetriever( int price ) : base( 4685 )
		{
			Movable = false;
			Hue = 1266;
			Name = "Corpse Retrieval";
			//ItemID = 4685;
			m_Price = price;
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				Ankhs.Resurrect( m, this );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 2 ) ) 
                  	{	
				 from.SendGump( new CorpseRetrieverGump( m_Price ) );		
		        	 from.CantWalk = true;	
			}
			else
			{
			from.SendMessage( "You are not close enough to use this.");
			}
		}


		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
		}

		public CorpseRetriever( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_Price );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		
			m_Price = reader.ReadInt();
		}
	}
}

namespace Server.Gumps
{	
	public class CorpseRetrieverGump : Gump
	{		   				
		private int m_Price;
						
		public CorpseRetrieverGump( int price ) : base( 150, 50 )
		{
			m_Price = price;
			this.Closable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(7, 5, 380, 182, 2620);
			this.AddAlphaRegion(19, 19, 355, 153);
			this.AddItem(310, 80, 7036);

			this.AddLabel(30, 30, 1152, @"You can bring your most recient corpse here for");
			this.AddLabel(30, 52, 1152, @"a Fee of 500 Gold taken from your bank");
			this.AddLabel(80, 97, 1152, @"Yes,  Bring corpse here");
			this.AddButton(32, 91, 10830, 10830, 1, GumpButtonType.Reply, 0);

			this.AddLabel(80, 137, 1152, @"No.  Cancel this request.");
			this.AddButton(32, 131, 10850, 10850, 2, GumpButtonType.Reply, 0);

		}

	
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
							
			from.CloseGump( typeof( CorpseRetrieverGump ) );

			if ( info.ButtonID == 1 || info.ButtonID == 2 )
			{
				
				
			Item corpse=from.Corpse;
                      	  if ( corpse!=null )
			{
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
							from.SendMessage( "You do not have enough gold in your bank for corpse retrieval" );  
							from.CantWalk = false;
							return;
						}
					}
					else
					{
						from.SendMessage( "You have elected to abandon your corpse." ); 
						from.CantWalk = false;
						return;
					}
						
				}
			 	from.Corpse.Location = new Point3D( from.Location );	
				       
				Effects.SendLocationParticles( EffectItem.Create( from.Location, from.Map, EffectItem.DefaultDuration ), 0x3728, 10, 30, 5052 );
					
				Effects.PlaySound( from.Location, from.Map, 0x201 );				
					
				from.Corpse.Map = from.Map;

				from.CantWalk = false;
			}
			else
			{
			from.SendMessage( "You have no corpse to claim." ); 
			from.CantWalk = false;
			}
		}
	}
}
}
 
 