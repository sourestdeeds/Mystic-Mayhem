using System;
using Server;
using System.Collections;
using System.Reflection;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class MonsterContractGump : Gump
	{
		private MonsterContract MCparent;
		
		public MonsterContractGump( Mobile from, MonsterContract parentMC ) : base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			from.CloseGump( typeof( MonsterContractGump ) );
			this.AddPage(0);
			this.AddBackground(0, 0, 300, 170, 5170);
			this.AddLabel(40, 40, 0, @"A Contract for: " + parentMC.AmountToKill + " " + parentMC.Monster);
			this.AddLabel(40, 60, 0, @"Amount Killed: " + parentMC.AmountKilled);
			this.AddLabel(40, 80, 0, @"Reward: " + parentMC.Reward);
			if ( parentMC.AmountKilled == 0 )
			{
				this.AddButton(150, 110, 2061, 2062, 3, GumpButtonType.Reply, 0);
				this.AddLabel(164, 108, 0, @"Cancel Contract");
			}
			if ( parentMC.AmountKilled != parentMC.AmountToKill )
			{
				this.AddButton(40, 110, 2061, 2062, 1, GumpButtonType.Reply, 0);
				this.AddLabel(54, 108, 0, @"Claim Corpse");
			//	this.AddButton(145, 110, 2061, 2062, 3, GumpButtonType.Reply, 0);
			//	this.AddLabel(159, 108, 0, @"Cancel Contract");
			}
			else
			{
				this.AddButton(40, 110, 2061, 2062, 2, GumpButtonType.Reply, 0);
				this.AddLabel(54, 108, 0, @"Claim Reward");
			}

			MCparent = parentMC;
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m_from = state.Mobile;
			
			if ( info.ButtonID == 1 )
			{
				m_from.SendMessage("Please choose the corpse to add.");
				m_from.Target = new MonsterCorpseTarget( MCparent );
			}
			if ( info.ButtonID == 2 && MCparent.AmountKilled == MCparent.AmountToKill)
			{
				MCparent.Delete();
				m_from.SendMessage("The reward has been placed in your bank!");
				m_from.BankBox.DropItem( new BankCheck( MCparent.Reward ) );
			}
			if ( info.ButtonID == 3 )
			{
				MCparent.Delete();
				m_from.SendMessage("The Contract has been canceled. A partial fee has been refunded!");
				m_from.AddToBackpack( new Gold(30) );
			}
		}
		
		private class MonsterCorpseTarget : Target
		{
			private MonsterContract MCparent;
			
			public MonsterCorpseTarget( MonsterContract parentMC ) : base( 15, false, TargetFlags.None )
			{
				MCparent = parentMC;
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
						string m_type = "a " + MCparent.Monster;
						m_type = m_type.ToLower();
						string m_type2 = "an " + MCparent.Monster;
						m_type2 = m_type2.ToLower();
						string m_corpse = MCcorpse.Owner.Name;
						m_corpse = m_corpse.ToLower();
//from.SendMessage (" type {0}  type2 {1}  corpse {2}", m_type, m_type2, m_corpse);
						
						if ( m_type == m_corpse || m_type2 == m_corpse )
						{
							MCparent.AmountKilled += 1;
							MCcorpse.Delete();	
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
