using System;
using Server.Commands;
using System.IO;
using System.Diagnostics;
using Server;

namespace Server.Misc
{
	public class RestartWarning : Timer
	{
		public static bool Enabled = true; // is the script enabled?

		private static TimeSpan RestartTime = TimeSpan.FromHours( 17.0 ); // time of day at which to restart


		private static TimeSpan Warning6Delay = TimeSpan.FromMinutes( 6.0 );//5
		private static TimeSpan Warning3Delay = TimeSpan.FromMinutes( 9.0 );//8
		private static TimeSpan Warning1Delay = TimeSpan.FromMinutes( 11.0 );//9

		private static bool m_Restarting;
		private static DateTime m_RestartTime;

		public static bool Restarting
		{
			get{ return m_Restarting; }
		}

		public static void Initialize()
		{
			CommandSystem.Register( "RestartWarning", AccessLevel.Administrator, new CommandEventHandler( Restart_OnCommand ) );
			new RestartWarning().Start();
		}

		public static void Restart_OnCommand( CommandEventArgs e )
		{
			if ( m_Restarting )
			{
				e.Mobile.SendMessage( "The server is already restarting." );
			}
			else
			{
				e.Mobile.SendMessage( "You have initiated server shutdown." );
				Enabled = true;
				m_RestartTime = DateTime.Now;
			}
		}

		public RestartWarning() : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
		{
			Priority = TimerPriority.FiveSeconds;

			m_RestartTime = DateTime.Now.Date + RestartTime;

			if ( m_RestartTime < DateTime.Now )
				m_RestartTime += TimeSpan.FromDays( 1.0 );
		}

		private void Warning12_Callback()
		{
			World.Broadcast( 0x22, true, "Daily Server Boot in 12 minutes" );
		}
		private void Warning6_Callback()
		{
			World.Broadcast( 0x22, true, "Daily Server Boot in 6 minutes" );
		}
		private void Warning3_Callback()
		{
			World.Broadcast( 0x22, true, "Daily Server Boot in 3 minutes" );
		}
		private void Warning1_Callback()
		{
			World.Broadcast( 0x22, true, "Daily Server Boot in 1 minutes" );
		}


		protected override void OnTick()
		{
			if ( m_Restarting || !Enabled )
				return;

			if ( DateTime.Now < m_RestartTime )
				return;
			m_Restarting = true;
			Warning12_Callback();
			Timer.DelayCall( Warning6Delay, new TimerCallback( Warning6_Callback ) );
			
			Timer.DelayCall( Warning3Delay, new TimerCallback( Warning3_Callback ) );
			Timer.DelayCall( Warning1Delay, new TimerCallback( Warning1_Callback ) );


		}
	}
}