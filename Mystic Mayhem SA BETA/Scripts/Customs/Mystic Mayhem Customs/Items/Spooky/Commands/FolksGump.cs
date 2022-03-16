using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Accounting;
using Server.Network;
using Server.Gumps;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
// using Knives.Chat3;

namespace Server.Gumps
{
	public class FolksGump : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Folks", AccessLevel.Counselor, new CommandEventHandler( FolksList_OnCommand ) );
		}

		[Usage( "Folks" )]
		[Description( "Lists all connected clients." )]
		private static void FolksList_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new FolksGump( e.Mobile ) );
		}

		public static bool OldStyle = PropsConfig.OldStyle;

		public static readonly int GumpOffsetX = 30;
		public static readonly int GumpOffsetY = 30;

		public static readonly int TextHue = 0;
		public static readonly int TextOffsetX = 2;

		public static readonly int OffsetGumpID = 0x0A40; // Pure black
		public static readonly int HeaderGumpID = 0x0E14; // Dark slate, textured
		public static readonly int  EntryGumpID = 0x0BBC; // Light offwhite, textured
		public static readonly int   BackGumpID = 0x13BE; // Gray slate/stoney
		public static readonly int    SetGumpID = 0x0E14; // Dark slate, textured

		public static readonly int SetWidth = 40;
		public static readonly int SetOffsetX = 4, SetOffsetY = 2;
		public static readonly int SetButtonID1 = 0x15E1; // Arrow pointing right
		public static readonly int SetButtonID2 = 0x15E5; // " pressed

		public static readonly int PrevWidth = 20;
		public static readonly int PrevOffsetX = 2, PrevOffsetY = 2;
		public static readonly int PrevButtonID1 = 0x15E3; // Arrow pointing left
		public static readonly int PrevButtonID2 = 0x15E7; // " pressed

		public static readonly int NextWidth = 20;
		public static readonly int NextOffsetX = 2, NextOffsetY = 2;
		public static readonly int NextButtonID1 = 0x15E1; // Arrow pointing right
		public static readonly int NextButtonID2 = 0x15E5; // " pressed

		public static readonly int OffsetSize = 1;

		public static readonly int EntryHeight = 20;
		public static readonly int BorderSize = 10;

		private static bool PrevLabel = false, NextLabel = false;

		private static readonly int PrevLabelOffsetX = PrevWidth + 1;
		private static readonly int PrevLabelOffsetY = 0;

		private static readonly int NextLabelOffsetX = -29;
		private static readonly int NextLabelOffsetY = 0;

		private static readonly int EntryWidth = 140;
		private static readonly int EntryCount = 15;

		private static readonly int TotalWidth = OffsetSize + EntryWidth + OffsetSize + SetWidth + OffsetSize;
		private static readonly int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

		private static readonly int BackWidth = BorderSize + TotalWidth + BorderSize;
		private static readonly int BackHeight = BorderSize + TotalHeight + BorderSize;

		private Mobile m_Owner;
		private ArrayList m_Mobiles;
		private int m_Page;

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

				Mobile a = x as Mobile;
				Mobile b = y as Mobile;

				if ( a == null || b == null )
					throw new ArgumentException();

				if ( a.AccessLevel > b.AccessLevel )
					return -1;
				else if ( a.AccessLevel < b.AccessLevel )
					return 1;
				else
					return Insensitive.Compare( a.Name, b.Name );
			}
		}

		public FolksGump( Mobile owner ) : this( owner, BuildList( owner ), 0 )
		{
		}

		public FolksGump( Mobile owner, ArrayList list, int page ) : base( GumpOffsetX, GumpOffsetY )
		{
			owner.CloseGump( typeof( FolksGump ) );

			m_Owner = owner;
			m_Mobiles = list;

			Initialize( page );
		}

		public static ArrayList BuildList( Mobile owner )
		{
			ArrayList list = new ArrayList();
			List<NetState> states = NetState.Instances;

			for ( int i = 0; i < states.Count; ++i )
			{
				Mobile m = states[i].Mobile;

				if ( m != null && (m == owner || !m.Hidden || owner.AccessLevel > m.AccessLevel) )
					list.Add( m );
			}

			list.Sort( InternalComparer.Instance );

			return list;
		}

		public void Initialize( int page )
		{
			m_Page = page;

			int count = m_Mobiles.Count - (page * EntryCount);

			if ( count < 0 )
				count = 0;
			else if ( count > EntryCount )
				count = EntryCount;

			int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1));

			AddPage( 0 );

			AddBackground( 0, 0, BackWidth, BorderSize + totalHeight + BorderSize, BackGumpID );
			AddImageTiled( BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID );

			int x = BorderSize + OffsetSize;
			int y = BorderSize + OffsetSize;

			int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

			if ( !OldStyle )
				AddImageTiled( x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID );

			AddLabel( x + TextOffsetX, y, TextHue, String.Format( "FOLKS {0} Pg {1} - {2}", m_Mobiles.Count, page+1, (m_Mobiles.Count + EntryCount - 1) / EntryCount ) );

			x += emptyWidth + OffsetSize;

			if ( OldStyle )
				AddImageTiled( x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID );
			else
				AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID );

			if ( page > 0 )
			{
				AddButton( x + PrevOffsetX, y + PrevOffsetY, PrevButtonID1, PrevButtonID2, 1, GumpButtonType.Reply, 0 );

				if ( PrevLabel )
					AddLabel( x + PrevLabelOffsetX, y + PrevLabelOffsetY, TextHue, "Previous" );
			}

			x += PrevWidth + OffsetSize;

			if ( !OldStyle )
				AddImageTiled( x, y, NextWidth, EntryHeight, HeaderGumpID );

			if ( (page + 1) * EntryCount < m_Mobiles.Count )
			{
				AddButton( x + NextOffsetX, y + NextOffsetY, NextButtonID1, NextButtonID2, 2, GumpButtonType.Reply, 1 );

				if ( NextLabel )
					AddLabel( x + NextLabelOffsetX, y + NextLabelOffsetY, TextHue, "Next" );
			}

			for ( int i = 0, index = page * EntryCount; i < EntryCount && index < m_Mobiles.Count; ++i, ++index )
			{
				x = BorderSize + OffsetSize;
				y += EntryHeight + OffsetSize;

				Mobile m = (Mobile)m_Mobiles[index];
				PlayerMobile pm = m as PlayerMobile;
				string ei = pm.ExtraInt.ToString();

				AddImageTiled( x, y, EntryWidth, EntryHeight, EntryGumpID );
				AddLabelCropped( x + TextOffsetX, y, EntryWidth - TextOffsetX, EntryHeight, GetHueFor( m ), m.Deleted ? "(deleted)" : m.Name );
			AddLabelCropped( x + 110 + TextOffsetX, y, 50, EntryHeight, pm.ExtraInt > 0 ? 1369 : 140, pm.ExtraInt == 0 ? " " : ei );

				x += EntryWidth + OffsetSize;

				if ( SetGumpID != 0 )
					AddImageTiled( x, y, SetWidth, EntryHeight, SetGumpID );

				if ( m.NetState != null && !m.Deleted )
					AddButton( x + SetOffsetX, y + SetOffsetY, 11401, 11402, i + 3, GumpButtonType.Reply, 0 );
					AddButton( x + 20 + SetOffsetX, y + 2 + SetOffsetY, 0x939, 0x939, i + 1003, GumpButtonType.Reply, 0 );
			}
		}

		private static int GetHueFor( Mobile m )
		{
			switch ( m.AccessLevel )
			{
				case AccessLevel.Owner: return 0x516;
				case AccessLevel.Administrator: return 0x516;
				case AccessLevel.Seer: return 0x144;
				case AccessLevel.GameMaster: return 0x21;
				case AccessLevel.Counselor: return 0x18B;
				case AccessLevel.Player: default:
				{
					if ( m.Kills >= 5 )
						return 0x21;
					else if ( m.Criminal )
						return 0x3B1;

					return 0; //x58;
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch ( info.ButtonID )
			{
				case 0: // Closed
				{
					return;
				}
				case 1: // Previous
				{
					if ( m_Page > 0 )
					{
						from.CloseGump( typeof(FolksGump) );
						from.SendGump( new FolksGump( from, m_Mobiles, m_Page - 1 ) );
					}

					break;
				}
				case 2: // Next
				{
					if ( (m_Page + 1) * EntryCount < m_Mobiles.Count )
					{
						from.CloseGump( typeof(FolksGump) );
						from.SendGump( new FolksGump( from, m_Mobiles, m_Page + 1 ) );
					}

					break;
				}
				default:
				{
					int index = (m_Page * EntryCount) + (info.ButtonID - 3);
// client button

					if ( index > 999 )
						index -= 1000;
//from.SendMessage ("{0}  {1}", info.ButtonID, index);

					if ( index >= 0 && index < m_Mobiles.Count )
					{
						Mobile m = (Mobile)m_Mobiles[index];

						if ( m.Deleted )
						{
							from.SendMessage( "That player has deleted their character." );
							from.CloseGump( typeof(FolksGump) );
							from.SendGump( new FolksGump( from, BuildList( from ), m_Page ) );
						}
						else if ( m.NetState == null )
						{
							from.SendMessage( "That player is no longer online." );
							from.CloseGump( typeof(FolksGump) );
							from.SendGump( new FolksGump( from, BuildList( from ), m_Page ) );
						}
						else if ( m == m_Owner || !m.Hidden || m_Owner.AccessLevel > m.AccessLevel )
						{
	//	FolksGump.WriteLine(from, m, m.NetState);
//aa
	if ( info.ButtonID > 1000 )
	{
							from.CloseGump( typeof(FolksGump) );
							from.SendGump( new FolksGump( from, BuildList( from ), m_Page ) );
from.SendGump( new ClientGump( from, m.NetState ) );
	}
	else
						{
							from.MoveToWorld( m.Location, m.Map );
							from.CloseGump( typeof(FolksGump) );
							from.SendGump( new FolksGump( from, BuildList( from ), m_Page ) );
						}

//aa
						}
						else
						{
							from.SendMessage( "You cannot see them." );
							from.CloseGump( typeof(FolksGump) );
							from.SendGump( new FolksGump( from, BuildList( from ), m_Page ) );
						}
					}

					break;
				}
			}

		}
		public static void AppendPath( ref string path, string toAppend )
		{
			path = Path.Combine( path, toAppend );

			if ( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
		}

	/*	public static void WriteLine( Mobile from, Mobile tst, NetState state )
		{

			try
			{
				string path = Core.BaseDirectory;

				Account acct = from.Account as Account;
				Account tact = tst.Account as Account;

				string name = ( acct == null ? from.Name : acct.Username );
				string toast =  tst.Name ;

				AppendPath( ref path, "Logs" );
				AppendPath( ref path, "Commands" );
				AppendPath( ref path, "Folks" );
				path = Path.Combine( path, String.Format("Folks.log") );

				using ( StreamWriter sw = new StreamWriter( path, true ) )
					sw.WriteLine( "{0}: {1}: Acct={2}: Name={3}: {4}: {5}", DateTime.Now, name, tact, toast, state.ToString(), state.Version == null ? "(null)" : state.Version.ToString() );
			}
			catch
			{
			}
		} */
	}
}