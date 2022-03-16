using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;

using Solaris.ItemStore;							//for connection to item store data objects

namespace Server.Items
{
	//this is the parent class and main functionality for all item storage keys
	public class BaseStoreKey : Item, IItemStoreObject
	{
		//itemid for item storage keys
		const int ITEM_ID = 0x176B;		//0x176B - full keyring graphic
		
		//set the # of columns of entries to display on the gump.. default is 2
		public virtual int DisplayColumns{ get{ return 2; } }
		
		//this is the non-static accessor for the entry structure info, so it can access the nonstatic overloaded functions in each child class
		public virtual List<StoreEntry> EntryStructure
		{
			get
			{
				return new List<StoreEntry>();
			}
		}

		//the ItemStore object which is contained by the keys.  This object has all the functionality in item storage management 
		protected ItemStore _Store;
		
		//public accessor for Store
		public ItemStore Store{ get{ return _Store; } }
		
		
		
		//base constructor for script-defined keys
		public BaseStoreKey( int hue ) : this( null, hue )
		{
		}
		
		//base constructor for custom defined keys
		public BaseStoreKey( ItemStore Store, int hue ) : base( ITEM_ID )
		{
			if( Store == null )
			{
				//this makes a call to an overloadable function, so any derived object that is constructed gets the store entries loaded here
				_Store = GenerateItemStore();
			}
			else
			{
				//connect the specified item store to this new set of keys
				_Store = Store;
			}
			
			//let the item store know that this keyring is its containing object
			_Store.Owner = this;
			
			_Store.MinWithdrawAmount = 1;
			
			Hue = hue;
			Weight = 1;
		}
		
		
		//TODO: fix column number munching with master keys
		//TODO: fix connection breaking with listentry when withdrawn from masterkey
		
		
		//serial constructor
		public BaseStoreKey( Serial serial ) : base( serial )
		{
		}
		
		//this allows to explicitly reset the store contents
		public void SetStore( ItemStore newstore )
		{
			_Store = newstore;
		}
		
		//the basic initialization of the item store
		protected virtual ItemStore GenerateItemStore()
		{

			//load the item entry structure.  Note that contents is specific because EntryStructure can be
			//overloaded in child entities
			
			ItemStore newstore = new ItemStore( StoreEntry.CloneList( EntryStructure ) );
			
			
			
			//write the new display column number to the store
			newstore.DisplayColumns = DisplayColumns;
			newstore.RegisterEntries();
			
			return newstore;
		}

		//IItemStoreObject methods
		
		//checks if the person trying to access the storekeys can do it
		public bool CanUse( Mobile from )
		{
			//definition for if a player can use these keys: needs to be in backpack.  Also, gamemasters+ have read-only access
			//Note: read-only access is maintained by the gump
			if( !IsChildOf( from.Backpack )  )
			{
				//TODO: look for cliloc equivalent?
				from.SendMessage( Name + " must be in your backpack to use." );
				return false;
			}
			
			return true;
			
		}
		
		//these are accessed through the context menu use
		public void Add( Mobile from )
		{
			_Store.AddItem( from );
		}
		
		//these are accessed through the context menu use
		public void Fill( Mobile from )
		{
			_Store.FillFromBackpack( from );
		}
		
		//context menu entries
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( CanUse( from ) )
			{
				list.Add( new KeyOpenEntry( from, this, 1 ) );
				list.Add( new KeyAddEntry( from, this, 2 ) );
				list.Add( new KeyFillEntry( from, this, 3 ) );
			}
		}
		


		
		//scans thru the contents for requested consumables and returns a list of all usable candidates.  The foundentries boolean
		//array holds recod of previously found resources from other IItemStoreObjects previously scanned, and thus are ignored
		public List<StoreEntry> FindConsumableEntries( Type[] types, int[] amounts, ref bool[] foundentries )
		{
			List<StoreEntry> stores = new List<StoreEntry>();
			
			for( int i = 0; i < types.Length; i++ )
			{
				//ignore it if this has already been found in another storage
				if( foundentries[i] )
				{
					continue;
				}
				
				//find a match in this key's store
				int index = StoreEntry.IndexOfType( _Store.StoreEntries, types[i], true );
				
				//check if there was a match, and there is a sufficient amount
				if( index > -1 && _Store.StoreEntries[index].Amount >= amounts[i] )
				{
					//add to the list to return
					stores.Add( _Store.StoreEntries[index] );
					
					//record the amount to consume, so if the operation is a success, the store entry will perform the consumption
					_Store.StoreEntries[index].ToConsume = amounts[i];
					
					//flag this entry as found
					foundentries[i] = true;
				}
			}
			
			
			
			return stores;
		}
		
		
		public StoreEntry FindConsumableEntry( Type[] types, int amount )
		{
			//find a match in this key's store
			int index = StoreEntry.IndexOfType( _Store.StoreEntries, types, true );
				
			//check if there was a match, and there is a sufficient amount
			if( index > -1 && _Store.StoreEntries[index].Amount >= amount )
			{
				//return a reference to this
				return _Store.StoreEntries[index];
			}
			
			//nothing suitable found, return null
			return null;
		}
		
		//look for the entry based on the entry type and the specified search parameters
		public StoreEntry FindEntryByEntryType( Type entrytype, int amount, object[] parameters )
		{
			foreach( StoreEntry entry in _Store.StoreEntries )
			{
				if( entry.GetType() == entrytype )
				{

					
					if( entry.Match( amount, parameters ) )
					{
						
						return entry;
					}
				}
			}
			return null;
			
			
		}
		
		
		
		//events
		
		public override void OnDoubleClick( Mobile from )
		{
			if( !CanUse( from ) )
			{
				return;
			}
			
			if( _Store != null )
			{
				//send doubleclick event to item store
				_Store.DoubleClick( from );
			}
			
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
			
			if( _Store != null )
			{
				//flag that there is item data to be written
				writer.Write( true );
				_Store.Serialize( writer );
			}
			else
			{
				writer.Write( false );
			}
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			switch( version )
			{
				case 0:
				default:
				{
					if( reader.ReadBool() )
					{
						_Store = new ItemStore( reader );
						
						//this makes sure the script listing synchronizes with the saved keys
						_Store.SynchronizeStore( EntryStructure );
						
						//sunch up the display column number from the item definition.
						_Store.DisplayColumns = DisplayColumns;
						
						//reference this so the store can connect back to this
						_Store.Owner = this;
					}
					break;
				}
				
				
			}
		}
		
		//static methods
		
		//special case of Consume, for only one type
		public static bool Consume( Container pack, Type type, int amount )
		{
			return Consume( pack, new Type[]{ type }, new int[]{ amount } );
		}
		
		//this performs a consume operation on a list of source items.  Since resources can be distributed among different items,
		//this handles a distributed search and take operation
		public static bool Consume( Container pack, Type[] types, int[] amounts )
		{
			//check if there are any BaseStoreKey or MasterItemStoreKey objects in the caster's backpack
			Item[] keysources = pack.FindItemsByType( new Type[]{ typeof( BaseStoreKey ), typeof( MasterItemStoreKey ) } );
			
			if( keysources == null || types == null || amounts == null )
			{
				return false;
			}
			
			//a list of any items that are found in the backpack after they were not found in the keys
			List<Type> backpacksources = new List<Type>();
			
			//the corresponding amounts to be withdrawn from the items found in the backpack
			List<int> backpackwithdrawamounts = new List<int>();
			
			
			//boolean array flag used to indicate which types have been found while in the middle of scanning all sources
			bool[] foundentries = new bool[ types.Length ];
			
			List<StoreEntry> consumeentries = new List<StoreEntry>();
			
			//go thru the list of found objects			
			foreach( Item key in keysources )
			{
				//utilizes IItemStoreObject interface function, defined by keys
				if( key is IItemStoreObject )
				{
					//scan this object for any usable candidates to withdraw from
					consumeentries.AddRange( ((IItemStoreObject)key).FindConsumableEntries( types, amounts, ref foundentries ) );
					
					//check if we're done
					if( consumeentries.Count == types.Length )
					{
						break;
					}
				}
			}
			
			//check if the operation was complete.  If not, look for any more in the backpack
			if( consumeentries.Count < types.Length )
			{
				for( int i = 0; i < types.Length; i++ )
				{
					//if this isn't found yet
					if( !foundentries[i] )
					{
						//find any item, and check if there's enough to consume
						
						Item[] items = pack.FindItemsByType( types[i] );

						int total = 0;
			
						for ( int j = 0; j < items.Length; j++ )
						{
							total += items[j].Amount;
						}
			
						//make sure the total found is sufficient
						if ( total >= amounts[i] )
						{
							//add this source to the list to be extracted from the backpack
							backpacksources.Add( types[i] );
							backpackwithdrawamounts.Add( amounts[i] );
							foundentries[i] = true;
						}
					}
				}
				
				//second pass, check if scanning the backpack has given us enough now	
				if( consumeentries.Count + backpacksources.Count < types.Length )
				{
					return false;
				}
			}
					
			//if we found everything we need, then consume them
			
			//perform the consumption from backpack
			foreach( StoreEntry entry in consumeentries )
			{
				entry.Consume(); 
				entry.RefreshParentGump();
			}
			
			//perform the consumption from backpack (if it was necessary)
			for( int i = 0; i < backpacksources.Count; i++ )
			{
				pack.ConsumeTotal( backpacksources[i], backpackwithdrawamounts[i] );
			}
			
			return true;
		}//static consume
		
		//this is used to overload the typically used Container method FindItemByType( Type type ) thing, allowing the keys to withdraw if it's not found
		public static Item FindItemByType( Container pack, Type type )
		{
			return FindItemByType( pack, type, 1 );
		}
		
		//note: this only works for stackable items.
		public static Item FindItemByType( Container pack, Type type, int amount )
		{
			//can't search a null pack!
			if( pack == null )
			{
				return null;
			}
			
			//first check the backpack if it exists
			Item bagitem = pack.FindItemByType( type );
			
			//if not, then look to pull from keys
			if( ( bagitem == null || bagitem.Amount < amount ) && CraftWithdraw( pack, new Type[]{ type }, amount - ( bagitem != null ? bagitem.Amount : 0 ) ) )
			{
				//the keys will have added this item to pack, so go looking for it again
				return pack.FindItemByType( type );
			}
			
			//return either the item found from the bag, or null if they weren't found in keys either
			return bagitem;
		}
		
		
		
		//this is used by the craft system to withdraw required resource type from any keys found in the specified container
		//Note: this is rather inefficient as it does this for every resource type.... unfortunately a better solution would mean
		//a more drastic modification of the craft engine
		public static bool CraftWithdraw( Container pack, Type[] types, int amount )
		{
			//check if there are any BaseStoreKey or MasterItemStoreKey objects in the caster's backpack
			Item[] keysources = pack.FindItemsByType( new Type[]{ typeof( BaseStoreKey ), typeof( MasterItemStoreKey ) } );
			
			if( keysources == null || types == null || amount == 0 )
			{
				return false;
			}
			
			//go thru the list of found objects			
			foreach( Item key in keysources )
			{
				//utilizes IItemStoreObject interface function, defined by keys
				if( key is IItemStoreObject )
				{
					//scan this object for any usable candidates to withdraw from
					StoreEntry entry = ((IItemStoreObject)key).FindConsumableEntry( types, amount );
					
					if( entry != null )
					{
						//if a valid entry was found, withdraw it to the container
						pack.AddItem( entry.Withdraw( amount ) );
						entry.RefreshParentGump();
						return true;
						
					}
				}
			}
			
			//if nothing was found, return false
			return false;
		}//static CraftWithdraw
		
		//this is used to withdraw based on a particular store entry, and specified parameters
		public static Item WithdrawByEntryType( Container pack, Type entrytype, int amount, object[] parameters )
		{
			//check if there are any BaseStoreKey or MasterItemStoreKey objects in the caster's backpack
			Item[] keysources = pack.FindItemsByType( new Type[]{ typeof( BaseStoreKey ), typeof( MasterItemStoreKey ) } );
			
			if( keysources == null || amount == 0 )
			{
				return null;
			}
			

			
			//go thru the list of found objects			
			foreach( Item key in keysources )
			{
				//utilizes IItemStoreObject interface function, defined by keys
				if( key is IItemStoreObject )
				{
					//scan this object for any usable candidates to withdraw from
					StoreEntry entry = ((IItemStoreObject)key).FindEntryByEntryType( entrytype, amount, parameters );
					
					
					
					if( entry != null )
					{
						Item item = entry.Withdraw( 1, true );
						
						entry.RefreshParentGump();

						return item;
					}
				}
			}

			return null;
			
		}
		
		
	}//class BaseStoreKey



}
