using System;
using Server;

namespace Server.Items
{
		public class DragonHeartSword : ThinLongsword
	{

		public override int ArtifactRarity{ get{ return 54; } }

		public override int InitMinHits{ get{ return 125; } }
		public override int InitMaxHits{ get{ return 125; } }


		[Constructable]
		public DragonHeartSword()
		{
			Hue = Utility.RandomList( 1157, 1175 );
			Name = "Dragon's Heart Longsword";
		
			WeaponAttributes.HitFireball = 20;
			WeaponAttributes.HitLightning = 20;
			Slayer = SlayerName.DragonSlaying;
			WeaponAttributes.HitLeechMana = 20;
		//	WeaponAttributes.MageWeapon = 1;
		//	WeaponAttributes.SelfRepair = 5;
			WeaponAttributes.HitHarm = 20;
			WeaponAttributes.HitMagicArrow = 20;
	
			Attributes.BonusStr = 5;
			Attributes.AttackChance = 10;
			Attributes.WeaponDamage = 45;
			Attributes.WeaponSpeed = 10;
			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 1;
			Attributes.BonusDex = 10;

		}

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			fire = 40;
			phys = 15;
			cold = 10;
			pois = 15;
			nrgy = 20;
			chaos = 0;
			direct = 0;
		}

		public DragonHeartSword( Serial serial ) : base( serial )
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