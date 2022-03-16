using System;
using Server;
using Server.Commands;
using Server.Network;
using Server.Regions;
using Server.Mobiles;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;
using Server.Items;

namespace Server.Commands
{

	public enum DecorateCommand
	{
		None,
		Turn,
		Up,
		Down,
		North,
		East,
		South,
		West
	}

	public class StaffDeco
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Deco", AccessLevel.Counselor, new CommandEventHandler( Deco_OnCommand ) );
		}

	//	private DecorateCommand m_Command;

	//	[CommandProperty( AccessLevel.GameMaster )]
	//	public DecorateCommand Command{ get{ return m_Command; } set{ m_Command = value; InvalidateProperties(); } }


		public static void Deco_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump(new InternalGump( e.Mobile ) );

		//	from.SendGump( new InternalGump( (PlayerMobile)from ) );
		}

		private class InternalGump : Gump
		{
			private Mobile m_From;

			public InternalGump( Mobile from ) : base( 50, 50 )
			{
				m_From = from;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);




			AddButton(166, 46, 4501, 4501, 1, GumpButtonType.Reply, 0); //north

			AddButton(163, 165, 4503, 4503, 2, GumpButtonType.Reply, 0); //east

			AddButton(46, 165, 4505, 4505, 3, GumpButtonType.Reply, 0); //south

			AddButton(46, 45, 4507, 4507, 4, GumpButtonType.Reply, 0); //west
			AddImage(30, 30, 5010);

			AddButton(107, 32, 4500, 4500, 5, GumpButtonType.Reply, 0); //up

			AddButton(106, 175, 4504, 4504, 6, GumpButtonType.Reply, 0); //down

			AddButton(175, 105, 4502, 4502, 7, GumpButtonType.Reply, 0); //turn
			AddButton(35, 105, 4506, 4506, 8, GumpButtonType.Reply, 0); //turn
			AddLabel(115, 105, 88, @"Staff");
			AddLabel(118, 125, 88, @"Deco");

			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				int command = 0;
			//	DecorateCommand command = DecorateCommand.None;

				switch ( info.ButtonID )
				{
					case 1: command = 1; break; //DecorateCommand.North; break;
					case 2: command = 2; break; //DecorateCommand.East; break;
					case 3: command = 3; break; //DecorateCommand.South; break;
					case 4: command = 4; break; //DecorateCommand.West; break;
					case 5: command = 5; break; //DecorateCommand.Up; break;
					case 6: command = 6; break; //DecorateCommand.Down; break;
					case 7: command = 7; break; //DecorateCommand.Turn; break;
					case 8: command = 7; break; //DecorateCommand.Turn; break;
				}

				if ( command != 0 ) //DecorateCommand.None )
				{
				//	m_Decorator.Command = command;
					sender.Mobile.Target = new InternalTarget( command );
				m_From.SendGump( new InternalGump( m_From ) );				
				}
			}
		}

		private class InternalTarget : Target
		{
		//	private InteriorDecorator m_Decorator;
			private int m_Command;

			public InternalTarget( int command ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;
				m_Command = command;

		//		m_Decorator = decorator;
			}

			protected override void OnTargetNotAccessible( Mobile from, object targeted )
			{
				OnTarget( from, targeted );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Item )
				{

					Item item = (Item)targeted;

					switch ( m_Command ) //m_Decorator.Command )
					{
						case 1:	North( item, from );	break;
						case 2:	East( item, from );	break;
						case 3:	South( item, from );	break;
						case 4:	West( item, from );	break;
						case 5:	Up( item, from );	break;
						case 6:	Down( item, from );	break;
						case 7:	Turn( item, from );	break;
						case 8:	Turn( item, from );	break;

					}

				}
			}

			private static void Turn( Item item, Mobile from )
			{
				FlipableAttribute[] attributes = (FlipableAttribute[])item.GetType().GetCustomAttributes( typeof( FlipableAttribute ), false );

				if( attributes.Length > 0 )
					attributes[0].Flip( item );
				else
					from.SendLocalizedMessage( 1042273 ); // You cannot turn that.
			}

			private static void North( Item item, Mobile from )
			{
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x, y - 1, z );


			}
			private static void East( Item item, Mobile from )
			{
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x + 1, y, z );

			}
			private static void South( Item item, Mobile from )
			{
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x, y + 1, z );

			}
			private static void West( Item item, Mobile from )
			{
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x - 1, y, z );

			}

			private static void Up( Item item, Mobile from )
			{


				item.Location = new Point3D( item.Location, item.Z + 1 );

			}

			private static void Down( Item item, Mobile from )
			{

				item.Location = new Point3D( item.Location, item.Z - 1 );

			}
		}
	}
}