using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class Sabrix : BaseCreature
	{
		//public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public Sabrix() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "lady sabrix";
			Body = 173;
			BaseSoundID = 0x388;

			SetHits( 300, 400 );
			SetMana( 90, 110 );
			SetStam( 125, 145 );

			SetStr( 115, 135 );
			SetDex( 125, 145 );
			SetInt( 90, 110 );



			SetDamage( 23, 29 );

			SetDamageType( ResistanceType.Physical, 100 );
		//	SetDamageType( ResistanceType.Fire, 100 );
		//	SetDamageType( ResistanceType.Cold, 100 );
		//	SetDamageType( ResistanceType.Poison, 100 );
		//	SetDamageType( ResistanceType.Energy, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 40, 60 );
			SetResistance( ResistanceType.Cold, 40, 60 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 40, 60 );

			SetSkill( SkillName.Wrestling, 115.1, 120.0 );
			SetSkill( SkillName.Tactics, 110.1, 120.0 );
			SetSkill( SkillName.MagicResist, 90.5, 100.0 );
			SetSkill( SkillName.Anatomy, 85.1, 95.00 );
			SetSkill( SkillName.Poisoning, 100.1, 110.0 );
			//SetSkill( SkillName.Magery, 90.1, 100.0 );
			//SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			//SetSkill( SkillName.Meditation, 90.1, 100.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 50;

			Tamable = false; 
         		//ControlSlots = 3; 
         		//MinTameSkill = 92;

			//PackItem( new MagicJewel( 2 ) );
			//if ( Utility.RandomDouble() <= 0.20 )
			//PackItem( new PetrafiedWood( Utility.RandomMinMax( 9, 15 ) ) );
			PackItem( new Gold( 150 ) );

			if ( Utility.RandomDouble() <= 0.30 )
			PackItem( new SabrixEye() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.MedScrolls );
			AddLoot( LootPack.Gems, 12 );
			AddLoot( LootPack.FilthyRich );

		}

		//public override HideType HideType{ get{ return HideType.Spined; } }
		//public override int Hides{ get{ return 5; } }
		//public override int Meat{ get{ return 1; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		//public override int TreasureMapLevel{ get{ return 4; } }
		//public override bool BardImmune{ get{ return true; } }
		//public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		//public override bool IsBondable{ get{ return false; } }
		//public override bool HasBreath{ get{ return true; } }

		public Sabrix( Serial serial ) : base( serial )
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