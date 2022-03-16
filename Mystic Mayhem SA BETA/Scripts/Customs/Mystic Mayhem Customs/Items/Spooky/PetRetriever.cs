using System;
using System.Collections;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;


namespace Server.Gumps
{
	
	public class PetRetriever : Item
	{
		private int m_Price;
		private InternalItem m_Item;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Price
		{
			get{ return m_Price; }
			set{ m_Price = value; }
		}

		[Constructable]
		public PetRetriever() : this( 1250 )
		{
		}

		[Constructable]
		public PetRetriever( int price ) : base( 4983 )
		{
			Movable = false;
			Hue = 1266;
			Name = "Pet Retrieval 1250 GP";

			m_Price = price;
			m_Item = new InternalItem( this );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 4 ) ) 
                  	{	
				 from.SendGump( new PetRetrieverGump( m_Price ) );		
	
			}
			else
			{
			from.SendMessage( "You are not close enough to use this.");
			}
		}

		public PetRetriever( Serial serial ) : base( serial )
		{
		}
		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( m_Item != null )
				m_Item.Location = new Point3D( X - 1, Y , Z );
		}

		public override void OnMapChange()
		{
			if ( m_Item != null )
				m_Item.Map = Map;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_Item != null )
				m_Item.Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_Price );
			writer.Write( m_Item );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		
			m_Price = reader.ReadInt();
			m_Item = reader.ReadItem() as InternalItem;
		}
		private class InternalItem : Item
		{
			private PetRetriever m_Item;

			public InternalItem( PetRetriever item ) : base( 4982 )
			{
				Movable = false;
				Hue = 1266;
				Name = "Use The Front";

				m_Item = item;
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void OnLocationChange( Point3D oldLocation )
			{
				if ( m_Item != null )
					m_Item.Location = new Point3D( X + 1, Y , Z );
			}

			public override void OnMapChange()
			{
				if ( m_Item != null )
					m_Item.Map = Map;
			}

			public override void OnAfterDelete()
			{
				base.OnAfterDelete();

				if ( m_Item != null )
					m_Item.Delete();
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version

				writer.Write( m_Item );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				m_Item = reader.ReadItem() as PetRetriever;
			}
		}
	}
}

namespace Server.Gumps
{	
	public class PetRetrieverGump : Gump
	{		   				
		private int m_Price;
						
		public PetRetrieverGump( int price ) : base( 150, 50 )
		{
			m_Price = price;
			this.Closable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(7, 5, 380, 182, 2620);
			this.AddAlphaRegion(19, 19, 355, 153);
			this.AddItem(310, 80, 8484);

			this.AddLabel(30, 30, 1152, @"You can bring your unshrunk pets here for");
			this.AddLabel(30, 52, 1152, @"a Fee of 1250 Gold taken from your bank");
			this.AddLabel(80, 97, 1152, @"Yes,  Bring my pets here");
			this.AddButton(32, 91, 10830, 10830, 1, GumpButtonType.Reply, 0);

			this.AddLabel(80, 137, 1152, @"No.  Cancel this request.");
			this.AddButton(32, 131, 10850, 10850, 2, GumpButtonType.Reply, 0);

		}

	
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
							
			from.CloseGump( typeof( PetRetrieverGump ) );

			if ( info.ButtonID == 1 || info.ButtonID == 2 )
			{
				if ( m_Price > 0 )
				{
					if ( info.ButtonID == 1 )
					{
						Mobile master = (Mobile)from;
						ArrayList pets = new ArrayList();

						foreach ( Mobile m in World.Mobiles.Values )
						{
							if ( m is BaseCreature )
							{
								BaseCreature bc = (BaseCreature)m;

								if ( (bc.Controlled && bc.ControlMaster == master) || (bc.Summoned && bc.SummonMaster == master) )
									pets.Add( bc );
							}
						}

						if ( pets.Count > 0 )
						{

							if ( Banker.Withdraw( from, m_Price ) )
							{
								from.SendLocalizedMessage( 1060398, m_Price.ToString() ); // Amount charged

							}
							else
							{
								from.SendMessage( "You do not have enough gold in your bank for pet retrieval" );  
								return;
							}

							for ( int i = 0; i < pets.Count; ++i )
							{
								Mobile pet = (Mobile)pets[i];

								if ( pet is IMount )
									((IMount)pet).Rider = null; // make sure it's dismounted

								pet.MoveToWorld( from.Location, from.Map );
							}
						}
						else
						{
							from.SendMessage( "There were no pets found for you." );
							return;
						}

					}
					else
					{
						from.SendMessage( "You have elected to abandon your pets." ); 
						return;
					}
				}
			}
		}
	}
}

 
 