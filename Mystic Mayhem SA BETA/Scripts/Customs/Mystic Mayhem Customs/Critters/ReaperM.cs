using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a reapers corpse" )]
	public class ReaperM : BaseCreature
	{
		[Constructable]
		public ReaperM() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a reaper";
			Body = 47;
			BaseSoundID = 442;

			SetStr( 150, 215 );
			SetDex( 66, 75 );
			SetInt( 190, 250 );

			SetHits( 200, 329 );
			SetStam( 0 );

			SetDamage( 15, 19 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 100.1, 125.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 3500;
			Karma = -3500;

			VirtualArmor = 40;
			if ( Utility.RandomDouble() <= 0.30 )
			PackItem( new PetrafiedWood( Utility.RandomMinMax( 9, 15 ) ) );
			PackItem( new Log( 10 ) );
			PackItem( new MandrakeRoot( Utility.RandomMinMax( 10, 20 ) ) );
			if ( Utility.RandomDouble() <= 0.01 )
			PackItem( new DishingStump() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 2; } }
		public override bool DisallowAllMoves{ get{ return false; } }

		public ReaperM( Serial serial ) : base( serial )
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