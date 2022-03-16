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
	public class HouseReport
	{
		public static void Initialize()
		{
			CommandSystem.Register( "HouseReport", AccessLevel.Administrator, new CommandEventHandler( HouseReport_OnCommand ) );
		}

		private static void HouseReport_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "Housereport.log" ) )
			{
				op.WriteLine( "House Account             Character" );

				foreach( Mobile MiW in World.Mobiles.Values )
				{
					string PG = " ";
					if( MiW is PlayerMobile )
					{
						PlayerMobile pm = (PlayerMobile)MiW;
						Account acct = pm.Account as Account;
					ArrayList list = GetHouses( pm );
					//PG = BaseHouse.GetHouses( pm );
						if ( list.Count == 0 )
						{ 
							PG = "---";
						}
						else
						{
							PG = "Yes";
						}  
  //   op.WriteLine( String.Format( "{0}   ", PG ) + String.Format( "{0}                   ", acct ) + String.Format( "{0}   ", pm.Name ) );
						//op.WriteLine( "{0}\t{1}\t{2}", PG, acct,pm.Name );
						op.WriteLine( "{0,-6}{1,-20}{2,-20}", PG, acct,pm.Name );

					}
				}
			}
			args.Mobile.SendMessage( "House Report done <runuo root>/Housereport.log" );

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