using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.Plants;

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class GardenersKey : BaseStoreKey
	{
		//set the # of columns of entries to display on the gump.. default is 2
		public override int DisplayColumns{ get{ return 1; } }
		
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new BeverageEntry( typeof( Pitcher ), BeverageType.Water, "Water", 0, 20, -3, 0 ) );
				entry.Add( new PotionEntry( typeof( GreaterCurePotion ), "Greater Cure", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterHealPotion ), "Greater Heal", 0, 20, -7, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterPoisonPotion ), "Greater Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterStrengthPotion ), "Greater Strength", 0, 20, -5, 0  ) );
				//the filled plant item is a new class, called PlantItem, so no special handling required to block filled plant bowls
				//being added
				entry.Add( new ResourceEntry( typeof( PlantBowl ), "Plant Bowl", 0, 25, 0, 0  ) );
				entry.Add( new ResourceEntry( typeof( FertileDirt ), "Fertile Dirt", 0, 25, 0, 0  ) );
				entry.Add( new ListEntry( typeof( Seed ), typeof( SeedListEntry ), "Seeds" ) );
				return entry;
			}
		}

		
		[Constructable]
		public GardenersKey() : base( 62 )		//hue 62
		{
			Name = "Gardener's Keys";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Gardener's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			
			return store;
		}
		
		//serial constructor
		public GardenersKey( Serial serial ) : base( serial )
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