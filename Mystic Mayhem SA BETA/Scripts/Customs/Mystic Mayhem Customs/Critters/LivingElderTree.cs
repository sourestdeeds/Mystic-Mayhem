using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class LivingElderTree : Item
	{

		[Constructable]
		public LivingElderTree( ) : base( Utility.RandomList( 3323, 3326, 3329  ) )
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
			switch ( Utility.Random( 7 ) )
			{
				case 0: Mobile woodelder = new ElderEnt();
					woodelder.MoveToWorld( this.Location, this.Map ); break;
				case 1: Mobile ashelder = new AshElderEnt();
					ashelder.MoveToWorld( this.Location, this.Map ); break;
				case 2: Mobile oakelder = new OakElderEnt();
					oakelder.MoveToWorld( this.Location, this.Map ); break;
				case 3: Mobile yewelder = new YewElderEnt();
					yewelder.MoveToWorld( this.Location, this.Map ); break;
				case 4: Mobile heartwoodelder = new HeartwoodElderEnt();
					heartwoodelder.MoveToWorld( this.Location, this.Map ); break;
				case 5: Mobile bloodelder = new BloodwoodElderEnt();
					bloodelder.MoveToWorld( this.Location, this.Map ); break;
				case 6: Mobile frostelder = new FrostwoodElderEnt();
					frostelder.MoveToWorld( this.Location, this.Map ); break;
//				case 7: Mobile verite = new VeriteElemental();
//					verite.MoveToWorld( this.Location, this.Map ); break;
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

		public LivingElderTree( Serial serial ) : base( serial )
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