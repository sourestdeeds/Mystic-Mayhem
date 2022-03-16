using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class TreasureHuntersKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 1; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new ResourceEntry( typeof( Lockpick ), "Lockpicks" ) );
				entry.Add( new ToolEntry( typeof( Shovel ), new Type[]{ typeof( SturdyShovel ) }, "Shovel", 0, 35, -10, -10 ) );
				entry.Add( new ListEntry( typeof( TreasureMap ), typeof( TreasureMapListEntry ), "Treasure Maps" ) );
				entry.Add( new ListEntry( typeof( SOS ), typeof( SOSListEntry ), "SOS's" ) );
				//TODO: define this entry
				entry.Add( new ListEntry( typeof( SpecialFishingNet ), typeof( SpecialFishingNetListEntry ), "Fishing Nets" ) );
				//TODO: how do you display net hue on the gump?  have a column's text hued the net color?  Have the name say "colored net?"  have whole text hued that color?
				
				return entry;
			}
		}
		
		
		
		[Constructable]
		public TreasureHuntersKey() : base( 1861 )		//hue 1861
		{
			Name = "Treasure Hunter's Keys";
		}
		
		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Treasure Hunter's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public TreasureHuntersKey( Serial serial ) : base( serial )
		{
		}
		
		//events
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}



}