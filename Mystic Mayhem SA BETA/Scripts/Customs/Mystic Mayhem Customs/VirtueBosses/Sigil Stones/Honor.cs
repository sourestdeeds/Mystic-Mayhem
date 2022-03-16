using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HonorStone : Item
	{
		[Constructable]
		public HonorStone( ) : base( 0x186D )
		{
			Name = "Sigil Stone Of Honor";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public HonorStone( Serial serial ) : base( serial )
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
