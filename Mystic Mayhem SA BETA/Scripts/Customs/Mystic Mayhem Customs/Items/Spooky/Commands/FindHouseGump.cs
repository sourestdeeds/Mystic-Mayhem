    //
 //  Written by Haazen June 2005
//   Edited by Busty in October 2005 to find houses
//This script does NOT include the findboat command!!!
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;
using Server.Accounting;

namespace Server.Gumps
{
	public class FindHouseGump : Gump
	{

		public static void Initialize()
		{
			CommandSystem.Register( "FH", AccessLevel.Counselor, new CommandEventHandler( FindHouse_OnCommand ) );
			CommandSystem.Register( "FindHouse", AccessLevel.Counselor, new CommandEventHandler( FindHouse_OnCommand ) );
		}

		[Usage( "FindHouse" )]
		[Description( "Finds all Houses in the world." )]
		public static void FindHouse_OnCommand( CommandEventArgs e )
		{
			ArrayList list = new ArrayList();
			string searchValue = "";
			string str = "";

			for ( int i = 0; i < e.Length; i++ )
			{
				str = e.GetString( i ).ToLower();
			}
			searchValue = str;
	//		 	World.Broadcast( 0x35, true, "{0}, str", str );

			foreach ( Item item in World.Items.Values )
			{
				if ( item is BaseHouse )

				{
					BaseHouse House = item as BaseHouse;
					if (searchValue != "" && House.Owner.Name.ToLower().StartsWith( searchValue ) )
					{
//World.Broadcast( 0x35, true, "{0}, Owner", House.Owner.Name.ToLower() );
						list.Add( House );
					}
					else if (searchValue == "" )
					{
						list.Add(House);
					}
 					
				}
			}
			list.Sort( InternalComparer.Instance );
			e.Mobile.SendGump( new FindHouseGump( e.Mobile, list, 1 ) );
		}

		private class InternalComparer : IComparer
		{
			public static readonly IComparer Instance = new InternalComparer();

			public InternalComparer()
			{
			}

			public int Compare( object x, object y )
			{
				if ( x == null && y == null )
					return 0;
				else if ( x == null )
					return -1;
				else if ( y == null )
					return 1;

				BaseHouse a = x as BaseHouse;
				BaseHouse b = y as BaseHouse;

				if ( a == null || b == null )
					throw new ArgumentException();

			//	if ( a.AccessLevel > b.AccessLevel )
			//		return -1;
			//	else if ( a.AccessLevel < b.AccessLevel )
			//		return 1;
			//	else
					return Insensitive.Compare( a.Owner.Name, b.Owner.Name );
			}
		}


		private ArrayList m_List;
		private int m_DefaultIndex;
		private int m_Page;
		private Mobile m_From;

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		public FindHouseGump( Mobile from, ArrayList list, int page ) : base( 50, 40 )
		{
			from.CloseGump( typeof( FindHouseGump ) );

			int Houses = 0;
			m_Page = page;
			m_From = from;
			int pageCount = 0;
			m_List = list;

			AddPage( 0 );

			AddBackground( 0, 0, 540, 315, 5054 );

			AddBlackAlpha( 10, 10, 520, 280 );

			if ( m_List == null )
			{
				return;
			}
			else
			{
				Houses = list.Count;

				if ( list.Count % 12 == 0 )
				{
					pageCount = (list.Count / 12);
				}
				else
				{
					pageCount = (list.Count / 12) + 1;
				}
			}

			AddLabelCropped( 32, 16, 120, 20, 1152, "Owner" );
			AddLabelCropped( 175,16, 120, 20, 1152, "Account");
			AddLabelCropped( 300, 16, 120, 20, 1152, "Location" );
			AddLabel( 80, 290, 93, String.Format( "Paradise House Locator      {0} Houses are in the world", Houses ));

			if ( page > 1 )
				AddButton( 470, 18, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
			else
				AddImage( 470, 18, 0x25EA );

			if ( pageCount > page )
				AddButton( 487, 18, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
			else
				AddImage( 487, 18, 0x25E6 );

			if ( m_List.Count == 0 )
				AddLabel( 135, 80, 1152, "There are no Houses in the world." );

			if ( page == pageCount )
			{
				for ( int i = (page * 12) -12; i < Houses; ++i )
					AddDetails( i, from );
			}
			else
			{
				for ( int i = (page * 12) -12; i < page * 12; ++ i )
					AddDetails( i, from );
			}
		}

		private void AddDetails( int index, Mobile from )
		{
			string owner;
			if ( index < m_List.Count )
			{
				try
				{
				int btn, btn2;
				int row;
				btn = (index) + 101;
				btn2 = (index) + 1101;
				row = index % 12;
		
				BaseHouse House = m_List[index] as BaseHouse;
				Account acct = House.Owner.Account as Account;

				Mobile houseOwner = House.Owner;
				Point3D loc = House.GetWorldLocation();

				Map map = House.Map;

				if ( houseOwner == null )
					owner = "nobody";
				else
					owner = houseOwner.Name;

				AddLabel(32, 40 +(row * 20), 1152, String.Format( "{0}", owner ));
				AddLabel(175, 40 +(row * 20), 1152, String.Format( "{0}", acct ));
				AddLabel(300, 40 +(row * 20), 1152, String.Format( "{0} {1}", loc, map));

				AddButton( 480, 45 +(row * 20), 2437, 2438, btn, GumpButtonType.Reply, 0 );
      				if (from.AccessLevel >= AccessLevel.GameMaster)
				AddButton( 500, 45 +(row * 20), 2437, 2438, btn2, GumpButtonType.Reply, 0 );
				}
				catch {}

			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			int buttonID = info.ButtonID;
			if ( buttonID == 2 )
			{
				m_Page ++;
				from.CloseGump( typeof( FindHouseGump ) );
				from.SendGump( new FindHouseGump( from, m_List, m_Page ) );
			}
			if ( buttonID == 1 )
			{
				m_Page --;
				from.CloseGump( typeof( FindHouseGump ) );
				from.SendGump( new FindHouseGump( from, m_List, m_Page ) );
			}
			if ( buttonID > 100 && buttonID < 1100 )
			{
				int index = buttonID - 101;
				BaseHouse House = m_List[index] as BaseHouse;
				Point3D xyz = House.GetWorldLocation();
				int x = xyz.X;
				int y = xyz.Y;
				int z = xyz.Z + 7;

				Point3D dest = new Point3D( x, y, z );
				from.MoveToWorld( dest, House.Map );
				
			}
			if ( buttonID > 1100 )
			{
				int index = buttonID - 1101;
				BaseHouse House = m_List[index] as BaseHouse;
				from.SendGump( new PropertiesGump( from, House ) );
			}
		}
	}
}