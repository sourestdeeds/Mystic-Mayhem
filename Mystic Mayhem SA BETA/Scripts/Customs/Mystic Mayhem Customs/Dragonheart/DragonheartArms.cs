using System;
using Server;

namespace Server.Items
{
		public class DragonHeartArms : PlateArms
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 11; } }
		public override int BasePoisonResistance{ get{ return 11; } }
		public override int BaseEnergyResistance{ get{ return 11; } }

		public override int ArtifactRarity{ get{ return 54; } }

		public override int InitMinHits{ get{ return 125; } }
		public override int InitMaxHits{ get{ return 125; } }

		public override int AosStrReq{ get{ return 75; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return 0; } }

		public override int ArmorBase{ get{ return 40; } }

		[Constructable]
		public DragonHeartArms()
		{
			Hue = Utility.RandomList( 1157, 1175 );
			Name = "Dragon's Heart Arms";
		
			ArmorAttributes.MageArmor = 1;
	
			Attributes.BonusStr = 5;
			Attributes.LowerManaCost = 15;
			Attributes.SpellDamage = 5;
			Attributes.BonusDex = 5;
			Attributes.NightSight = 1;
			Attributes.AttackChance = 10;
			Attributes.DefendChance = 10;
			Attributes.WeaponSpeed = 5;
			Attributes.WeaponDamage = 7;
			Attributes.Luck = 250;
		}

		public DragonHeartArms( Serial serial ) : base( serial )
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
			switch ( version )
			{
				case 0:
				{
					break;
				}
			}
		}
	}
}