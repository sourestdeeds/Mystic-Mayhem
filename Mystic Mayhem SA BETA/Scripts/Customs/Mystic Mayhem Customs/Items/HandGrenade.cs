using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Mobiles;

namespace Server.Items
{
	public class HandGrenade : Item
	{
		[Constructable]
		public HandGrenade() : base( 3699 )
		{
			Weight = 1.0;
			Hue = 1161;
			Name = "Holy Hand Grenade";
		}

		public HandGrenade( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
 			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		private Timer m_KaBoom;
		private int m_Counts;
		public bool m_ends;

		public override void OnDoubleClick( Mobile from )
		{
			m_ends = false;
			m_Counts = 0;
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042010 ); // You must have the object in your backpack to use it.
				return;
			}
			this.Hue = 33;
			from.SendMessage ("Thou pullest the Holy Pin. Thou must count to three.");

			from.Prompt = new CountPrompt( this );
			//if ( m_KaBoom == null )
			//{

				m_KaBoom = Timer.DelayCall( TimeSpan.FromSeconds( 0.75 ), TimeSpan.FromSeconds( 1.0 ), 9, new TimerStateCallback( Detonate_OnTick ), new object[]{ from, 3 } );
			//}

		}

		private void Detonate_OnTick( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;
			Mobile from = (Mobile)states[0];
			int timer = (int)states[1];
this.Hue = ( this.Hue == 1161 ? this.Hue = 33 : this.Hue = 1161 );

			m_Counts += 1;

			if ( m_Counts == 9 )
			{
				Targeting.Target.Cancel( from );
				KaBoom( from );
				if ( !m_ends )
					from.SendMessage("Thou failed to lobbest the Holy Hand Grenade!");
			}

		}

		public void KaBoom( Mobile from ) //, bool direct, Point3D loc, Map map )
		{
			if ( this.Deleted || m_ends )
				return;

			Point3D loc = from.Location;
			Map map = from.Map;

			from.Hits = 5;
			from.Stam = 5;
			from.Mana = 5;

			Effects.PlaySound( loc, map, 0x207 );
			Effects.SendLocationEffect( loc, map, 0x36BD, 20 );
			this.Delete();
		}

		private class CountPrompt : Prompt
		{
			private HandGrenade m_HHG;

			public CountPrompt( HandGrenade hhg )
			{
				m_HHG = hhg;
				if( hhg.Deleted )
					return;
			}

			public override void OnResponse( Mobile from, string text )
			{

				Point3D loc = from.Location;
				Map map = from.Map;

				if ( text.Length == 5 && text == "1 2 3")
				{
					from.Target = new InternalTarget( m_HHG );

				}
				else if ( !m_HHG.Deleted )
				{
					m_HHG.KaBoom ( from );
					from.SendMessage("Thou countest badly!");
				}
			}

			public override void OnCancel( Mobile from )
			{
			}
		}
		public class InternalTarget : Target
		{
			private HandGrenade m_HHG;

			public InternalTarget( HandGrenade hhg) : base( 12, false, TargetFlags.None )
			{
				m_HHG = hhg;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				try{
					if ( targeted is PlayerMobile )
					{
						from.Kill();
						from.CriminalAction( true );
					}
					else if ( targeted is Mobile )
					{
						Mobile mob = (Mobile)targeted;

						if ( mob is BaseChampion || /*mob is BaseSubChampion ||*/ mob is Harrower || mob is BaseVendor || mob is BaseEscortable || mob is Clone || mob.Blessed)
						{
							from.SendMessage("Thou cans't lobbist at that creature!");
							m_HHG.m_ends = true;
						}
						else
						{
							int damage = Utility.RandomMinMax( 10, 15 );
							Point3D loc = mob.Location;
							Map map = mob.Map;
							from.SendMessage("You lobbest thou the Holy Hand Grenade in the direction of thine foe, who, being naughty in thine sight, shall snuff it.");

							mob.Freeze( TimeSpan.FromSeconds( 20 ) );
							mob.Hits -= 400;
							AOS.Damage( mob, from, damage, 0, 100, 0, 0, 0 );
							Effects.PlaySound( loc, map, 0x207 );
							Effects.SendLocationEffect( loc, map, 0x36BD, 20 );
							m_HHG.Delete();

						}
					}
					else
					{
						Targeting.Target.Cancel( from );
						m_HHG.KaBoom ( from );
						from.SendMessage("Thou throwest very badly!");
					}

				}
				catch{}
			}
		}
	}
} 