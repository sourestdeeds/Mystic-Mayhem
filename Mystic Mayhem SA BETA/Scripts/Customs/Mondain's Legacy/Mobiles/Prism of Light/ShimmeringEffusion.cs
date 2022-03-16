using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a shimmering effusion corpse" )]
	public class ShimmeringEffusion : BasePeerless
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		[Constructable]
		public ShimmeringEffusion() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shimmering effusion";
			Body = 0x105;			

			SetStr( 509, 538 );
			SetDex( 354, 381 );
			SetInt( 1513, 1578 );

			SetHits( 25000 );

			SetDamage( 27, 31 );
			
			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );
			
			SetResistance( ResistanceType.Physical, 75, 76 );
			SetResistance( ResistanceType.Fire, 60, 65 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 76, 80 );
			SetResistance( ResistanceType.Energy, 75, 78 );

			SetSkill( SkillName.Wrestling, 100.2, 101.4 );
			SetSkill( SkillName.Tactics, 105.5, 102.1 );
			SetSkill( SkillName.MagicResist, 150 );
			SetSkill( SkillName.Magery, 150.0 );
			SetSkill( SkillName.EvalInt, 150.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			
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
			
			c.DropItem( new CapturedEssence() );
			c.DropItem( new ShimmeringCrystals() );			
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 4 ) )
				{
					case 0: c.DropItem( new ShimmeringEffusionStatuette() );	break;
					case 1: c.DropItem( new CorporealBrumeStatuette() );	break;
					case 2: c.DropItem( new MantraEffervescenceStatuette() ); break;
					case 3: c.DropItem( new FetidEssenceStatuette() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new FerretImprisonedInCrystal() );		
						
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new CrystallineRing() );	
					
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
			
			if ( Utility.RandomDouble() < 0.6 )				
				c.DropItem( new ParrotItem() );
		}
			
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override bool HasFireRing{ get{ return true; } }
		public override double FireRingChance{ get{ return 0.1; } }

		public override int GetIdleSound() { return 0x1BF; }
		public override int GetAttackSound() { return 0x1C0; }
		public override int GetHurtSound() { return 0x1C1; }
		public override int GetDeathSound()	{ return 0x1C2; }
		
		#region Helpers
		public override bool CanSpawnHelpers{ get{ return true; } }
		public override int MaxHelpersWaves{ get{ return 4; } }
		public override double SpawnHelpersChance{ get{ return 0.1; } }
		
		public override void SpawnHelpers()
		{
			int amount = 1;
		
			if ( Altar != null )
				amount = Altar.Fighters.Count;
				
			if ( amount > 5 )
				amount = 5;
			
			for ( int i = 0; i < amount; i ++ )
			{				
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new MantraEffervescence(), 2 ); break;
					case 1: SpawnHelper( new CorporealBrume(), 2 ); break;
					case 2: SpawnHelper( new FetidEssence(), 2 ); break;
				}				
			}
		}
		#endregion

		public ShimmeringEffusion( Serial serial ) : base( serial )
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
	}
}
