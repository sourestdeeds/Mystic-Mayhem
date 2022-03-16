using System;
using Server;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	public class LivingBonePile : Item
	{

		[Constructable]
		public LivingBonePile( ) : base( 0x1B09 + Utility.Random( 8 ) )
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
				case 0: Mobile wood = new Ent();
					wood.MoveToWorld( this.Location, this.Map ); break;
				case 1: Mobile ash = new AshEnt();
					ash.MoveToWorld( this.Location, this.Map ); break;
				case 2: Mobile oak = new OakEnt();
					oak.MoveToWorld( this.Location, this.Map ); break;
				case 3: Mobile yew = new YewEnt();
					yew.MoveToWorld( this.Location, this.Map ); break;
				case 4: Mobile heartwood = new HeartwoodEnt();
					heartwood.MoveToWorld( this.Location, this.Map ); break;
				case 5: Mobile blood = new BloodwoodEnt();
					blood.MoveToWorld( this.Location, this.Map ); break;
				case 6: Mobile frost = new FrostwoodEnt();
					frost.MoveToWorld( this.Location, this.Map ); break;
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

		public LivingBonePile( Serial serial ) : base( serial )
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