using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using Server.Factions;
using Server.Network;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Spells.Fifth;
using Server.Spells.Seventh;
using Server.Items;
using Server.Targeting;
using Server.Misc;



namespace Server.Mobiles
{
	[CorpseName( "the remains of an effigy" )]
	public class Effigy: BaseSpecialCreature
	{
		private DateTime m_NextAbility = DateTime.Now + TimeSpan.FromSeconds(30);
        //private DateTime m_NextWall = DateTime.Now + TimeSpan.FromSeconds(30);
            
		private DateTime m_Delay;
		private DateTime m_DelayOne;
		private DateTime m_DelayTwo;
		//private DateTime m_DelayFour;

		[Constructable]
		public Effigy() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "An Anti-Virtuous Effigy";
			Title = "Of Sinister Composition";
			Body = 950;
			Hue = 0;
			BaseSoundID = 0x56C;

			SetStr( 900, 1100 );
			SetDex( 100, 200 );
			SetInt( 500, 1500 );

			SetHits( 50000 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 95, 100 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 60, 80 );
			SetResistance( ResistanceType.Energy, 60, 80 );

			SetSkill( SkillName.EvalInt, 120.0, 150.0 );
			SetSkill( SkillName.Magery, 120.0, 150.0 );
			SetSkill( SkillName.Meditation, 120.0, 150.0 );
			SetSkill( SkillName.MagicResist, 120.0, 180.0 );
			SetSkill( SkillName.Necromancy, 120.0, 180.0 );
			SetSkill( SkillName.Tactics, 120.0, 150.0 );
			SetSkill( SkillName.Macing, 120.0, 150.0 );
			SetSkill( SkillName.Wrestling, 120.0, 150.0);
			SetSkill( SkillName.Swords, 120.0, 150.0);

			Fame = 75500;
			Karma = -75500;

			VirtualArmor = 30;

			
		}
		
		 public override void OnThink()
        {
            if (Alive && !Blessed && Map != Map.Internal && Map != null && Combatant != null)
            {
                if (m_NextAbility <= DateTime.Now /*&& Hits > HitsMax * 0.03 */)
                {
                    switch (Utility.Random(3))
                    {
	                    case 1: DoBlizzard();
                            break;
                            
                             case 2: DoBlizzard();
                            break;
                     
                        case 0: DoSummon();
                            break;
                       
                    }
                    m_NextAbility = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(11) + 60);
                }
                /*if (m_NextWall <= DateTime.Now)
                {
                    if (Utility.RandomBool())
                    {
                        DoCircleOfIce();
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(40);
                    }
                    else
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(5);
                }*/
            }
	}
		 #region Summon

        public void DoSummon()
        {
	        //Summon Effect
	        Effects.SendLocationEffect( new Point3D( X + 1, Y, Z + 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y, Z ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y, Z - 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z + 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z - 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 11 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 7 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 3 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z - 1 ), Map, 0x3728, 13 );
	        Effects.SendLocationEffect( new Point3D( X , Y + 1, Z ), Map, 0x374A, 15, 569, 0 );
	        Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3709, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x208);
			
            
			if (this.Map != null)
            {
                Map map = this.Map;
                int amount = Utility.RandomMinMax(1, 3);

                for (int l = 0; l < amount; ++l)
                {
                    for (int k = 0; k < 1; ++k)
                    {
                        bool validLocation = false;
                        Point3D loc = this.Location;
                        for (int j = 0; !validLocation && j < 10; ++j)
                        {
                            int x = X + Utility.Random(11) - 5;
                            int y = Y + Utility.Random(11) - 5;
                            int z = map.GetAverageZ(x, y);

                            if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                                loc = new Point3D(x, y, Z);
                            else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                                loc = new Point3D(x, y, z);
                        }
                        switch (Utility.Random(2))
                        {
                            case 0: Simulacrum effy = new Simulacrum();
                                effy.MoveToWorld(loc, map);
                                break;
                            case 1: Antagonist bad = new Antagonist();
                                bad.MoveToWorld(loc, map);
                                break;
                		}   
            		}
                }
            }
        }
        #endregion
        
        #region Blizzard

        public void DoBlizzard()
        {
            for (int i = 0; i < 30; ++i)
            {
                int x = X + Utility.Random(25) - 12;
                int y = Y + Utility.Random(25) - 12;
                int z = Map.GetAverageZ(x, y);

                Point3D loc = new Point3D(x, y, z);

                if (Map.CanFit(loc, 0, true))
                {
                    double delay = 5 * Utility.RandomDouble();
                    Timer.DelayCall(TimeSpan.FromSeconds(delay), new TimerStateCallback(BlizzardEffect_Callback), new object[]{this, loc});
                    Timer.DelayCall(TimeSpan.FromSeconds(delay + 2.5), new TimerStateCallback(BlizzardDamage_Callback), new object[] { this, loc });
                }
            }
        }

        public static void BlizzardEffect_Callback(object state)
        {
            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
            IEntity from = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z + 50), ag.Map);
            Effects.SendMovingEffect(from, to, 4534, 0, 0, false, true, 0, 0);
            Effects.PlaySound(loc, ag.Map, 0x1E5);
        }

        public static void BlizzardDamage_Callback(object state)
        {
            //Point3D loc = (Point3D)state;

            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            IPooledEnumerable eable = ag.Map.GetMobilesInRange(loc, 0);
            foreach (Mobile m in eable)
            {
                if (m.Blessed || m == null || m.Map == Map.Internal || m.Map == null || !m.Alive || !ag.CanBeHarmful(m))                
                    return;

                else if (m is PlayerMobile)
                {
                    PlayerMobile p = m as PlayerMobile;

                    if (p.AccessLevel == AccessLevel.Player)
                        AOS.Damage(p, ag, 100, 0, 0, 0, 100, 0);
                    else
                        p.SendMessage("With your godly powers you avoid the damage");
                }

                else if (m is BaseCreature)
                {
                    BaseCreature b = m as BaseCreature;

                    if (b.IsEnemy(ag))
                        AOS.Damage(b, ag, 300, 0, 0, 0, 100, 0);
                }
            }
	        eable.Free();
        }
        #endregion
        
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
						from.SendAsciiMessage( "You cannot move!" );

						from.Freeze( TimeSpan.FromSeconds( 4.0 ) );
						break;
					}
					case 2:
					{
						from.SendAsciiMessage( "You have been turned into stone!" );

						Polymorph( from );
						break;
					}
				}
		    	}
		    	catch{}
		}
		
		/*public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}*/
		
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
				m.HueMod = 2419;
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

		public override void GenerateLoot()
		{
			//AddLoot( LootPack.UltraRich, 3 );
			//AddLoot( LootPack.Meager );
			AddLoot( LootPack.SuperBoss, 10 );
		}
		
		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
		
		
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
			c.DropItem( new SacrificeStone() );
			c.DropItem( new HandGrenade() );
			
			switch ( Utility.Random( 110 )) 
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

				case 60: 				c.DropItem( new PowerScroll( SkillName.Fletching, 120 ) ); break;  
				case 61: case 62: 			c.DropItem( new PowerScroll( SkillName.Fletching, 115 ) ); break; 
				case 63: case 64: case 65: 		c.DropItem( new PowerScroll( SkillName.Fletching, 110 ) ); break; 
				case 66: case 67: case 68: case 69: 	c.DropItem( new PowerScroll( SkillName.Fletching, 105 ) ); break;

				case 70: 				c.DropItem( new PowerScroll( SkillName.Inscribe, 120 ) ); break;  
				case 71: case 72: 			c.DropItem( new PowerScroll( SkillName.Inscribe, 115 ) ); break; 
				case 73: case 74: case 75: 		c.DropItem( new PowerScroll( SkillName.Inscribe, 110 ) ); break; 
				case 76: case 77: case 78: case 79: 	c.DropItem( new PowerScroll( SkillName.Inscribe, 105 ) ); break;
  
				case 80: 				c.DropItem( new PowerScroll( SkillName.Cartography, 120 ) ); break;  
				case 81: case 82: 			c.DropItem( new PowerScroll( SkillName.Cartography, 115 ) ); break; 
				case 83: case 84: case 85: 		c.DropItem( new PowerScroll( SkillName.Cartography, 110 ) ); break; 
				case 86: case 87: case 88: case 89: 	c.DropItem( new PowerScroll( SkillName.Cartography, 105 ) ); break;

				case 90: 				c.DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 91: case 92: 			c.DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 93: case 94: case 95: 		c.DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 96: case 97: case 98: case 99: 	c.DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break;

				case 100: 				c.DropItem( new PowerScroll( SkillName.Cooking, 120 ) ); break;  
				case 101: case 102: 			c.DropItem( new PowerScroll( SkillName.Cooking, 115 ) ); break; 
				case 103: case 104: case 105: 		c.DropItem( new PowerScroll( SkillName.Cooking, 110 ) ); break; 
				case 106: case 107: case 108: case 109: c.DropItem( new PowerScroll( SkillName.Cooking, 105 ) ); break; 
      			}
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
		
		
		public override void OnActionCombat()
		{
			if ( Combatant == null || Frozen || Paralyzed || Map == null || Map == Map.Internal )
				base.OnActionCombat();
			else if ( DateTime.Now > m_Delay )
			{
				double chance = Utility.RandomDouble();

				if ( DateTime.Now > m_DelayOne && chance < 0.3 ) // 50%
				{
					// It looked like it delt 67 damage, presuming 70% fire res thats about 223 damage delt before resistance.
					Ability.MultiFireball( this, Combatant, 223 );
					m_DelayOne = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				
				else if ( DateTime.Now > m_DelayTwo && chance < 0.3 ) // 10%
				{
					Ability.SoulDrain( this );
					m_DelayTwo = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}

				m_Delay = DateTime.Now + TimeSpan.FromSeconds( 5 );
			}

			base.OnActionCombat();
		}
		
		private bool m_SpeedBoost;
		
		private const double SpeedBoostScalar = 1.2;
		
		private void CheckSpeedBoost()
		{
			if( Hits < (HitsMax / 4 ) )
			{
				if( !m_SpeedBoost )
				{
					ActiveSpeed /= SpeedBoostScalar;
					PassiveSpeed /= SpeedBoostScalar;
					m_SpeedBoost = true;
				}				
			}
			else if( m_SpeedBoost )
			{
					ActiveSpeed *= SpeedBoostScalar;
					PassiveSpeed *= SpeedBoostScalar;
					m_SpeedBoost = false;
			}
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.ML; } }
		public override bool Unprovokable{ get{ return Core.ML; } }
		public override bool Uncalmable{ get{ return Core.ML; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool CanAnimateDead{ get{ return true; } }
		public override BaseCreature Animates{ get{ return new Gzemnid2(); } }
		public override bool DoesMultiFirebreathing { get { return true; } }
        public override double MultiFirebreathingChance { get { return 0.05; } }
        public override int BreathDamagePercent { get { return 80; } }
        public override int BreathMaxTargets { get { return 5; } }
        public override int BreathMaxRange { get { return 5; } }
        public override bool DoesTeleporting { get { return true; } }
		public override double TeleportingChance { get { return 0.10; } }
		public override bool DoesTripleBolting { get { return true; } }
		public override double TripleBoltingChance { get { return 0.05; } }
		public override bool DoesNoxStriking { get { return true; } }
        public override double NoxStrikingChance { get { return 0.05; } }
        public override bool DoesEarthquaking { get { return true; } }
        public override double EarthquakingChance { get { return 0.25; } }
        public override double BreathDamageScalar{ get{ return (0.0004); } }

        public override bool DoesLifeDraining { get { return true; } }
        public override double LifeDrainingChance 
        {
            get
                {
                if( Hits < ( HitsMax / Utility.RandomMinMax( 1, 3 ) ) )
                    return 0.125;

                return 0.0;
                }
            }
      	
        public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}
		
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		

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
			

			/*AuraOfMinax rm = new AuraOfMinax();
			rm.Team = this.Team;
			rm.Combatant = this.Combatant;
			rm.NoKillAwards = true;

			if ( rm.Backpack == null )
			{
				Backpack pack = new Backpack();
				pack.Movable = false;
				rm.AddItem( pack );
			}

			for ( int i = 0; i < 2; i++ )
			{
				LootPack.FilthyRich.Generate( this, rm.Backpack, true, LootPack.GetLuckChanceForKiller( this ) );
				LootPack.FilthyRich.Generate( this, rm.Backpack, false, LootPack.GetLuckChanceForKiller( this ) );
			}

			Effects.PlaySound(this, Map, GetDeathSound());
			Effects.SendLocationEffect( Location, Map, 0x3709, 30, 10, 1175, 0 );
			rm.MoveToWorld( Location, Map );

			Delete();
			return false;*/
			
			return base.OnBeforeDeath();
		}


		public Effigy( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( m_SpeedBoost );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			switch( version )
			{
				case 1:
				{
					m_SpeedBoost = reader.ReadBool();
					break;
				}
			}
		}
	}
}