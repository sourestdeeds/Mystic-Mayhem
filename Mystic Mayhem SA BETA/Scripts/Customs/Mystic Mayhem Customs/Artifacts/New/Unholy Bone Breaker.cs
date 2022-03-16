using System;
using Server;

namespace Server.Items
{
	public class UnholyBoneBreaker : VikingSword
	{
		public override int ArtifactRarity{ get{ return 25; } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.BleedAttack; } }

		public override int InitMinHits{ get{ return 125; } }
		public override int InitMaxHits{ get{ return 125; } }

		[Constructable]
		public UnholyBoneBreaker()
		{
			Name = "Unholy Bone Breaker";
            Hue = 1324;
            Weight = 2.0;
			
            WeaponAttributes.HitLeechHits = 30;
			WeaponAttributes.HitLightning = 30;
			WeaponAttributes.MageWeapon = 1;
			WeaponAttributes.UseBestSkill = 1;

			Attributes.AttackChance = 5;
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 10;
			Attributes.ReflectPhysical = 5;
			Attributes.SpellChanneling = 1;

            Slayer = SlayerName.Exorcism;
			StrRequirement = 50;
        }

		public UnholyBoneBreaker( Serial serial ) : base( serial )
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