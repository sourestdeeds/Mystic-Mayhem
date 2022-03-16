using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HumilityStone : Item
	{
		[Constructable]
		public HumilityStone( ) : base( 0x1869 )
		{
			Name = "Sigil Stone Of Humility";
			Stackable = false;
			LootType = LootType.Cursed;
			Weight = 1.0;
		}

		public HumilityStone( Serial serial ) : base( serial )
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
