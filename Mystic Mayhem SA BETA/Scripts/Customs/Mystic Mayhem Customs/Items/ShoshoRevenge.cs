using System;
using Server;

namespace Server.Items
{
	public class ShoshoRevenge : Yumi
	{
		public override int ArtifactRarity{ get{ return 22; } }

      public override int AosMinDamage{ get{ return 22; } } 
      public override int AosMaxDamage{ get{ return 25; } } 
      public override int AosSpeed{ get{ return 30; } } 
 
      public override int OldMinDamage{ get{ return 22; } } 
      public override int OldMaxDamage{ get{ return 25; } } 
      public override int OldSpeed{ get{ return 30; } } 

		public override int InitMinHits{ get{ return 125; } }
		public override int InitMaxHits{ get{ return 125; } }

		[Constructable]
		public ShoshoRevenge()
		{
			Name = "Shosho Revenge";
			Hue = 143;
				Attributes.CastRecovery = 2;
				Attributes.CastSpeed = 2;
				WeaponAttributes.HitLightning = 40;
				WeaponAttributes.HitMagicArrow = 60;
				WeaponAttributes.HitHarm = 45;
				WeaponAttributes.HitLeechHits = 60;
				WeaponAttributes.HitLeechStam = 90;
				WeaponAttributes.HitLowerDefend = 100;
				Attributes.WeaponDamage = 100;
				Attributes.AttackChance = 60;
				Attributes.BonusDex = 40;
				WeaponAttributes.UseBestSkill = 1;
				Attributes.WeaponSpeed = 75;
		}

		public ShoshoRevenge( Serial serial ) : base( serial )
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
		}
	}
}