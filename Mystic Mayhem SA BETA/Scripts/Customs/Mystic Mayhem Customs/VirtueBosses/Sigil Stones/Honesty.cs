using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HonestyStone : Item
	{
		[Constructable]
		public HonestyStone( ) : base( 0x186a )
		{
			Name = "Sigil Stone Of Honesty";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public HonestyStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
