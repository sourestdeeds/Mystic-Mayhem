using System;
using System.Reflection;
using Server.Commands;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class JailPetCmd
	{
		private static Point3D m_JailLoc = new Point3D( 5380, 1086, 0 );

		public static void Initialize()
		{
			CommandSystem.Register( "JailPet", AccessLevel.Counselor, new CommandEventHandler( JailPet_OnCommand ) );
		}   
     
		[Usage( "JailPet" )]
		[Description( "Send Pet to Jail" )]

		public static void JailPet_OnCommand( CommandEventArgs e )
		{
			
			e.Mobile.Target = new JailPetCmdTarget();

		}	


		private class JailPetCmdTarget : Target
		{
			public JailPetCmdTarget() : base( 15, false, TargetFlags.None )
			{
			}
		
			protected override void OnTarget( Mobile from, object targ )
			{
			   try{
				BaseCreature t=(BaseCreature)targ;

				if ( t == null )
					return;

				t.Location = m_JailLoc;
				t.Map = Map.Felucca;
			   }
			   catch{}
			}
		}
	}
}