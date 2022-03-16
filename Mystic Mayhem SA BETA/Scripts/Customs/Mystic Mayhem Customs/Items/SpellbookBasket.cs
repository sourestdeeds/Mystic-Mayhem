using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpellbookBasket : BaseContainer
	{
		public override int DefaultGumpID{ get{ return 0x108; } }
		public override int DefaultDropSound{ get{ return 0x4F; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 19, 47, 163, 76 ); }
		}

		[Constructable]
		public SpellbookBasket() : base( 0x24D9 )
		{
			Movable = true;
			Hue = 1259;
			Name = "Spellbook Basket";
			LootType = LootType.Blessed;
		}

		public SpellbookBasket( Serial serial ) : base( serial )
		{
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Spellbook )
			{  
			Spellbook spellb = (Spellbook)dropped;			
			DropItem ( spellb );
			return true;
			}
		/*	else if ( dropped is SOSBook )
			{  
			SOSBook spellb = (SOSBook)dropped;			
			DropItem ( spellb );
			return true;
			}
			else if ( dropped is SOSBookT )
			{  
			SOSBookT spellb = (SOSBookT)dropped;			
			DropItem ( spellb );
			return true;
			}
			else if ( dropped is ContractBook )
			{  
			ContractBook spellb = (ContractBook)dropped;			
			DropItem ( spellb );
			return true;
			}
			else if ( dropped is TMapBook )
			{  
			TMapBook spellb = (TMapBook)dropped;			
			DropItem ( spellb );
			return true;
			}
			else if ( dropped is RecallRune )
			{  
			RecallRune spellb = (RecallRune)dropped;			
			DropItem ( spellb );
			return true;
			} */
			else
		
			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
		{
			if ( dropped is Spellbook )
			{
			Spellbook spellb = (Spellbook)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			}
		/*	else if ( dropped is SOSBook )
			{
			SOSBook spellb = (SOSBook)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			}
			else if ( dropped is SOSBookT )
			{
			SOSBookT spellb = (SOSBookT)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			}
			else if ( dropped is ContractBook )
			{
			ContractBook spellb = (ContractBook)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			}
			else if ( dropped is TMapBook )
			{
			TMapBook spellb = (TMapBook)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			}
			else if ( dropped is RecallRune )
			{
			RecallRune spellb = (RecallRune)dropped;
			spellb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( spellb );
			return true;
			} */
			else

			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
