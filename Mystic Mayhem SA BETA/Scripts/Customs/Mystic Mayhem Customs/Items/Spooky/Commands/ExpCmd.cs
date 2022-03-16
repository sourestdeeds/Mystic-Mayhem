using System;
using System.Reflection;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;

namespace Server.Commands
{
	public class ExpCmd
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Exp", AccessLevel.Player, new CommandEventHandler( Exp_OnCommand ) );
		}   
     
		[Usage( "Exp" )]
		[Description( "Displays Fame and Karma" )]

		public static void Exp_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump(new ExpGump( e.Mobile ) );
			
			//e.Mobile.Target = new ExpCmdTarget();
			//e.Mobile.SendMessage( " Karma = {0}", e.Mobile.Karma );
			//e.Mobile.SendMessage( " Fame = {0}", e.Mobile.Fame );
		}	

	}

	public class ExpGump : Gump
	{
		public string Color( string text, int color ) 
		{ 
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text ); //  :X6
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}

		private const int LabelColor = 0x24E5;
		public ExpGump( Mobile from ) : base( 250, 50 )
		{
			from.CloseGump( typeof( ExpGump ) );
			AddPage( 0 );
			string header = String.Format( "<center><i>{0} Experience</i></center>", from.Name );
			AddImage( 100, 100, 2080 );
			AddImage( 118, 137, 2081 );
			AddImage( 118, 207, 2081 );
			AddImage( 118, 277, 2081 );
			AddImage( 118, 347, 2083 );

			AddHtml( 147, 108, 210, 18, Color( header, 200 ), false, false );

			AddButton( 240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0 );

			AddImage( 140, 138, 2091 );
			AddImage( 140, 335, 2091 );

			AddImage( 128, 152, 2086 );
			string FK = String.Format ( "<center>Fame & Karma</center>" );
			AddHtml( 165, 150, 160, 18, Color( FK, 200 ), false, false ); // Karma & Fame

			AddHtmlLocalized( 153, 168, 160, 18, 3010072, LabelColor, false, false ); // Fame
			AddHtml( 280, 168, 75, 18, FormatStat( from.Fame ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 3010073, LabelColor, false, false ); // Karma
			AddHtml( 280, 186, 75, 18, FormatStat( from.Karma ), false, false );

			AddImage( 128, 206, 2086 );
			string virtue = String.Format ( "<center>Virtues</center>" );
			AddHtml( 165, 204, 160, 18, Color( virtue, 200 ), false, false ); // Virtures

			AddHtmlLocalized( 153, 222, 160, 18, 1012015, LabelColor, false, false ); // Compassion
			AddHtml( 280, 222, 75, 18, FormatStat( from.Virtues.Compassion ), false, false );

			AddHtmlLocalized( 153, 240, 160, 18, 1012017, LabelColor, false, false ); // Honor
			AddHtml( 280, 240, 75, 18, FormatStat( from.Virtues.Honor ), false, false );

			AddHtmlLocalized( 153, 258, 160, 18, 1012019, LabelColor, false, false ); // Justice
			AddHtml( 280, 258, 75, 18, FormatStat( from.Virtues.Justice ), false, false );

			AddHtmlLocalized( 153, 276, 160, 18, 1012020, LabelColor, false, false ); // Sacrifice
			AddHtml( 280, 276, 75, 18, FormatStat( from.Virtues.Sacrifice ), false, false );

			AddHtmlLocalized( 153, 294, 160, 18, 1012022, LabelColor, false, false ); // Valor
			AddHtml( 280, 294, 75, 18, FormatStat( from.Virtues.Valor ), false, false );
		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			from.CloseGump( typeof( ExpGump ) );
		}
	}
}