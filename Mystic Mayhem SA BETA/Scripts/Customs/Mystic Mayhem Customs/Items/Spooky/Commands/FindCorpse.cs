  //
 //  Written by Haazen June 2005
//
using System;
using System.Collections;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class FindCorpseGump : Gump
	{

		public static void Initialize()
		{
			CommandSystem.Register( "FindCorpse", AccessLevel.Counselor, new CommandEventHandler( FindCorpse_OnCommand ) );
		}

		[Usage( "FindCorpse" )]
		[Description( "Finds all player corpses in the world." )]
		public static void FindCorpse_OnCommand( CommandEventArgs e )
		{
			ArrayList list = new ArrayList();

			foreach ( Item item in World.Items.Values )
			{
				if ( item is Corpse )
				{
					Corpse C = item as Corpse;
					if ( C.Owner != null && C.Killer != null && C.Owner.Player )
				//	if ( C.Owner != null && C.Killer != null && (C.Amount == 400 || C.Amount == 401 || C.Amount == 605 || C.Amount == 606) )
					list.Add( C );
				}
			}
			e.Mobile.SendGump( new FindCorpseGump( e.Mobile, list, 1 ) );
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

		public FindCorpseGump( Mobile from, ArrayList list, int page ) : base( 50, 40 )
		{
			from.CloseGump( typeof( FindCorpseGump ) );

			int corpses = 0;
			m_Page = page;
			m_From = from;
			int pageCount = 0;
			m_List = list;

			AddPage( 0 );

			AddBackground( 0, 0, 520, 315, 5054 );

			AddBlackAlpha( 10, 10, 500, 280 );

			if ( m_List == null )
			{
				return;
			}
			else
			{
				corpses = list.Count;

				if ( list.Count % 12 == 0 )
				{
					pageCount = (list.Count / 12);
				}
				else
				{
					pageCount = (list.Count / 12) + 1;
				}
			}

			AddLabelCropped( 32, 16, 100, 20, 1152, "Corpse Name" );
			//AddLabelCropped( 132, 16, 120, 20, 1152, "Owner" );
			AddLabelCropped( 292, 16, 120, 20, 1152, "Location" );
			AddLabel( 80, 290, 93, String.Format( "Paradise Corpse Locator       {0} Player corpses in the land", corpses ));

			if ( page > 1 )
				AddButton( 470, 18, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
			else
				AddImage( 470, 18, 0x25EA );

			if ( pageCount > page )
				AddButton( 487, 18, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
			else
				AddImage( 487, 18, 0x25E6 );

			if ( m_List.Count == 0 )
				AddLabel( 135, 80, 1152, "There are no corpses in the world." );

			if ( page == pageCount )
			{
				for ( int i = (page * 12) -12; i < corpses; ++i )
					AddDetails( i );
			}
			else
			{
				for ( int i = (page * 12) -12; i < page * 12; ++ i )
					AddDetails( i );
			}
		}

		private void AddDetails( int index )
		{
		   try{
			if ( index < m_List.Count )
			{
				int btn;
				int row;
				btn = (index) + 101;
				row = index % 12;
		
				Corpse corpse = m_List[index] as Corpse;

				AddLabel(32, 40 +(row * 20), 1152, String.Format( "{0}", corpse.Name ));
			//	AddLabel(132, 40 +(row * 20), 1152, String.Format( "{0}", corpse.Owner ));
				AddLabel(280, 40 +(row * 20), 1152, String.Format( "{0} {1}", corpse.GetWorldLocation(), corpse.Map));

				AddButton( 480, 45 +(row * 20), 2437, 2438, btn, GumpButtonType.Reply, 0 );

			}
		   }
		   catch{}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			int buttonID = info.ButtonID;
			if ( buttonID == 2 )
			{
				m_Page ++;
				from.CloseGump( typeof( FindCorpseGump ) );
				from.SendGump( new FindCorpseGump( from, m_List, m_Page ) );
			}
			if ( buttonID == 1 )
			{
				m_Page --;
				from.CloseGump( typeof( FindCorpseGump ) );
				from.SendGump( new FindCorpseGump( from, m_List, m_Page ) );
			}
			if ( buttonID > 100 )
			{
				int index = buttonID - 101;
				Corpse corpse = m_List[index] as Corpse;
				Point3D xyz = corpse.GetWorldLocation();
				int x = xyz.X;
				int y = xyz.Y;
				int z = xyz.Z;

				Point3D dest = new Point3D( x, y, z );
				from.MoveToWorld( dest, corpse.Map );
				
			}
		}
	}
}