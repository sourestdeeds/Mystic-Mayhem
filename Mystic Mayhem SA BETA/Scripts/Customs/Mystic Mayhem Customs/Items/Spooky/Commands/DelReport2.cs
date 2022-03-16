using System;
using System.IO;
using System.Collections;
using Server;
using Server.Commands;
using Server.Network;
using Server.Multis;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Misc
{
	public class DelReport2
	{
		public static void Initialize()
		{
			CommandSystem.Register( "DelReport2", AccessLevel.Administrator, new CommandEventHandler( DelReport_OnCommand ) );
		}

		private static void DelReport_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "DelReport2.log" ) )
			{
				op.WriteLine( "Account             Character" );

				foreach( Mobile MiW in World.Mobiles.Values )
				//foreach (Account acct in Accounts.Table.Values)
				{
					string hyn = " ";
					float PG = 0;
					DateTime minTime = DateTime.Now - TimeSpan.FromDays( 15.0 );
					int xLong = 0, yLat = 0, xMins = 0, yMins = 0;
					bool xEast = false, ySouth = false;
					Point3D location = new Point3D( 0, 0, 0 );
					string mmap = " ";
					if( MiW is PlayerMobile )
					{
					  try
					  {

						PlayerMobile pm = (PlayerMobile)MiW;
						Account acct = pm.Account as Account;
						PG += SearchForGold( pm.BankBox );
						PG += SearchForGold( pm.Backpack );
						if ( acct == null)
						{
						args.Mobile.SendMessage( "Account Found" );
						}
						if (acct.LastLogin <= minTime && pm.GameTime <= TimeSpan.FromHours( 5.0 ))
						{

							ArrayList list = GetHouses( pm );

							if ( list.Count == 0 )
							{ 
								hyn = "---";
							}
							else
							{
								hyn = "Yes";
							}  

				
							if ( list.Count == 1 )
							{

								BaseHouse sel = (BaseHouse)list[0];

								Map map = sel.Map;
								mmap = map.Name;
								bool valid = Sextant.Format( sel.Location, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth );

								if ( valid )
						  		location = sel.Location; 							}  
				if ( hyn == "Yes" )
				{op.WriteLine( "{0,-18}{1,-20}{2,-9}{3,-25}{4,-20}\t{5,-9}{6}", acct, pm.Name, PG, acct.LastLogin, pm.GameTime, mmap, location );}
				else
				{op.WriteLine( "{0,-18}{1,-20}{2,-9}{3,-25}{4,-20}", acct, pm.Name, PG, acct.LastLogin, pm.GameTime );}
						}
					  }
					  catch
					  {
					  }

					}
				}
			}
			args.Mobile.SendMessage( "Delete Report done <runuo root>/DelReport2.log" );

		}
		public static float SearchForGold( Container c )
		{
			float goldcount = 0;
		   try
		   {
			//float goldcount = 0;

			foreach( Item i in c.Items )
				if( i is Container )
					goldcount += SearchForGold( (Container)i );
				else if( i is Gold )
					goldcount += ((Gold)i).Amount;
				else if( i is BankCheck )
					goldcount += ((BankCheck)i).Worth;

		   }
		   catch {  }
			return goldcount;
		}

		public static ArrayList GetHouses( Mobile owner )
		{
			ArrayList list = new ArrayList();

			Account acct = owner.Account as Account;

			if ( acct == null )
			{
				list.AddRange( BaseHouse.GetHouses( owner ) );
			}
			else
			{
				for ( int i = 0; i < acct.Length; ++i )
				{
					Mobile mob = acct[i];

					if ( mob != null )
						list.AddRange( BaseHouse.GetHouses( mob ) );
				}
			}
			return list;
		}
	}
}