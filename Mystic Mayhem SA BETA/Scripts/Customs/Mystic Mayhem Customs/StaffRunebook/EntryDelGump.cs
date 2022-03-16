using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;

namespace Server.Gumps
{
	public class EntryDelGump : Gump
	{
		private Mobile m_From;
		private int m_Page;
		private StaffRunebook m_Book;
		private int m_Index;


		public EntryDelGump( Mobile from, StaffRunebookEntry e, int index, StaffRunebook book, int page ) : base( 100, 25 )
		{
			m_From = from;
			m_Page = page;
			m_Book = book;
			m_Index = index;

			m_From.CloseGump( typeof( EntryDelGump ) );

			AddBackground( 10, 180, 200, 110, 9270 );
			AddBackground( 20, 190, 181, 91, 3000 );

			AddRadio( 35, 235, 9721, 9724, true, 1 ); // accept/yes radio
			AddRadio( 135, 235, 9721, 9724, false, 2 ); // decline/no radio
			AddHtmlLocalized(72, 235, 200, 30, 1049016, 0x7fff , false , false ); // Yes
			AddHtmlLocalized(172, 235, 200, 30, 1049017, 0x7fff , false , false ); // No
			AddButton( 80, 255, 2130, 2129, 3, GumpButtonType.Reply, 0 ); // Okay button


			AddLabel( 80, 190, 93, "Remove" );
			AddLabel( 40, 210, 88, String.Format( "{0}", e.Description ) );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			if(info == null || state == null || state.Mobile == null) return;
            
			int radiostate = -1;
			if(info.Switches.Length > 0)
			{
				radiostate = info.Switches[0];
			}
			switch(info.ButtonID)
			{
				default:
				{
					if(radiostate == 1 ) // && m_Item != null )
					{    // accept
						m_Book.Entries.RemoveAt( m_Index );
					}

					break;
				}
			}

			from.SendGump( new StaffRunebookGump( from, m_Book, m_Page ) );

		}
	}

}

