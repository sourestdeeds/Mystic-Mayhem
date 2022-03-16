//Written by Haazen Mar 2008
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Engines.Craft;
using Server.ContextMenus;
using Server.Prompts;

namespace Server.Items
{
	public class StaffRunebook : Item
	{

		private ArrayList m_Entries;
		private string m_Description;

		public ArrayList Entries
		{
			get
			{
				return m_Entries;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				m_Description = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public StaffRunebook( ) : base( 8788 )
		{
			Weight = (Core.SE ? 1.0 : 3.0);
			LootType = LootType.Blessed;
			Name = "Staff Rune Book";
			Hue = 0x461;

			m_Entries = new ArrayList();
		}

		public StaffRunebook( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_Entries.Count );

			for ( int i = 0; i < m_Entries.Count; ++i )
				((StaffRunebookEntry)m_Entries[i]).Serialize( writer );

			writer.Write( m_Description );


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			int count = reader.ReadInt();

			m_Entries = new ArrayList( count );

			for ( int i = 0; i < count; ++i )
				m_Entries.Add( new StaffRunebookEntry( reader ) );

			m_Description = reader.ReadString();

		}

		public bool CheckAccess( Mobile m )
		{
			return true;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Description != null && m_Description.Length > 0 )
				list.Add( m_Description );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 3 ) )  //aa 1
			{

				from.CloseGump( typeof( StaffRunebookGump ) );
				from.SendGump( new StaffRunebookGump( from, this, 1 ) );
			}
		}

		public bool OnMarkSpot( Mobile from, string text )
		{
			if ( m_Entries.Count >= 900 )
			{
				from.SendMessage( "This Book is Full" );
				return false;
			}
			string ent = text;
			string des = ( ent == "" ? "Indescript" : ent );

			StaffRunebookEntry entry = new StaffRunebookEntry( from.Location, from.Map, des );
			m_Entries.Add( entry );

			return true;
		}

		public void DropRune( Mobile from, StaffRunebookEntry e, int index, int page )
		{
			from.SendGump( new EntryDelGump(from, e, index, this, page ) );
		//	m_Entries.RemoveAt( index );

		//	from.SendLocalizedMessage( 502421 ); // You have removed the rune.
		}

		private class RenamePrompt : Prompt
		{
			private string m_entry;

			public RenamePrompt( string entry )
			{
				m_entry = entry;
			}

			public override void OnResponse( Mobile from, string text )
			{

				m_entry = text;
				from.SendLocalizedMessage( 1010474 ); // The etching on the rune has been changed.

			}
		}
	}

	public class StaffRunebookEntry
	{
		private Point3D m_Location;
		private Map m_Map;
		private string m_Description;

		public Point3D Location
		{
			get{ return m_Location; }
		}

		public Map Map
		{
			get{ return m_Map; }
		}

		public string Description
		{
			get{ return m_Description; }
		}

		public StaffRunebookEntry( Point3D loc, Map map, string desc )
		{
			m_Location = loc;
			m_Map = map;
			m_Description = desc;
		}

		public StaffRunebookEntry( GenericReader reader )
		{
			int version = reader.ReadByte();

			m_Location = reader.ReadPoint3D();
			m_Map = reader.ReadMap();
			m_Description = reader.ReadString();

		}

		public void Serialize( GenericWriter writer )
		{

			writer.Write( (byte) 0 ); // version

			writer.Write( m_Location );
			writer.Write( m_Map );
			writer.Write( m_Description );
		}
	}
}