using System;
using Server.Mobiles;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a snake corpse" )]
	public class MedusaSnake : BaseCreature
	{
		[Constructable]
		public MedusaSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a snake";
			Body = 92; //52;
			Hue = 767; //Utility.RandomSnakeHue();
			BaseSoundID = 0xDB;

			SetStr( 42, 54 );
			SetDex( 26, 35 );
			SetInt( 16, 20 );

			SetHits( 35, 39 );
			SetMana( 0 );

			SetDamage( 11, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );

			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 35.1, 40.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );
			SetSkill( SkillName.Wrestling, 19.3, 34.0 );

			Fame = 600;
			Karma = -600;

			VirtualArmor = 26;

			Tamable = false;
			PackGold( 45, 79 );
			int tm = Utility.Random( 3, 3 );
			TimeSpan duration = TimeSpan.FromMinutes( tm );
			new UnsummonTimer( this, this, duration ).Start();

		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public override bool DeathAdderCharmable{ get{ return true; } }

		public override int Meat{ get{ return 1; } }

		public MedusaSnake(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}