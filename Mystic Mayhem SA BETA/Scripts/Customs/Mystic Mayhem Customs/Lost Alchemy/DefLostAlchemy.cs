using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefLostAlchemy : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; } // <CENTER>ALCHEMY MENU</CENTER>
		}
  
                public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>Lost Alchemy Menu</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefLostAlchemy();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefLostAlchemy() : base( 1, 1, 1.25 )// base( 1, 1, 3.1 )
		{
		}

		public static void CheckStump( Mobile from, int range, out bool stump )
		{
			stump = false;

			Map map = from.Map;

			if ( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( from.Location, range );

			foreach ( Item item in eable )
			{
				Type type = item.GetType();

				if ( item.ItemID == 6245 || item.ItemID == 6246 )
					stump = true;

				if ( stump )
					break;
			}

			eable.Free();

			for ( int x = -range; (!stump) && x <= range; ++x )
			{
				for ( int y = -range; (!stump) && y <= range; ++y )
				{
					StaticTile[] tiles = map.Tiles.GetStaticTiles( from.X+x, from.Y+y, true );

					for ( int i = 0; (!stump) && i < tiles.Length; ++i )
					{
						int id = tiles[i].ID & 0x3FFF;

						if ( id == 6245 || id == 6246 )
							stump = true;
					}
				}
			}
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.
    
                        bool stump;
                           
			CheckStump( from, 2, out stump );
     
                        if ( stump )
			return 0;
   
   
                       //return 1044267; //"You must be near an anvil and a forge to smith items."; //600000

                        from.SendMessage( "You must be near a dishing stump to make these potions!" );
			return 1019045; // I can't reach that.
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x242 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				from.AddToBackpack( new Bottle() );

				return 500287; // You fail to create a useful potion.
			}
			else
			{
				from.PlaySound( 0x240 ); // Sound of a filling bottle

				if ( quality == -1 )
					return 1048136; // You create the potion and pour it into a keg.
				else
					return 500279; // You pour the potion into a bottle...
			}
		}

		public override void InitCraftList()
		{
			int index = -1;
   
			index = AddCraft( typeof( RevitalizePotion ), "Revitalize Potion", "Revitalize Potion", 107.0, 115.0, typeof( BatWing ), "BatWing", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			
			index = AddCraft( typeof( ManaPotion ), "Mana Potion", "Mana Potion", 101.0, 110.0, typeof( PigIron ), "PigIron", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( TotalManaRefreshPotion ), "Mana Potion", "Total Mana Potion", 113.0, 120.0, typeof( PigIron ), "PigIron", 30 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			index = AddCraft( typeof( ShrinkPotion ), "Shrink Potion", "Shrink Potion", 109.9, 120.0, typeof( Bone ), "Bone", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			index = AddCraft( typeof( ResurrectPotion ), "Resurrect Potion", "Resurrect Potion", 102.0, 110.0, typeof( NoxCrystal ), "NoxCrystal", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( PetResurrectPotion ), "Resurrect Potion", "Pet Resurrect Potion", 111.0, 120.0, typeof( DaemonBlood ), "DaemonBlood", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );


			index = AddCraft( typeof( RepairPotion ), "Repair Potion", "Repair Potion", 114.9, 120.0, typeof( PetrafiedWood ), "PetrafiedWood", 15 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );


		}
	}
}
