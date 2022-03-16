using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.Network;

namespace Server.Items
{
	public class TrashBasket : Container
	{

		public override int DefaultMaxWeight{ get{ return 0; } } // A value of 0 signals unlimited weight

		public override int DefaultGumpID{ get{ return 0x3F; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 19, 47, 163, 76 ); }
		}

	/*	public override bool CanStore( Mobile m )
		{
			return true; 
		} */

		[Constructable]
		public TrashBasket() : base( 0xE79 )
		{
			Name = "Trash Basket";
			Hue = 33;
			Movable = true;
			LootType = LootType.Blessed;
		}

		public TrashBasket( Serial serial ) : base( serial )
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

			if ( Items.Count > 0 )
			{
				m_Timer = new EmptyTimer( this );
				m_Timer.Start();
			}
		}

//aa
		public override void UpdateTotal(Item sender, TotalType type, int delta)
		{
			base.UpdateTotal(sender,type,delta);
			if(type==TotalType.Weight)
			{
				if ( Parent is Item )
					( Parent as Item ).UpdateTotal( sender, type, (int)(delta*1)*-1 );
				else if ( Parent is Mobile )
					( Parent as Mobile ).UpdateTotal( sender, type, (int)(delta*1)*-1 );
			}
		}
		public override int GetTotal(TotalType type)
		{
			if(type==TotalType.Weight)
				return (int)(base.GetTotal(type)*(1.0-1));
			return base.GetTotal(type);
		}
//aa

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			Item drop = dropped as Item;
			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			if ( drop.LootType == LootType.Blessed  || drop.Insured )
			{
				//PublicOverheadMessage( MessageType.Regular, 0x3B2, true, "This can not trash items that are Blessed or Insured!" );
				from.SendMessage("This can not trash items that are Blessed or Insured!" );
				return false;
			}

			if ( TotalItems >= 50 )
			{
				Empty( );
			}
			else
			{
				from.SendMessage(" The item will be deleted in 30 seconds");

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			Item drop = item as Item;
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			if ( drop.LootType == LootType.Blessed || drop.Insured )
			{
				//PublicOverheadMessage( MessageType.Regular, 0x3B2, true, "This can not trash items that are Blessed or Insured!" );
				from.SendMessage("This can not trash items that are Blessed or Insured!" );
				return false;
			}

			if ( TotalItems >= 50 )
			{
				Empty( ); 
			}
			else
			{
				from.SendMessage(" The item will be deleted in 30 seconds");

				if ( m_Timer != null )
					m_Timer.Stop();
				else
					m_Timer = new EmptyTimer( this );

				m_Timer.Start();
			}

			return true;
		}

		public void Empty( )
		{
			List<Item> items = this.Items;

			if ( items.Count > 0 )
			{
				//PublicOverheadMessage( MessageType.Regular, 0x3B2, true, "Empting Trash Basket!" );

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					((Item)items[i]).Delete();
				}
			}

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private Timer m_Timer;

		private class EmptyTimer : Timer
		{
			private TrashBasket m_Basket;

			public EmptyTimer( TrashBasket basket ) : base( TimeSpan.FromSeconds( 30.0 ) )
			{
				m_Basket = basket;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Basket.Empty( );
			}
		}
	}
}