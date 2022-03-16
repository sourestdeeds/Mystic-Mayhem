using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x26BD, 0x26C7 )]
	public class DoomKiss : BaseSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }
////////////////////////////////////////////added for poisoning purposes/////////////////////////////////
		//public override WeaponAbility ThirdAbility{get{return WeaponAbility.InfectiousStrike;}}
/////////////////////////////////////////////////////////////////////////////////////////////////////////	

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 14; } }
		public override int AosMaxDamage{ get{ return 16; } }
		public override int AosSpeed{ get{ return 37; } }
		public override float MlSpeed{ get{ return 2.75f; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 14; } }
		public override int OldMaxDamage{ get{ return 16; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 96; } }
		public override int InitMaxHits{ get{ return 96; } }

		public override SkillName DefSkill{ get{ return SkillName.Swords; } }

		[Constructable]
		public DoomKiss() : base( 0x26BD )
		{
			Name = "Doom Kiss";
			Attributes.SpellChanneling = 1;
			Attributes.CastRecovery = 6;
			Attributes.CastSpeed = 2;
			Attributes.AttackChance = 50;
			Attributes.WeaponSpeed = 35;
			Attributes.WeaponDamage = 300;
			Hue = 1194;
			//Speed = 1.25;
			Weight = 4.0;
		}

		public DoomKiss( Serial serial ) : base( serial )
		{
		}

///////////////////////////////////Poison Onhit//////////////////////////////////////////////////////
		/*public override void OnHit( Mobile attacker, Mobile defender, double damage )
		{
			base.OnHit( attacker, defender, damage );

   		if (PoisonCharges > 0 && Utility.RandomDouble() >= .75)
            		{
                		defender.ApplyPoison(attacker, m_Poison);
				BaseWeapon weapon = attacker.Weapon as BaseWeapon;
                		--weapon.PoisonCharges;
            		}
		}*/
///////////////////////////////////Poison Onhit//////////////////////////////////////////////////////


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}