using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a medusa corpse" )]
	public class Medusa2 : BaseCreature
	{
		[Constructable]
		public Medusa2() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Medusa";
			Body = 728; 

			SetStr( 1235, 1391 );
			SetDex( 128, 139 );
			SetInt( 537, 664 );

			SetHits( 70000 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 55, 65 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 60, 75 );

			SetSkill( SkillName.Anatomy, 110.6, 116.1 );
			SetSkill( SkillName.EvalInt, 100.0, 114.4 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.Meditation, 118.2, 127.8 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 111.9, 134.5 );
			SetSkill( SkillName.Wrestling, 119.7, 128.9 );
		}

		public override int GetIdleSound() { return 1557; } 
		public override int GetAngerSound() { return 1554; } 
		public override int GetHurtSound() { return 1556; } 
		public override int GetDeathSound()	{ return 1555; }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 5 );
		}

		public Medusa2( Serial serial ) : base( serial )
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