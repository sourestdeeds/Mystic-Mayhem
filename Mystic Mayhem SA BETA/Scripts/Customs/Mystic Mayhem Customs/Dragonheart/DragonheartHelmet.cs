using System;
using Server;

namespace Server.Items
{
		public class DragonHeartHelm : CloseHelm
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
		public DragonHeartHelm()
		{
			Hue = Utility.RandomList( 1157, 1175 );
			Name = "Dragon's Heart Helmet";
		
			ArmorAttributes.MageArmor = 1;
		//	ArmorAttributes.SelfRepair = 5;
	
			Attributes.LowerRegCost = 5;
			Attributes.LowerManaCost = 5;
			Attributes.AttackChance = 3;
			Attributes.DefendChance = 3;
			Attributes.BonusDex = 5;
			Attributes.BonusStam = 15;
			Attributes.BonusInt = 5;
			Attributes.ReflectPhysical = 7;
			Attributes.Luck = 250;
		}

		public DragonHeartHelm( Serial serial ) : base( serial )
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