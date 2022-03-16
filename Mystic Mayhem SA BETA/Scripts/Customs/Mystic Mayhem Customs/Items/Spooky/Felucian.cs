using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class Felucian
	{
	//	public static double ChestChance = .10;         // Chance that a paragon will carry a paragon chest
		public static Map[] Maps         = new Map[]    // Maps that paragons will spawn on
		{
			Map.Felucca
		};

		public static Type[] Artifacts = new Type[]
		{
			typeof( KatrinasCrook ), typeof( JaanasStaff ), 
			typeof( DragonsEnd ), typeof( SentinelsGuard ),
			typeof( LordBlackthornsExemplar ), typeof( HolySword ), 
			typeof( LeggingsOfEmbers ), typeof( CandelabraOfSouls ),
			typeof( RoseOfTrinsic ), typeof( SamuraiHelm ),
			typeof( ShaminoCrossbow ), typeof( TapestryOfSosaria ),
			typeof( CompassionArms ), typeof( HonestyGorget ),
			typeof( HonorLegs ), typeof( HumilityCloak ),
			typeof( JusticeBreastplate ), typeof( SacrificeSollerets ), 
			typeof( SpiritualityHelm ), typeof( ValorGauntlets ),
			typeof( QuiverOfInfinity ), typeof( HearthOfHomeFireDeed ),
			typeof( KnightsBascinet ), typeof( KnightsCloseHelm ),
			typeof( KnightsFemalePlateChest ), typeof( KnightsNorseHelm ),
			typeof( KnightsPlateArms ), typeof( KnightsPlateChest ),
			typeof( KnightsPlateGloves ), typeof( KnightsPlateGorget ),
			typeof( KnightsPlateHelm ), typeof( KnightsPlateLegs ),
			typeof( ScoutArms ), typeof( ScoutBustier ),
			typeof( ScoutChest ), typeof( ScoutCirclet ),
			typeof( ScoutFemaleChest ), typeof( ScoutGloves ),
			typeof( ScoutGorget ), typeof( ScoutLegs ),
			typeof( ScoutSmallPlateJingasa ), typeof( SorcererArms ),
			typeof( SorcererChest ), typeof( SorcererFemaleChest ),
			typeof( SorcererGloves ), typeof( SorcererGorget ),
			typeof( SorcererHat ), typeof( SorcererLegs ),
			typeof( SorcererSkirt ), typeof( ConjurersGrimoire ),
			typeof( ConjurersTrinket ), typeof( FountainOfLifeDeed ),
			typeof( DupresShield ), typeof( OssianGrimoire ),
			typeof( EtoileBleue ), typeof( NovoBleue ),
			typeof( BrokenArmoireDeed ), typeof( BrokenBedDeed ), 
			typeof( BrokenBookcaseDeed ), typeof( BrokenChestOfDrawersDeed ),
			typeof( BrokenCoveredChairDeed ), typeof( BrokenFallenChairDeed ), 
			typeof( BrokenVanityDeed ), typeof( StandingBrokenChairDeed ),
			typeof( AwesomeDisturbingPortraitDeed ), typeof( BedOfNailsDeed ),
			typeof( BoneCouchDeed ), typeof( BoneTableDeed ),
			typeof( BoneThroneDeed ), typeof( CreepyPortraitDeed ),
			typeof( DisturbingPortraitDeed ), typeof( HaunterMirrorDeed ),
			typeof( SacrificialAltarDeed ), typeof( UnsettlingPortraitDeed ), 
			typeof( AppleTrunkDeed ), typeof( BlueDecorativeRugDeed ),
			typeof( BlueFancyRugDeed ), typeof( BluePlainRugDeed ),
			typeof( BoilingCauldronDeed ), typeof( CherryBlossomTreeDeed ), 
			typeof( CherryBlossomTrunkDeed ), typeof( CinnamonFancyRugDeed ), 
			typeof( CurtainsDeed ), typeof( FountainDeed ), 
			typeof( PeachTreeDeed ), typeof( GoldenDecorativeRugDeed ), 
			typeof( GuillotineDeed ), typeof( HangingAxesDeed ), 
			typeof( HangingSwordsDeed ), typeof( HouseLadderDeed ), 
			typeof( IronMaidenDeed ), typeof( LargeFishingNetDeed ), 
			typeof( PeachTrunkDeed ), typeof( PinkFancyRugDeed ),
			typeof( RedPlainRugDeed ), typeof( ScarecrowDeed ),  
			typeof( SmallFishingNetDeed ), typeof( PinkFancyRugDeed ),  
			typeof( PeachTrunkDeed ), typeof( StoneStatueDeed ),  
			typeof( SuitOfGoldArmorDeed ), typeof( SuitOfSilverArmorDeed ),  
			typeof( TableWithBlueClothDeed ), typeof( TableWithOrangeClothDeed ),  
			typeof( TableWithPurpleClothDeed ), typeof( TableWithRedClothDeed ),  
			typeof( UnmadeBedDeed ), typeof( VanityDeed ),  
			typeof( WallTorchDeed ), typeof( WoodenCoffinDeed ),
			typeof( ConjurersGarb ), typeof( SolesOfProvidence ) 
			
			
		}; 

	//	public static int    Hue   = 0x501;        // IsFelucian hue
		
		// Buffs
		public static double HitsBuff   = 2.0;
		public static double StrBuff    = 1.05;
		public static double IntBuff    = 1.20;
		public static double DexBuff    = 1.20;
		public static double SkillsBuff = 1.20;
		public static double SpeedBuff  = 1.20;
		public static double FameBuff   = 1.40;
		public static double KarmaBuff  = 1.40;
		public static int    DamageBuff = 3;

		public static void Convert( BaseCreature bc )
		{
			if ( bc.IsFelucian )
				return;

		//	bc.Hue = Hue;

			if ( bc.HitsMaxSeed >= 0 )
				bc.HitsMaxSeed = (int)( bc.HitsMaxSeed * HitsBuff );
			
			bc.RawStr = (int)( bc.RawStr * StrBuff );
			bc.RawInt = (int)( bc.RawInt * IntBuff );
			bc.RawDex = (int)( bc.RawDex * DexBuff );

			bc.Hits = bc.HitsMax;
			bc.Mana = bc.ManaMax;
			bc.Stam = bc.StamMax;

			for( int i = 0; i < bc.Skills.Length; i++ )
			{
				Skill skill = (Skill)bc.Skills[i];

				if ( skill.Base > 0.0 )
					skill.Base *= SkillsBuff;
			}

			bc.PassiveSpeed /= SpeedBuff;
			bc.ActiveSpeed /= SpeedBuff;

			bc.DamageMin += DamageBuff;
			bc.DamageMax += DamageBuff;

			if ( bc.Fame > 0 )
				bc.Fame = (int)( bc.Fame * FameBuff );

			if ( bc.Fame > 32000 )
				bc.Fame = 32000;

			// TODO: Mana regeneration rate = Sqrt( buffedFame ) / 4

			if ( bc.Karma != 0 )
			{
				bc.Karma = (int)( bc.Karma * KarmaBuff );

				if( Math.Abs( bc.Karma ) > 32000 )
					bc.Karma = 32000 * Math.Sign( bc.Karma );
			}
		}

		public static void UnConvert( BaseCreature bc )
		{
			if ( !bc.IsFelucian )
				return;

		//	bc.Hue = 0;

			if ( bc.HitsMaxSeed >= 0 )
				bc.HitsMaxSeed = (int)( bc.HitsMaxSeed / HitsBuff );
			
			bc.RawStr = (int)( bc.RawStr / StrBuff );
			bc.RawInt = (int)( bc.RawInt / IntBuff );
			bc.RawDex = (int)( bc.RawDex / DexBuff );

			bc.Hits = bc.HitsMax;
			bc.Mana = bc.ManaMax;
			bc.Stam = bc.StamMax;

			for( int i = 0; i < bc.Skills.Length; i++ )
			{
				Skill skill = (Skill)bc.Skills[i];

				if ( skill.Base > 0.0 )
					skill.Base /= SkillsBuff;
			}
			
			bc.PassiveSpeed *= SpeedBuff;
			bc.ActiveSpeed *= SpeedBuff;

			bc.DamageMin -= DamageBuff;
			bc.DamageMax -= DamageBuff;

			if ( bc.Fame > 0 )
				bc.Fame = (int)( bc.Fame / FameBuff );
			if ( bc.Karma != 0 )
				bc.Karma = (int)( bc.Karma / KarmaBuff );
		}

		public static bool CheckConvert( BaseCreature bc )
		{
			return CheckConvert( bc, bc.Location, bc.Map );
		}

		public static bool CheckConvert( BaseCreature bc, Point3D location, Map m )
		{
			if ( !Core.AOS )
				return false;

			if ( Array.IndexOf( Maps, m ) == -1 )
				return false;

			if ( bc is BaseChampion || bc is Harrower || bc is BaseVendor || bc is BaseEscortable || bc is Clone )
				return false;

			int fame = bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 1 / Math.Round( 20.0 - ( fame / 3200 ));

			return true; //( chance > Utility.RandomDouble() );
		}

		public static bool CheckArtifactChance( Mobile m, BaseCreature bc )
		{
			if ( !Core.AOS )
				return false;

			double fame = (double)bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 0.025 / ( Math.Max( 10, 100 * ( 0.83 - Math.Round( Math.Log( Math.Round( fame / 6000, 3 ) + 0.001, 10 ), 3 ) ) ) * ( 100 - Math.Sqrt( m.Luck ) ) / 100.0 );

			return chance > Utility.RandomDouble();
		}

		public static void GiveArtifactTo( Mobile m )
		{
			Item item = (Item)Activator.CreateInstance( Artifacts[Utility.Random(Artifacts.Length)] );

			if ( m.AddToBackpack( item ) )
				m.SendMessage( "For your valor in combating the fallen beast, a special artifact has been bestowed on you." );
			else
				m.SendMessage( "As your backpack is full, your reward for destroying the legendary creature has been placed at your feet." );
		} 
	}
}