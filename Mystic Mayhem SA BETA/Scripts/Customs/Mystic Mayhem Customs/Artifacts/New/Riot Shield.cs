using System;
using Server;


namespace Server.Items
{
	public class RiotShield : BaseShield
	{
        public override int BasePhysicalResistance { get { return 15; } }
        public override int BaseFireResistance { get { return 13; } }
        public override int InitMinHits { get { return 250; } }
        public override int InitMaxHits { get { return 250; } }
        public override int AosStrReq { get { return 50; } }

       [Constructable]
		public RiotShield() : base( 7107 )
		{
			

            Name = "Riot Shield";
            Hue = 1324;

            Attributes.CastRecovery = 1;
            Attributes.BonusInt = 10;
            Attributes.DefendChance = 15;
            Attributes.SpellChanneling = 1;
            Attributes.AttackChance = 15;

		
       }

        public RiotShield(Serial serial)
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
