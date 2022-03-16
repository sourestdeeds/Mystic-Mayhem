using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a sand worm corpse" )]
	public class SandWorm : BaseCreature
	{
		[Constructable]
		public SandWorm() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sand worm";
			Body = 150;
			BaseSoundID = 447;
			Hue = 450;

			SetStr( 251, 425 );
			SetDex( 87, 135 );
			SetInt( 87, 155 );

			SetHits( 151, 255 );

			SetDamage( 20, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 55, 70 );

			SetSkill( SkillName.MagicResist, 70.1, 85.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );
			SetSkill( SkillName.Magery, 80.1, 90.0 );
			SetSkill( SkillName.Meditation, 110.1, 120.0 );
			SetSkill( SkillName.EvalInt, 110.1, 120.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 70;
			//CanSwim = true;
			//CantWalk = true;

			PackItem( new Sand() );
			PackItem( new Sand() );

			//if ( 0.2 >= Utility.RandomDouble() )
			//	PackItem( new SpecialFishingNet() );
		//	if ( Utility.RandomDouble() <= 0.02 )
		//	PackItem( new MagicJewel( 1 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 1 );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int Meat{ get{ return 10; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Black; } }
		public override bool BardImmune{ get{ return true; } }

		public SandWorm( Serial serial ) : base( serial )
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