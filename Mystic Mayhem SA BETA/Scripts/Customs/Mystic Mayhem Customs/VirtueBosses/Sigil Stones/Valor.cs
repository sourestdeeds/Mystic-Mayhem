using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class ValorStone : Item
	{
		[Constructable]
		public ValorStone( ) : base( 0x186E )
		{
			Name = "Sigil Stone Of Valor";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public ValorStone( Serial serial ) : base( serial )
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
