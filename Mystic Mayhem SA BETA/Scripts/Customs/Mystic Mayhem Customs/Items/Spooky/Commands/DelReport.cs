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
	public class DelReport
	{
		public static void Initialize()
		{
			CommandSystem.Register( "DelReport", AccessLevel.Administrator, new CommandEventHandler( DelReport_OnCommand ) );
		}

		private static void DelReport_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "DelReport.log" ) )
			{
				op.WriteLine( "Account             Character" );

				foreach( Mobile MiW in World.Mobiles.Values )
				//foreach (Account acct in Accounts.Table.Values)
				{
					string hyn = " ";
					DateTime minTime = DateTime.Now - TimeSpan.FromDays( 90.0 );
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
						if ( acct == null)
						{
						args.Mobile.SendMessage( "Account Found" );
						}
						if (acct.LastLogin <= minTime)
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
				{op.WriteLine( "{0,-18}{1,-20}\t{2,-9}{3}", acct, pm.Name, mmap, location );}
				else
				{op.WriteLine( "{0,-18}{1,-20}", acct, pm.Name );}
						}
					  }
					  catch
					  {
					  }

					}
				}
			}
			args.Mobile.SendMessage( "Delete Report done <runuo root>/DelReport.log" );

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