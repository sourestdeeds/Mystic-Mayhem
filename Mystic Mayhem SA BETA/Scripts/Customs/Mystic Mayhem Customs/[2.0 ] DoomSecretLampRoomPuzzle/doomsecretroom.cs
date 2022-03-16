using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using System.Collections.Generic;

namespace Server.Misc
{
	public class DoomSecretRoom : Region
	{
		public static DoomSecretRoom MainRegion = new DoomSecretRoom();
		public static WandererOfTheVoid Wanderer = null;
		public DoomSecretRoom(): base("Secret Room", Map.Malas, 80, new Rectangle2D(465, 92, 9, 9))
  
            {
             Register();
            }

		public override bool OnResurrect( Mobile from )
		{
			return false;
		}

		public void CheckWanderer()
            {
            if ( Wanderer == null )
             return;
            List<Mobile> mobs = GetMobiles();
            if (mobs.Count == 0)
            {
            if ( Wanderer != null )
            {
            if ( !Wanderer.Deleted )
            Wanderer.Delete();
            Wanderer = null;
           }
          }
          for (int i = 0; i < mobs.Count; ++i)
          {
          if (mobs[i] is PlayerMobile)
          {
          if (((PlayerMobile)mobs[i]).AccessLevel == AccessLevel.Player && ((PlayerMobile)mobs[i]).CheckAlive())
                   return;
                 }
               }
               if ( Wanderer != null )
               {
               if ( !Wanderer.Deleted )
                Wanderer.Delete();
               Wanderer = null;
              }
            }

		public static void Initialize() 
		{
			EventSink.Disconnected += new DisconnectedEventHandler( EventSink_Disconnected );
			new DoomPorter( new Point3D( 468, 92, -1 ), 6173, 696 );
			new DoomPorter( new Point3D( 469, 92, -1 ), 6177, 638 );
			new DoomPorter( new Point3D( 470, 92, -1 ), 6175, 133 );
			new UnimportantItem();
			new DoomBox();
		}

		public static void EventSink_Disconnected( DisconnectedEventArgs e ) //done
		{
			if ( e.Mobile.Region == MainRegion && e.Mobile.AccessLevel == AccessLevel.Player )
			{
				e.Mobile.Kill();
			}
		}

		public static void GassEffect()
		{
			Point3D loc = new Point3D( 465 + Utility.Random( 1, 8 ) , 92 + Utility.Random( 1, 8 ) , 6 );
			Effects.SendLocationEffect( loc, Map.Malas, 4518, 16, 1, 1166, 0 );
			Effects.PlaySound( loc, Map.Malas, 0x231 );
		}

		public override void OnEnter( Mobile m )
		{
			if ( m is PlayerMobile || ( m is BaseCreature && ((BaseCreature)m).Controlled ) )
			{
				DoomSecretRoomTimer t = new DoomSecretRoomTimer( m );
				t.Start();
			}
		}

		public override void OnExit( Mobile m )
		{
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( s is MarkSpell || s is GateTravelSpell )
			{
				m.SendMessage( "You can not cast that here" );
				return false;
			}
			return base.OnBeginSpellCast( m, s );
		}

		private class UnimportantItem : DeleteingItem
		{
			public UnimportantItem() : base( 7978 )
			{
				Location = new Point3D( 467, 92, -1 );
				Map = Map.Malas;
				Movable = false;
			}

			public UnimportantItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
			}
		}

		private class DoomBox : DeleteingItem
		{
			public DoomBox() : base( 3712 )
			{
				this.Map = Map.Malas;
				this.Location = new Point3D( 469, 96, 5 );
				this.Movable = false;
			}

			public override void OnDoubleClick( Mobile from )
			{
				if ( from.InRange( this.Location, 1 ) )
				{
					if ( DoomSecretRoom.Wanderer != null )
						return;
					DoomSecretRoom.Wanderer = new WandererOfTheVoid();
					DoomSecretRoom.Wanderer.Location = new Point3D( 470, 96, -1 );
					DoomSecretRoom.Wanderer.Map = Map.Malas;
					DoomSecretRoom.Wanderer.Combatant = from;
				}
			}

			public DoomBox( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
			}
		}

		private class DoomPorter : DeleteingItem
		{
			public DoomPorter( Point3D loc, int itemID, int hue ) : base( itemID )
			{
				Hue = hue;
				Location = loc;
				Map = Map.Malas;
				Movable = false;
			}

			public override bool OnMoveOver( Mobile from )
			{
				if ( !( from is PlayerMobile ) )
					return true;
				from.Map = Map.Malas;
				from.Location = new Point3D( 349, 176, 14 );
				return false;
			}

			public DoomPorter( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
			}
		}

		private class DoomSecretRoomTimer : Timer
		{
			private Mobile from;
			private int Count;
			private int DoPoison = 0;
			public DoomSecretRoomTimer( Mobile m ) : base( TimeSpan.FromSeconds( 0 ), TimeSpan.FromSeconds( 2.5 ) )
			{
				from = m;
				Count = 0;
				DoPoison = 0;
			}

			protected override void OnTick()
			{
				Count++;
				if ( from == null || from.Region != DoomSecretRoom.MainRegion || !from.CheckAlive() || from.AccessLevel != AccessLevel.Player )
				{
					this.Stop();
					DoomSecretRoom.MainRegion.CheckWanderer();
					return;
				}
				int MainCounts = Count/24;
				Poison PoisonLevel = null;
				if ( MainCounts < 1 )
					PoisonLevel = Poison.Lesser;
				else if ( MainCounts < 2 )
					PoisonLevel = Poison.Regular;
				else if ( MainCounts < 3 )
					PoisonLevel = Poison.Greater;
				else if ( MainCounts < 4 )
					PoisonLevel = Poison.Deadly;
				else PoisonLevel = Poison.Lethal;
				if ( DoPoison == 2 )
				{
					GassEffect();
					GassEffect();
					GassEffect();
				}
				if ( from != null && ( DoPoison == 4 || Count == 0 ) )
				{
					if ( from.Poison == null || from.Poison.Level < PoisonLevel.Level )
					{
						from.Poison = PoisonLevel;
					}
					GassEffect();
					GassEffect();
					GassEffect();
					DoPoison = 0;
				}
				DoPoison++;
				if ( MainCounts >= 5 )
				{
					from.Damage( 8 );
				}
			}
		}
	}
}