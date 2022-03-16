using System;
using System.Reflection;
using Server.Commands;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class UnblessCmd
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Unbless", AccessLevel.Player, new CommandEventHandler( Unbless_OnCommand ) );
		}   
     
		[Usage( "Unbless" )]
		[Description( "Sets caster to Unblessed" )]

		public static void Unbless_OnCommand( CommandEventArgs e )
		{
			if( e.Mobile.Blessed == true)
			{
			e.Mobile.Blessed = false;
			e.Mobile.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Head );
			e.Mobile.PlaySound( 0x1EA );
			e.Mobile.SendMessage( "Your immunity has been stopped.");
			}
		}	



	}
}