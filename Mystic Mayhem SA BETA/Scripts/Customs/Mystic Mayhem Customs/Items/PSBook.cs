using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Targeting;
using System.Collections;
using Server.Network;
using Server.Multis;
using Server.Gumps;

namespace Server.Items
{
	#region Book
	public class PSBook : Item, ISecurable
	{
		private ArrayList m_Entries;
		public ArrayList Entries{get{ return m_Entries; }}

 		private Mobile m_Owner;
		private SecureLevel m_Level;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SecureLevel Level
		{
			get{ return m_Level; }
			set{ m_Level = value; }
		}

		[Constructable]
		public PSBook() : base(8793)
		{
			Weight = 1.0;
		//	LootType = LootType.Blessed;
			m_Entries = new ArrayList();

			Hue = 1153;
			Name = "PowerScroll Book";
			m_Level = SecureLevel.Anyone;
		}

		public PSBook(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);

			writer.WriteEncodedInt( (int) m_Entries.Count );

			for ( int i = 0; i < m_Entries.Count; ++i )
			{
				PowerScroll scroll = m_Entries[i] as PowerScroll;
				int skill = (int)scroll.Skill;
				writer.WriteEncodedInt(skill);
				double amount = scroll.Value;
				writer.Write(amount);
			}
			writer.Write( m_Owner );
			writer.Write( (int) m_Level );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			int count = reader.ReadEncodedInt();

			m_Entries = new ArrayList( count );

			for ( int i = 0; i < count; ++i )
			{
				SkillName skill = (SkillName)reader.ReadEncodedInt();
				double amount = reader.ReadDouble();
				PowerScroll scroll = new PowerScroll(skill,amount);
				m_Entries.Add(scroll);
				scroll.Delete();
			}

			m_Owner = reader.ReadMobile() as PlayerMobile;
			m_Level = (SecureLevel)reader.ReadInt();

		}
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add(1053099 ,"{0}\t{1}","Scrolls in book: ", m_Entries.Count.ToString());
		}
		public override void OnDoubleClick( Mobile from )
		{
		/*	if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "Book must be in your backpack to use it." );
			} */

			if ( m_Owner == null )
			{
				m_Owner = from;
				this.Name = m_Owner.Name.ToString() + "'s PowerScrolls";

				from.SendMessage( "This book has been assigned to you." );
			}
			else if ( m_Entries.Count == 0 )
			{
				from.SendLocalizedMessage( 1062381 );
			}
			else if ( from is PlayerMobile )
			{
				from.CloseGump( typeof( PSBookGump ) );
				from.SendGump( new PSBookGump( from, this ) );
			}
		}
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is PowerScroll )
			{
			if ( m_Owner == null )
			{
				m_Owner = from;
				this.Name = m_Owner.Name.ToString() + "'s PowerScrolls";

				from.SendMessage( "This book has been assigned to you." );
				return false;
			}
			//	if ( !IsChildOf( from.Backpack ) )
			//	{
			//		from.SendMessage( "Book must be in your backpack to use it." );
			//		return false;
			//	}
				if ( m_Entries.Count < 20 )//will hold 20
				{
					PowerScroll scroll = (PowerScroll)dropped;
					this.Entries.Add(scroll);

					InvalidateProperties();

					from.SendMessage( "Scroll added to book." );

					if ( from is PlayerMobile )
					{
						from.CloseGump( typeof( PSBookGump ) );
						from.SendGump( new PSBookGump( from, this ) );
					}

					dropped.Delete();
					return true;
				}
				else
				{
					from.SendMessage( "The book is full." ); 
					return false;
				}
			}

			from.SendMessage( "That is not a powerscroll." ); 
			return false;
		}
	}
	#endregion
	#region Gump
	public class PSBookGump : Gump
	{
		private int y;
		public Mobile m_From;
		public PSBook m_Book;

		public PSBookGump(Mobile from, PSBook book)	: base( 0, 0 )
		{
			m_From = from;
			m_Book = book;

			y = (m_Book.Entries.Count -1)*20;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);

                        AddBackground( 10, 50, 460, 265, 9270 ); // x was 230  120+y
                        AddBackground( 22, 60, 211, 246, 3000 ); //-17 -19  101+y
			AddBackground( 248, 60, 211, 246, 3000); // 101+y

			AddBackground( 238, 60, 5, 246, 2701 );

			AddLabel(65, 65, 190, "PowerScroll Book");
			AddLabel(36, 83, 199, "Skill");
			AddLabel(146, 83, 199, "Value");
			if ( from == m_Book.Owner )
				AddLabel(191, 83, 199, "Drop");

		//	string lab = "Owner is " + m_Book.Owner.Name.ToString();

			AddLabel( 295, 65, 190, "Owner is " );
			AddLabel( 350, 65, 190, m_Book.Owner.Name.ToString() );
			AddLabel(260, 83, 199, "Skill");
			AddLabel(370, 83, 199, "Value");
			if ( from == m_Book.Owner )
				AddLabel(415, 83, 199, "Drop");
			
			int y2 = 0;
			int offset = 0;
			int butNumb = 1234;
			for(int i = 0; i < m_Book.Entries.Count; i++)
			{
				PowerScroll scroll = m_Book.Entries[i] as PowerScroll;
				if ( i == 10 )
					y2 = 0;
				if ( i > 9 )
					offset = 225;
				else
					offset = 0;
				AddLabel(32 + offset, 100+y2, 195, scroll.Skill.ToString());
				AddLabel(152 + offset, 100+y2, 195, scroll.Value.ToString());
				if ( from == m_Book.Owner )
					AddButton(197 + offset, 103+y2, 1209, 1210, butNumb, GumpButtonType.Reply, 0);
				y2+=20;
				butNumb++;
			}
		}		
		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			int bp;//button pushed
			switch(info.ButtonID)
			{
				case 1234:bp = 0;break;
				case 1235:bp = 1;break;
				case 1236:bp = 2;break;
				case 1237:bp = 3;break;
				case 1238:bp = 4;break;
				case 1239:bp = 5;break;
				case 1240:bp = 6;break;
				case 1241:bp = 7;break;
				case 1242:bp = 8;break;
				case 1243:bp = 9;break;
				case 1244:bp = 10;break;
				case 1245:bp = 11;break;
				case 1246:bp = 12;break;
				case 1247:bp = 13;break;
				case 1248:bp = 14;break;
				case 1249:bp = 15;break;
				case 1250:bp = 16;break;
				case 1251:bp = 17;break;
				case 1252:bp = 18;break;
				case 1253:bp = 19;break;
				default:return;//break;
			}

			PowerScroll scroll = m_Book.Entries[bp] as PowerScroll;
			SkillName sklnm = scroll.Skill;
			double sklval = scroll.Value;
			PowerScroll newScroll = new PowerScroll(sklnm, sklval);
			m_From.AddToBackpack(newScroll);
			m_Book.Entries.RemoveAt(bp);
			m_Book.InvalidateProperties();
				m_From.CloseGump( typeof( PSBookGump ) );
			if(m_Book.Entries.Count > 0)
				m_From.SendGump( new PSBookGump( m_From, m_Book ) );
		}
	}
	#endregion
}