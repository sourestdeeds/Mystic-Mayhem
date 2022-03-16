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
using Server.Spells.Seventh;
using Server.Items;
using Server.Targeting;



namespace Server.Mobiles
{
	[CorpseName( "the remains of Minax" )]
	public class MinaxBoss : BaseSpecialCreature
	{
            
		private DateTime m_Delay;
		private DateTime m_DelayOne;
		private DateTime m_DelayTwo;
		private DateTime m_DelayThree;
		//private DateTime m_DelayFour;
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
     	public DateTime m_NextTalk;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 6 ))		   
				{
					case 0: Say("You dare to attack me here? Do you even KNOW whom you face? You will suffer! You will ALL suffer!");
							PlaySound(1066); 
							break;
					case 1: Say("You know nothing of me! You know nothing of what I must do! You will suffer! You will all suffer!"); 
							PlaySound(1071); 
							break;
					case 2: Say("Must I be interupted at every turn? Enough of this!"); 
							PlaySound(1055);
							break;
					case 3: Say("Silence dog! You have no purpose but to die by my hand!"); 
							PlaySound(1073);
							break;
					case 4: Say("I cannot be caged! I cannot be controlled! Understand this as you die, ever pathetic, ever fools!");
							PlaySound(1094);
							break;
					case 5: Say("You bore me."); 
							PlaySound(1073);
							break;
				};
		
			}
		}

		public override Faction FactionAllegiance { get { return Minax.Instance; } }
		public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Evil; } }

		[Constructable]
		public MinaxBoss() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Minax";
			Title = "the Dark Temptress";
			Body = 401;
			Hue = 33770;
			BaseSoundID = 0x4B0;

			SetStr( 900, 1100 );
			SetDex( 100, 200 );
			SetInt( 500, 1500 );

			SetHits( 15000 );

			SetDamage( 10, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 80 );
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
			Female = true;

			DoomKiss weapon = new DoomKiss();

			weapon.Skill = SkillName.Wrestling;
			weapon.Movable = false;

			AddItem( weapon );

			FemaleStuddedChest chest = new FemaleStuddedChest();
			chest.Movable = true;
			chest.Hue = 1175;
			AddItem( chest );

			Sandals legs = new Sandals();
			legs.Movable = true;
			legs.Hue = 1175;
			AddItem( legs );

			LeatherGloves gloves = new LeatherGloves();
			gloves.Movable = true;
			gloves.Hue = 1175;
			AddItem( gloves );

			Cloak back = new Cloak();
			back.Movable = true;
			back.Hue = 1194;
			AddItem( back );

			HairItemID = 8252; // Long Hair
			HairHue = 1175;
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
		}
			/*c.DropItem( new Ruby( Utility.RandomMinMax( 16, 250 ) ) );
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
			c.DropItem( new JusticeStone() );
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
  			}*/
  			
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
				else if ( DateTime.Now > m_DelayTwo && chance < 0.3 ) // 30%
				{
					Ability.FlameWave( this );
					m_DelayTwo = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				else if ( DateTime.Now > m_DelayThree && chance < 0.3 ) // 10%
				{
					Ability.FlameCross( this );
					m_DelayThree = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}
				//else if ( DateTime.Now > m_DelayFour && chance < 0.3 ) // 10%
				//{
				//	Ability.SoulDrain( this );
				//	m_DelayFour = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				//}

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
		public override BaseCreature Animates{ get{ return new AuraOfMinax2(); } }
		public override bool DoesMultiFirebreathing { get { return true; } }
        public override double MultiFirebreathingChance { get { return 0.4; } }
        public override int BreathDamagePercent { get { return 80; } }
        public override int BreathMaxTargets { get { return 5; } }
        public override int BreathMaxRange { get { return 5; } }
        public override bool DoesTeleporting { get { return true; } }
		public override double TeleportingChance { get { return 0.10; } }
		public override bool DoesTripleBolting { get { return true; } }
		public override double TripleBoltingChance { get { return 0.10; } }
		public override bool DoesNoxStriking { get { return true; } }
        public override double NoxStrikingChance { get { return 0.10; } }
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

		

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{

			CheckSpeedBoost();
			base.OnDamage( amount, from, willKill );
						
			if ( willKill )
				Say( "Very skilled, indeed...you shall never defeat me though...I shall return!" );
				
			base.OnDamage( amount, from, willKill );				
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
			
					
			MysteriousImage2 rm = new MysteriousImage2();
			rm.Team = this.Team;
			rm.CantWalk = true;
			Effects.PlaySound(this, Map, GetDeathSound());
			Effects.SendLocationEffect( Location, Map, 0x3709, 30, 10, 0, 0 );
			rm.MoveToWorld( Location, Map );

			if ( !base.OnBeforeDeath() )
				return false;

			MagicalLockbox2 magicallockbox2 = new MagicalLockbox2();
			magicallockbox2.MoveToWorld( Location, Map );

			Effects.SendLocationEffect( Location, Map, 0x376A, 10, 1 );
			return true;

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


		public MinaxBoss( Serial serial ) : base( serial )
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