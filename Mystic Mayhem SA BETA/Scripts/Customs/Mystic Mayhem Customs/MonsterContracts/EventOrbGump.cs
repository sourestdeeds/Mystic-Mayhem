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
	public class EventOrbGump : Gump
	{
		private EventOrb MCparent;
		
		public EventOrbGump( Mobile from, EventOrb parentMC ) : base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			from.CloseGump( typeof( EventOrbGump ) );
			this.AddPage(0);
			this.AddBackground(0, 0, 300, 170, 5170);
			this.AddLabel(40, 40, 0, @"A Contract for: " + parentMC.Monster);
			this.AddLabel(40, 60, 0, @"Amount Killed: " + parentMC.AmountKilled);

			this.AddButton(40, 110, 2061, 2062, 1, GumpButtonType.Reply, 0);
			this.AddLabel(54, 108, 0, @"Claim Corpse");


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
		}
		
		private class MonsterCorpseTarget : Target
		{
			private EventOrb MCparent;
			
			public MonsterCorpseTarget( EventOrb parentMC ) : base( 15, false, TargetFlags.None )
			{
				MCparent = parentMC;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Corpse )
				{
					Corpse MCcorpse = (Corpse)o;
					
					if ( MCcorpse.Channeled )
					{
						from.SendMessage("This corpse has been desecrated and can not be claimed!");
						return;
					}

						string m_type = "a " + MCparent.Monster;
						m_type = m_type.ToLower();
						string m_type2 = "an " + MCparent.Monster;
						m_type2 = m_type2.ToLower();
						string m_corpse = MCcorpse.Owner.Name;
						m_corpse = m_corpse.ToLower();
						
						if ( m_type == m_corpse || m_type2 == m_corpse )
						{
							MCparent.AmountKilled += 1;
							MCcorpse.Delete();
							MCparent.CallGump( from );	
						}
						else
							from.SendMessage("That corpse is not of the correct type!");

				}
				else
					from.SendMessage("That is not a corpse");
				//from.SendGump( new EventOrbGump( from, this ) );
			}
		}
	}
}
