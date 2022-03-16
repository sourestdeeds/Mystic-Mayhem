using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a chief paroxysmus corpse" )]
	public class ChiefParoxysmus: BasePeerless
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		[Constructable]
		public ChiefParoxysmus() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a chief paroxysmus";
			Body = 0x100;

			SetStr( 1232, 1400 );
			SetDex( 76, 82 );
			SetInt( 76, 85 );

			SetHits( 100000 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 50, 60 );
			
			SetSkill( SkillName.Wrestling, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Anatomy, 120.0 );
			SetSkill( SkillName.Poisoning, 120.0 );
			
			SpawnBulbous();
			
			PackResources( 8 );
			//PackTalismans( 5 );
		}
		
		public ChiefParoxysmus( Serial serial ) : base( serial )
		{
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
			c.DropItem( new LardOfParoxysmus() );
			
			switch ( Utility.Random( 3 ) )
			{
				case 0: c.DropItem( new ParoxysmusDinner() ); break;
				case 1: c.DropItem( new ParoxysmusCorrodedStein() ); break;
				case 2: c.DropItem( new StringOfPartsOfParoxysmusVictims() ); break;
			}
			
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
			
			if ( Utility.RandomBool() )
				c.DropItem( new SweatOfParoxysmus() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new ParoxysmusSwampDragonStatuette() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new ScepterOfTheChief() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new TangleApron() );
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 39 ) )
				{
					case 0: c.DropItem( new AssassinChest() ); break;
					case 1: c.DropItem( new AssassinArms() ); break;
					case 2: c.DropItem( new DeathChest() );	break;		
					case 4: c.DropItem( new MyrmidonArms() ); break;
					case 5: c.DropItem( new MyrmidonLegs() ); break;
					case 6: c.DropItem( new MyrmidonGorget() ); break;
					case 7: c.DropItem( new LeafweaveGloves() ); break;
					case 8: c.DropItem( new LeafweaveLegs() ); break;
					case 9: c.DropItem( new LeafweavePauldrons() ); break;
					case 10: c.DropItem( new PaladinGloves() ); break;
					case 11: c.DropItem( new PaladinGorget() ); break;
					case 12: c.DropItem( new PaladinArms() ); break;
					case 13: c.DropItem( new HunterArms() ); break;
					case 14: c.DropItem( new HunterGloves() ); break;
					case 15: c.DropItem( new HunterLegs() ); break;
					case 16: c.DropItem( new HunterChest() ); break;
					case 17: c.DropItem( new GreymistArms() ); break;
					case 18: c.DropItem( new GreymistGloves() ); break;
					case 19: c.DropItem( new GreymistChest() ); break;
					case 20: c.DropItem( new GreymistLegs() ); break;
					case 21: c.DropItem( new AssassinGloves() ); break;
					case 23: c.DropItem( new AssassinLegs() ); break;
					case 24: c.DropItem( new Evocaricus() ); break;
					case 25: c.DropItem( new MalekisHonor() ); break;
					case 26: c.DropItem( new LeafweaveChest() ); break;
					case 27: c.DropItem( new Feathernock() ); break;
					case 28: c.DropItem( new Swiftflight() ); break;
					case 29: c.DropItem( new MyrmidonChest() ); break;
					case 30: c.DropItem( new MyrmidonCloseHelm() ); break;
					case 31: c.DropItem( new MyrmidonGloves() ); break;
					case 32: c.DropItem( new DeathGloves() );	break;
					case 33: c.DropItem( new DeathArms() );	break;	
					case 34: c.DropItem( new DeathLegs() );	break;	
					case 35: c.DropItem( new DeathBoneHelm() );	break;
					case 36: c.DropItem( new PaladinChest() ); break;
					case 37: c.DropItem( new PaladinHelm() ); break;
					case 38: c.DropItem( new PaladinLegs() ); break;
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

		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }		
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
		public override bool CanAreaPoison{ get{ return true; } }
		public override Poison HitAreaPoison{ get{ return Poison.Lethal; } }
		
		public override int GetDeathSound()	{ return 0x56F; }
		public override int GetAttackSound() { return 0x570; }
		public override int GetIdleSound() { return 0x571; }
		public override int GetAngerSound() { return 0x572; }
		public override int GetHurtSound() { return 0x573; }

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			base.OnDamage( amount, from, willKill );
			
			// eats pet or summons
			if ( from is BaseCreature )
			{
				BaseCreature creature = (BaseCreature) from;
				
				if ( creature.Controlled || creature.Summoned )
				{
					Heal( creature.Hits );					
					creature.Kill();				
					
					Effects.PlaySound( Location, Map, 0x574 );
				}
			}
			
			// teleports player near
			if ( from is PlayerMobile && !InRange( from.Location, 1 ) )
			{
				Combatant = from;
				
				from.MoveToWorld( GetSpawnPosition( 1 ), Map );				
				from.FixedParticles( 0x376A, 9, 32, 0x13AF, EffectLayer.Waist );
				from.PlaySound( 0x1FE );
			}
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
		
		public virtual void SpawnBulbous()
		{
			for ( int i = 0; i < 3; i ++ )
			{
				Mobile blobus = new BulbousPutrification();
				blobus.MoveToWorld( GetSpawnPosition( 4 ), Map );
			}
		}
	}
}
