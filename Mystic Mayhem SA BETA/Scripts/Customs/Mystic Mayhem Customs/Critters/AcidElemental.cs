//st
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a acid elemental corpse" )]
	public class AcidElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public AcidElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a acid elemental";
			Body = 16;
			BaseSoundID = 278;
			Hue = 57;

			SetStr( 326, 355 );
			SetDex( 66, 85 );
			SetInt( 271, 295 );

			SetHits( 196, 213 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 85.0 );
			SetSkill( SkillName.Magery, 70.1, 85.0 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );
			SetSkill( SkillName.Anatomy, 30.1, 60.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 40;
			Tamable = false;


			PackItem( new BlackPearl( 3 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Meager );
		//st	AddLoot( LootPack.Potions );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public AcidElemental( Serial serial ) : base( serial )
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