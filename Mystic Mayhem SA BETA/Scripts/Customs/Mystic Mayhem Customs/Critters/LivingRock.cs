using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class LivingRock : Item
	{

		[Constructable]
		public LivingRock( ) : base( Utility.RandomList( 4963, 4964, 4965, 4966, 4967, 4968, 4969, 4970, 4971, 4972, 4973, 6001, 6002, 6003, 6004, 6005,6006, 6007, 6008, 6009, 6010, 6011, 6012 ) )
		{


		}		
		
		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m is PlayerMobile )
			{
			if ( Parent == null && Utility.InRange( Location, m.Location, 4 ) && !Utility.InRange( Location, oldLocation, 4 ) )
				Transform( );
			}
		}
		public override bool CheckLift(Mobile from, Item item, ref LRReason reject)
		{
			return false;
		}

		public void Transform( ) //Mobile m )
		{
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
			Effects.PlaySound( this, this.Map, 0x208 );
			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
			Effects.PlaySound( this, this.Map, 0x307 );
			switch ( Utility.Random( 8 ) )
			{
				case 0: Mobile earth = new EarthElemental();
					earth.MoveToWorld( this.Location, this.Map ); break;
				case 1: Mobile bronze = new BronzeElemental();
					bronze.MoveToWorld( this.Location, this.Map ); break;
				case 2: Mobile copper = new CopperElemental();
					copper.MoveToWorld( this.Location, this.Map ); break;
				case 3: Mobile dull = new DullCopperElemental();
					dull.MoveToWorld( this.Location, this.Map ); break;
				case 4: Mobile golden = new GoldenElemental();
					golden.MoveToWorld( this.Location, this.Map ); break;
				case 5: Mobile shadow = new ShadowIronElemental();
					shadow.MoveToWorld( this.Location, this.Map ); break;
				case 6: Mobile valorite = new ValoriteElemental();
					valorite.MoveToWorld( this.Location, this.Map ); break;
				case 7: Mobile verite = new VeriteElemental();
					verite.MoveToWorld( this.Location, this.Map ); break;
			//	case 8: Mobile blaze = new BlazeElemental();
			//		blaze.MoveToWorld( this.Location, this.Map ); break;
			//	case 9: Mobile ice = new IceElemental();
			//		ice.MoveToWorld( this.Location, this.Map ); break;
			//	case 10: Mobile toxic = new ToxicElemental();
			//		toxic.MoveToWorld( this.Location, this.Map ); break;
			//	case 11: Mobile electrum = new ElectrumElemental();
			//		electrum.MoveToWorld( this.Location, this.Map ); break;
			//	case 12: Mobile platinum = new PlatinumElemental();
			//		platinum.MoveToWorld( this.Location, this.Map ); break;


			}

			this.Delete();
		}

		public LivingRock( Serial serial ) : base( serial )
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
	}
}