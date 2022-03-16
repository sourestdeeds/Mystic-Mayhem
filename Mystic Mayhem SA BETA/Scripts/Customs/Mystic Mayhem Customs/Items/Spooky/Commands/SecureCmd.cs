using System;
using Server.Commands;
using System.Reflection;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;
using Server.Network;

namespace Server.Commands
{
	public class SecureCmd
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Secure", AccessLevel.Player, new CommandEventHandler( Secure_OnCommand ) );
		}   
     
		[Usage( "Secure" )]
		[Description( "Prompts for Secure" )]

		public static void Secure_OnCommand( CommandEventArgs e )
		{
			Mobile mob = e.Mobile;
			mob.DoSpeech("I wish to secure this", new int[] {0x25}, MessageType.Regular, 0x3B2);
		}	
	}

	public class LockDownCmd
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Lockdown", AccessLevel.Player, new CommandEventHandler( Lockdown_OnCommand ) );
		}   
     
		[Usage( "Lockdown" )]
		[Description( "Prompts for Lockdown" )]

		public static void Lockdown_OnCommand( CommandEventArgs e )
		{
			Mobile mob = e.Mobile;
			mob.DoSpeech("I wish to lock this down", new int[] {0x23}, MessageType.Regular, 0x3B2);
		}	
	}

	public class ReleaseCmd
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Release", AccessLevel.Player, new CommandEventHandler( Lockdown_OnCommand ) );
		}   
     
		[Usage( "Release" )]
		[Description( "Prompts for Release" )]

		public static void Lockdown_OnCommand( CommandEventArgs e )
		{
			Mobile mob = e.Mobile;
			mob.DoSpeech("I wish to release this", new int[] {0x24}, MessageType.Regular, 0x3B2);
		}	
	}
}
//DoSpeech("I wish to lock this down", new int[] {0x23}, MessageType.Regular, 0x3B2);

//DoSpeech("I wish to release this", new int[] {0x24}, MessageType.Regular, 0x3B2);