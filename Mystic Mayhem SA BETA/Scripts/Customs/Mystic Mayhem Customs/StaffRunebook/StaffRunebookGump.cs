  //
 //  Written by Haazen June 2005
//
using System;
using System.Collections;
using Server;
using Server.Commands;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class StaffRunebookGump : Gump
	{

		private StaffRunebook m_Book;

		public StaffRunebook Book{ get{ return m_Book; } }

		private int m_DefaultIndex;
		private int m_Page;
		private Mobile m_From;

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		public StaffRunebookGump( Mobile from, StaffRunebook book, int page ) : base( 550, 200 )
		{
			m_Book = book;
			from.CloseGump( typeof( StaffRunebookGump ) );

			int entries = 0;
			m_Page = page;
			m_From = from;
			int pageCount = 0;

			AddPage( 0 );

			AddBackground( 0, 0, 415, 320, 9270 );
			AddBackground( 10, 10, 197, 273, 3000 );
			AddBackground( 208, 10, 197, 273, 3000 );

			AddBackground( 110, 285, 130, 20, 3000 );
		//	AddBackground( 238, 60, 5, 246, 257 );


			AddLabelCropped( 52, 16, 100, 20, 93, "Desctrption" );
			AddLabelCropped( 252, 16, 100, 20, 93, "Desctrption" );
		//	AddLabelCropped( 260, 16, 120, 20, 93, "Location" );
		//	AddLabelCropped( 390, 16, 120, 20, 93, "Facet" );
		//	AddLabel( 35, 285, 93, "Staff Rune Book");
			AddLabel( 20, 285, 93, "Mark Spot");
			AddLabel( 325, 285, 93, String.Format( "Page {0}", page ));
			AddButton( 90, 288, 0x15E2, 0x15E6, 10, GumpButtonType.Reply, 0 );
			AddTextEntry( 110, 285, 200, 20, 10, 20, "" );
			AddButton( 390, 290, 0x938,0x938, 3, GumpButtonType.Reply, 0 ); //rename

			if ( m_Book.Entries.Count == 0 )
			{
				return;
			}
			else
			{
				entries = m_Book.Entries.Count;

				if ( entries % 24 == 0 )
				{
					pageCount = (entries / 24);
				}
				else
				{
					pageCount = (entries / 24) + 1;
				}
			}


			if ( page > 1 )
				AddButton( 270, 288, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
			else
				AddImage( 270, 288, 0x25EA );

			if ( pageCount > page )
				AddButton( 287, 288, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
			else
				AddImage( 287, 288, 0x25E6 );

			if ( m_Book.Entries.Count == 0 )
				AddLabel( 135, 80, 1152, "There are no location." );

			if ( page == pageCount )
			{
				for ( int i = (page * 24) -24; i < entries; ++i )
					AddDetails( i );
			}
			else
			{
				for ( int i = (page * 24) -24; i < page * 24; ++ i )
					AddDetails( i );
			}
		}

		private void AddDetails( int index )
		{
		   try{
			if ( index < m_Book.Entries.Count )
			{
				StaffRunebookEntry e = (StaffRunebookEntry)m_Book.Entries[index];

				string desc = e.Description;

				int btn;
				int row;
				int offset;
				btn = (index) + 101;
				row = index % 12;
				if ( index % 24 < 12 )
					offset = 0;
				else
					offset = 200;
		

				AddLabel(52 + offset, 40 +(row * 20), 395, String.Format( "{0}", e.Description ));
			//	AddLabel(260, 40 +(row * 20), 395, String.Format( "{0}", e.Location ));
			//	AddLabel(390, 40 +(row * 20), 395, String.Format( "{0}", e.Map));

				AddButton( 20 + offset, 40 +(row * 20), 5601, 5605, btn, GumpButtonType.Reply, 0 ); // goto
				AddButton( 182 + offset, 45 +(row * 20), 2437, 2438, btn + 1000, GumpButtonType.Reply, 0 ); // delete
			//	AddButton( 250, 45 +(row * 20), 2087, 2087, btn + 2000, GumpButtonType.Reply, 0 ); // description


			}
		   }
		   catch{}
		}


		private class InternalPrompt : Prompt
		{
			private StaffRunebook m_Book;

			public InternalPrompt( StaffRunebook book )
			{
				m_Book = book;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( m_Book.Deleted || !from.InRange( m_Book.GetWorldLocation(), 1 ) )
					return;

				if ( m_Book.CheckAccess( from ) )
				{
					m_Book.Description = Utility.FixHtml( text.Trim() );

					from.CloseGump( typeof( StaffRunebookGump ) );
					from.SendGump( new StaffRunebookGump( from, m_Book, 1 ) );

					from.SendMessage( "The book's title has been changed." );
				}
				else
				{
					from.SendLocalizedMessage( 502416 ); // That cannot be done while the book is locked down.
				}
			}

			public override void OnCancel( Mobile from )
			{
				from.SendLocalizedMessage( 502415 ); // Request cancelled.

				if ( !m_Book.Deleted && from.InRange( m_Book.GetWorldLocation(), 1 ) )
				{
					from.CloseGump( typeof( StaffRunebookGump ) );
					from.SendGump( new StaffRunebookGump( from, m_Book, 1 ) );
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			int buttonID = info.ButtonID;

			if ( buttonID == 3 ) // Rename book
			{
				if ( m_Book.CheckAccess( from ) )
				{
					from.SendLocalizedMessage( 502414 ); // Please enter a title for the runebook:
					from.Prompt = new InternalPrompt( m_Book );
				}
				else
				{
					from.SendLocalizedMessage( 502413 ); // That cannot be done while the book is locked down.
				}
			}

			if ( buttonID == 2 )
			{
				m_Page ++;
				from.CloseGump( typeof( StaffRunebookGump ) );
				from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );
			}
			if ( buttonID == 1 )
			{
				m_Page --;
				from.CloseGump( typeof( StaffRunebookGump ) );
				from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );
			}
			if ( buttonID == 10 )
			{
				from.CloseGump( typeof( StaffRunebookGump ) );
				TextRelay entry = info.GetTextEntry( 20 );
				string text = ( entry == null ? "" : entry.Text.Trim() );
				m_Book.OnMarkSpot( from, text );
				from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );
			}

			int index = 0;
			StaffRunebookEntry e = null;

			if ( buttonID > 100 && buttonID < 1000)
			{
				index = buttonID - 101;
				e = (StaffRunebookEntry)m_Book.Entries[index];

				Point3D xyz = e.Location;
				int x = xyz.X;
				int y = xyz.Y;
				int z = xyz.Z;

				Point3D dest = new Point3D( x, y, z );
				from.MoveToWorld( dest, e.Map );
				from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );
				
			}

			if ( buttonID > 1000 && buttonID < 2000 )
			{
				index = buttonID - 1101;
				e = (StaffRunebookEntry)m_Book.Entries[index];
				m_Book.DropRune( from, e, index, m_Page );

			//	from.CloseGump( typeof( StaffRunebookGump ) );
			//	from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );
			}
		/*	if ( buttonID > 2000 )
			{
				index = buttonID - 2101;

				e = (StaffRunebookEntry)m_Book.Entries[index];
				from.CloseGump( typeof( StaffRunebookGump ) );
				from.SendGump( new DescGump( from, m_Page, e ) );
				from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );

			}
		*/
		}
	}
}