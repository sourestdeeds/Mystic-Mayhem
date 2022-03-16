using System;
using Server;

namespace Server.Items
{
	public class ArmsOfTheHarrower : BoneArms
	{
		public override int LabelNumber{ get{ return 1061095; } } // Arms of the Harrower
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePoisonResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ArmsOfTheHarrower()
		{
			Name = "Arms of the Harrower";
			Hue = 0x4F6;
			Attributes.RegenHits = 3;
			Attributes.RegenStam = 2;
			Attributes.WeaponDamage = 15;
		}

		public ArmsOfTheHarrower( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
			{
				if ( Hue == 0x55A )
					Hue = 0x4F6;

				PoisonBonus = 0;
			}
		}
	}
}