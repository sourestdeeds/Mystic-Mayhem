using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a pair of pants" )]
	public class RunningPants : BaseCreature
	{
		[Constructable]
		public RunningPants() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a pair of running pants";
			Body = 430;

			SetStr( 50 );
			SetDex( 10 );
			SetInt( 5 );

			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 20 );

			SetSkill( SkillName.MagicResist, 9.8 );
			SetSkill( SkillName.Tactics, 7.2 );
			SetSkill( SkillName.Wrestling, 6.1 );

			Fame = 300;
			Karma = 300;

			VirtualArmor = 10;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 10.0;

			PackItem( new ShortPants() );

		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 25; } }

		public override int GetIdleSound() { return 1320; } 
		public override int GetAttackSound() { return 1346; }
		public override int GetAngerSound() { return 1354; } 
		public override int GetHurtSound() { return 1344; } 
		public override int GetDeathSound()	{ return 1343; }

		public RunningPants(Serial serial) : base(serial)
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