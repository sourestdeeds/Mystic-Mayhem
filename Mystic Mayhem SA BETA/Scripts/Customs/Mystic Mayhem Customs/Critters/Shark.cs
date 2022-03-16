//aa
using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a shark corpse" )]
	public class Shark : BaseCreature
	{
		[Constructable]
		public Shark()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shark";
			Body = 0x97;
		//	BaseSoundID = 0x8A;
			Hue = Utility.RandomList( 282, 700, 1105 );

			SetStr( 321, 349 );
			SetDex( 266, 285 );
			SetInt( 296, 310 );

			SetHits( 1235, 1247 );

			SetDamage( 43, 46 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 75, 80 );
			SetResistance( ResistanceType.Poison, 80, 85 );
			SetResistance( ResistanceType.Energy, 80, 85 );

			SetSkill( SkillName.MagicResist, 115.1, 120.0 );
			SetSkill( SkillName.Tactics, 119.2, 129.0 );
			SetSkill( SkillName.Wrestling, 119.2, 129.0 );
			SetSkill( SkillName.EvalInt, 90.1, 110.0 );
			SetSkill( SkillName.Magery, 110.1, 120.0 );

			Fame = 2000;
			Karma = -2000;

			VirtualArmor = 46;
			CanSwim = true;
			CantWalk = true;
			PackGold( 200, 400 );
			if( Utility.RandomDouble() < .20 )
				PackItem( new MessageInABottle( Map.Felucca ) );
			if( Utility.RandomDouble() < .30 )
				PackItem( new WhitePearl() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.HighScrolls );
			AddLoot( LootPack.Gems, 12 );

		}

		public override int Meat { get { return 10; } }

		public Shark( Serial serial ) : base( serial )
		{
		}

	/*	public override void OnDoubleClick( Mobile from )
		{
			if( from.AccessLevel >= AccessLevel.GameMaster )
				Jump();
		}

		public virtual void Jump()
		{
			if( Utility.RandomBool() )
				Animate( 3, 16, 1, true, false, 0 );
			else
				Animate( 4, 20, 1, true, false, 0 );
		}

		public override void OnThink()
		{
			if( Utility.RandomDouble() < .005 ) // slim chance to jump
				Jump();

			base.OnThink();
		} */

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}