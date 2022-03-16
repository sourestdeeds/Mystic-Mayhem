using System;
using Server;
using Server.Network;
using Server.Regions;
using Server.Mobiles;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;
using Server.Engines.Plants;

namespace Server.Items
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

	public class InteriorDecorator : Item
	{
		private DecorateCommand m_Command;

		[CommandProperty( AccessLevel.GameMaster )]
		public DecorateCommand Command{ get{ return m_Command; } set{ m_Command = value; InvalidateProperties(); } }

		[Constructable]
		public InteriorDecorator() : base( 0xFC1 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public override int LabelNumber{ get{ return 1041280; } } // an interior decorator

		public InteriorDecorator( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !CheckUse( this, from ) )
				return;

			from.SendGump( new InternalGump( (PlayerMobile)from, this ) );
		}

		public static bool InHouse( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );

			return ( house != null && house.IsCoOwner( from ) );
		}

		public static bool CheckUse( InteriorDecorator tool, Mobile from )
		{
			if ( !InHouse( from ) )
				from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
			else
				return true;

			return false;
		}

		private class InternalGump : Gump
		{
			private PlayerMobile m_From;
			private InteriorDecorator m_Decorator;

			public InternalGump( PlayerMobile from, InteriorDecorator decorator ) : base( 50, 50 )
			{
				m_From = from;
				m_Decorator = decorator;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(29, 20, 143, 167, 9200);
			AddAlphaRegion(36, 27, 127, 152);

			AddButton(92, 35, 5600, 5604, 1, GumpButtonType.Reply, 0);
			AddLabel(95, 46, 88, @"N");

			AddButton(123, 65, 5601, 5605, 2, GumpButtonType.Reply, 0);
			AddLabel(113, 62, 88, @"E");

			AddButton(92, 94, 5602, 5606, 3, GumpButtonType.Reply, 0);
			AddLabel(95, 76, 88, @"S");

			AddButton(59, 65, 5603, 5607, 4, GumpButtonType.Reply, 0);
			AddLabel(78, 62, 88, @"W");

			AddButton(54, 136, 5600, 5604, 5, GumpButtonType.Reply, 0);
			AddLabel(52, 152, 88, @"Up");

			AddButton(130, 136, 5602, 5606, 6, GumpButtonType.Reply, 0);
			AddLabel(123, 152, 88, @"Down");

			AddButton(90, 118, 22406, 22406, 7, GumpButtonType.Reply, 0);
			AddLabel(85, 139, 88, @"Turn");

			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				DecorateCommand command = DecorateCommand.None;

				switch ( info.ButtonID )
				{
					case 1: command = DecorateCommand.North; break;
					case 2: command = DecorateCommand.East; break;
					case 3: command = DecorateCommand.South; break;
					case 4: command = DecorateCommand.West; break;
					case 7: command = DecorateCommand.Turn; break;
					case 5: command = DecorateCommand.Up; break;
					case 6: command = DecorateCommand.Down; break;
				}

				if ( command != DecorateCommand.None )
				{
					m_Decorator.Command = command;
					sender.Mobile.Target = new InternalTarget( m_Decorator );
				m_From.SendGump( new InternalGump( m_From, m_Decorator ) );				
				}
			}
		}

		private class InternalTarget : Target
		{
			private InteriorDecorator m_Decorator;

			public InternalTarget( InteriorDecorator decorator ) : base( -1, false, TargetFlags.None )
			{
				CheckLOS = false;

				m_Decorator = decorator;
			}

			protected override void OnTargetNotAccessible( Mobile from, object targeted )
			{
				OnTarget( from, targeted );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{

			/*	if ( targeted == m_Decorator )
				{
					m_Decorator.Command = DecorateCommand.None;
					from.SendGump( new InternalGump( from, m_Decorator ) );
				}
				else */ if ( targeted is Item && InteriorDecorator.CheckUse( m_Decorator, from ) )
				{
					BaseHouse house = BaseHouse.FindHouseAt( from );
					Item item = (Item)targeted;
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
//YARD ITEM


					if ( item is AddonComponent ||item is YardItem || item is YardTreeMulti || item is YardFountain
						|| item is YardIronGate || item is YardShortIronGate
						|| item is YardLightWoodGate || item is YardDarkWoodGate
						|| item is YardStair || gravestone || plant || other)
					{
						switch ( m_Decorator.Command )
						{
							case DecorateCommand.North:	North( item, from, house );	break;
							case DecorateCommand.East:	East( item, from, house );	break;
							case DecorateCommand.South:	South( item, from, house );	break;
							case DecorateCommand.West:	West( item, from, house );	break;
							case DecorateCommand.Up:	Up( item, from );	break;
							case DecorateCommand.Down:	Down( item, from );	break;
							case DecorateCommand.Turn:	Turn( item, from );	break;
						}
					}
//YARD ITEM
					else if ( house == null || !house.IsCoOwner( from ) )
					{
						from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
					}
					else if ( item.Parent != null || !house.IsInside( item ) )
					{
						from.SendLocalizedMessage( 1042270 ); // That is not in your house.
					}
					else if ( !house.IsLockedDown( item ) && !house.IsSecure( item ) && !(item is AddonComponent))    //Addons ????
					{
						from.SendLocalizedMessage( 1042271 ); // That is not locked down.
					}
					else if ( item.TotalWeight + item.PileWeight > 100 )
					{
						from.SendLocalizedMessage( 1042272 ); // That is too heavy.
					}
					else
					{
						switch ( m_Decorator.Command )
						{
							case DecorateCommand.North:	North( item, from, house );	break;
							case DecorateCommand.East:	East( item, from, house );	break;
							case DecorateCommand.South:	South( item, from, house );	break;
							case DecorateCommand.West:	West( item, from, house );	break;
							case DecorateCommand.Up:	Up( item, from );	break;
							case DecorateCommand.Down:	Down( item, from );	break;
							case DecorateCommand.Turn:	Turn( item, from );	break;
						}
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

			private static void North( Item item, Mobile from, BaseHouse house )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;

				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x, y - 1, z );

				if( item.Y < from.Location.Y - 10 )
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "That is beyond your reach!" );
				}

				else if( !house.IsInside( item ) && item is AddonComponent || item is YardItem 
					|| item is YardTreeMulti || item is YardFountain || item is YardIronGate 
					|| item is YardShortIronGate || item is YardLightWoodGate 
					|| item is YardDarkWoodGate || item is YardStair || gravestone || plant || other)
				{
				}

				else if (!house.IsInside( item ))
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "You cannot move that any further!" );
				}
			}
			private static void East( Item item, Mobile from, BaseHouse house )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x + 1, y, z );
				if( item.X > from.Location.X + 10 )
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "That is beyond your reach!" );
				}

				else if( !house.IsInside( item ) && item is AddonComponent ||item is YardItem 
					|| item is YardTreeMulti || item is YardFountain || item is YardIronGate 
					|| item is YardShortIronGate || item is YardLightWoodGate 
					|| item is YardDarkWoodGate || item is YardStair || gravestone || plant || other)
				{
				}

				else if (!house.IsInside( item ))
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "You cannot move that any further!" );
				}
			}
			private static void South( Item item, Mobile from, BaseHouse house )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x, y + 1, z );
				if( item.Y > from.Location.Y + 10 )
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "That is beyond your reach!" );
				}

				else if( !house.IsInside( item ) && item is AddonComponent ||item is YardItem 
					|| item is YardTreeMulti || item is YardFountain || item is YardIronGate 
					|| item is YardShortIronGate || item is YardLightWoodGate 
					|| item is YardDarkWoodGate || item is YardStair || gravestone || plant || other)
				{
				}

				else if (!house.IsInside( item ))
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "You cannot move that any further!" );
				}
			}
			private static void West( Item item, Mobile from, BaseHouse house )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
				int x = item.X;
				int y = item.Y;
				int z = item.Z;
				item.Location = new Point3D( x - 1, y, z );
				if( item.X < from.Location.X - 10 )
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "That is beyond your reach!" );
				}

				else if( !house.IsInside( item ) && item is AddonComponent ||item is YardItem 
					|| item is YardTreeMulti || item is YardFountain || item is YardIronGate 
					|| item is YardShortIronGate || item is YardLightWoodGate 
					|| item is YardDarkWoodGate || item is YardStair || gravestone || plant || other)
				{
				}

				else if (!house.IsInside( item ))
				{
					item.Location = new Point3D( x, y, z );
					from.SendMessage( "You cannot move that any further!" );
				}
			}

			private static void Up( Item item, Mobile from )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
				int floorZ = GetFloorZ( item );

//YARD ITEM
				if( item is AddonComponent ||item is YardItem || item is YardTreeMulti || item is YardFountain
						|| item is YardIronGate || item is YardShortIronGate
						|| item is YardLightWoodGate || item is YardDarkWoodGate
						|| item is YardStair || gravestone || plant || other)
					item.Location = new Point3D( item.Location, item.Z + 1 );
//YARD ITEM
				else if ( floorZ > int.MinValue && item.Z < (floorZ + 19) ) // Confirmed : no height checks here
					item.Location = new Point3D( item.Location, item.Z + 1 );
				else
					from.SendLocalizedMessage( 1042274 ); // You cannot raise it up any higher.
			}

			private static void Down( Item item, Mobile from )
			{
	bool gravestone = ( (item.ItemID >= 0x1165 && item.ItemID <= 0x1182) ||
			(item.ItemID >= 0x1E9A && item.ItemID <= 0x1EA6) ||
			(item.ItemID >= 0x14F7 && item.ItemID <= 0x14FA) ||    //anchor rope
			(item.ItemID >= 0xB20 && item.ItemID <= 0xB25) ||    //lamp post
			(item.ItemID >= 0x41F && item.ItemID <= 0x429) ||	//gruesome
			(item.ItemID >= 0x1370 && item.ItemID <= 0x1375) ||	// bridle brush
			(item.ItemID >= 0xF3B && item.ItemID <= 0xF3C) ||	// horse biscuit
			(item.ItemID >= 0x1E2A && item.ItemID <= 0x1E2B) );
// gravestones  hanging nets  oars
// other items
	bool other = false;
	if ( item is MailBox )
		other = true;

	bool plant = false;
	PlantItem pi = null;
	if ( item is PlantItem )
		pi= (PlantItem)item;
	if ( item is PlantItem && !pi.IsGrowable )
		plant = true;
				int floorZ = GetFloorZ( item );
//YARD ITEM
				if( item is AddonComponent ||item is YardItem || item is YardTreeMulti || item is YardFountain
						|| item is YardIronGate || item is YardShortIronGate
						|| item is YardLightWoodGate || item is YardDarkWoodGate
						|| item is YardStair || gravestone || plant || other)
					item.Location = new Point3D( item.Location, item.Z - 1 );
//YARD ITEM
				else if ( floorZ > int.MinValue && item.Z > GetFloorZ( item ) )
					item.Location = new Point3D( item.Location, item.Z - 1 );
				else
					from.SendLocalizedMessage( 1042275 ); // You cannot lower it down any further.
			}

			private static int GetFloorZ( Item item )
			{
				Map map = item.Map;

				if ( map == null )
					return int.MinValue;

				StaticTile[] tiles = map.Tiles.GetStaticTiles( item.X, item.Y, true );

				int z = int.MinValue;

				for ( int i = 0; i < tiles.Length; ++i )
				{
					StaticTile tile = tiles[i];
					ItemData id = TileData.ItemTable[tile.ID & TileData.MaxItemValue];

					int top = tile.Z; // Confirmed : no height checks here

					if ( id.Surface && !id.Impassable && top > z && top <= item.Z )
						z = top;
				}

				return z;
			}
		}
	}
}