using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a reptalon corpse" )]
	public class Reptalon : BaseMount
	{
		public override bool StatLossAfterTame { get { return false; } }
		[Constructable]
		public Reptalon() : this( "a reptalon" )
		{
		}

		[Constructable]
		public Reptalon( string name ) : base( name, 276, 0x3e90, AIType.AI_NecroMage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 276;
			BaseSoundID = 0x4FB;

			SetHits( 900, 950 );
			SetMana( 700, 750 );
			SetStam( 40, 55 );

			SetStr( 1021, 1030 );
			SetDex( 125, 175 );
			SetInt( 500, 750 );



			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 25 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Wrestling, 110.1, 120.5 );
			SetSkill( SkillName.Tactics, 110.1, 120.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Anatomy, 90.1, 100.00 );
			SetSkill( SkillName.Poisoning, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.Necromancy, 90.1, 100.0 );
			SetSkill( SkillName.SpiritSpeak, 90.1, 100.0 );

			Fame = 14000;
			Karma = -14000;

			VirtualArmor = 70;

			Tamable = true; 
         		ControlSlots = 4; 
         		MinTameSkill = 101.1;

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.Gems, 12 );
			AddLoot( LootPack.FilthyRich );

		}
		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;

			return base.GetAngerSound();
		}

		public override HideType HideType{ get{ return HideType.Spined; } }
		public override int Hides{ get{ return 5; } }

		public override bool SubdueBeforeTame{ get{ return true; } }

		public override int Meat{ get{ return 10; } }
		//public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		//public override int TreasureMapLevel{ get{ return 4; } }
		//public override bool BardImmune{ get{ return true; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool IsBondable{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.MortalStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.CrushingBlow;
			}
		}

		public Reptalon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}