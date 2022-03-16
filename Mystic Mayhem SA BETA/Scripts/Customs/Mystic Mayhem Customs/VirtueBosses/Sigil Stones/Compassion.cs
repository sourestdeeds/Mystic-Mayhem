using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class CompassionStone : Item
	{
		[Constructable]
		public CompassionStone( ) : base( 0x1870 )
		{
			Name = "Sigil Stone Of Compassion";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public CompassionStone( Serial serial ) : base( serial )
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
