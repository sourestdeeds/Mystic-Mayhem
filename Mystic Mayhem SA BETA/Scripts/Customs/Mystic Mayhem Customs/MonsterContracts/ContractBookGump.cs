  //
 //  Written by Haazen June 2005
//
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
//using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class ContractBookGump : Gump
	{
		private ContractBook m_Book;

		public ContractBook Book{ get{ return m_Book; } }


		public string GetName( string name )
		{
			if ( name == null || (name = name.Trim()).Length <= 0 )
				return "(indescript)";

			return name;
		}

		private void AddBackground()
		{
			AddPage( 0 );

			this.AddBackground(0, 0, 370, 385, 9380);
			this.AddLabel(130,5,0,"Monster Contracts");

		}

		private void AddDetails( int index )
		{
			if ( index < m_Book.Entries.Count )
			{
				int btn;
				btn = (index * 2) + 1;
		
				ContractBookEntry e = (ContractBookEntry)m_Book.Entries[index];

				int amt = e.Amount, kills = e.Killed;
				int rwd = e.Reward;
				string type = e.Type;
				AddLabel(60, 40 +(index * 20), 0, type);
				AddLabel(218, 40 +(index * 20), 0, String.Format( "{0} of {1}", kills, amt ));
				AddLabel(282, 40 +(index * 20), 0, String.Format( "{0}", rwd));

				// buttons

				AddButton( 330, 45 +(index * 20), 2437, 2438, btn, GumpButtonType.Reply, 0 );
				
				if ( kills < amt )
				{
				AddButton( 33, 45 +(index * 20), 216, 216, btn + 1, GumpButtonType.Reply, 0 );
				}

			}
		}

		public ContractBookGump( Mobile from, ContractBook book ) : base( 150, 200 )
		{
			m_Book = book;

			AddBackground();

			for ( int i = 0; i < m_Book.Entries.Count; ++i )
			{
				AddDetails( i );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			if ( m_Book.Deleted || !from.InRange( m_Book.GetWorldLocation(), 1 ) || !Multis.DesignContext.Check( from ) )
				return;

			int buttonID = info.ButtonID;

			int index = (buttonID / 2);
			int drp = buttonID % 2; // 1 = drop 0 = teleport

			if ( index >= 0 && index < m_Book.Entries.Count && drp == 1 )
			{
				ContractBookEntry e = (ContractBookEntry)m_Book.Entries[index];

				if ( m_Book.CheckAccess( from ) )
				{
					m_Book.DropContract( from, e, index );
					from.CloseGump( typeof( ContractBookGump ) );
					from.SendGump( new ContractBookGump( from, m_Book ) );
				}
				else
				{
					from.SendLocalizedMessage( 502413 ); // That cannot be done while the book is locked down.
				}

			}
			 else	if (index >= 1 && index < m_Book.Entries.Count +1 && drp == 0)
			{

				index = index - 1;
				
				ContractBookEntry e = (ContractBookEntry)m_Book.Entries[index];

				from.SendMessage("Please choose the corpse to add.");
				from.Target = new MonsterCorpseTarget( e, m_Book, index );

			}  
		}

		private class MonsterCorpseTarget : Target
		{
			private ContractBookEntry MC;
			private ContractBook CB;
			private int indx;
			
			public MonsterCorpseTarget( ContractBookEntry e, ContractBook bk, int index ) : base( 15, false, TargetFlags.None )
			{
				MC = e;
				indx = index;
				CB = bk;
			}


			protected override void OnTarget( Mobile from, object o )
			{
				try
				{
				if ( o is Corpse )
				{
					Corpse MCcorpse = (Corpse)o;
					
					if ( MCcorpse.Channeled )
					{
						from.SendMessage("This corpse has been desecrated and can not be claimed!");
						return;
					}
				//	if ( MCcorpse.Killer == from )
					if ( MCcorpse.CanLoot( from, null ) )
					{

						string type = MC.Type;
						int reward = MC.Reward;
						int gen = MC.Gen;
						int amount = MC.Amount;
						int killed = MC.Killed;

						string m_type = "a " + MC.Type;
						m_type = m_type.ToLower();
						string m_type2 = "an " + MC.Type;
						m_type2 = m_type2.ToLower();
						string m_corpse = MCcorpse.Owner.Name;
						m_corpse = m_corpse.ToLower();

						if ( m_type == m_corpse || m_type2 == m_corpse )
						{
							CB.Entries.RemoveAt( indx );
							killed += 1;
							CB.Entries.Add( new ContractBookEntry( type, reward, gen, amount, killed ) );
							MCcorpse.Delete();
							CB.CallGump( from );
	
						}
						else
							from.SendMessage("That corpse is not of the correct type!");
					}
					else
						from.SendMessage("You cannot claim someone elses work!");
				}
				else
					from.SendMessage("That is not a corpse");
				}
				catch{from.SendMessage("That Corpse is lost");}
			}
		}
	}
}