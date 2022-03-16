  //
 //  Written by Haazen June 2005
//
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
	public class ContractBook : Item
	{
		private ArrayList m_Entries;
		private int m_DefaultIndex;


		[Constructable]
		public ContractBook( ) : base( Core.AOS ? 0x22C5 : 0xEFA )
		{
			Weight = (Core.SE ? 1.0 : 3.0);
			LootType = LootType.Blessed;
			Hue = 90;
			Name = "ContractBook";

			Layer = Layer.OneHanded;
			m_Entries = new ArrayList();
			m_DefaultIndex = -1;

		}


		public ArrayList Entries
		{
			get
			{
				return m_Entries;
			}
		}

		public ContractBookEntry Default
		{
			get
			{
				if ( m_DefaultIndex >= 0 && m_DefaultIndex < m_Entries.Count )
					return (ContractBookEntry)m_Entries[m_DefaultIndex];

				return null;
			}
			set
			{
				if ( value == null )
					m_DefaultIndex = -1;
				else
					m_DefaultIndex = m_Entries.IndexOf( value );
			}
		}

		public ContractBook( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.CheckAlive() && IsChildOf( from.Backpack ) )
				list.Add( new NameBookEntry( from, this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( m_Entries.Count );

			for ( int i = 0; i < m_Entries.Count; ++i )
				((ContractBookEntry)m_Entries[i]).Serialize( writer );
			writer.Write( m_DefaultIndex );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			LootType = LootType.Blessed;

			if( Core.SE && Weight == 3.0 )
				Weight = 1.0;

			int version = reader.ReadInt();

			int count = reader.ReadInt();

			m_Entries = new ArrayList( count );

			for ( int i = 0; i < count; ++i )
				m_Entries.Add( new ContractBookEntry( reader ) );

			m_DefaultIndex = reader.ReadInt();


		}

		public void DropContract( Mobile from, ContractBookEntry e, int index )
		{
			if ( m_DefaultIndex == index )
				m_DefaultIndex = -1;

			m_Entries.RemoveAt( index );

				MonsterContract ms = new MonsterContract( );
				ms.AmountKilled = e.Killed;
				ms.AmountToKill = e.Amount;
				ms.Gen = e.Gen;
				ms.Monster = e.Type;
				ms.Reward = e.Reward;
			ms.Name = "a Contract: " + e.Amount + " " + e.Type + "s";

				from.AddToBackpack( ms );

				from.SendMessage( "You have removed the Monster Contract" );


		}

		public bool IsOpen( Mobile toCheck )
		{
			NetState ns = toCheck.NetState;

		/*	if ( ns == null )
				return false;

			List<Gump> gumps = ns.Gumps;

			for ( int i = 0; i < gumps.Count; ++i )
			{
				if ( gumps[i] is ContractBookGump )
				{
					ContractBookGump gump = (ContractBookGump)gumps[i];

					if ( gump.Book == this )
						return true;
				}
			} */

			if ( ns != null ) {
				foreach ( Gump gump in ns.Gumps ) {
					ContractBookGump bookGump = gump as ContractBookGump;

					if ( bookGump != null && bookGump.Book == this ) {
						return true;
					}
				}
			}

			return false;
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 1 ) )
			{
				CallGump( from );
			//	from.CloseGump( typeof( ContractBookGump ) );
			//	from.SendGump( new ContractBookGump( from, this ) );
			}
		}
		public void CallGump( Mobile from )
		{
			from.CloseGump( typeof( ContractBookGump ) );
			from.SendGump( new ContractBookGump( from, this ) );
		}

		public bool CheckAccess( Mobile m )
		{
			return true;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is MonsterContract )
			{
				if ( !CheckAccess( from ) )
				{
					from.SendLocalizedMessage( 502413 ); // That cannot be done while the book is locked down.
				}
				else if ( IsOpen( from ) )
				{
					from.SendLocalizedMessage( 1005571 ); // You cannot place objects in the book while viewing the contents.
				}
				else if ( m_Entries.Count < 15 )
				{
					MonsterContract mc = (MonsterContract)dropped;


					m_Entries.Add( new ContractBookEntry( mc.Monster, mc.Reward, mc.Gen, mc.AmountToKill, mc.AmountKilled ) );

					dropped.Delete();

					from.Send( new PlaySound( 0x42,GetWorldLocation() ) );

					return true;

				}
				else
				{
					from.SendMessage( "This Book is full" );
				}
			}

			return false;
		}

		private class NameBookEntry : ContextMenuEntry
		{
			private Mobile m_From;
			private ContractBook m_Book;

			public NameBookEntry( Mobile from, ContractBook book ) : base( 6216 )
			{
				m_From = from;
				m_Book = book;
			}

			public override void OnClick()
			{
				if ( m_From.CheckAlive() && m_Book.IsChildOf( m_From.Backpack ) )
				{
					m_From.Prompt = new NameBookPrompt( m_Book );
					m_From.SendLocalizedMessage( 1062479 ); // Type in the new name of the book:
				}
			}
		}

		private class NameBookPrompt : Prompt
		{
			private ContractBook m_Book;

			public NameBookPrompt( ContractBook book )
			{
				m_Book = book;
			}

			public override void OnResponse( Mobile from, string text )
			{
				if ( text.Length > 40 )
					text = text.Substring( 0, 40 );

				if ( from.CheckAlive() && m_Book.IsChildOf( from.Backpack ) )
				{
					m_Book.Name = Utility.FixHtml( text.Trim() );

					from.SendMessage( "This Book name has been changed" );
				}
			}

			public override void OnCancel( Mobile from )
			{
			}
		}
	}

	public class ContractBookEntry
	{
		private string m_type;
		private int m_reward;
		private int m_gen;
		private int m_amount;
		private int m_killed;

		public string Type
		{
			get{ return m_type; }
		}

		public int Reward
		{
			get{ return m_reward; }
		}

		public int Gen
		{
			get{ return m_gen; }
		}

		public int Amount
		{
			get{ return m_amount; }
		}

		public int Killed
		{
			get{ return m_killed; }
		}

		public ContractBookEntry( string type, int reward, int gen, int amount, int killed ) 
		{
			m_type = type;
			m_reward = reward;
			m_gen = gen;
			m_amount = amount;
			m_killed = killed;
		}

		public ContractBookEntry( GenericReader reader )
		{
			int version = reader.ReadByte();

			m_type = reader.ReadString();
			m_reward = reader.ReadInt();
			m_gen = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_killed = reader.ReadInt();

		}

		public void Serialize( GenericWriter writer )
		{

			writer.Write( (byte) 0 ); // version

			writer.Write( m_type );
			writer.Write( m_reward );
			writer.Write( m_gen );
			writer.Write( m_amount );
			writer.Write( m_killed );

		}
	}
}