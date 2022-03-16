using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseManaRefreshPotion : BasePotion
	{
		public abstract double Refresh{ get; }

		public BaseManaRefreshPotion( PotionEffect effect/*, int amount*/ ) : base( 0xF0B, effect/*, amount*/ )
		{
		}

		public BaseManaRefreshPotion( Serial serial ) : base( serial )
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

		public override void Drink( Mobile from )
		{
			if ( from.Mana < from.ManaMax )
			{
				if ( from.BeginAction( typeof( BaseManaRefreshPotion ) ) )
				{
					from.Mana += Scale( from, (int)(Refresh * from.ManaMax) );

					BasePotion.PlayDrinkEffect( from );

					this.Amount--;
					if (this.Amount <= 0)
						this.Delete();

					Timer.DelayCall( TimeSpan.FromSeconds( 10.0 ), new TimerStateCallback( ReleaseManaLock ), from );
				}
				else
					from.LocalOverheadMessage( MessageType.Regular, 0x22, true, "You must wait 10 seconds before using another mana potion." );

			}
			else
				from.SendMessage( "You decide against drinking this potion, as you are already at full mana." );
		}

		private static void ReleaseManaLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseManaRefreshPotion ) );
		}
	}
}
