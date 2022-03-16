using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RunebookPouch : Pouch
	{
		[Constructable]
		public RunebookPouch() : this( 1 )
		{
			Movable = true;
			Hue = 0x38;
			Name = " Book Pouch ";
			LootType = LootType.Blessed;
			MaxItems = 25;
		}

		[Constructable]
		public RunebookPouch( int amount )
		{
		}

		public RunebookPouch( Serial serial ) : base( serial )
		{
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Runebook )
			{  
			Runebook runeb = (Runebook)dropped;			
			DropItem ( runeb );
			return true;
			}
		/*	else if ( dropped is SOSBook )
			{  
			SOSBook runeb = (SOSBook)dropped;			
			DropItem ( runeb );
			return true;
			} */
			else if ( dropped is SOSBookT )
			{  
			SOSBookT runeb = (SOSBookT)dropped;			
			DropItem ( runeb );
			return true;
			}
		/*	else if ( dropped is ContractBook )
			{  
			ContractBook runeb = (ContractBook)dropped;			
			DropItem ( runeb );
			return true;
			} */
			else if ( dropped is TMapBook )
			{  
			TMapBook runeb = (TMapBook)dropped;			
			DropItem ( runeb );
			return true;
			}
			else if ( dropped is RecallRune )
			{  
			RecallRune runeb = (RecallRune)dropped;			
			DropItem ( runeb );
			return true;
			}
			else if ( dropped is StaffRunebook )
			{  
			StaffRunebook runeb = (StaffRunebook)dropped;			
			DropItem ( runeb );
			return true;
			}
			else
		
			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
		{
			if ( dropped is Runebook )
			{
			Runebook runeb = (Runebook)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			}
		/*	else if ( dropped is SOSBook )
			{
			SOSBook runeb = (SOSBook)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			} */
			else if ( dropped is SOSBookT )
			{
			SOSBookT runeb = (SOSBookT)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			}
		/*	else if ( dropped is ContractBook )
			{
			ContractBook runeb = (ContractBook)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			} */
			else if ( dropped is TMapBook )
			{
			TMapBook runeb = (TMapBook)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			}
			else if ( dropped is RecallRune )
			{
			RecallRune runeb = (RecallRune)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			}
			else if ( dropped is StaffRunebook )
			{
			StaffRunebook runeb = (StaffRunebook)dropped;
			runeb.Location = new Point3D( p.X, p.Y, 0 );
			AddItem ( runeb );
			return true;
			}
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
