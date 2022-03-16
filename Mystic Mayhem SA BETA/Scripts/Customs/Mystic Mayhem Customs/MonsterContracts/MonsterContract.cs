using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x14EF, 0x14F0 )]
	public class MonsterContract : Item
	{
		private string m_type;
		private int reward;
		private int gen;
		private int m_amount;
		private int m_killed;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public string Monster
		{
			get{ return m_type; }
			set{ m_type = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Reward
		{
			get{ return reward; }
			set{ reward = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Gen
		{
			get{ return gen; }
			set{ gen = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountToKill
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountKilled
		{
			get{ return m_killed; }
			set{ m_killed = value; }
		}
		
		[Constructable]
		public MonsterContract( int gen ) : base( 0x14EF )
		{
			Movable = true;
			//LootType = LootType.Blessed;
			Gen = gen; //Utility.Random( 6 );
			Monster = GetRandomMonster( Gen );
			AmountToKill = Utility.Random( 10 ) + 5;
			Reward = 97 * (Gen + 1) * AmountToKill + 500; //Utility.Random( 150 )
			Name = "a Contract: " + AmountToKill + " " + Monster + "s";
			AmountKilled = 0;
		}
		
		[Constructable]
		public MonsterContract( ) : base( 0x14EF )
		{
			Movable = true;
			//LootType = LootType.Blessed;
			Monster = "";
			AmountToKill = 0;
			//Reward = gpreward;
			Name = "a monster contract";
			AmountKilled = 0;
		}

	/*	public override void OnSingleClick( Mobile from ) 
		{ 
			Name = "a Contract: " + AmountToKill + " " + Monster + "s";
			//if ( Validate() ) 
			base.OnSingleClick( from ); 
		} */
		
		public override void OnDoubleClick( Mobile from )
		{
			//Name = "a Contract: " + AmountToKill + " " + Monster + "s";
			if( IsChildOf( from.Backpack ) )
			{
				from.SendGump( new MonsterContractGump( from, this ) );
			} 
			else 
		    {
		    	from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
		    }
		}
		
		public MonsterContract( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		
			writer.Write( m_type );
			writer.Write( gen );
			writer.Write( reward );
			writer.Write( m_amount );
			writer.Write( m_killed );
		}

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
			
			m_type = reader.ReadString();
			gen = reader.ReadInt();
			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_killed = reader.ReadInt();
			LootType = LootType.Blessed;
		}
		
		public string GetRandomMonster( int genre )
		{
			switch ( genre )
			{
				case 0:
					switch (Utility.Random( 11 ) )
					{
						case 0:	return "Mongbat";
						case 1:	return "Skeleton";
						case 2:	return "Headless One";
						case 3:	return "Eagle";
						case 4: return "Earth Elemental";
						case 5:	return "Zombie";
						case 6:	return "Wraith";
						case 7:	return "Harpy";
						case 8:	return "Giant Spider";
						case 9: return "Scorpion";
						case 10: return "Slime";
						default: return "Eagle";
					}
				case 1:
					switch (Utility.Random( 12 ) )
					{
						case 0:	return "Gargoyle";
						case 1:	return "Imp";
						case 2:	return "Bogling";
						case 3:	return "Alligator";
						case 4: return "Gazer";
						case 5:	return "Ogre";
						case 6:	return "Ettin";
						case 7:	return "Troll";
						case 8:	return "Reaper";
						case 9:	return "Mummy";
						case 10: return "Fire Elemental";
						case 11: return "Sea Serpent";
						default: return "Ettin";
					}
				case 2:
					switch (Utility.Random( 9 ) )
					{
						case 0:	return "Wanderer of the Void";
						case 1:	return "Phoenix";
						case 2:	return "Lich";
						case 3:	return "Dread Spider";
						case 4:	return "Drake";
						case 5: return "Wyvern";
						case 6:	return "Cyclops";
						case 7:	return "Stone Gargoyle";
						case 8:	return "Sand Vortex";
						default: return "Wyvern";
					}
				case 3:
					switch (Utility.Random( 10 ) )
					{
						case 0:	return "Blood Elemental";
						case 1:	return "Poison Elemental";
						case 2:	return "Elder Gazer";
						case 3:	return "Arctic Ogre Lord";
						case 4:	return "Ogre Lord";
						case 5:	return "Titan";
						case 6: return "Efreet";
						case 7:	return "Ice Elemental";
						case 8:	return "Kraken";
						case 9:	return "Fire Steed";
						default: return "Titan";
					}
				case 4:
					switch (Utility.Random( 10 ) )
					{
						case 0:	return "Dragon";
						case 1:	return "Lich Lord";
						case 2:	return "Succubus";
						case 3:	return "Rotting Corpse";
						case 4:	return "White Wyrm";
						case 5: return "Fan Dancer";
						case 6:	return "Oni";
						case 7:	return "Juka Lord";
						case 8:	return "Hiryu";
						case 9:	return "Cu Sidhe";
						default: return "White Wyrm";
					}
				case 5:
					switch (Utility.Random( 10 ) )
					{
						case 0:	return "Ancient Wyrm";
						case 1:	return "Ancient Dragon";
						case 2:	return "Shadow Wyrm";
						case 3:	return "Fleshrenderer";
						case 4:	return "Guardian Dragon";
						case 5:	return "Bone Demon";
						case 6: return "Leviathan";
						case 7:	return "Yamandon";
						case 8:	return "Charger of the Fallen";
						case 9:	return "Skeletal Dragon";
						default: return "Ancient Wyrm";
					}
				default:
					switch (Utility.Random( 4 ) )
					{
						case 0:	return "Ancient Wyrm";
						case 1:	return "Dragon";
						case 2:	return "White Wyrm";
						case 3:	return "Phoenix";
						default: return "Dragon";
					}
			}
		}
	}
}
