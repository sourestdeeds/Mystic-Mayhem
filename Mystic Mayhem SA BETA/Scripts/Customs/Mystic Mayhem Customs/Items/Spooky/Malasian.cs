/*
	Jon		24/05/10	- Ln 18: Added new Artifacts.
						- Ln 170: Lowered drop rate from 1 to 0.2
*/
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class Malasian
	{
	//	public static double ChestChance = .10;         // Chance that a paragon will carry a paragon chest
		public static Map[] Maps         = new Map[]    // Maps that paragons will spawn on
		{
			Map.Malas
		};
//Jon
		public static Type[] Artifacts = new Type[]
		{
			
			typeof( Antiquity ), typeof( ArmsoftheBeast ),
			typeof( BloodTrail ), typeof( BloodyWrist ),
			typeof( BowOfHarps ), typeof( ChildOfDeath ),
			typeof( FeverFall ), typeof( GlovesOfTheHardWorker ),
			typeof( HandsofTabulature ), typeof( Kamadon ),
			typeof( LaFemme ), typeof( PurposeOfPain ),
			typeof( Revenge ), typeof( RingOfDeath ),
			typeof( RiotShield ), typeof( SatanicHelm ),
			typeof( StandStill ), typeof( StealthyChest ),
			typeof( TacticalMask ), typeof( ThickNeck ),
			typeof( UnholyBoneBreaker ), typeof( ValasCompromise ),
			typeof( Valicious ), typeof( WizardsStrongArm ),
			typeof( HumanFlesh ), typeof( NightsGift ),
			typeof( StatPants ), typeof( ArmsofExpertCooking ),
			typeof( CapofExpertCooking ), typeof( GlovesofExpertCooking ),
			typeof( GorgetofExpertCooking ), typeof( LegsofExpertCooking ),
			typeof( TunicofExpertCooking ),
			typeof( CapofExpertFletching ), typeof( GlovesofExpertFletching ),
			typeof( GorgetofExpertFletching ), typeof( LegsofExpertFletching ),
			typeof( TunicofExpertFletching ),
			typeof( CapofExpertLumberjacking ), typeof( GlovesofExpertLumberjacking ),
			typeof( GorgetofExpertLumberjacking ), typeof( LegsofExpertLumberjacking ),
			typeof( TunicofExpertLumberjacking ),
			typeof( CapofExpertMining ), typeof( GlovesofExpertMining ),
			typeof( GorgetofExpertMining ), typeof( LegsofExpertMining ),
			typeof( TunicofExpertMining ),
			typeof( CapofExpertSmithy ), typeof( GlovesofExpertSmithy ),
			typeof( GorgetofExpertSmithy ), typeof( LegsofExpertSmithy ),
			typeof( TunicofExpertSmithy ),
			typeof( CapofExpertTailoring ), typeof( GlovesofExpertTailoring ),
			typeof( GorgetofExpertTailoring ), typeof( LegsofExpertTailoring ),
			typeof( TunicofExpertTailoring ),
			typeof( CapofExpertTinkering ), typeof( GlovesofExpertTinkering ),
			typeof( GorgetofExpertTinkering ), typeof( LegsofExpertTinkering ),
			typeof( TunicofExpertTinkering )
			    
			
		}; 

	//	public static int    Hue   = 0x501;        // IsMalasian hue
		
		// Buffs
		public static double HitsBuff   = 0.75;
		public static double StrBuff    = 0.75;
		public static double IntBuff    = 2.0;
		public static double DexBuff    = 0.75;
		public static double SkillsBuff = 1.50;
		public static double SpeedBuff  = 1.50;
		public static double FameBuff   = 1.0;
		public static double KarmaBuff  = 1.0;
		public static int    DamageBuff = -5;

		public static void Convert( BaseCreature bc )
		{
			if ( bc.IsMalasian )
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
			if ( !bc.IsMalasian )
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
//Jon
			double chance = 0.05 / ( Math.Max( 10, 100 * ( 0.83 - Math.Round( Math.Log( Math.Round( fame / 6000, 3 ) + 0.001, 10 ), 3 ) ) ) * ( 100 - Math.Sqrt( m.Luck ) ) / 100.0 );

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