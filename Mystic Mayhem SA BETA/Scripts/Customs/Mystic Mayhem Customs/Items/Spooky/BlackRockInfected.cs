using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class Blackrock
	{
		public static double ChestChance = .10;         // Chance that a paragon will carry a paragon chest
		public static Map[] Maps         = new Map[]    // Maps that paragons will spawn on
		{
			Map.Ilshenar,
            Map.Felucca,
            Map.Malas,
            Map.Tokuno
		};
		
		public static Type[] Artifacts = new Type[]
		{
			typeof( LegacyOfTheDreadLord ),
				typeof( TheDragonSlayer ),
				typeof( ArmorOfFortune ),
				typeof( GauntletsOfNobility ),
				typeof( HelmOfInsight ),
				typeof( HolyKnightsBreastplate ),
				typeof( JackalsCollar ),
				typeof( LeggingsOfBane ),
				typeof( MidnightBracers ),
				typeof( OrnateCrownOfTheHarrower ),
				typeof( ShadowDancerLeggings ),
				typeof( TunicOfFire ),
				typeof( VoiceOfTheFallenKing ),
				typeof( BraceletOfHealth ),
				typeof( OrnamentOfTheMagician ),
				typeof( RingOfTheElements ),
				typeof( RingOfTheVile ),
				typeof( Aegis ),
				typeof( ArcaneShield ),
				typeof( AxeOfTheHeavens ),
				typeof( BladeOfInsanity ),
				typeof( BoneCrusher ),
				typeof( BreathOfTheDead ),
				typeof( Frostbringer ),
				typeof( SerpentsFang ),
				typeof( StaffOfTheMagi ),
				typeof( TheBeserkersMaul ),
				typeof( TheDryadBow ),
				typeof( DivineCountenance ),
				typeof( HatOfTheMagi ),
				typeof( HuntersHeaddress ),
				typeof( SpiritOfTheTotem ),
				typeof( CoifOfBane ), typeof( TunicOfBane ),
				typeof( ArmsOfTheFallenKing ), typeof( CapOfTheFallenKing ),
				typeof( GlovesOfTheFallenKing ), typeof( LegsOfTheFallenKing ),
				typeof( TunicOfTheFallenKing ), typeof( CoifOfFire ),
				typeof( LeggingsOfFire ), typeof( ArmsOfFortune ),
				typeof( CapOfFortune ), typeof( GlovesOfFortune ),
				typeof( GorgetOfFortune ), typeof( LegsOfFortune ),
				typeof( ArmsOfTheHarrower ), typeof( GlovesOfTheHarrower ),
				typeof( LegsOfTheHarrower ), typeof( TunicOfTheHarrower ),
				typeof( HolyKnightsArmPlates ), typeof( HolyKnightsGloves ),
				typeof( HolyKnightsGorget ), typeof( HolyKnightsPlateHelm ),
				typeof( HolyKnightsLegging ), typeof( InquisitorsArms ),
				typeof( InquisitorsGorget ), typeof( InquisitorsHelm ),
				typeof( InquisitorsLeggings ), typeof( InquisitorsTunic ),
				typeof( ArmorOfInsight ), typeof( ArmsOfInsight ),
				typeof( GlovesOfInsight ), typeof( GorgetOfInsight ),
				typeof( LegsOfInsight ), typeof( JackalsArms ),
				typeof( JackalsGloves ), typeof( JackalsHelm ),
				typeof( JackalsLeggings ), typeof( JackalsTunic ),
				typeof( MidnightGloves ), typeof( MidnightHelm ),
				typeof( MidnightLegs ), typeof( MidnightTunic ),
				typeof( ArmorOfNobility ), typeof( ArmsOfNobility ),
				typeof( LegsOfNobility ), typeof( ShadowDancerArms ),
				typeof( ShadowDancerCap ), typeof( ShadowDancerGloves ),
				typeof( ShadowDancerGorget ), typeof( ShadowDancerTunic ),
				typeof( DivineArms ), typeof( DivineGloves ), typeof( DivineGorget ),
				typeof( DivineLeggings ), typeof( DivineTunic ), typeof( HuntersArms ),
				typeof( HuntersGloves ), typeof( HuntersGorget ), typeof( HuntersLeggings ),
				typeof( HuntersTunic ), typeof( TotemArms ), typeof( TotemGloves ),
				typeof( TotemGorget ), typeof( TotemLeggings ), typeof( TotemTunic ),
				typeof( BraceletOfTheElements ), typeof( EarringsOfTheElements ),
				typeof( EarringsOfHealth ), typeof( RingOfHealth ), typeof( EarringsOfTheMagician ),
				typeof( RingOfTheMagician ), typeof( BraceletOfTheVile ), typeof( EarringsOfTheVile ),
				typeof( ArmsOfAegis ), typeof( GlovesOfAegis ), typeof( GorgetOfAegis ),
				typeof( HelmOfAegis ), typeof( LeggingsOfAegis ), typeof( TunicOfAegis ),
				typeof( ArcaneArms ), typeof( ArcaneCap ), typeof( ArcaneGloves ),
				typeof( ArcaneGorget ), typeof( ArcaneLeggings ), typeof( ArcaneTunic )
		}; 



        public static int Hue = 1175;        // Paragon hue
		
		// Buffs
		public static double HitsBuff   = 10.0;
		public static double StrBuff    = 2.05;
		public static double IntBuff    = 2.20;
		public static double DexBuff    = 2.20;
		public static double SkillsBuff = 3.20;
		public static double SpeedBuff  = 5.0;
		public static double FameBuff   = 2.40;
		public static double KarmaBuff  = 2.40;
		public static int    DamageBuff = 25;

		public static void Convert( BaseCreature bc )
		{
            if (bc.IsBlackrock)
				return;

			bc.Hue = Hue;

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
            if (!bc.IsBlackrock)
				return;

			bc.Hue = 0;

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

			if ( /*bc is BaseChampion ||*/ bc is Harrower || bc is BaseVendor || bc is BaseEscortable || bc is Clone )
				return false;

			int fame = bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 0.025 / Math.Round( 20.0 - ( fame / 3200 ));

			return ( chance > Utility.RandomDouble() );
		}

		public static bool CheckArtifactChance( Mobile m, BaseCreature bc )
		{
			if ( !Core.AOS )
				return false;

			double fame = (double)bc.Fame;

			if ( fame > 32000 )
				fame = 32000;

			double chance = 1 / ( Math.Max( 10, 100 * ( 0.83 - Math.Round( Math.Log( Math.Round( fame / 6000, 3 ) + 0.001, 10 ), 3 ) ) ) * ( 100 - Math.Sqrt( m.Luck ) ) / 100.0 );

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