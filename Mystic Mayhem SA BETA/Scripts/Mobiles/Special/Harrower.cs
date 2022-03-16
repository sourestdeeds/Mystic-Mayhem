using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class Harrower : BaseCreature
	{
//Jon
		private DateTime m_Delay;
		private DateTime m_DelayOne;
		private DateTime m_DelayTwo;
		private DateTime m_DelayThree;
		private DateTime m_DelayFour;
//Jon		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
     	public DateTime m_NextTalk;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 6 ))		   
				{
					case 0: Say("Pathetic Mortals!"); break;
					case 1: Say("You cannot comprehend my power!"); break;
					case 2: Say("Sweet Death, embrace mine enemies!"); break;
					case 3: Say("You shall all face my wrath!"); break;
					case 4: Say("Behold true terror!"); break;
					case 5: Say("*laughs*"); break;
				};
		
			}
		}

		public Type[] UniqueArtifacts{ get{ return new Type[] { 
            typeof( AcidProofRobe ) }; } }

		public Type[] SharedArtifacts{ get{ return new Type[] { 
            typeof( TheRobeOfBritanniaAri ) }; } }

		public Type[] DecorationArtifacts{ get { return new Type[] {
            typeof( SkullPole ),
            typeof( EvilIdol ),
            typeof( Pier ),
            typeof( SmallRockWater ),
            typeof( SmallRocksWater ) }; } }

        public MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { }; } }

		private bool m_TrueForm;
		private Item m_GateItem;
		private List<HarrowerTentacles> m_Tentacles;
		private Timer m_Timer;

		private class SpawnEntry
		{
			public Point3D m_Location;
			public Point3D m_Entrance;

			public SpawnEntry( Point3D loc, Point3D ent )
			{
				m_Location = loc;
				m_Entrance = ent;
			}
		}

		private static SpawnEntry[] m_Entries = new SpawnEntry[]
			{
				new SpawnEntry( new Point3D( 5242, 945, -40 ), new Point3D( 1176, 2638, 0 ) ),	// Destard
				new SpawnEntry( new Point3D( 5225, 798, 0 ), new Point3D( 1176, 2638, 0 ) ),	// Destard
				new SpawnEntry( new Point3D( 5556, 886, 30 ), new Point3D( 1298, 1080, 0 ) ),	// Despise
				new SpawnEntry( new Point3D( 5187, 615, 0 ), new Point3D( 4111, 432, 5 ) ),		// Deceit
				new SpawnEntry( new Point3D( 5319, 583, 0 ), new Point3D( 4111, 432, 5 ) ),		// Deceit
				new SpawnEntry( new Point3D( 5713, 1334, -1 ), new Point3D( 2923, 3407, 8 ) ),	// Fire
				new SpawnEntry( new Point3D( 5860, 1460, -2 ), new Point3D( 2923, 3407, 8 ) ),	// Fire
				new SpawnEntry( new Point3D( 5328, 1620, 0 ), new Point3D( 5451, 3143, -60 ) ),	// Terathan Keep
				new SpawnEntry( new Point3D( 5690, 538, 0 ), new Point3D( 2042, 224, 14 ) ),	// Wrong
				new SpawnEntry( new Point3D( 5609, 195, 0 ), new Point3D( 514, 1561, 0 ) ),		// Shame
				new SpawnEntry( new Point3D( 5475, 187, 0 ), new Point3D( 514, 1561, 0 ) ),		// Shame
				new SpawnEntry( new Point3D( 6085, 179, 0 ), new Point3D( 4721, 3822, 0 ) ),	// Hythloth
				new SpawnEntry( new Point3D( 6084, 66, 0 ), new Point3D( 4721, 3822, 0 ) ),		// Hythloth
				new SpawnEntry( new Point3D( 5499, 2003, 0 ), new Point3D( 2499, 919, 0 ) ),	// Covetous
				new SpawnEntry( new Point3D( 5579, 1858, 0 ), new Point3D( 2499, 919, 0 ) )		// Covetous
			};

		private static ArrayList m_Instances = new ArrayList();

		public static ArrayList Instances{ get{ return m_Instances; } }

		public static Harrower Spawn( Point3D platLoc, Map platMap )
		{
			if ( m_Instances.Count > 0 )
				return null;

			SpawnEntry entry = m_Entries[Utility.Random( m_Entries.Length )];

			Harrower harrower = new Harrower();

			harrower.MoveToWorld( entry.m_Location, Map.Felucca );

			harrower.m_GateItem = new HarrowerGate( harrower, platLoc, platMap, entry.m_Entrance, Map.Felucca );

			return harrower;
		}

		public static bool CanSpawn
		{
			get
			{
				return ( m_Instances.Count == 0 );
			}
		}

		[Constructable]
		public Harrower() : base( AIType.AI_Mage, FightMode.Closest, 18, 1, 0.2, 0.4 )
		{
			m_Instances.Add( this );

			Name = "the harrower";
			BodyValue = 146;

			SetStr( 900, 1000 );
			SetDex( 125, 500 );
			SetInt( 1000, 2500 );

			Fame = 225000;
			Karma = -225000;

			VirtualArmor = 60;

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 60, 80 );
			SetResistance( ResistanceType.Energy, 60, 80 );

			SetSkill( SkillName.Wrestling, 90.1, 200.0 );
			SetSkill( SkillName.Tactics, 90.2, 200.0 );
			SetSkill( SkillName.MagicResist, 120.2, 500.0 );
			SetSkill( SkillName.Magery, 120.0, 200 );
			SetSkill( SkillName.EvalInt, 120.0, 200 );
			SetSkill( SkillName.Meditation, 120.0, 200 );

			m_Tentacles = new List<HarrowerTentacles>();

			m_Timer = new TeleportTimer( this );
			m_Timer.Start();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosHarrower, 10 );
		
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
			c.DropItem( new PSBook() );
			c.DropItem( new HandGrenade() );
		
		if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 32 ) )
				{
					case 0: c.DropItem( new BraceletOfHealth() ); break;
					case 1: c.DropItem( new OrnamentOfTheMagician() ); break;
					case 2: c.DropItem( new RingOfTheElements() ); break;
					case 3: c.DropItem( new RingOfTheVile() ); break;
					case 4: c.DropItem( new Aegis() ); break;
					case 5: c.DropItem( new LegacyOfTheDreadLord() ); break;
					case 6: c.DropItem( new TheDragonSlayer() ); break;
					case 7: c.DropItem( new ArmorOfFortune() ); break;
					case 8: c.DropItem( new GauntletsOfNobility() ); break;
					case 9: c.DropItem( new HelmOfInsight() ); break;
					case 10: c.DropItem( new HolyKnightsBreastplate() ); break;
					case 11: c.DropItem( new JackalsCollar() ); break;
					case 12: c.DropItem( new LeggingsOfBane() ); break;
					case 13: c.DropItem( new MidnightBracers() ); break;
					case 14: c.DropItem( new OrnateCrownOfTheHarrower() ); break;
					case 15: c.DropItem( new ShadowDancerLeggings() ); break;
					case 16: c.DropItem( new TunicOfFire() ); break;
					case 17: c.DropItem( new VoiceOfTheFallenKing() ); break;
					case 18: c.DropItem( new ArcaneShield() ); break;
					case 19: c.DropItem( new AxeOfTheHeavens() ); break;
					case 20: c.DropItem( new BladeOfInsanity() ); break;
					case 21: c.DropItem( new BoneCrusher() ); break;
					case 22: c.DropItem( new BreathOfTheDead() ); break;
					case 23: c.DropItem( new Frostbringer() ); break;
					case 24: c.DropItem( new SerpentsFang() ); break;
					case 25: c.DropItem( new StaffOfTheMagi() ); break;
					case 26: c.DropItem( new TheBeserkersMaul() ); break;
					case 27: c.DropItem( new TheDryadBow() ); break;
					case 28: c.DropItem( new DivineCountenance() ); break;
					case 29: c.DropItem( new HatOfTheMagi() ); break;
					case 30: c.DropItem( new HuntersHeaddress() ); break;
					case 31: c.DropItem( new SpiritOfTheTotem() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new ClothingBlessDeed() ); break;
					case 1: c.DropItem( new ItemBlessDeed() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new ClothingBlessDeed() ); break;
					case 1: c.DropItem( new ItemBlessDeed() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new ClothingBlessDeed() ); break;
					case 1: c.DropItem( new ItemBlessDeed() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new ClothingBlessDeed() ); break;
					case 1: c.DropItem( new ItemBlessDeed() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: c.DropItem( new RangerArms() ); break;
					case 1: c.DropItem( new RangerChest() ); break;
					case 2: c.DropItem( new RangerGloves() ); break;
					case 3: c.DropItem( new RangerGorget() ); break;
					case 4: c.DropItem( new RangerLegs() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: c.DropItem( new PhoenixSleeves() ); break;
					case 1: c.DropItem( new PhoenixChest() ); break;
					case 2: c.DropItem( new PhoenixGloves() ); break;
					case 3: c.DropItem( new PhoenixGorget() ); break;
					case 4: c.DropItem( new PhoenixLegs() ); break;
					case 5: c.DropItem( new PhoenixHelm() ); break;
					
				}
			}
			
			if ( Utility.RandomDouble() < 0.4 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new EtoileBleue() ); break;
					case 1: c.DropItem( new NovoBleue() ); break;
				}
			}
			
		}


//Jon
		public override void OnActionCombat()
		{
			if ( Combatant == null || Frozen || Paralyzed || Map == null || Map == Map.Internal )
				base.OnActionCombat();
			else if ( DateTime.Now > m_Delay )
			{
				double chance = Utility.RandomDouble();

				if ( DateTime.Now > m_DelayOne && chance < 0.5 ) // 50%
				{
					// It looked like it delt 67 damage, presuming 70% fire res thats about 223 damage delt before resistance.
					Ability.MultiFireball( this, Combatant, 223 );
					m_DelayOne = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				else if ( DateTime.Now > m_DelayTwo && chance < 0.8 ) // 30%
				{
					Ability.FlameWave( this );
					m_DelayTwo = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				else if ( DateTime.Now > m_DelayThree && chance < 0.9 ) // 10%
				{
					Ability.FlameCross( this );
					m_DelayThree = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}
				else if ( DateTime.Now > m_DelayFour && chance < 0.9 ) // 10%
				{
					Ability.SoulDrain( this );
					m_DelayFour = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}

				m_Delay = DateTime.Now + TimeSpan.FromSeconds( 5 );
			}

			base.OnActionCombat();
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		private static readonly double[] m_Offsets = new double[]
			{
				Math.Cos( 000.0 / 180.0 * Math.PI ), Math.Sin( 000.0 / 180.0 * Math.PI ),
				Math.Cos( 040.0 / 180.0 * Math.PI ), Math.Sin( 040.0 / 180.0 * Math.PI ),
				Math.Cos( 080.0 / 180.0 * Math.PI ), Math.Sin( 080.0 / 180.0 * Math.PI ),
				Math.Cos( 120.0 / 180.0 * Math.PI ), Math.Sin( 120.0 / 180.0 * Math.PI ),
				Math.Cos( 160.0 / 180.0 * Math.PI ), Math.Sin( 160.0 / 180.0 * Math.PI ),
				Math.Cos( 200.0 / 180.0 * Math.PI ), Math.Sin( 200.0 / 180.0 * Math.PI ),
				Math.Cos( 240.0 / 180.0 * Math.PI ), Math.Sin( 240.0 / 180.0 * Math.PI ),
				Math.Cos( 280.0 / 180.0 * Math.PI ), Math.Sin( 280.0 / 180.0 * Math.PI ),
				Math.Cos( 320.0 / 180.0 * Math.PI ), Math.Sin( 320.0 / 180.0 * Math.PI ),
			};

		public void Morph()
		{
			if ( m_TrueForm )
				return;

			m_TrueForm = true;

			Name = "the true harrower";
			BodyValue = 780;
			Hue = 0x497;

			Hits = HitsMax;
			Stam = StamMax;
			Mana = ManaMax;

			ProcessDelta();

			Say( 1049499 ); // Behold my true form!

			Map map = this.Map;

			if ( map != null )
			{
				for ( int i = 0; i < m_Offsets.Length; i += 2 )
				{
					double rx = m_Offsets[i];
					double ry = m_Offsets[i + 1];

					int dist = 0;
					bool ok = false;
					int x = 0, y = 0, z = 0;

					while ( !ok && dist < 10 )
					{
						int rdist = 10 + dist;

						x = this.X + (int)(rx * rdist);
						y = this.Y + (int)(ry * rdist);
						z = map.GetAverageZ( x, y );

						if ( !(ok = map.CanFit( x, y, this.Z, 16, false, false ) ) )
							ok = map.CanFit( x, y, z, 16, false, false );

						if ( dist >= 0 )
							dist = -(dist + 1);
						else
							dist = -(dist - 1);
					}

					if ( !ok )
						continue;

					HarrowerTentacles spawn = new HarrowerTentacles( this );

					spawn.Team = this.Team;

					spawn.MoveToWorld( new Point3D( x, y, z ), map );

					m_Tentacles.Add( spawn );
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax{ get{ return m_TrueForm ? 250000 : 60000; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public override int ManaMax{ get{ return 5000; } }

		public Harrower( Serial serial ) : base( serial )
		{
			m_Instances.Add( this );
		}

		public override void OnAfterDelete()
		{
			m_Instances.Remove( this );

			base.OnAfterDelete();
		}

		public override bool DisallowAllMoves{ get{ return m_TrueForm; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_TrueForm );
			writer.Write( m_GateItem );
			writer.WriteMobileList<HarrowerTentacles>( m_Tentacles );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TrueForm = reader.ReadBool();
					m_GateItem = reader.ReadItem();
					m_Tentacles = reader.ReadStrongMobileList<HarrowerTentacles>();

					m_Timer = new TeleportTimer( this );
					m_Timer.Start();

					break;
				}
			}
		}

		public void GivePowerScrolls()
		{
			List<Mobile> toGive = new List<Mobile>();
			List<DamageStore> rights = BaseCreature.GetLootingRights( this.DamageEntries, this.HitsMax );

			for ( int i = rights.Count - 1; i >= 0; --i )
			{
				DamageStore ds = rights[i];

				if ( ds.m_HasRight )
					toGive.Add( ds.m_Mobile );
			}

			if ( toGive.Count == 0 )
				return;

			// Randomize
			for ( int i = 0; i < toGive.Count; ++i )
			{
				int rand = Utility.Random( toGive.Count );
				Mobile hold = toGive[i];
				toGive[i] = toGive[rand];
				toGive[rand] = hold;
			}

			for ( int i = 0; i < 16; ++i )
			{
				int level;
				double random = Utility.RandomDouble();

				if ( 0.1 >= random )
					level = 25;
				else if ( 0.25 >= random )
					level = 20;
				else if ( 0.45 >= random )
					level = 15;
				else if ( 0.70 >= random )
					level = 10;
				else
					level = 5;

				Mobile m = toGive[i % toGive.Count];

				m.SendLocalizedMessage( 1049524 ); // You have received a scroll of power!
				m.AddToBackpack( new StatCapScroll( 225 + level ) );

				if ( m is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)m;

					for ( int j = 0; j < pm.JusticeProtectors.Count; ++j )
					{
						Mobile prot = (Mobile)pm.JusticeProtectors[j];

						if ( prot.Map != m.Map || prot.Kills >= 5 || prot.Criminal || !JusticeVirtue.CheckMapRegion( m, prot ) )
							continue;

						int chance = 0;

						switch ( VirtueHelper.GetLevel( prot, VirtueName.Justice ) )
						{
							case VirtueLevel.Seeker: chance = 60; break;
							case VirtueLevel.Follower: chance = 80; break;
							case VirtueLevel.Knight: chance = 100; break;
						}

						if ( chance > Utility.Random( 100 ) )
						{
							prot.SendLocalizedMessage( 1049368 ); // You have been rewarded for your dedication to Justice!
							prot.AddToBackpack( new StatCapScroll( 225 + level ) );
						}
					}
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( m_TrueForm )
			{
				List<DamageStore> rights = BaseCreature.GetLootingRights( this.DamageEntries, this.HitsMax );

				for ( int i = rights.Count - 1; i >= 0; --i )
				{
					DamageStore ds = rights[i];

					if ( ds.m_HasRight && ds.m_Mobile is PlayerMobile )
						PlayerMobile.ChampionTitleInfo.AwardHarrowerTitle( (PlayerMobile)ds.m_Mobile );
				}

				if ( !NoKillAwards )
				{
					GivePowerScrolls();

					Map map = this.Map;

					if ( map != null )
					{
						for ( int x = -16; x <= 16; ++x )
						{
							for ( int y = -16; y <= 16; ++y )
							{
								double dist = Math.Sqrt(x*x+y*y);

								if ( dist <= 16 )
									new GoodiesTimer( map, X + x, Y + y ).Start();
							}
						}
					}

					m_DamageEntries = new Dictionary<Mobile, int>();

					for ( int i = 0; i < m_Tentacles.Count; ++i )
					{
						Mobile m = m_Tentacles[i];

						if ( !m.Deleted )
							m.Kill();

						RegisterDamageTo( m );
					}

					m_Tentacles.Clear();

					RegisterDamageTo( this );
					AwardArtifact( GetArtifact() );

					if ( m_GateItem != null )
						m_GateItem.Delete();
				}

				return base.OnBeforeDeath();
			}
			else
			{
				Morph();
				return false;
			}
		}

		Dictionary<Mobile, int> m_DamageEntries;

		public virtual void RegisterDamageTo( Mobile m )
		{
			if( m == null )
				return;

			foreach( DamageEntry de in m.DamageEntries )
			{
				Mobile damager = de.Damager;

				Mobile master = damager.GetDamageMaster( m );

				if( master != null )
					damager = master;

				RegisterDamage( damager, de.DamageGiven );
			}
		}

		public void RegisterDamage( Mobile from, int amount )
		{
			if( from == null || !from.Player )
				return;

			if( m_DamageEntries.ContainsKey( from ) )
				m_DamageEntries[from] += amount;
			else
				m_DamageEntries.Add( from, amount );

			from.SendMessage(String.Format("Total Damage: {0}", m_DamageEntries[from]) );
		}

		public void AwardArtifact( Item artifact )
		{
			if (artifact == null )
				return;

			int totalDamage = 0;

			Dictionary<Mobile, int> validEntries = new Dictionary<Mobile, int>();

			foreach (KeyValuePair<Mobile, int> kvp in m_DamageEntries)
			{
				if( IsEligible( kvp.Key, artifact ) )
				{
					validEntries.Add( kvp.Key, kvp.Value );
					totalDamage += kvp.Value;
				}
			}

			int randomDamage = Utility.RandomMinMax( 1, totalDamage );

			totalDamage = 0;

			foreach (KeyValuePair<Mobile, int> kvp in m_DamageEntries)
			{
				totalDamage += kvp.Value;

				if( totalDamage > randomDamage )
				{
					GiveArtifact( kvp.Key, artifact );
					break;
				}
			}
		}

		public void GiveArtifact( Mobile to, Item artifact )
		{
			if ( to == null || artifact == null )
				return;

			Container pack = to.Backpack;

			if ( pack == null || !pack.TryDropItem( to, artifact, false ) )
				artifact.Delete();
			else
				to.SendLocalizedMessage( 1062317 ); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
		}

		public bool IsEligible( Mobile m, Item Artifact )
		{
			return m.Player && m.Alive && m.InRange( Location, 32 ) && m.Backpack != null && m.Backpack.CheckHold( m, Artifact, false );
		}

        public Item GetArtifact()
        {
            double random = Utility.RandomDouble();

            if (random < 0.30)
            {
                double random1 = Utility.Random(29);
                if (random1 <= 4)
                    return CreateArtifact(UniqueArtifacts);
                else if (random1 >= 5 && random1 <= 14)
                    return CreateArtifact(SharedArtifacts);
                else
                    return CreateArtifact(DecorationArtifacts);
            }
            return null;
        }

        public Item CreateArtifact(Type[] list)
        {
            if (list.Length == 0)
                return null;

            int random = Utility.Random(list.Length);

            Type type = list[random];

            Item artifact = Loot.Construct(type);

            if (artifact is MonsterStatuette && StatueTypes.Length > 0)
            {
                ((MonsterStatuette)artifact).Type = StatueTypes[Utility.Random(StatueTypes.Length)];
                ((MonsterStatuette)artifact).LootType = LootType.Regular;
            }

            return artifact;
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

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 5.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

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

				if ( 0.25 < Utility.RandomDouble() )
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

		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				Priority = TimerPriority.TwoFiftyMS;

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

				Gold g = new Gold( 750, 1250 );
				
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
	}
}