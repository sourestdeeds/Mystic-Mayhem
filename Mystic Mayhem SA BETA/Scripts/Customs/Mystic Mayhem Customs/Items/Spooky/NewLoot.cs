using System;
using System.IO;
using System.Reflection;
using Server;
using Server.Items;

namespace Server
{
	public class NewLoot
	{
		#region List definitions
/*		private static Type[] m_SEWeaponTypes = new Type[]
			{
				typeof( Bokuto ), typeof( Daisho ), typeof( Kama ),
				typeof( Lajatang ), typeof( NoDachi ), typeof( Sai ),
				typeof( Tekagi ), typeof( Tessen ), typeof( Tetsubo ),
				typeof( Yumi ), typeof( Wakizashi ), typeof( Nunchaku )
			};

		public static Type[] SEWeaponTypes{ get{ return m_SEWeaponTypes; } }



		private static Type[] m_SEArmorTypes = new Type[]
			{
			typeof( LeatherNinjaBelt ), typeof( LeatherNinjaBelt2 ), 	typeof( LeatherNinjaHood ), 
			typeof( LeatherNinjaJacket ), 	typeof( LeatherNinjaMitts ), 
			typeof( LeatherNinjaPants ),	typeof( LeatherDo ),
			typeof( LeatherHaidate ), 	typeof( LeatherHiroSode ), 
			typeof( LeatherJingasa ), 	typeof( LeatherKabuto ), 
			typeof( LeatherMempo ), 	typeof( LeatherSuneate ),
			typeof( PlateDo ), 		typeof( PlateHaidate ), 
			typeof( PlateHatsuburi ), 	typeof( PlateHeavyJingasa ), 
			typeof( PlateHiroSode ), 	typeof( PlateKabuto ),
			typeof( PlateLightJingasa ), 	typeof( PlateMempo ),
			typeof( PlateSmallJingasa ), 	typeof( PlateSuneata ),
			typeof( StuddedDo ), 		typeof( StuddedHaidate ),
			typeof( StuddedMempo ), 	typeof( StuddedSuneate )
			};

		public static Type[] SEArmorTypes{ get{ return m_SEArmorTypes; } }



	        private static Type[] m_SEClothingTypes = new Type[]
            	{
                	typeof( Kasa ), 		typeof( ClothNinjaHood ),
                	typeof( ClothNinjaJacket ), 	typeof( JinBaori ),
			typeof( HakamaShita ),		typeof( MaleKimono ),
			typeof( FemaleKimono ), 	typeof( Kamishimo ),
			typeof( Hakama ), 		typeof( TattsukeHakama ),
			typeof( Waraji ), 		typeof( NinjaTabi ),
			typeof( Obi )
            	};
        	public static Type[] SEClothingTypes{ get{ return m_SEClothingTypes; } }	
*/
		private static Type[] m_SpellweavingScrollTypes = new Type[]
			{
				typeof( ArcaneCircleScroll ),		typeof( GiftOfRenewalScroll ),		typeof( ImmolatingWeaponScroll ),	typeof( AttuneWeaponScroll ),
				typeof( ThunderstormScroll ),			typeof( NatureFuryScroll ),	typeof( ReaperFormScroll ),	typeof( WildfireScroll ),
				typeof( EssenceOfWindScroll ),			typeof( DryadAllureScroll ),	typeof( EtherealVoyageScroll ),	typeof( WordOfDeathScroll ),
				typeof( GiftOfLifeScroll ),	typeof( ArcaneEmpowermentScroll )
			};

		public static Type[] SpellweavingScrollTypes{ get{ return m_SpellweavingScrollTypes; } }	


		#endregion

		#region Accessors


		public static SpellScroll RandomSpellweavingScroll()
		{

			return Construct( m_SpellweavingScrollTypes ) as SpellScroll;
		}

 /*       
		public static BaseClothing RandomSEClothing()
        	{
            		return Construct( m_SEClothingTypes ) as BaseClothing;
        	}


		public static BaseWeapon RandomSEWeapon()
		{

			return Construct( m_SEWeaponTypes ) as BaseWeapon;
		}


		public static BaseArmor RandomSEArmor()
		{
			return Construct( m_SEArmorTypes ) as BaseArmor;
		}

*/

		#endregion

		#region Construction methods
		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}

		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		public static Item Construct( params Type[][] types )
		{
			int totalLength = 0;

			for ( int i = 0; i < types.Length; ++i )
				totalLength += types[i].Length;

			if ( totalLength > 0 )
			{
				int index = Utility.Random( totalLength );

				for ( int i = 0; i < types.Length; ++i )
				{
					if ( index >= 0 && index < types[i].Length )
						return Construct( types[i][index] );

					index -= types[i].Length;
				}
			}

			return null;
		}
		#endregion
	}
}
