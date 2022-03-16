using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using System;
using System.Collections;
using Server.Factions;

namespace Server.Mobiles
{
[CorpseName( "an ancient dragon corpse" )]
	public class Thaxll : BaseSpecialCreature
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override double BreathDamageScalar{ get{ return (0.004); } }
		public override bool DoesEarthquaking { get { return true; } }
        public override double EarthquakingChance { get { return 0.50; } }
        public override bool DoesTripleBolting { get { return true; } }
		public override double TripleBoltingChance { get { return 0.25; } }

		private DateTime m_AbilityDelay;
		private DateTime m_Delay = DateTime.Now;
		private Timer m_TeleTimer;

		[Constructable]
		public Thaxll( ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{			

			Name = "Thaxll'ssillyia";
			Title = "the Shadow Dragon";
			Body = 46;
			BaseSoundID = 0x16A;

			Hue = 1175; //2;

			SetStr( 650, 700 );
			SetDex( 725, 775 );
			SetInt( 1225, 1275 );

			SetHits( 50000 );

			SetDamage( 25, 30 );

			switch ( Utility.Random( 3 ) )
			{
			case 0:
			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );			

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 25, 50 );
			SetResistance( ResistanceType.Cold, 95 );
			SetResistance( ResistanceType.Poison, 85 );
			SetResistance( ResistanceType.Energy, 70, 80 );
			break;

			case 1:
			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Energy, 20 );			

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 95 );
			SetResistance( ResistanceType.Cold, 75 );
			SetResistance( ResistanceType.Poison, 85 );
			SetResistance( ResistanceType.Energy, 25, 50 );
			break;

			case 2:
			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 40 );			

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 65, 75 );
			SetResistance( ResistanceType.Cold, 0, 65 );
			SetResistance( ResistanceType.Poison, 85 );
			SetResistance( ResistanceType.Energy, 95 );
			break;
			}

			SetSkill( SkillName.EvalInt, 250.0 ); // 200
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.Anatomy, 300.0 );
			SetSkill( SkillName.Wrestling, 200.0 );
			SetSkill( SkillName.MagicResist, 1000.0 );
			SetSkill( SkillName.Tactics, 300.0 );

			Fame = 50000;
			Karma = -50000;

			VirtualArmor = 85;
			
			m_AbilityDelay = DateTime.Now + TimeSpan.FromSeconds( 30 );

			Tamable = false;

			m_TeleTimer = new TeleportTimer( this );
			m_TeleTimer.Start();

		//	PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );
			
		}
		
		public override void OnActionCombat()
		{
			if ( Combatant == null || Frozen || Paralyzed || Map == null || Map == Map.Internal )
				base.OnActionCombat();
			else if ( DateTime.Now > m_AbilityDelay )
			{
				if ( Utility.RandomBool() )
					Ability.ShinraTensei( this, Combatant );
				else
				{
					Mobile target = Ability.FindRandomTarget( this );

					if ( target == null )
						return;

					Ability.BanshoTenin( this, target );
				}

				m_AbilityDelay = DateTime.Now + TimeSpan.FromSeconds( 30 );
			}

			base.OnActionCombat();
		}

		/*public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( DateTime.Now > m_AbilityDelay )
			{
				this.Say( "Kal Vas Tal [Wing Buffet]" );
				m_AbilityDelay = DateTime.Now + TimeSpan.FromSeconds( 30 );
				reflect = true;
			}
		}*/

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 8 );

			//AddLoot( LootPack.Gems, Utility.Random( 20, 25 ) );
		}
		public override void OnDeath( Container c )
		{

			c.DropItem( new Ruby( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Amber( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Amethyst( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Citrine( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Diamond( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Emerald( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Sapphire( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new StarSapphire( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Tourmaline( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new MagicJewel( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Platinum( Utility.RandomMinMax( 16, 5000 ) ) );
			c.DropItem( new ValorStone() );
			c.DropItem( new HandGrenade() );

			if ( Utility.RandomDouble() <= 0.25 )
				c.DropItem( new ConjurersTrinket() );
 
			switch ( Utility.Random( 75 )) 
			{ 
				case 0: 				c.DropItem( new PowerScroll( SkillName.Blacksmith, 120 ) ); break;  
				case 1: case 2: 			c.DropItem( new PowerScroll( SkillName.Blacksmith, 115 ) ); break; 
				case 3: case 4: case 5: 		c.DropItem( new PowerScroll( SkillName.Blacksmith, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	c.DropItem( new PowerScroll( SkillName.Blacksmith, 105 ) ); break; 
  
				case 10: 				c.DropItem( new PowerScroll( SkillName.Tailoring, 120 ) ); break;  
				case 11: case 12: 			c.DropItem( new PowerScroll( SkillName.Tailoring, 115 ) ); break; 
				case 13: case 14: case 15: 		c.DropItem( new PowerScroll( SkillName.Tailoring, 110 ) ); break; 
				case 16: case 17: case 18: case 19: 	c.DropItem( new PowerScroll( SkillName.Tailoring, 105 ) ); break;  

				case 20: 				c.DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 21: case 22: 			c.DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 23: case 24: case 25: 		c.DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 26: case 27: case 28: case 29: 	c.DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break; 
  
				case 30: 				c.DropItem( new PowerScroll( SkillName.Mining, 120 ) ); break;  
				case 31: case 32: 			c.DropItem( new PowerScroll( SkillName.Mining, 115 ) ); break; 
				case 33: case 34: case 35: 		c.DropItem( new PowerScroll( SkillName.Mining, 110 ) ); break; 
				case 36: case 37: case 38: case 39: 	c.DropItem( new PowerScroll( SkillName.Mining, 105 ) ); break;
 
				case 40: 				c.DropItem( new PowerScroll( SkillName.Carpentry, 120 ) ); break;  
				case 41: case 42: 			c.DropItem( new PowerScroll( SkillName.Carpentry, 115 ) ); break; 
				case 43: case 44: case 45: 		c.DropItem( new PowerScroll( SkillName.Carpentry, 110 ) ); break; 
				case 46: case 47: case 48: case 49: 	c.DropItem( new PowerScroll( SkillName.Carpentry, 105 ) ); break; 

				case 50: 				c.DropItem( new PowerScroll( SkillName.Alchemy, 120 ) ); break;  
				case 51: case 52: 			c.DropItem( new PowerScroll( SkillName.Alchemy, 115 ) ); break; 
				case 53: case 54: case 55: 		c.DropItem( new PowerScroll( SkillName.Alchemy, 110 ) ); break; 
				case 56: case 57: case 58: case 59: 	c.DropItem( new PowerScroll( SkillName.Alchemy, 105 ) ); break; 
			}
 
			switch ( Utility.Random( 75 )) 
			{

				case 0: 				c.DropItem( new PowerScroll( SkillName.Fletching, 120 ) ); break;  
				case 1: case 2: 			c.DropItem( new PowerScroll( SkillName.Fletching, 115 ) ); break; 
				case 3: case 4: case 5: 		c.DropItem( new PowerScroll( SkillName.Fletching, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	c.DropItem( new PowerScroll( SkillName.Fletching, 105 ) ); break;

				case 10: 				c.DropItem( new PowerScroll( SkillName.Inscribe, 120 ) ); break;  
				case 11: case 12: 			c.DropItem( new PowerScroll( SkillName.Inscribe, 115 ) ); break; 
				case 13: case 14: case 15: 		c.DropItem( new PowerScroll( SkillName.Inscribe, 110 ) ); break; 
				case 16: case 17: case 18: case 19: 	c.DropItem( new PowerScroll( SkillName.Inscribe, 105 ) ); break;
  
				case 20: 				c.DropItem( new PowerScroll( SkillName.Cartography, 120 ) ); break;  
				case 21: case 22: 			c.DropItem( new PowerScroll( SkillName.Cartography, 115 ) ); break; 
				case 23: case 24: case 25: 		c.DropItem( new PowerScroll( SkillName.Cartography, 110 ) ); break; 
				case 26: case 27: case 28: case 29: 	c.DropItem( new PowerScroll( SkillName.Cartography, 105 ) ); break;

				case 30: 				c.DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 31: case 32: 			c.DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 33: case 34: case 35: 		c.DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 36: case 37: case 38: case 39: 	c.DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break;

				case 40: 				c.DropItem( new PowerScroll( SkillName.Cooking, 120 ) ); break;  
				case 41: case 42: 			c.DropItem( new PowerScroll( SkillName.Cooking, 115 ) ); break; 
				case 43: case 44: case 45: 		c.DropItem( new PowerScroll( SkillName.Cooking, 110 ) ); break; 
				case 46: case 47: case 48: case 49: 	c.DropItem( new PowerScroll( SkillName.Cooking, 105 ) ); break; 
      			}
			base.OnDeath( c );
		}
		
		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 500, 1000 );
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
		
		public override bool OnBeforeDeath()
		{

				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -12; x <= 12; ++x )
					{
						for ( int y = -12; y <= 12; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 12 )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			
					
			return base.OnBeforeDeath();
		}

		public void DoSpecialAbility( Mobile target )
		{

			switch ( Utility.Random( 10 ) )
			{
				case 1: case 2: case 3: ThrowFirePool( TimeSpan.FromSeconds( 10 ), 30, 40, target ); break;
				case 4: TeleportMe( this ); break;
				case 5: case 6: case 7: DrainLife(); break;
			}

		}

		public override int BreathComputeDamage()
		{
			int damage = (int)(Utility.RandomMinMax(200, 300 ) );

			return damage;
		}

		public override void OnDamagedBySpell( Mobile attacker )
		{
			switch ( Utility.Random( 12 ) )
			{
				case 1: DrainLife(); break;

				case 2: case 3: case 4: SendEBolt( attacker ); break;
				case 5: case 6: ThrowFirePool( TimeSpan.FromSeconds( 10 ), 30, 40, attacker ); break;
				case 7: case 8: case 9: ManaSnatch( attacker ); break;

			}
		}

		public void SendEBolt( Mobile to )
		{
			this.MovingParticles( to, 0x379F, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, 70, 0, 30, 30, 0, 40 );// dmg,phy,fir,cold,pois,ener
		}

		public void ManaSnatch( Mobile to )
		{
			this.MovingParticles( to, 0x36D4, 7, 0, false, true, 93, 0, 9502, 4019, 0x160, EffectLayer.Waist, 0 ); // fireball
			to.Mana -= Utility.Random( 20, 20 );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			if ( attacker is Clone )
			{
				BaseCreature clone = (BaseCreature)attacker;
				Effects.SendLocationParticles( EffectItem.Create( clone.Location, clone.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				Effects.PlaySound( clone, clone.Map, 0x201 );
				clone.Delete();
			}

			int st = Utility.Random( 10, 10 );

			attacker.Damage( Utility.Random( st, 10 ), this );
			attacker.Stam -= Utility.Random( st, 10 );
			attacker.Mana -= Utility.Random( st, 10 );

			if ( 0.2 > Utility.RandomDouble() && attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controlled && c.ControlMaster != null )
				{
					c.ControlTarget = c.ControlMaster;
					c.ControlOrder = OrderType.Attack;
					c.Combatant = c.ControlMaster;
				}
			}

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			defender.Damage( Utility.Random( 10, 10 ), this );
			defender.Stam -= Utility.Random( 10, 10 );
			defender.Mana -= Utility.Random( 10, 10 );

			if ( 0.2 > Utility.RandomDouble() )
			{
				defender.Combatant = null;

			}

			if ( 0.2 > Utility.RandomDouble() && defender is BaseCreature )
			{
				BaseCreature c = (BaseCreature)defender;

				if ( c.Controlled && c.ControlMaster != null )
				{
					c.Combatant = null;
					c.Hits = (int)(c.Hits / 4);
				}
			}

			DoSpecialAbility( defender );
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			switch ( Utility.Random( 15 ) )
			{
			case 0: case 1: case 2:
				if ( !willKill )
				{
					if ( from != null && from != this && InRange( from, 4 ) )
					{
						ThrowFirePool( TimeSpan.FromSeconds( 10 ), 30, 40, from );
					}
				}
			break;
			
			}

			base.OnDamage( amount, from, willKill );
		}

		public void ThrowFirePool( TimeSpan duration, int minDamage, int maxDamage, Mobile target )
		{
			if ( (target != null && target.Map == null) || this.Map == null )
				return;

			int pools = Utility.RandomMinMax( 1, 2 );

			for ( int i = 0; i < pools; ++i )
			{
				FirePool fp = new FirePool( duration, minDamage, maxDamage );

				if ( target != null && target.Map != null )
				{
					fp.MoveToWorld( target.Location, target.Map );
					continue;
				}

				bool validLocation = false;
				Point3D loc = this.Location;
				Map map = this.Map;

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

				fp.MoveToWorld( loc, map );
			}
		}

		public void TeleportTarget( Mobile target )
		{
			Map map = target.Map;
			if ( map != null )
			{
				// try 10 times to find a teleport spot
				for ( int i = 0; i < 10; ++i )
				{
					int x = X + (Utility.RandomMinMax( 8, 12 ) * (Utility.RandomBool() ? 1 : -1));
					int y = Y + (Utility.RandomMinMax( 8, 12 ) * (Utility.RandomBool() ? 1 : -1));
					int z = Z;

					if ( !map.CanFit( x, y, z, 16, false, false ) )
						continue;

					Point3D from = target.Location;
					Point3D to = new Point3D( x, y, z );

					if ( !InLOS( to ) )
						continue;

					target.Location = to;
					target.ProcessDelta();

					target.Combatant = null;

					Effects.SendLocationParticles( EffectItem.Create( from, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					Effects.PlaySound( to, map, 0x1FE );

					break;

				}
			}
		}

		private Timer m_SoundTimer;
		private bool m_HasTeleportedAway;


		public void TeleportMe( Mobile target )
		{
			if ( Utility.Random(15) == 0 )
			{
				Map map = this.Map;

				if ( map != null )
				{
					int z = 0;
					int x = 5184;
					int y = 1005;

					switch( Utility.Random( 8 ) )
					{
						case 0: x = 5186; y = 1010; break;
						case 1: x = 5162; y = 994; break;
						case 2: x = 5141; y = 994; break;
						case 3: x = 5134; y = 965; break;
						case 4: x = 5148; y = 966; break;
						case 5: x = 5148; y = 979; break;
						case 6: x = 5189; y = 1000; break;
						case 7: x = 5143; y = 965; break;
					}

					Point3D from = this.Location;
					Point3D to = new Point3D( x, y, z );

					this.Location = to;
					this.ProcessDelta();

					Effects.SendLocationParticles( EffectItem.Create( from, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					Effects.PlaySound( to, map, 0x1FE );

				//	break;
				}
			}
		}

		public void DrainLife()
		{
			if( this.Map == null )
				return;

			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel the life drain out of you!" );

				int toDrain = Utility.RandomMinMax( 20, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
		//	if ( caster.Body.IsMale )
			if ( Utility.Random( 3 ) == 0 )
				reflect = true;
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
		//	if ( caster.Body.IsMale )
				scalar = 25; // 10x
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled

		public override bool BardImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }		
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 6 ) )
			{
				default:
				case 0: return WeaponAbility.Dismount;
				case 1: return WeaponAbility.BleedAttack;
				case 2: return WeaponAbility.CrushingBlow;
				case 3: return WeaponAbility.MortalStrike;
				case 4: return WeaponAbility.DoubleStrike;
				case 5: return WeaponAbility.WhirlwindAttack;


			}
		}

		private class TeleportTimer : Timer
		{
			private Mobile m_Owner;

			private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 15.0 ), TimeSpan.FromSeconds( 15.0 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				Map map = m_Owner.Map;

				if ( map == null )
					return;

				if ( 0.10 < Utility.RandomDouble() )
					return;

				Mobile toTeleport = null;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) )
					{
						toTeleport = m;
						break;
					}
				}

				if ( toTeleport != null )
				{
					int offset = Utility.Random( 8 ) * 2;

					Point3D to = m_Owner.Location;

					for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = m_Owner.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = m_Owner.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, m_Owner.Z ) )
						{
							to = new Point3D( x, y, m_Owner.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Mobile m = toTeleport;

					Point3D from = m.Location;

					m.Location = to;

					Server.Spells.SpellHelper.Turn( m_Owner, toTeleport );
					Server.Spells.SpellHelper.Turn( toTeleport, m_Owner );

					m.ProcessDelta();

					Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					m.PlaySound( 0x1FE );

					m_Owner.Combatant = toTeleport;
				}
			}
		}

		public Thaxll( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( BaseSoundID == 0x16A )
				BaseSoundID = 0xA8;

			m_TeleTimer = new TeleportTimer( this );
			m_TeleTimer.Start();
		}
	}
}