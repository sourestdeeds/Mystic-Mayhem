using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
    public class ConjurersTrinket : BaseTalisman
	{
		[Constructable]
        public ConjurersTrinket() : base(0x2F58)
		{
            Name = "Conjurer's Trinket";
            Hue = 1141; //  0x1B5;

            Slayer = TalismanSlayerName.Undead;

			Attributes.BonusStr = 1;
            Attributes.RegenHits = 2;
            Attributes.AttackChance = 10;
            Attributes.WeaponDamage = 20;
		}

		public ConjurersTrinket( Serial serial ) : base( serial )
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
