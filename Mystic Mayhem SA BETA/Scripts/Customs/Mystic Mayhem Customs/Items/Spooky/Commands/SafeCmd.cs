using System;
using System.Reflection;
using Server.Commands;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using System.Collections;

namespace Server.Commands
{
	public class SafeCmd
	{
	//	int x = Utility.RandomList( 4445,4446,4447,4448 );
	//	int y = Utility.RandomList( 1152,1153,1154,1155 );
	//	private static Point3D m_SafeLoc = new Point3D( x, y, 0 );

		public static void Initialize()
		{
			CommandSystem.Register( "Safe", AccessLevel.Counselor, new CommandEventHandler( Safe_OnCommand ) );
		}   
     
		[Usage( "Safe" )]
		[Description( "Send Player to Safety" )]

		public static void Safe_OnCommand( CommandEventArgs e )
		{
			
			e.Mobile.Target = new SafeCmdTarget();

		}	


		private class SafeCmdTarget : Target
		{
			public SafeCmdTarget() : base( 15, false, TargetFlags.None )
			{
			}
		
			protected override void OnTarget( Mobile from, object targ )
			{
			   try{
				PlayerMobile t=(PlayerMobile)targ;

				if ( t == null )
					return;
		int x = Utility.RandomList( 4445,4446,4447,4448 );
		int y = Utility.RandomList( 1152,1153,1154,1155 );
Point3D to = new Point3D( x, y, 0 );
				t.Location = to;
				t.Map = Map.Felucca;
			   }
			   catch{}
			}
		}
	}
}