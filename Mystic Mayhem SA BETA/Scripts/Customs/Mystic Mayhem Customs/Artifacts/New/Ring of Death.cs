using System;
using Server;

namespace Server.Items
{
	public class RingOfDeath : GoldRing
	{
		public override int ArtifactRarity{ get{ return 11; } }
        
		[Constructable]
		public RingOfDeath()
		{
            Name = "Ring Of Death";
            Hue = 1324;
			Attributes.BonusInt = 7;
			Attributes.CastRecovery = 3;
            Attributes.DefendChance = 5;
			Attributes.LowerManaCost = 10;
			}

		public RingOfDeath( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x554 )
				Hue = 0x554;
		}
	}
}