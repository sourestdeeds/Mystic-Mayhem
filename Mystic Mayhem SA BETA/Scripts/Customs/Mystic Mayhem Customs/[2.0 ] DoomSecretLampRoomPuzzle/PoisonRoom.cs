using System;
using System.Collections;
using Server;
using Server.Regions;
using Server.Spells.Sixth;
using Server.Spells.Seventh;
using System.Collections.Generic;
namespace Server.Misc
{
	
      public class PoisonRoom
      {
      public PoisonRoomRegion MainRegion;
      public PoisonRoom( int StartX, int StartY, int EndX, int EndY, Map map, string name )
      {
       MainRegion = new PoisonRoomRegion( name, map, this, new Rectangle2D( new Point2D( StartX, StartY ), new Point2D( EndX, EndY ) )  );
      }
  }

  public class PoisonRoomRegion : Region
  {
   public PoisonRoom Room;
   public PoisonRoomRegion( string name, Map map, PoisonRoom room, Rectangle2D area ) : base( name, map, 51, area )
   {
    Room = room;
   }
         
		public override void OnEnter( Mobile m )
		{
			PoisonRoomRegionTimer t = new PoisonRoomRegionTimer( m );
			t.Start();
		}

		public override void OnExit( Mobile m )
		{
		}

		public override bool OnResurrect( Mobile from )
		{
			return false;
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

		private class PoisonRoomRegionTimer : Timer
		{
			public PoisonRoomRegionTimer( Mobile from ) : base( TimeSpan.FromSeconds( 1 ) )
			{
			}

			protected override void OnTick()
			{
			}
		}
	}
}