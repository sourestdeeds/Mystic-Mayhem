using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Menus; 
using Server.Menus.Questions; 

namespace Server.Items 
{ 
	public class TravelWand : Item
	{ 
		[Constructable] 
		public TravelWand() : base( 0xDF2 ) 
		{ 
			Hue = 1161; 
			Movable = true; 
			Name = "a Wand of Traveling"; 
			LootType = LootType.Blessed;
		} 

		public TravelWand( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( from.AccessLevel < AccessLevel.GameMaster && from.Region.IsPartOf( typeof( Regions.Jail ) ) )
			{
				from.SendMessage("Ha! Ha! Ha! You are a looser. That does not work here.");
				return;
			}
			from.SendGump( new TravelWandGump( from ) ); 
		} 
		public override void OnDoubleClickDead( Mobile from )
		{ 
			if ( from.AccessLevel < AccessLevel.GameMaster && from.Region.IsPartOf( typeof( Regions.Jail ) ) )
			{
				from.SendMessage("Ha! Ha! Ha! You are a looser. That does not work here.");
				return;
			}
			from.SendGump( new TravelWandGump( from ) ); 
		}

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
}
