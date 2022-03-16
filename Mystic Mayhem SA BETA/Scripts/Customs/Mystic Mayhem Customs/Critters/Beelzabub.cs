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
[CorpseName( "a devil corpse" )]
	public class Beelzabub : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override double BreathDamageScalar{ get{ return (0.004); } }

		private DateTime m_Delay = DateTime.Now;
		private Timer m_TeleTimer;

		[Constructable]
		public Beelzabub( ) : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{			

			Name = "Beelzabub";
			Title = "the Archfiend";
			Body = 318;
			BaseSoundID = 0x165;

			Hue = 33; //2;

			SetStr( 650, 700 );
			SetDex( 725, 775 );
			SetInt( 1225, 1275 );

			SetHits( 100000 );

			SetDamage( 86, 102 );

			switch ( Utility.Random( 3 ) )
			{
			case 0:
			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );			

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 60, 70 );
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
			SetResistance( ResistanceType.Energy, 70, 80 );
			break;

			case 2:
			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 40 );			

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 65, 75 );
			SetResistance( ResistanceType.Cold, 55, 65 );
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

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 85;

			Tamable = false;

			m_TeleTimer = new TeleportTimer( this );
			m_TeleTimer.Start();

		//	PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );
			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 5 );

			AddLoot( LootPack.Gems, Utility.Random( 20, 25 ) );
		}
		public override void OnDeath( Container c )
		{

			c.DropItem( new Gold( 18000, 20000 ) );
			c.DropItem( new Silver( 500, 1200 ) );
			switch ( Utility.Random( 2 ) )
			{
				case 0: c.DropItem( new SpellbookBasket( ) ); break;
			}
			c.DropItem( new Brimstone( Utility.RandomMinMax( 30, 40 ) ) );

			int amt = Utility.RandomMinMax( 90, 100 );

			c.DropItem( new CapturedSoul( amt ) );
		/*	amt = Utility.RandomMinMax( 40, 45 );
			switch ( Utility.Random( 2 ) )
			{
				case 0: c.DropItem( new ArmageddonHides( amt ) ); break;
				case 1: c.DropItem( new FrostHides( amt ) ); break;
			}
			amt = Utility.RandomMinMax( 40, 45 );
			switch ( Utility.Random( 2 ) )
			{
				case 0: c.DropItem( new VulconHides( amt ) ); break;
				case 1: c.DropItem( new AquasHides( amt ) ); break;
			} */
			switch ( Utility.Random( 7 ) )
			{
				case 0: c.DropItem( new Blight(  ) ); break;
				case 1: c.DropItem( new CapturedEssence(  ) ); break;
				case 2: c.DropItem( new Corruption(  ) ); break;
				case 3: c.DropItem( new DiseasedBark(  ) ); break;
				case 4: c.DropItem( new DreadHornMane(  ) ); break;
				case 5: c.DropItem( new EyeOfTheTravesty(  ) ); break;
				case 6: c.DropItem( new GrizzledBones(  ) ); break;
			}
			switch ( Utility.Random( 7 ) )
			{
				case 0: c.DropItem( new LardOfParoxysmus(  ) ); break;
				case 1: c.DropItem( new LuminescentFungi(  ) ); break;
				case 2: c.DropItem( new Muculent(  ) ); break;
				case 3: c.DropItem( new ParasiticPlant(  ) ); break;
				case 4: c.DropItem( new Putrefication(  ) ); break;
				case 5: c.DropItem( new Scourge(  ) ); break;
				case 6: c.DropItem( new Taint(  ) ); break;
			}

			if ( Utility.RandomDouble() <= 0.05 )
				c.DropItem( new ShieldOfSalvation() );

			if ( Utility.RandomDouble() <= 0.99 ) //.05
			{
				int lvl = 6;
				TreasureMap map = new TreasureMap( lvl, Map.Felucca );
				Point2D loc = map.ChestLocation;
				if ( loc.X >= 5116 && loc.X <= 6144 && loc.Y >= 2304 && loc.Y <= 4095 )  //Lost Lands Area
					map.ChestMap = Map.Felucca;
				else
					map.ChestMap = Map.Trammel;
				c.DropItem( map );
			}
 
			switch ( Utility.Random( 120 )) 
			{ 
			//	case 0: 				c.DropItem( new PowerScroll( SkillName.Blacksmith, 120 ) ); break;  
			//	case 1: case 2: 			c.DropItem( new PowerScroll( SkillName.Blacksmith, 115 ) ); break; 
			//	case 3: case 4: case 5: 		c.DropItem( new PowerScroll( SkillName.Blacksmith, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	c.DropItem( new PowerScroll( SkillName.Blacksmith, 105 ) ); break; 
  
			//	case 10: 				c.DropItem( new PowerScroll( SkillName.Tailoring, 120 ) ); break;  
			//	case 11: case 12: 			c.DropItem( new PowerScroll( SkillName.Tailoring, 115 ) ); break; 
			//	case 13: case 14: case 15: 		c.DropItem( new PowerScroll( SkillName.Tailoring, 110 ) ); break; 
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
 
			switch ( Utility.Random( 100 )) 
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

	/*	public void Polymorph( Mobile m )
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

				m.BodyMod = Utility.RandomList( 94, 51 );//, 261, 260 );
				m.HueMod = Utility.Random( 2, 100 );//SlimeHue();
				m.SendMessage (68, "Now you are a puddle of gooo");

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 1.0 ) )
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
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
			}
		} */

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

			case 5:
				SpawnImp( from );
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

		public void SpawnImp( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int newImp = Utility.RandomMinMax( 1, 1 ); //3 6

			for ( int i = 0; i < newImp; ++i )
			{
				Changeling imp = new Changeling();
				imp.BodyValue = 74;
				imp.Hue = 33;
				imp.Name = "Imp Devil";

				imp.Team = this.Team;

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 6 ) - 1;
					int y = Y + Utility.Random( 6 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				imp.MoveToWorld( loc, map );
				imp.Combatant = target;
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
					int z = 19;
					int x = 2117;
					int y = 581;

					switch( Utility.Random( 8 ) )
					{
						case 0: x = 2136; y = 598; break;
						case 1: x = 2147; y = 567; break;
						case 2: x = 2054; y = 572; break;
						case 3: x = 2077; y = 542; break;
						case 4: x = 2111; y = 539; break;
						case 5: x = 2146; y = 531; break;
						case 6: x = 2064; y = 604; break;
						case 7: x = 2136; y = 569; break;
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
				scalar = 10; // 10x
		}


	/*	public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;
			else
				return 0x579; //Notice sound.

			return base.GetAngerSound();
		}
		public override int GetDeathSound()
		{
			return 0x576;
		}
		public override int GetAttackSound()
		{
			return 0x577;
		}
		public override int GetIdleSound()
		{
			return 0x578;
		}
		public override int GetHurtSound()
		{
			return 0x57A;
		} */

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

		public Beelzabub( Serial serial ) : base( serial )
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