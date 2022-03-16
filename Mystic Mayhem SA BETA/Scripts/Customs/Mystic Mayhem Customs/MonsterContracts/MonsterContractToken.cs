using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{

	public class MonsterContractToken : Item
	{
		public Timer m_DeleteTimer;
		private DateTime m_TimeEnd;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime TimeEnd
		{
			get{ return m_TimeEnd; }
			set{ m_TimeEnd = value; }
		}

		[Constructable]
		public MonsterContractToken() : base (0x23C)
		{
			Weight = 1.0;
			Name = "monster contract token";

			m_DeleteTimer = new DeleteTimer( this );
			m_DeleteTimer.Start();
			m_TimeEnd = DateTime.Now + TimeSpan.FromHours( 24.2 );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.CloseGump( typeof( InternalGump ) );
				from.SendGump( new InternalGump( from, this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class InternalGump : Gump
		{
			private MonsterContractToken m_Token;
			private Mobile m_From;

			public InternalGump( Mobile from, MonsterContractToken token ) : base( 150, 50 )
			{
				m_Token = token;
				m_From = from;

				AddBackground( 0, 0, 350, 250, 0xA28 );

				AddButton( 60, 25, 0x868, 0x869, 1, GumpButtonType.Reply, 0 );
				AddLabel( 105, 30, 0x486, "Newbie Monster Contract" ); 						AddButton( 60, 60, 0x868, 0x869, 2, GumpButtonType.Reply, 0 );
				AddLabel( 105, 65, 0x486, "Novice Monster Contract" );
				AddButton( 60, 95, 0x868, 0x869, 3, GumpButtonType.Reply, 0 );
				AddLabel( 105, 100, 0x486, "Advanced Monster Contract" );
				AddButton( 60, 130, 0x868, 0x869, 4, GumpButtonType.Reply, 0 );
				AddLabel( 105, 135, 0x486, "Expert Monster Contract" );
				AddButton( 60, 165, 0x868, 0x869, 5, GumpButtonType.Reply, 0 );
				AddLabel( 105, 170, 0x486, "Master Monster Contract" );
				AddButton( 60, 200, 0x868, 0x869, 6, GumpButtonType.Reply, 0 );
				AddLabel( 105, 205, 0x486, "Grand Master Monster Contract" ); 
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Token.Deleted || info.ButtonID == 0 )
					return;

				if ( info.ButtonID == 1 )
				{
					MonsterContract ms = new MonsterContract( 0 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
				if ( info.ButtonID == 2 )
				{
					MonsterContract ms = new MonsterContract( 1 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
				if ( info.ButtonID == 3 )
				{
					MonsterContract ms = new MonsterContract( 2 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
				if ( info.ButtonID == 4 )
				{
					MonsterContract ms = new MonsterContract( 3 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
				if ( info.ButtonID == 5 )
				{
					MonsterContract ms = new MonsterContract( 4 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
				if ( info.ButtonID == 6 )
				{
					MonsterContract ms = new MonsterContract( 5 );
					m_From.AddToBackpack( ms );
					m_From.CloseGump( typeof( InternalGump ) );
					m_Token.Delete();
				}
			}
		}

		public MonsterContractToken( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
			writer.Write( m_TimeEnd );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
			m_TimeEnd = reader.ReadDateTime();
			m_DeleteTimer = new DeleteTimer( this );
			m_DeleteTimer.Start();
		}
		private class DeleteTimer : Timer
		{ 
			private MonsterContractToken di;


			public DeleteTimer( MonsterContractToken item ) : base( TimeSpan.FromMinutes( 20.0 ), TimeSpan.FromMinutes( 20.0 ) )
			{ 
				di = item;
			}

			protected override void OnTick() 
			{
				if ( di.Deleted )
				{
					Stop();
					return;
				}
			
				if ( DateTime.Now >= di.m_TimeEnd )
				{
					Stop();
					di.Delete();
				}

			}
		}
	}
}