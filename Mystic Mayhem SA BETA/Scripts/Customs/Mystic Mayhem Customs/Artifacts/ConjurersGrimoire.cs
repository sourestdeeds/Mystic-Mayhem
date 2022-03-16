using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Commands;
//using Server.Engines.Craft;
using Server.Network;
using Server.Spells;
using Server.Targeting;

namespace Server.Items
{
	public class ConjurersGrimoire : Spellbook  // Item
	{		
        [Constructable]
        public ConjurersGrimoire() : base()
		{
            Name = "Conjurer's Grimoire";
            Hue = 1141; // 0x1B5;

            SkillBonuses.SetValues(0, SkillName.Magery, 15.0);
            Slayer = SlayerName.Silver;
            Attributes.BonusInt = 8;
            Attributes.SpellDamage = 15;
            Attributes.LowerManaCost = 10;
		} 

		public ConjurersGrimoire( Serial serial ) : base( serial )
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