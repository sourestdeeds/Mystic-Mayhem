using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SacrificeStone : Item
	{
		[Constructable]
		public SacrificeStone( ) : base( 0x186C )
		{
			Name = "Sigil Stone Of Sacrifice";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public SacrificeStone( Serial serial ) : base( serial )
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
