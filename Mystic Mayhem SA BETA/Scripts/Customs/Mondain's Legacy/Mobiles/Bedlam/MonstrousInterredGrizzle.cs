using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a monstrous interred grizzle corpse" )]
	public class MonstrousInterredGrizzle : BasePeerless
	{
		[Constructable]
		public MonstrousInterredGrizzle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a monstrous interred grizzle";
			Body = 0x103;			
			BaseSoundID = 589;

			SetStr( 1198, 1207 );
			SetDex( 127, 135 );
			SetInt( 595, 646 );

			SetHits( 50000 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 48, 52 );
			SetResistance( ResistanceType.Fire, 77, 82 );
			SetResistance( ResistanceType.Cold, 56, 61 );
			SetResistance( ResistanceType.Poison, 32, 40 );
			SetResistance( ResistanceType.Energy, 69, 71 );

			SetSkill( SkillName.Wrestling, 112.6, 116.9 );
			SetSkill( SkillName.Tactics, 118.5, 119.2 );
			SetSkill( SkillName.MagicResist, 120 );
			SetSkill( SkillName.Anatomy, 111.0, 111.7 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.EvalInt, 100 );
			SetSkill( SkillName.Meditation, 100 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;
			PackResources( 8 );
			//PackTalismans( 5 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			c.DropItem( new GrizzledBones() );
			
			switch ( Utility.Random( 4 ) )
			{
				case 0: c.DropItem( new TombstoneOfTheDamned() ); break;
				case 1: c.DropItem( new GlobOfMonstreousInterredGrizzle() ); break;
				case 2: c.DropItem( new MonsterousInterredGrizzleMaggots() ); break;
				case 3: c.DropItem( new GrizzledSkullCollection() ); break;
			}			
			
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
				
			if ( Utility.RandomDouble() < 0.05 )				
				c.DropItem( new GrizzledMareStatuette() );
							
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 37 ) )
				{
					case 0: c.DropItem( new AssassinChest() ); break;
					case 1: c.DropItem( new AssassinArms() ); break;
					case 2: c.DropItem( new DeathChest() );	break;		
					case 3: c.DropItem( new MyrmidonArms() ); break;
					case 4: c.DropItem( new MyrmidonLegs() ); break;
					case 5: c.DropItem( new MyrmidonGorget() ); break;
					case 6: c.DropItem( new LeafweaveGloves() ); break;
					case 7: c.DropItem( new LeafweaveLegs() ); break;
					case 8: c.DropItem( new LeafweavePauldrons() ); break;
					case 9: c.DropItem( new PaladinGloves() ); break;
					case 10: c.DropItem( new PaladinGorget() ); break;
					case 11: c.DropItem( new PaladinArms() ); break;
					case 12: c.DropItem( new HunterArms() ); break;
					case 13: c.DropItem( new HunterGloves() ); break;
					case 14: c.DropItem( new HunterLegs() ); break;
					case 15: c.DropItem( new HunterChest() ); break;
					case 16: c.DropItem( new GreymistArms() ); break;
					case 17: c.DropItem( new GreymistGloves() ); break;
					case 18: c.DropItem( new GreymistChest() ); break;
					case 19: c.DropItem( new GreymistLegs() ); break;
					case 20: c.DropItem( new AssassinGloves() ); break;
					case 21: c.DropItem( new AssassinLegs() ); break;
					case 22: c.DropItem( new Evocaricus() ); break;
					case 23: c.DropItem( new MalekisHonor() ); break;
					case 24: c.DropItem( new LeafweaveChest() ); break;
					case 25: c.DropItem( new Feathernock() ); break;
					case 26: c.DropItem( new Swiftflight() ); break;
					case 27: c.DropItem( new MyrmidonChest() ); break;
					case 28: c.DropItem( new MyrmidonCloseHelm() ); break;
					case 29: c.DropItem( new MyrmidonGloves() ); break;
					case 30: c.DropItem( new DeathGloves() );	break;
					case 31: c.DropItem( new DeathArms() );	break;	
					case 32: c.DropItem( new DeathLegs() );	break;	
					case 33: c.DropItem( new DeathBoneHelm() );	break;
					case 34: c.DropItem( new PaladinChest() ); break;
					case 35: c.DropItem( new PaladinHelm() ); break;
					case 36: c.DropItem( new PaladinLegs() ); break;
				}
			}
				
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: c.DropItem( new GrizzleGauntlets() ); break;
					case 1: c.DropItem( new GrizzleGreaves() ); break;
					case 2: c.DropItem( new GrizzleHelm() ); break;
					case 3: c.DropItem( new GrizzleTunic() ); break;
					case 4: c.DropItem( new GrizzleVambraces() ); break;
				}
			}	
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 16 ) )
				{
					case 0: c.DropItem( new ArcaneCircleScroll() ); break;
					case 1: c.DropItem( new GiftOfRenewalScroll() ); break;
					case 2: c.DropItem( new ImmolatingWeaponScroll() ); break;
					case 3: c.DropItem( new AttuneWeaponScroll() ); break;
					case 4: c.DropItem( new ThunderstormScroll() ); break;
					case 5: c.DropItem( new NatureFuryScroll() ); break;
					case 6: c.DropItem( new SummonFeyScroll() ); break;
					case 7: c.DropItem( new SummonFiendScroll() ); break;
					case 8: c.DropItem( new ReaperFormScroll() ); break;
					case 9: c.DropItem( new WildfireScroll() ); break;
					case 10: c.DropItem( new EssenceOfWindScroll() ); break;
					case 11: c.DropItem( new DryadAllureScroll() ); break;
					case 12: c.DropItem( new EtherealVoyageScroll() ); break;
					case 13: c.DropItem( new WordOfDeathScroll() ); break;
					case 14: c.DropItem( new GiftOfLifeScroll() ); break;
					case 15: c.DropItem( new ArcaneEmpowermentScroll() ); break;
				}
			}	
			
			if ( Utility.RandomDouble() < 0.02 )
			{
				switch ( Utility.Random( 21 ) )
				{
					case 0: c.DropItem( new BloodwoodSpirit() ); break;
					case 1: c.DropItem( new Boomstick() ); break;
					case 2: c.DropItem( new BrightsightLenses() ); break;
					case 3: c.DropItem( new HelmOfSwiftness() ); break;
					case 4: c.DropItem( new QuiverOfElements() ); break;
					case 5: c.DropItem( new QuiverOfRage() ); break;
					case 6: c.DropItem( new TotemOfVoid() ); break;
					case 7: c.DropItem( new WildfireBow() ); break;
					case 8: c.DropItem( new Windsong() ); break;
					case 9: c.DropItem( new AegisOfGraceNonElf() ); break;
					case 10: c.DropItem( new BladeDance() ); break;
					case 11: c.DropItem( new Bonesmasher() ); break;
					case 12: c.DropItem( new FeyLeggingsNonElf() ); break;
					case 13: c.DropItem( new FleshRipper() ); break;
					case 14: c.DropItem( new PadsOfTheCuSidhe() ); break;
					case 15: c.DropItem( new RaedsGlory() ); break;
					case 16: c.DropItem( new RighteousAnger() ); break;
					case 17: c.DropItem( new RobeOfTheEclipse() ); break;
					case 18: c.DropItem( new RobeOfTheEquinox() ); break;
					case 19: c.DropItem( new SoulSeeker() ); break;
					case 20: c.DropItem( new TalonBite() ); break;
				}
			}	
		}

		public override bool GivesMinorArtifact{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( Utility.RandomDouble() < 0.15 )
				CacophonicAttack( defender );
		}
		
		public override void OnDamage( int amount, Mobile from, bool willKill )
		{			
			if ( Utility.RandomDouble() < 0.15 )
				CacophonicAttack( from );
				
			if ( Utility.RandomDouble() < 0.3 )
				DropOoze();
			
			base.OnDamage( amount, from, willKill );
		}		
		
		public override void OnThink()
		{
			base.OnThink();
			
			if ( Combatant == null )
				return;	
				
			if ( Hits > 0.8 * HitsMax && Utility.RandomDouble() < 0.0025 )
				FireRing();
		}
		
		private static int[] m_Tiles = new int[]
		{
			-2, 0,
			2, 0,
			2, -2,
			2, 2,
			-2, -2,
			-2, 2,
			0, 2,
			1, 0,
			0, -2
		};
		
		public override void FireRing()
		{
			for ( int i = 0; i < m_Tiles.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_Tiles[ i ];
				p.Y += m_Tiles[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, Utility.RandomBool() ? 0x3E31 : 0x3E27, 100 );
			}
		}
		
		public override int GetDeathSound()	{ return 0x57F; }
		public override int GetAttackSound() { return 0x580; }
		public override int GetIdleSound() { return 0x581; }
		public override int GetAngerSound() { return 0x582; }
		public override int GetHurtSound() { return 0x583; }

		public MonstrousInterredGrizzle( Serial serial ) : base( serial )
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
		}
		
		private static Hashtable m_Table;
		
		public virtual void CacophonicAttack( Mobile to )
		{
			if ( m_Table == null )
				m_Table = new Hashtable();
		
			if ( to.Alive && to.Player && m_Table[ to ] == null )
			{
				to.Send( SpeedControl.WalkSpeed );
				to.SendLocalizedMessage( 1072069 ); // A cacophonic sound lambastes you, suppressing your ability to move.
				to.PlaySound( 0x584 );
				
				m_Table[ to ] = Timer.DelayCall( TimeSpan.FromSeconds( 30 ), new TimerStateCallback( EndCacophonic_Callback ), to );
			}
		}
		
		private void EndCacophonic_Callback( object state )
		{
			if ( state is Mobile )
				CacophonicEnd( (Mobile) state );
		}		
		
		public virtual void CacophonicEnd( Mobile from )
		{
			if ( m_Table == null )
				m_Table = new Hashtable();
				
			m_Table[ from ] = null;
				
			from.Send( SpeedControl.Disable );
		}
		
		public static bool UnderCacophonicAttack( Mobile from )
		{
			if ( m_Table == null )
				m_Table = new Hashtable();
			
			return m_Table[ from ] != null;
		}
		
		private DateTime m_NextDrop = DateTime.Now;
		
		public virtual void DropOoze()
		{
			int amount = Utility.RandomMinMax( 1, 3 );
			bool corrosive = Utility.RandomBool();
			
			for ( int i = 0; i < amount; i ++ )
			{
				Item ooze = new InfernalOoze( corrosive );				
				Point3D p = new Point3D( Location );
				
				for ( int j = 0; j < 5; j ++ )
				{
					p = GetSpawnPosition( 2 );
					bool found = false;
				
					foreach( Item item in Map.GetItemsInRange( p, 0 ) )
						if ( item is InfernalOoze )
						{
							found = true;
							break;
						}
						
					if ( !found )
						break;			
				}
				
				ooze.MoveToWorld( p, Map );
			}
			
			if ( Combatant != null )
			{
				if ( corrosive )
					Combatant.SendLocalizedMessage( 1072071 ); // A corrosive gas seeps out of your enemy's skin!
				else
					Combatant.SendLocalizedMessage( 1072072 ); // A poisonous gas seeps out of your enemy's skin!
			}
		}
	}
	
	public class InfernalOoze : Item
	{		
		private bool m_Corrosive;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Corrosive
		{
			get{ return m_Corrosive; }
			set{ m_Corrosive = value; }
		}
		
		[Constructable]
		public InfernalOoze() : this ( false )
		{
		}
		
		[Constructable]
		public InfernalOoze( bool corrosive ) : base( 0x122A )
		{
			Movable = false;
			Hue = 0x95;
			
			m_Corrosive = corrosive;			
			Timer.DelayCall( TimeSpan.FromSeconds( 30 ), new TimerCallback( Morph ) );
		}
		
		private Hashtable m_Table;
		
		public override bool OnMoveOver( Mobile m )
		{
			
			if ( m_Table == null )
				m_Table = new Hashtable();
			
			if ( ( m is BaseCreature && ((BaseCreature) m).Controlled ) || m.Player )
				m_Table[ m ] = Timer.DelayCall( TimeSpan.FromSeconds( 1 ), TimeSpan.FromSeconds( 1 ), new TimerStateCallback( Damage_Callback ), m );
			
			return base.OnMoveOver( m );
			
		}
		
		public override bool OnMoveOff( Mobile m )
		{			
			if ( m_Table == null )
				m_Table = new Hashtable();
				
			if ( m_Table[ m ] is Timer )
			{
				Timer timer = (Timer) m_Table[ m ];
				
				timer.Stop();
				
				m_Table[ m ] = null;
			}
			
			return base.OnMoveOff( m );
		}

		public InfernalOoze( Serial serial ) : base( serial )
		{
		}		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			
			writer.Write( (bool) m_Corrosive );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			m_Corrosive = reader.ReadBool();
		}		
		
		private void Damage_Callback( object state )
		{
			if ( state is Mobile )
				Damage( (Mobile) state );
		}
		
		public virtual void Damage( Mobile m )
		{			
			if ( !m.Alive )
				StopTimer( m );
			
			if ( m_Corrosive )
			{
				for ( int i = 0; i < m.Items.Count; i ++ )
				{
					IDurability item = m.Items[ i ] as IDurability;
	
					if ( item != null && Utility.RandomDouble() < 0.25 )
					{						
						if ( item.HitPoints > 10 )
							item.HitPoints -= 10;
						else
							item.HitPoints -= 1;
					}
				}
			}
			else
				AOS.Damage( m, 40, 0, 0, 0, 100, 0 );
		}		
		
		public virtual void Morph()
		{
			ItemID += 1;
			
			Timer.DelayCall( TimeSpan.FromSeconds( 5 ), new TimerCallback( Decay ) );
		}
		
		public virtual void StopTimer( Mobile m )
		{
			if ( m_Table[ m ] is Timer )
			{
				Timer timer = (Timer) m_Table[ m ];				
				timer.Stop();			
				m_Table[ m ] = null;	
			}
		}
		
		public virtual void Decay()
		{			
			if ( m_Table == null )
				m_Table = new Hashtable();
				
			foreach ( DictionaryEntry entry in m_Table )
				if ( entry.Value is Timer )
					((Timer) entry.Value).Stop();
			
			m_Table.Clear();
			
			Delete();
		}
	}
}
