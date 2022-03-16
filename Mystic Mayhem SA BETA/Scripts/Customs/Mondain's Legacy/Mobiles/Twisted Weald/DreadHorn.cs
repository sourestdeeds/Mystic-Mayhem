using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Misc;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a dread horns corpse" )]	
	public class DreadHorn : BasePeerless
	{		
		public virtual int StrikingRange{ get{ return 12; } }
	
		[Constructable]
		public DreadHorn() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Dread Horn";
			Body = 257;
			BaseSoundID = 0xA8;

			SetStr( 812, 999 );
			SetDex( 507, 669 );
			SetInt( 1206, 1389 );

			SetHits( 50000 );
			SetStam( 507, 669 );
			SetMana( 1206, 1389 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 40, 53 );
			SetResistance( ResistanceType.Fire, 50, 63 );
			SetResistance( ResistanceType.Cold, 50, 62 );
			SetResistance( ResistanceType.Poison, 67, 73 );
			SetResistance( ResistanceType.Energy, 60, 73 );

			SetSkill( SkillName.Wrestling, 90.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.MagicResist, 110.0 );
			SetSkill( SkillName.Poisoning, 120.0 );
			SetSkill( SkillName.Magery, 110.0 );
			SetSkill( SkillName.EvalInt, 110.0 );
			SetSkill( SkillName.Meditation, 110.0 );

            Fame = 15000;  //Guessing here
            Karma = -15000;  //Guessing here

            PackArcaneScroll(1, 3);
			
			PackResources( 8 );
			//PackTalismans( 5 );	
			
			m_Change = DateTime.Now;
			m_Stomp = DateTime.Now;
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}	
		
		public override void OnThink()
		{
			base.OnThink();
			
			if ( Combatant != null )
			{
				if ( m_Change < DateTime.Now && Utility.RandomDouble() < 0.2 )
					ChangeOpponent();					
				
				if ( m_Stomp < DateTime.Now && Utility.RandomDouble() < 0.1 )
					HoofStomp();
			}
				// exit ilsh 1313, 936, 32
		}
		
		public override void Damage( int amount, Mobile from )
		{
			base.Damage( amount, from );
						
			if ( Combatant == null || Hits > HitsMax * 0.05 || Utility.RandomDouble() > 0.1 )
				return;	
							
			new InvisibilitySpell( this, null ).Cast();
			
			Target target = Target;
			
			if ( target != null )
				target.Invoke( this, this );
				
			Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerCallback( Teleport ) );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			c.DropItem( new DreadHornMane() );	
			
			if ( Utility.RandomDouble() < 0.6 )
				c.DropItem( new TaintedMushroom() );
			
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
				
			if ( Utility.RandomDouble() < 0.5 )
				c.DropItem( new MangledHeadOfDreadhorn() );
				
			if ( Utility.RandomDouble() < 0.5 )
				c.DropItem( new HornOfTheDreadhorn() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new PristineDreadHorn() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new DreadFlute() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new DreadsRevenge() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new TangleApron() );
				
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
			
			if ( Utility.RandomDouble() < 0.03 )
			{
				switch ( Utility.Random( 21 ) )
				{
					case 0: c.DropItem( new BloodwoodSpirit() ); break;
					case 1: c.DropItem( new Boomstick() ); break;
					case 2: c.DropItem( new BrightsightLenses() ); break;
					case 3: c.DropItem( new HelmOfSwiftnessNonElf() ); break;
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
		
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Regular; } } 
		
		public override int Meat{ get{ return 5; } }
		public override MeatType MeatType{ get{ return MeatType.Ribs; } }
		
		public override bool GivesMinorArtifact{ get{ return true; } }
        public override bool Unprovokable{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }		
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public DreadHorn( Serial serial ) : base( serial )
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
			
			m_Change = DateTime.Now;
			m_Stomp = DateTime.Now;
		}
		
		private DateTime m_Change;
		private DateTime m_Stomp;
		
		public void Teleport()
		{										
			// 20 tries to teleport
			for ( int tries = 0; tries < 20; tries ++ )
			{
				int x = Utility.RandomMinMax( 5, 7 ); 
				int y = Utility.RandomMinMax( 5, 7 );
				
				if ( Utility.RandomBool() )
					x *= -1;
					
				if ( Utility.RandomBool() )
					y *= -1;
				
				Point3D p = new Point3D( X + x, Y + y, 0 );
				IPoint3D po = new LandTarget( p, Map ) as IPoint3D;
				
				if ( po == null )
					continue;
					
				SpellHelper.GetSurfaceTop( ref po );

				if ( InRange( p, 12 ) && InLOS( p ) && Map.CanSpawnMobile( po.X, po.Y, po.Z ) )
				{					
					
					Point3D from = Location;
					Point3D to = new Point3D( po );
	
					Location = to;
					ProcessDelta();
					
					FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
					PlaySound( 0x1FE );
										
					break;					
				}
			}		
			
			RevealingAction();
		}
		
		public void ChangeOpponent()
		{
			Mobile agro, best = null;
			double distance, random = Utility.RandomDouble();
			
			if ( random < 0.75 )
			{			
				// find random target relatively close
				for ( int i = 0; i < Aggressors.Count && best == null; i ++ )
				{
					agro = Validate( Aggressors[ i ].Attacker );
					
					if ( agro == null )
						continue;				
				
					distance = StrikingRange - GetDistanceToSqrt( agro );
					
					if ( distance > 0 && distance < StrikingRange - 2 && InLOS( agro.Location ) )
					{
						distance /= StrikingRange;
						
						if ( random < distance )
							best = agro;
					}
				}		
			}	
			else
			{
				int damage = 0;
				
				// find a player who dealt most damage
				for ( int i = 0; i < DamageEntries.Count; i ++ )
				{
					agro = Validate( DamageEntries[ i ].Damager );
					
					if ( agro == null )
						continue;
					
					distance = GetDistanceToSqrt( agro );
						
					if ( distance < StrikingRange && DamageEntries[ i ].DamageGiven > damage && InLOS( agro.Location ) )
					{
						best = agro;
						damage = DamageEntries[ i ].DamageGiven;
					}
				}
			}
			
			if ( best != null )
			{
				// teleport
				best.Location = BasePeerless.GetSpawnPosition( Location, Map, 1 );
				best.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				best.PlaySound( 0x1FE );
				
				Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerCallback( delegate()
				{
					// poison
					best.ApplyPoison( this, HitPoison );
					best.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
					best.PlaySound( 0x474 );
				} ) );
				
				m_Change = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 5, 10 ) );
			}
		}
		
		public void HoofStomp()
		{		
			foreach ( Mobile m in GetMobilesInRange( StrikingRange ) )
			{
				Mobile valid = Validate( m );
				
				if ( valid != null && Affect( valid ) )
					valid.SendLocalizedMessage( 1075081 ); // *Dreadhorn’s eyes light up, his mouth almost a grin, as he slams one hoof to the ground!*
			}		
			
			// earthquake
			PlaySound( 0x2F3 );
				
			Timer.DelayCall( TimeSpan.FromSeconds( 30 ), new TimerCallback( delegate{ StopAffecting(); } ) );
						
			m_Stomp = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 40, 50 ) );
		}
		
		public Mobile Validate( Mobile m )
		{			
			Mobile agro;
					
			if ( m is BaseCreature )
				agro = ( (BaseCreature) m ).ControlMaster;
			else
				agro = m;
			
			if ( !CanBeHarmful( agro, false ) || !agro.Player || Combatant == agro )
				return null;	
			
			return agro;
		}
		
		private static Dictionary<Mobile,bool> m_Affected;
		
		public static bool IsUnderInfluence( Mobile mobile )
		{
			if ( m_Affected != null )
			{
				if ( m_Affected.ContainsKey( mobile ) )
					return true;
			}
			
			return false;
		}
		
		public static bool Affect( Mobile mobile )
		{
			if ( m_Affected == null )
				m_Affected = new Dictionary<Mobile,bool>();
				
			if ( !m_Affected.ContainsKey( mobile ) )
			{
				m_Affected.Add( mobile, true );
				return true;
			}
			
			return false;
		}
		
		public static void StopAffecting()
		{
			if ( m_Affected != null )
				m_Affected.Clear();
		}
	}
}
