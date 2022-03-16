//by henry_r
//01/10/08
using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class GlobeOfSosariaMoongate : AddonComponent
	{
		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		[Constructable]
		public GlobeOfSosariaMoongate() : base( 0X3660 )
		{
			Movable = false;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.Player ) return;
			if ( from.InRange( GetWorldLocation(), 1 ) ) UseGate( from );
			else from.SendLocalizedMessage( 500446 );
		}

		public override bool HandlesOnMovement{ get{ return true; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile )
			{
				if ( !Utility.InRange( m.Location, this.Location, 1 ) && Utility.InRange( oldLocation, this.Location, 1 ) )
					m.CloseGump( typeof( MoongateGump ) );
			}
		}

		public bool UseGate( Mobile m )
		{
			if ( m.Criminal )
			{
				m.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if ( Server.Spells.SpellHelper.CheckCombat( m ) )
			{
				m.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
				return false;
			}
			else if ( m.Spell != null )
			{
				m.SendLocalizedMessage( 1049616 ); // You are too busy to do that at the moment.
				return false;
			}
			else
			{
				m.CloseGump( typeof( MoongateGump ) );
				m.SendGump( new MoongateGump( m, this ) );
				if ( !m.Hidden || m.AccessLevel == AccessLevel.Player ) Effects.PlaySound( m.Location, m.Map, 0x20E );

				return true;
			}
		}

		public GlobeOfSosariaMoongate( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}	

	public class GlobeOfSosariaMoongateAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GlobeOfSosariaMoongateDeed(); } }

		[Constructable]
		public GlobeOfSosariaMoongateAddon()
		{
			AddonComponent m_light = new AddonComponent( 7888 );
			m_light.Light = LightType.Circle300;
			AddComponent( m_light, 0, 0, 0 );
			AddComponent( new AddonComponent( 0X3657 ), 0, 0, 0 );			
			AddComponent( new GlobeOfSosariaMoongate(), 0, 0, 0 );
			Name = "Globe Of Sosaria";
		}

		public GlobeOfSosariaMoongateAddon( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class GlobeOfSosariaMoongateDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GlobeOfSosariaMoongateAddon(); } }
		public override int LabelNumber{ get{ return 1023948; } }

		[Constructable]
		public GlobeOfSosariaMoongateDeed()
		{
			Name = "globe of sosaria deed";
		}

		public GlobeOfSosariaMoongateDeed( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}