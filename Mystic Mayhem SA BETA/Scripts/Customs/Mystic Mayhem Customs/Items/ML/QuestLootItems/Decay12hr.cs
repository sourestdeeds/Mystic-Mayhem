//aa
using System; 
using System.Collections;
using Server.Items; 
using Server.Mobiles; 
using Server.Misc;
using Server.Network;

namespace Server.Items 
{ 
   	public class Decay12hr: Item 
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
		public Decay12hr() : base( 0x318D )
		{
			Weight = 0.1;
			Name = "Decay 12 hr";
			Hue = 63;

			m_DeleteTimer = new DeleteTimer( this );
			m_DeleteTimer.Start();
			m_TimeEnd = DateTime.Now + TimeSpan.FromHours( 12.0 );
		}

            	public Decay12hr( Serial serial ) : base ( serial ) 
            	{             
           	}


           	public override void Serialize( GenericWriter writer ) 
           	{ 
              		base.Serialize( writer ); 
              		writer.Write( (int) 0 ); 
			writer.Write( m_TimeEnd );
           	} 
            
           	public override void Deserialize( GenericReader reader ) 
           	{ 
              		base.Deserialize( reader ); 
              		int version = reader.ReadInt();

			m_TimeEnd = reader.ReadDateTime();
			m_DeleteTimer = new DeleteTimer( this );
			m_DeleteTimer.Start();

           	} 

		private class DeleteTimer : Timer
		{ 
			private Decay12hr di;


			public DeleteTimer( Decay12hr item ) : base( TimeSpan.FromMinutes( 20.0 ), TimeSpan.FromMinutes( 20.0 ) )
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