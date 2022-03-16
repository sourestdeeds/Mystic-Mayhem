using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class Lissith : BaseCreature
	{
		//public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public Lissith() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "lady lissith";
			Body = 0x9D;
			BaseSoundID = 0x388;

			SetHits( 250, 350 );
			SetMana( 70, 85 );
			SetStam( 110, 125 );

			SetStr( 75, 85 );
			SetDex( 115, 125 );
			SetInt( 75, 85 );



			SetDamage( 20, 25 );

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

			SetSkill( SkillName.Wrestling, 110.1, 120.0 );
			SetSkill( SkillName.Tactics, 100.1, 110.0 );
			SetSkill( SkillName.MagicResist, 85.5, 95.0 );
			SetSkill( SkillName.Anatomy, 75.1, 85.00 );
			SetSkill( SkillName.Poisoning, 115.1, 120.0 );
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
			PackItem( new Gold( 100 ) );

			if ( Utility.RandomDouble() <= 0.30 )
			PackItem( new LissithsSilk() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.HighScrolls );
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

		public Lissith( Serial serial ) : base( serial )
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