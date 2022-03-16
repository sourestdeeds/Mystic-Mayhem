using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{

	public class EventOrb : Item
	{
		private string m_type;
		private string m_owner;

		private int m_killed;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string Monster
		{
			get{ return m_type; }
			set{ m_type = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Owner
		{
			get{ return m_owner; }
			set{ m_owner = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountKilled
		{
			get{ return m_killed; }
			set{ m_killed = value; InvalidateProperties(); }
		}
		
		[Constructable]
		public EventOrb( ) : base( 0x186D )
		{

			Monster = "";
			Owner = "";
			//Hue = 1072;

			Name = "Event Orb";
			AmountKilled = 0;
		}

		public void CallGump( Mobile from )
		{
			from.CloseGump( typeof( EventOrbGump ) );
			from.SendGump( new EventOrbGump( from, this ) );
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( Owner == "" )
			{
				Owner = from.Name;
				Movable = false;
				Name = Owner + "s" + " " + Monster + "s";
			}


			if( IsChildOf( from.Backpack ) )
			{
				if( from.Name == Owner)
				{
					CallGump( from );
				}
				else
				{
					from.SendMessage( "This does not belong to you!");
				}
			} 
			else 
			{
		    	from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
			}
		}

		public override void GetProperties(ObjectPropertyList list)
		{				base.GetProperties (list);
			list.Add( 1060662,"{0}\t{1}" ,"Count", AmountKilled );
		}
		
		public EventOrb( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		
			writer.Write( m_type );
			writer.Write( m_owner );

			writer.Write( m_killed );
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
			
			m_type = reader.ReadString();
			m_owner = reader.ReadString();

			m_killed = reader.ReadInt();
			LootType = LootType.Blessed;
		}
	}
}
