using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class AdventurersKey : BaseStoreKey
	{
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				entry.Add( new ResourceEntry( typeof( Bandage ), "Bandages" ) );
				entry.Add( new ResourceEntry( typeof( BolaBall ), "Bola Balls" ) );
				entry.Add( new ResourceEntry( typeof( Bola ), "Bola" ) );
				entry.Add( new ResourceEntry( typeof( ZoogiFungus ), "Zoogi Fungus" ) );
				entry.Add( new ResourceEntry( typeof( PowderOfTranslocation ), "Powder of Trans." ) );
				return entry;
			}
		}
		
		
		
		[Constructable]
		public AdventurersKey() : base( 1151 )		//hue 1151
		{
			Name = "Adventurer's Keys";
		}
		
		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();
			
			//properties of this storage device
			store.Label = "Adventurer's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			
			return store;
		}
		
		//serial constructor
		public AdventurersKey( Serial serial ) : base( serial )
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