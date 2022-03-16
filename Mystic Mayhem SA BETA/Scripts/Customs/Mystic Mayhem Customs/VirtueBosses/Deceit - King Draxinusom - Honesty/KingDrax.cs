using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;
using Server.Targeting;
using Server.Spells;
using Server.Misc;
using Server.Network;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Spells.Fifth;
using Server.Spells.Seventh;

namespace Server.Mobiles
{
	[CorpseName( "the remains of king draxinusom" )]
	public class KingDrax : BaseSpecialCreature
	{
		private DateTime m_NextWall = DateTime.Now + TimeSpan.FromSeconds(20);
		
		private DateTime m_Delay;
		private DateTime m_DelayOne;

		[Constructable]
		public KingDrax() : base( AIType.AI_NecroMage, FightMode.Weakest, 10, 1, 0.1, 0.4 )
		{
			Name = "King Draxinusom";
			Title = "The Fallen Paladin";
			Body = 400;
			Hue = 0x83EC;

			SetStr( 305, 425 );
			SetDex( 72, 150 );
			SetInt( 505, 5000 );

			SetHits( 50000 );
			SetStam( 102, 300 );

			SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 120.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.Meditation, 120.0 );
            SetSkill( SkillName.Necromancy, 120.0, 300.0 );
            SetSkill (SkillName.SpiritSpeak, 120.0, 300.0 );
			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 200.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 30;
			Female = false;

			Item shroud = new HoodedShroudOfShadows();

			shroud.Movable = false;
			shroud.Hue = 1194;

			AddItem( shroud );

			Scimitar weapon = new Scimitar();

			weapon.Skill = SkillName.Wrestling;
			weapon.Hue = 2419;
			weapon.Movable = false;

			AddItem( weapon );
			
			
			

			//new SkeletalMount().Rider = this;
			AddItem( new VirtualMountItem( this ) );
			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
			
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
			c.DropItem( new HonestyStone() );
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
		
		public override void OnThink()
        {
            if (Alive && !Blessed && Map != Map.Internal && Map != null && Combatant != null)
            {
                /*if (m_NextAbility <= DateTime.Now )
                {
                    switch (Utility.Random(3))
                    {
                        case 1: DoBlizzard();
                            break;
                        case 2: DoSummon();
                            break;
                        case 0: DoIceNova();
                            break;
                    }
                    m_NextAbility = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(11) + 12);
                }*/
                if (m_NextWall <= DateTime.Now)
                {
                    if (Utility.RandomBool())
                    {
                        DoCircleOfIce();
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(120);
                    }
                    else
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(60);
                }
            }
        }
        
        public void Polymorph( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;

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

				m.BodyMod = 56;
				m.HueMod = 0;

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 3.0 ) )
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
		}
		
		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;
			if ( 0.6 >= Utility.RandomDouble() ) // 60% chance to polymorph attacker into a ratman
				Polymorph( target );
		}
		
		
        
        #region CircleOfIce

        public void DoCircleOfIce()
        {
            for (int x = -3; x <= 3; ++x)
            {
                for (int y = -3; y <= 3; ++y)
                {
                    double dist = Math.Sqrt(x * x + y * y);

                    if (dist > 2 && dist < 4)
                    {
                        Point3D loc = new Point3D(X + x, Y + y, Z);
                        if (Map.CanFit(loc, 0, true))
                        {
                            Item item = new InternalItem();
                            item.MoveToWorld(loc, Map);
                            Effects.SendLocationParticles(EffectItem.Create(loc, Map, EffectItem.DefaultDuration), 0x376A, 9, 10, 5029);
                        }
                    }
                }
            }
        }

        [DispellableField]
        private class InternalItem : Item
        {            
            public override bool BlocksFit { get { return true; } }
            private Timer m_Timer;

            public InternalItem()
                : base(0x08E2)
            {
                Movable = false;
                ItemID = Utility.RandomList(3792, 3790, 7400, 7401, 7394, 7601, 4651, 7583, 7395, 7402);
                Name = "A Desecrated Corpse";
                Hue = 0;
                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(30.0));
                m_Timer.Start();
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }
            
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)1); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                switch (version)
                {
                    case 1:
                        {
                            break;
                        }
                    case 0:
                        {
                            break;
                        }
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }
        }

        private class InternalTimer : Timer
        {
            private InternalItem m_Item;

            public InternalTimer(InternalItem item, TimeSpan duration)
                : base(duration)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
        #endregion
		
		public override void OnActionCombat()
		{
			if ( Combatant == null || Frozen || Paralyzed || Map == null || Map == Map.Internal )
				base.OnActionCombat();
			else if ( DateTime.Now > m_Delay )
			{
				double chance = Utility.RandomDouble();

				{
				 if ( DateTime.Now > m_DelayOne && chance < 0.3 ) // 10%
				
				Ability.SoulDrain( this );
					m_DelayOne = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}

				m_Delay = DateTime.Now + TimeSpan.FromSeconds( 30 );
			}

			base.OnActionCombat();
		}

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			if ( mount is Mobile )
				((Mobile)mount).Delete();
		
			
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
			
			
		
		
		

		private bool m_SpeedBoost;

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			CheckSpeedBoost();
			base.OnDamage( amount, from, willKill );
		}

        private const double SpeedBoostScalar = 10;
		
		private void CheckSpeedBoost()
		{
			if( Hits < (HitsMax / 4 ) )
			{
				if( !m_SpeedBoost )
				{
					ActiveSpeed /= SpeedBoostScalar;
					PassiveSpeed /= SpeedBoostScalar;
                    CurrentSpeed /= SpeedBoostScalar;
					m_SpeedBoost = true;
				}				
			}
			else if( m_SpeedBoost )
			{
					ActiveSpeed *= SpeedBoostScalar;
					PassiveSpeed *= SpeedBoostScalar;
                    CurrentSpeed *= SpeedBoostScalar;
					m_SpeedBoost = false;
			}
		}
 
		public void ChangeCombatant()
		{
			ForceReacquire();
			BeginFlee( TimeSpan.FromSeconds( 2.5 ) );
		}

		private class VirtualMount : IMount
		{
			private VirtualMountItem m_Item;

			public Mobile Rider
			{
				get { return m_Item.Rider; }
				set { }
			}

			public VirtualMount( VirtualMountItem item )
			{
				m_Item = item;
			}

			public virtual void OnRiderDamaged( int amount, Mobile from, bool willKill )
			{
			}
		}

		private class VirtualMountItem : Item, IMountItem
		{
			private Mobile m_Rider;
			private VirtualMount m_Mount;

			public Mobile Rider { get { return m_Rider; } }

			public VirtualMountItem( Mobile mob )
				: base( 0x3EBB )
			{
				Layer = Layer.Mount;

				Movable = false;
				Hue = 1175;

				m_Rider = mob;
				m_Mount = new VirtualMount( this );
			}

			public IMount Mount
			{
				get { return m_Mount; }
			}

			public VirtualMountItem( Serial serial )
				: base( serial )
			{
				m_Mount = new VirtualMount( this );
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int)0 ); // version

				writer.Write( (Mobile)m_Rider );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();

				m_Rider = reader.ReadMobile();

				if( m_Rider == null )
					Delete();
			}
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		
		
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool CanAnimateDead{ get{ return true; } }
		public override BaseCreature Animates{ get{ return new RottingCorpse(); } }
		public override bool DoesMultiFirebreathing { get { return true; } }
        public override double MultiFirebreathingChance { get { return 0.4; } }
        public override int BreathDamagePercent { get { return 80; } }
        public override int BreathMaxTargets { get { return 5; } }
        public override int BreathMaxRange { get { return 5; } }
       // public override bool DoesTeleporting { get { return true; } }
		//public override double TeleportingChance { get { return 0.10; } }
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

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.5 >= Utility.RandomDouble() ) // 10% chance to drop or throw an unholy bone
				AddUnholyBone( defender, 0.25 );
				
			DoSpecialAbility( defender );
				
			CheckSpeedBoost();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.5 >= Utility.RandomDouble() ) // 10% chance to drop or throw an unholy bone
				AddUnholyBone( attacker, 0.25 );
				
			DoSpecialAbility( attacker );
		}

		public override void AlterDamageScalarFrom( Mobile caster, ref double scalar )
		{
			base.AlterDamageScalarFrom( caster, ref scalar );

			if ( 0.5 >= Utility.RandomDouble() ) // 10% chance to throw an unholy bone
				AddUnholyBone( caster, 1.0 );
		}

		public void AddUnholyBone( Mobile target, double chanceToThrow )
		{
			if( this.Map == null )
				return;

			if ( chanceToThrow >= Utility.RandomDouble() )
			{
				Direction = GetDirectionTo( target );
				MovingEffect( target, 0xF7E, 10, 1, true, false, 0x496, 0 );
				new DelayTimer( this, target ).Start();
			}
			else
			{
				new UnholyBone().MoveToWorld( Location, Map );
			}
		}

		private class DelayTimer : Timer
		{
			private Mobile m_Mobile;
			private Mobile m_Target;

			public DelayTimer( Mobile m, Mobile target ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_Target = target;
			}

			protected override void OnTick()
			{
				if ( m_Mobile.CanBeHarmful( m_Target ) )
				{
					m_Mobile.DoHarmful( m_Target );
					AOS.Damage( m_Target, m_Mobile, Utility.RandomMinMax( 10, 20 ), 100, 0, 0, 0, 0 );
					new UnholyBone().MoveToWorld( m_Target.Location, m_Target.Map );
				}
			}
		}
		
		

		public KingDrax( Serial serial ) : base( serial )
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