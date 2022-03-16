using System;
using Server.Network;

namespace Server.Misc
{


	public class RKTimer : Timer
	{
		private Mobile m_Mobile;

		public RKTimer( Mobile m, double t) : base( TimeSpan.FromSeconds( t ))
		{
			m_Mobile = m;
			m.Blessed = true;
			m.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Head );
			m.PlaySound( 0x1EA );
			m.SendMessage( "You are granted temporary immunity!");

		}

		protected override void OnTick()
		{
			if( m_Mobile.Blessed == true)
			{
			Mobile m = m_Mobile as Mobile;
			m_Mobile.Blessed = false;
			m.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Head );
			m.PlaySound( 0x1EA );
			m.SendMessage( "Your temporary immunity has ended.");

			Stop();
			}
		}
	}
}

// RKTimer: Anti-RezKill Timer, makes the player invulnerable for [t] seconds from start time.
// scripted by Tashanna
// --------------------------
// When using this script, add the following lines to playermobile.cs in the OnLogin function (around line 244)
// Check for login of Blessed player and make unblessed
// if( ( pm.Blessed == true) &amp;&amp; ( pm.AccessLevel == AccessLevel.Player))
// pm.Blessed = false;