using System;
using Server;
using Server.Items;
using Server.Spells.Fifth;
using Server.Spells.Seventh;

namespace Server.Mobiles
{
	[CorpseName( "medusa is dead" )]
	public class Medusa : BaseCreature
	{
		[Constructable]
		public Medusa() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Medusa";
			Body = 87;
			BaseSoundID = 644;
			Hue = 1268;

			SetStr( 416, 505 );
			SetDex( 96, 115 );
			SetInt( 366, 455 );

			SetHits( 900, 1003 );

			SetDamage( 18, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 65, 75 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 75, 85 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.Meditation, 65.4, 75.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 19000;
			Karma = -19000;

			VirtualArmor = 70;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public override void OnDeath( Container c )
		{
			switch ( Utility.Random( 120 )) 
			{ 
				case 0: c.DropItem( new FlorianeArms() ); break; 
				case 1: c.DropItem( new FlorianeBustier() ); break;
				case 2: c.DropItem( new FlorianeChest() ); break;
				case 3: c.DropItem( new FlorianeGloves() ); break;
				case 4: c.DropItem( new FlorianeHeadress() ); break;
				case 5: c.DropItem( new FlorianeSkirt() ); break;
      			}
			base.OnDeath( c );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
		    	try{
				this.PassiveSpeed = 0.2;
				this.ActiveSpeed = 0.1;
				//if ( 0.9 < Utility.RandomDouble() )
				//	return;

				switch ( Utility.Random( 40 ) )
				{
					case 0:
					{
						from.SendLocalizedMessage( 1004014 ); // You have been stunned!
						from.Freeze( TimeSpan.FromSeconds( 2.0 ) );
						break;
					}
					case 1:
					{
						from.SendAsciiMessage( "Medusa's paralizes you!" );

						from.Freeze( TimeSpan.FromSeconds( 4.0 ) );
						break;
					}
					case 2:
					{
						from.SendAsciiMessage( "Medusa's death stare turns you to stone!" );

						Polymorph( from );
						break;
					}
				}
		    	}
		    	catch{}
		}


		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}

		public override void OnDamagedBySpell( Mobile from )
		{
			base.OnDamagedBySpell( from );

			DoSpecialAbility( from );
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;

			if ( 0.15 >= Utility.RandomDouble() ) // 20% chance to more ratmen
				SpawnSnake( target );

		}

		public void SpawnSnake( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int snakes = 0;

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m is MedusaSnake ) // || m is RatmanArcher9 || m is RatmanMage9 )
					++snakes;
			}

			if ( snakes < 8 )
			{
				PlaySound( 0x3D );

				int newSnakes = Utility.RandomMinMax( 3, 4 );

				for ( int i = 0; i < newSnakes; ++i )
				{
					BaseCreature snake;
					snake = new MedusaSnake();
					snake.PassiveSpeed /= 2.50;
					snake.ActiveSpeed /= 2.50;

				/*	switch ( Utility.Random( 5 ) )
					{
						default:
						case 0: case 1:	snake = new Ratman9(); break;
						case 2: case 3:	snake = new RatmanArcher9(); break;
						case 4:			snake = new RatmanMage9(); break;
					} */

					snake.Team = this.Team;

					bool validLocation = false;
					Point3D loc = this.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					snake.MoveToWorld( loc, map );
					snake.Combatant = target;
				}
			}
		}
		public void Polymorph( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;
			if ( m is Clone )
				return;

			if ( mount != null )
				mount.Rider = null;

			if ( m.Mounted )
				return;

			if ( m.BeginAction( typeof( PolymorphSpell ) ) )
			{
				Item disarm = m.FindItemOnLayer( Layer.OneHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				disarm = m.FindItemOnLayer( Layer.TwoHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				m.BodyMod = 0xE;
				m.HueMod = 0;
				m.Frozen = true;
				m.Blessed = true;

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 20.0 ) )
			{
				m_Owner = owner;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( PolymorphSpell ) ) )
				{
					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.Blessed = false;
					m_Owner.Frozen = false;
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
			}
		}



		public Medusa( Serial serial ) : base( serial )
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