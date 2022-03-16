using System;
using Server;


namespace Server.Items
{
    public class ArmsoftheBeast : PlateArms
	{
        public override int BasePhysicalResistance { get { return 12; } }
        public override int BaseFireResistance { get { return 11; } }
        public override int BaseColdResistance { get { return 9; } }
        public override int BasePoisonResistance { get { return 8; } }
        public override int BaseEnergyResistance { get { return 12; } }
        public override int InitMinHits { get { return 125; } }
        public override int InitMaxHits { get { return 125; } }
        public override int AosStrReq { get { return 50; } }

       [Constructable]
		public ArmsoftheBeast() 
		{
			

            Name = "Arms of the Beast";
            Hue = 1324;

            Attributes.BonusStr = 10;
            Attributes.SpellChanneling = 1;
            Attributes.AttackChance = 5;
            Attributes.BonusHits = 2;
		    Attributes.BonusMana = 3;
            Attributes.DefendChance = 5;
            Attributes.ReflectPhysical = 10;
            Attributes.RegenMana = 3;
            Attributes.LowerManaCost = 4;
       
       }

        public ArmsoftheBeast(Serial serial)
            : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}
	}
}
