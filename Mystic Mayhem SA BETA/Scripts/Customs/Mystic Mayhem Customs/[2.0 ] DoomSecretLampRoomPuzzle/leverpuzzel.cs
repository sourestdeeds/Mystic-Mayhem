using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	public class LeverPuzzel
	{
		private static Statue statue1 = new Statue( 1, new Point3D( 319, 70, 18 ) );
		private static Statue statue2 = new Statue( 2, new Point3D( 329, 60, 18 ) );
		private static Point3D Square1 = new Point3D( 316, 65, -1 );
		private static Point3D Square2 = new Point3D( 324, 58, -1 );
		private static Point3D Square3 = new Point3D( 332, 64, -1 );
		private static Point3D Square4 = new Point3D( 323, 72, -1 );
		private static Lever Lever1 = new Lever( 1, new Point3D( 316, 64, 5 ) );
		private static Lever Lever2 = new Lever( 2, new Point3D( 323, 58, 5 ) );
		private static Lever Lever3 = new Lever( 3, new Point3D( 332, 63, 5 ) );
		private static Lever Lever4 = new Lever( 4, new Point3D( 323, 71, 5 ) );
	
		private static Point3D Square5 = new Point3D( 324, 64, -1 );

		public static void Initialize()
		{
			new MiddleThing();
			GenerateAnswers();
		}
		private static int Answer1, Answer2, Answer3, Answer4, Attempt1, Attempt2, Attempt3, Attempt4;

		public static void DoAttempt()
		{
			int number = 0;
			Mobile Thief = PlayerAtPoint( Square5 );
			Mobile Player1 = PlayerAtPoint( Square1 );
			Mobile Player2 = PlayerAtPoint( Square2 );
			Mobile Player3 = PlayerAtPoint( Square3 );
			Mobile Player4 = PlayerAtPoint( Square4 );
			if ( Thief != null && Player1 != null && Player2 != null && Player3 != null && Player4 != null )
			{
				if ( Answer1 == Attempt1 )
					number++;
				if ( Answer2 == Attempt2 )
					number++;
				if ( Answer3 == Attempt3 )
					number++;
				if ( Answer4 == Attempt4 )
					number++;
				if ( number == 4 )
				{
					Thief.BoltEffect( 0 );
					StatuesTalk( 4 );
					ThiefTPortTimer t = new ThiefTPortTimer( Thief );
					t.Start();
				}
				else
				{
					StatuesTalk( number );
					ArrayList list = new ArrayList();
					Point3D loc = new Point3D( 0, 0, 0 );
					for ( int i = 0; i < 5; ++i )
					{

						if ( i == 0 )
							loc = Square1;
						else if ( i == 1 )
							loc = Square2;
						else if ( i == 2 )
							loc = Square3;
						else if ( i == 3 )
							loc = Square4;
						else if ( i == 4 )
							loc = Square5;
						IPooledEnumerable ip = Map.Malas.GetMobilesInRange( loc, 4 );
						foreach ( Mobile m in ip )
						{
							if ( m is PlayerMobile && !list.Contains( m ) )
								list.Add( m );
						}
						ip.Free();
					}
					
					DropTimer t = new DropTimer();
					t.Start();

					for ( int i = 0; i < list.Count; ++i )
					{
						((Mobile)list[i]).Say( "OUCH!" );
						((Mobile)list[i]).SendMessage( 437,"A speeding rock hits you in the head" );
						AOS.Damage( (Mobile)list[i], 250 - ( ClosestDistance( ((Mobile)list[i]) ) * 30 ), 100, 0, 0, 0, 0 );
					}
					Attempt1 = 0; Attempt2 = 0; Attempt3 = 0; Attempt4 = 0;
				}
			}
			Lever1.Pressed = false;
			Lever2.Pressed = false;
			Lever3.Pressed = false;
			Lever4.Pressed = false;
		}

		public static int ClosestDistance( Mobile m )
		{
			double i = m.GetDistanceToSqrt( Square1 );
			double b = m.GetDistanceToSqrt( Square2 );
			if ( b < i ) 
				i = b;
			 b = m.GetDistanceToSqrt( Square3 );
			if ( b < i ) 
				i = b;
			b = m.GetDistanceToSqrt( Square4 );
			if ( b < i ) 
				i = b;
			b = m.GetDistanceToSqrt( Square5 );
			if ( b < i ) 
				i = b;
			return (int)i;
		}	

		public static void StatuesTalk( int number )
		{
			if ( statue1 != null )
				statue1.Talk( number );
			else
				statue1 = new Statue( 1, new Point3D( 319, 70, 18 ) );
			if ( statue2 != null )
				statue2.Talk( number );
			else
				statue2 = new Statue( 2, new Point3D( 329, 60, 18 ) );
		}

		public static Mobile PlayerAtPoint( Point3D loc )
		{
			ArrayList list = new ArrayList();
			IPooledEnumerable ip = Map.Malas.GetMobilesInRange( loc, 0 );
			foreach ( Mobile m in ip )
			{
				if ( m is PlayerMobile && m.CheckAlive() )
					list.Add( m );
			}
			ip.Free();
			if ( list.Count == 0 )
				return null;
			return (Mobile)list[ Utility.Random( 0, list.Count - 1 ) ];
		}

		public static void Hit( int ID )
		{
			if ( Attempt4 != 0 )
				DoAttempt();
			if ( ID == Attempt1 || ID == Attempt2 || ID == Attempt3 || ID == Attempt4 )
				return;
			if ( Attempt1 == 0 )
				Attempt1 = ID;
			else if ( Attempt2 == 0 )
				Attempt2 = ID;
			else if ( Attempt3 == 0 )
				Attempt3 = ID;
			else if ( Attempt4 == 0 )
			{
				Attempt4 = ID;
				DoAttempt();
			}
			else
				DoAttempt();
		}

		public static void GenerateAnswers()
		{
			Answer1 = 0; Answer2 = 0; Answer3 = 0; Answer4 = 0; Attempt1 = 0; Attempt2 = 0; Attempt3 = 0; Attempt4 = 0;
			int i = Utility.Random( 1, 4 );
			bool pass = false;
			Answer1 = i;
			while( !pass )
			{
				i = Utility.Random( 1, 4 );
				if ( i != Answer1 && i != Answer2 && i != Answer3 && i != Answer4 )
				{
					if ( Answer2 == 0 )
						Answer2 = i;
					else if ( Answer3 == 0 )
						Answer3 = i;
					else
						Answer4 = i;
				}
				else if ( Answer4 != 0 )
					pass = true;
			}
			
		}

		private class DropTimer : Timer
		{
			public DropTimer() : base( TimeSpan.FromSeconds( 0.72 ) )
			{
				Effects.SendMovingEffect( new Entity( Serial.Zero, new Point3D( Square1.X, Square1.Y, Square1.Z + 80 ), Map.Malas ), new Entity( Serial.Zero, new Point3D( Square1.X, Square1.Y, Square1.Z + 2 ), Map.Malas ), 0x11B8, 5, 16, false, false );
				Effects.SendMovingEffect( new Entity( Serial.Zero, new Point3D( Square2.X, Square2.Y, Square2.Z + 80 ), Map.Malas ), new Entity( Serial.Zero, new Point3D( Square2.X, Square2.Y, Square2.Z + 2 ), Map.Malas ), 0x11B8, 5, 16, false, false );
				Effects.SendMovingEffect( new Entity( Serial.Zero, new Point3D( Square3.X, Square3.Y, Square3.Z + 80 ), Map.Malas ), new Entity( Serial.Zero, new Point3D( Square3.X, Square3.Y, Square3.Z + 2 ), Map.Malas ), 0x11B8, 5, 16, false, false );
				Effects.SendMovingEffect( new Entity( Serial.Zero, new Point3D( Square4.X, Square4.Y, Square4.Z + 80 ), Map.Malas ), new Entity( Serial.Zero, new Point3D( Square4.X, Square4.Y, Square4.Z + 2 ), Map.Malas ), 0x11B8, 5, 16, false, false );
				Effects.SendMovingEffect( new Entity( Serial.Zero, new Point3D( Square5.X, Square5.Y, Square5.Z + 80 ), Map.Malas ), new Entity( Serial.Zero, new Point3D( Square5.X, Square5.Y, Square5.Z + 2 ), Map.Malas ), 0x11B8, 5, 16, false, false );
			}

			protected override void OnTick()
			{
				Effects.SendLocationEffect( new Point3D( Square1.X, Square1.Y, Square1.Z + 2 ), Map.Malas, 0x36B0, 16, 1, 0, 0 );
				Effects.SendLocationEffect( new Point3D( Square2.X, Square2.Y, Square2.Z + 2 ), Map.Malas, 0x36B0, 16, 1, 0, 0 );
				Effects.SendLocationEffect( new Point3D( Square3.X, Square3.Y, Square3.Z + 2 ), Map.Malas, 0x36B0, 16, 1, 0, 0 );
				Effects.SendLocationEffect( new Point3D( Square4.X, Square4.Y, Square4.Z + 2 ), Map.Malas, 0x36B0, 16, 1, 0, 0 );
				Effects.SendLocationEffect( new Point3D( Square5.X, Square5.Y, Square5.Z + 2 ), Map.Malas, 0x36B0, 16, 1, 0, 0 );

				Effects.PlaySound( Square1, Map.Malas, 0x307 );
				Effects.PlaySound( Square2, Map.Malas, 0x307 );
				Effects.PlaySound( Square3, Map.Malas, 0x307 );
				Effects.PlaySound( Square4, Map.Malas, 0x307 );
				Effects.PlaySound( Square5, Map.Malas, 0x307 );
			}
		}

		private class ThiefTPortTimer : Timer
		{
			private Mobile thief;
			public ThiefTPortTimer( Mobile from ) : base( TimeSpan.FromSeconds( 1.5 ) )
			{
				thief = from;
			}

			protected override void OnTick()
			{
				thief.BoltEffect( 0 );
				thief.Location = new Point3D( 470, 96, -1 );
				thief.Map = Map.Malas;
				LeverPuzzel.GenerateAnswers();
			}
		}

		private class MiddleThing : DeleteingItem
		{
			public MiddleThing() : base( 6178 )
			{
				this.Location = new Point3D( 324, 64, -1 );
				this.Map = Map.Malas;
				this.Movable = false;
				this.Hue = 375;
			}

			public MiddleThing( Serial serial ) : base( serial )
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

		private class Statue : DeleteingItem
		{
			public Statue( int Version, Point3D location ) : base( 4823 + Version )
			{
				Location = location;
				Map = Map.Malas;
				Movable = false;
				Hue = 706;
			}

			public void Talk( int correct )
			{
				if ( correct == 4 )
					PublicOverheadMessage( 0, 0, false, "You may pass to the secret room" );
				else
					PublicOverheadMessage( 0, 0, false, "You only got " + correct.ToString() + ". The gate remains Closed" );
			}

			public Statue( Serial serial ) : base( serial )
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
		
		private class Lever : DeleteingItem
		{
			public int ID;
			private bool pressed = false;
			public bool Pressed { get { return pressed; } set {
				if ( value == true )
					this.ItemID = 0x108C;
				else
					this.ItemID = 4238;
				pressed = value;
			} }

			public Lever( int LeverID, Point3D location ) : base( 4238 )
			{
				ID = LeverID;
				Location = location;
				Map = Map.Malas;
				Movable = false;
				Hue = 437;
			}

			public override void OnDoubleClick( Mobile from )
			{
				if ( pressed == true )
					return;
				switch ( ID )
				{
					case 1:
					{
						if ( from.Location == Square1 )
						{
							PublicOverheadMessage( 0, 0, false, "*Click*" );
							Pressed = true;
							LeverPuzzel.Hit( ID );
						}
						break;
					}
					case 2:
					{
						if ( from.Location == Square2 )
						{
							PublicOverheadMessage( 0, 0, false, "*Click*" );
							Pressed = true;
							LeverPuzzel.Hit( ID );
						}
						break;
					}
					case 3:
					{
						if ( from.Location == Square3 )
						{
							PublicOverheadMessage( 0, 0, false, "*Click*" );
							Pressed = true;
							LeverPuzzel.Hit( ID );
						}
						break;
					}
					case 4:
					{
						if ( from.Location == Square4 )
						{
							PublicOverheadMessage( 0, 0, false, "*Click*" );
							Pressed = true;
							LeverPuzzel.Hit( ID );
						}
						break;
					}
				}
			}

			public Lever( Serial serial ) : base( serial )
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
	}
}

namespace Server.Items
{
	public class DeleteingItem : Item
	{
		public static ArrayList dellist = new ArrayList();
		public static void Initialize()
		{
			foreach ( Item item in dellist )
			{
				World.RemoveItem( item );
			}
		   	
		}

		public DeleteingItem( int ID ) : base( ID )
		{
		}

		public DeleteingItem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			dellist.Add( this );
		}
	}
}