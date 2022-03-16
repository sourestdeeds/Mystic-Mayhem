using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SpiritualityStone : Item
	{
		[Constructable]
		public SpiritualityStone( ) : base( 0x186F )
		{
			Name = "Sigil Stone Of Spirituality";
			Hue = 1150;
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public SpiritualityStone( Serial serial ) : base( serial )
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
