using System;
using Server.Items;
using Server.Network;
using Server.Misc;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	
	public class MagicalLockbox2 : MetalGoldenChest
	{
		[Constructable]
		public MagicalLockbox2 ()
                                                                                          
		{
                                      Name = "The Treasure Horde Of Minax"; 
                                      Hue = 1194;
            Locked = true;
			TrapLevel = 5;
			TrapPower = 5;
			TrapType = TrapType.PoisonTrap;

			DropItem( new Ruby( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amber( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amethyst( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Citrine( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Diamond( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Emerald( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Sapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new StarSapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Tourmaline( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new MagicJewel( Utility.RandomMinMax( 16, 10 ) ) );
			DropItem( new Ruby( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amber( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amethyst( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Citrine( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Diamond( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Emerald( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Sapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new StarSapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Tourmaline( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new MagicJewel( Utility.RandomMinMax( 16, 10 ) ) );
			DropItem( new Ruby( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amber( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amethyst( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Citrine( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Diamond( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Emerald( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Sapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new StarSapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Tourmaline( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new MagicJewel( Utility.RandomMinMax( 16, 10 ) ) );
			DropItem( new Ruby( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amber( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amethyst( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Citrine( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Diamond( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Emerald( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Sapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new StarSapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Tourmaline( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new MagicJewel( Utility.RandomMinMax( 16, 10 ) ) );
			DropItem( new Ruby( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amber( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Amethyst( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Citrine( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Diamond( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Emerald( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Sapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new StarSapphire( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new Tourmaline( Utility.RandomMinMax( 16, 25 ) ) );
			DropItem( new MagicJewel( Utility.RandomMinMax( 16, 10 ) ) );
			DropItem( new JusticeStone() );
			DropItem( new HandGrenade() );
			
			switch ( Utility.Random( 110 ) ) 
			{ 
				case 0: 				DropItem( new PowerScroll( SkillName.Blacksmith, 120 ) ); break;  
				case 1: case 2: 			DropItem( new PowerScroll( SkillName.Blacksmith, 115 ) ); break; 
				case 3: case 4: case 5: 		DropItem( new PowerScroll( SkillName.Blacksmith, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	DropItem( new PowerScroll( SkillName.Blacksmith, 105 ) ); break; 
  
				case 10: 				DropItem( new PowerScroll( SkillName.Tailoring, 120 ) ); break;  
				case 11: case 12: 			DropItem( new PowerScroll( SkillName.Tailoring, 115 ) ); break; 
				case 13: case 14: case 15: 		DropItem( new PowerScroll( SkillName.Tailoring, 110 ) ); break; 
				case 16: case 17: case 18: case 19: 	DropItem( new PowerScroll( SkillName.Tailoring, 105 ) ); break;  

				case 20: 				DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 21: case 22: 			DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 23: case 24: case 25: 		DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 26: case 27: case 28: case 29: 	DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break; 
  
				case 30: 				DropItem( new PowerScroll( SkillName.Mining, 120 ) ); break;  
				case 31: case 32: 			DropItem( new PowerScroll( SkillName.Mining, 115 ) ); break; 
				case 33: case 34: case 35: 		DropItem( new PowerScroll( SkillName.Mining, 110 ) ); break; 
				case 36: case 37: case 38: case 39: 	DropItem( new PowerScroll( SkillName.Mining, 105 ) ); break;
 
				case 40: 				DropItem( new PowerScroll( SkillName.Carpentry, 120 ) ); break;  
				case 41: case 42: 			DropItem( new PowerScroll( SkillName.Carpentry, 115 ) ); break; 
				case 43: case 44: case 45: 		DropItem( new PowerScroll( SkillName.Carpentry, 110 ) ); break; 
				case 46: case 47: case 48: case 49: 	DropItem( new PowerScroll( SkillName.Carpentry, 105 ) ); break; 

				case 50: 				DropItem( new PowerScroll( SkillName.Alchemy, 120 ) ); break;  
				case 51: case 52: 			DropItem( new PowerScroll( SkillName.Alchemy, 115 ) ); break; 
				case 53: case 54: case 55: 		DropItem( new PowerScroll( SkillName.Alchemy, 110 ) ); break; 
				case 56: case 57: case 58: case 59: 	DropItem( new PowerScroll( SkillName.Alchemy, 105 ) ); break; 

				case 60: 				DropItem( new PowerScroll( SkillName.Fletching, 120 ) ); break;  
				case 61: case 62: 			DropItem( new PowerScroll( SkillName.Fletching, 115 ) ); break; 
				case 63: case 64: case 65: 		DropItem( new PowerScroll( SkillName.Fletching, 110 ) ); break; 
				case 66: case 67: case 68: case 69: 	DropItem( new PowerScroll( SkillName.Fletching, 105 ) ); break;

				case 70: 				DropItem( new PowerScroll( SkillName.Inscribe, 120 ) ); break;  
				case 71: case 72: 			DropItem( new PowerScroll( SkillName.Inscribe, 115 ) ); break; 
				case 73: case 74: case 75: 		DropItem( new PowerScroll( SkillName.Inscribe, 110 ) ); break; 
				case 76: case 77: case 78: case 79: 	DropItem( new PowerScroll( SkillName.Inscribe, 105 ) ); break;
  
				case 80: 				DropItem( new PowerScroll( SkillName.Cartography, 120 ) ); break;  
				case 81: case 82: 			DropItem( new PowerScroll( SkillName.Cartography, 115 ) ); break; 
				case 83: case 84: case 85: 		DropItem( new PowerScroll( SkillName.Cartography, 110 ) ); break; 
				case 86: case 87: case 88: case 89: 	DropItem( new PowerScroll( SkillName.Cartography, 105 ) ); break;

				case 90: 				DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 91: case 92: 			DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 93: case 94: case 95: 		DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 96: case 97: case 98: case 99: 	DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break;

				case 100: 				DropItem( new PowerScroll( SkillName.Cooking, 120 ) ); break;  
				case 101: case 102: 			DropItem( new PowerScroll( SkillName.Cooking, 115 ) ); break; 
				case 103: case 104: case 105: 		DropItem( new PowerScroll( SkillName.Cooking, 110 ) ); break; 
				case 106: case 107: case 108: case 109: DropItem( new PowerScroll( SkillName.Cooking, 105 ) ); break; 
      			}
      			
      			switch ( Utility.Random( 110 ) ) 
			{ 
				case 0: 				DropItem( new PowerScroll( SkillName.Blacksmith, 120 ) ); break;  
				case 1: case 2: 			DropItem( new PowerScroll( SkillName.Blacksmith, 115 ) ); break; 
				case 3: case 4: case 5: 		DropItem( new PowerScroll( SkillName.Blacksmith, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	DropItem( new PowerScroll( SkillName.Blacksmith, 105 ) ); break; 
  
				case 10: 				DropItem( new PowerScroll( SkillName.Tailoring, 120 ) ); break;  
				case 11: case 12: 			DropItem( new PowerScroll( SkillName.Tailoring, 115 ) ); break; 
				case 13: case 14: case 15: 		DropItem( new PowerScroll( SkillName.Tailoring, 110 ) ); break; 
				case 16: case 17: case 18: case 19: 	DropItem( new PowerScroll( SkillName.Tailoring, 105 ) ); break;  

				case 20: 				DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 21: case 22: 			DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 23: case 24: case 25: 		DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 26: case 27: case 28: case 29: 	DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break; 
  
				case 30: 				DropItem( new PowerScroll( SkillName.Mining, 120 ) ); break;  
				case 31: case 32: 			DropItem( new PowerScroll( SkillName.Mining, 115 ) ); break; 
				case 33: case 34: case 35: 		DropItem( new PowerScroll( SkillName.Mining, 110 ) ); break; 
				case 36: case 37: case 38: case 39: 	DropItem( new PowerScroll( SkillName.Mining, 105 ) ); break;
 
				case 40: 				DropItem( new PowerScroll( SkillName.Carpentry, 120 ) ); break;  
				case 41: case 42: 			DropItem( new PowerScroll( SkillName.Carpentry, 115 ) ); break; 
				case 43: case 44: case 45: 		DropItem( new PowerScroll( SkillName.Carpentry, 110 ) ); break; 
				case 46: case 47: case 48: case 49: 	DropItem( new PowerScroll( SkillName.Carpentry, 105 ) ); break; 

				case 50: 				DropItem( new PowerScroll( SkillName.Alchemy, 120 ) ); break;  
				case 51: case 52: 			DropItem( new PowerScroll( SkillName.Alchemy, 115 ) ); break; 
				case 53: case 54: case 55: 		DropItem( new PowerScroll( SkillName.Alchemy, 110 ) ); break; 
				case 56: case 57: case 58: case 59: 	DropItem( new PowerScroll( SkillName.Alchemy, 105 ) ); break; 

				case 60: 				DropItem( new PowerScroll( SkillName.Fletching, 120 ) ); break;  
				case 61: case 62: 			DropItem( new PowerScroll( SkillName.Fletching, 115 ) ); break; 
				case 63: case 64: case 65: 		DropItem( new PowerScroll( SkillName.Fletching, 110 ) ); break; 
				case 66: case 67: case 68: case 69: 	DropItem( new PowerScroll( SkillName.Fletching, 105 ) ); break;

				case 70: 				DropItem( new PowerScroll( SkillName.Inscribe, 120 ) ); break;  
				case 71: case 72: 			DropItem( new PowerScroll( SkillName.Inscribe, 115 ) ); break; 
				case 73: case 74: case 75: 		DropItem( new PowerScroll( SkillName.Inscribe, 110 ) ); break; 
				case 76: case 77: case 78: case 79: 	DropItem( new PowerScroll( SkillName.Inscribe, 105 ) ); break;
  
				case 80: 				DropItem( new PowerScroll( SkillName.Cartography, 120 ) ); break;  
				case 81: case 82: 			DropItem( new PowerScroll( SkillName.Cartography, 115 ) ); break; 
				case 83: case 84: case 85: 		DropItem( new PowerScroll( SkillName.Cartography, 110 ) ); break; 
				case 86: case 87: case 88: case 89: 	DropItem( new PowerScroll( SkillName.Cartography, 105 ) ); break;

				case 90: 				DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 91: case 92: 			DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 93: case 94: case 95: 		DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 96: case 97: case 98: case 99: 	DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break;

				case 100: 				DropItem( new PowerScroll( SkillName.Cooking, 120 ) ); break;  
				case 101: case 102: 			DropItem( new PowerScroll( SkillName.Cooking, 115 ) ); break; 
				case 103: case 104: case 105: 		DropItem( new PowerScroll( SkillName.Cooking, 110 ) ); break; 
				case 106: case 107: case 108: case 109: DropItem( new PowerScroll( SkillName.Cooking, 105 ) ); break; 
      			}
  			
  				if ( Utility.RandomDouble() < 0.1 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: DropItem( new EtoileBleue() ); break;
					case 1: DropItem( new NovoBleue() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: DropItem( new RangerArms() ); break;
					case 1: DropItem( new RangerChest() ); break;
					case 2: DropItem( new RangerGloves() ); break;
					case 3: DropItem( new RangerGorget() ); break;
					case 4: DropItem( new RangerLegs() ); break;
				}
			}
			
			if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: DropItem( new PhoenixSleeves() ); break;
					case 1: DropItem( new PhoenixChest() ); break;
					case 2: DropItem( new PhoenixGloves() ); break;
					case 3: DropItem( new PhoenixGorget() ); break;
					case 4: DropItem( new PhoenixLegs() ); break;
					case 5: DropItem( new PhoenixHelm() ); break;
					
				}
			}
			
			if ( Utility.RandomDouble() < 0.05 )
			{
				switch ( Utility.Random( 32 ) )
				{
					case 0: DropItem( new BraceletOfHealth() ); break;
					case 1: DropItem( new OrnamentOfTheMagician() ); break;
					case 2: DropItem( new RingOfTheElements() ); break;
					case 3: DropItem( new RingOfTheVile() ); break;
					case 4: DropItem( new Aegis() ); break;
					case 5: DropItem( new LegacyOfTheDreadLord() ); break;
					case 6: DropItem( new TheDragonSlayer() ); break;
					case 7: DropItem( new ArmorOfFortune() ); break;
					case 8: DropItem( new GauntletsOfNobility() ); break;
					case 9: DropItem( new HelmOfInsight() ); break;
					case 10: DropItem( new HolyKnightsBreastplate() ); break;
					case 11: DropItem( new JackalsCollar() ); break;
					case 12: DropItem( new LeggingsOfBane() ); break;
					case 13: DropItem( new MidnightBracers() ); break;
					case 14: DropItem( new OrnateCrownOfTheHarrower() ); break;
					case 15: DropItem( new ShadowDancerLeggings() ); break;
					case 16: DropItem( new TunicOfFire() ); break;
					case 17: DropItem( new VoiceOfTheFallenKing() ); break;
					case 18: DropItem( new ArcaneShield() ); break;
					case 19: DropItem( new AxeOfTheHeavens() ); break;
					case 20: DropItem( new BladeOfInsanity() ); break;
					case 21: DropItem( new BoneCrusher() ); break;
					case 22: DropItem( new BreathOfTheDead() ); break;
					case 23: DropItem( new Frostbringer() ); break;
					case 24: DropItem( new SerpentsFang() ); break;
					case 25: DropItem( new StaffOfTheMagi() ); break;
					case 26: DropItem( new TheBeserkersMaul() ); break;
					case 27: DropItem( new TheDryadBow() ); break;
					case 28: DropItem( new DivineCountenance() ); break;
					case 29: DropItem( new HatOfTheMagi() ); break;
					case 30: DropItem( new HuntersHeaddress() ); break;
					case 31: DropItem( new SpiritOfTheTotem() ); break;
				}
			}
			

//Strange Gems Begin

			
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeAmethyst1(  ) );	break;
					case 1:	DropItem( new StrangeAmethyst2(  ) );	break;
					case 2:	DropItem( new StrangeAmethyst3(  ) );	break;
				}
			
			
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeCitrine1(  ) );	break;
					case 1:	DropItem( new StrangeCitrine2(  ) );	break;
					case 2:	DropItem( new StrangeCitrine3(  ) );	break;
				}
			
			
				switch ( Utility.Random( 4 ) )
				{
					case 0:	DropItem( new StrangeDiamond1(  ) );	break;
					case 1:	DropItem( new StrangeDiamond2(  ) );	break;
					case 2:	DropItem( new StrangeDiamond3(  ) );	break;
					case 3:	DropItem( new StrangeDiamond4(  ) );	break;
				}
			
			
			DropItem( new StrangeEmerald1(  ) );
			
	
				switch ( Utility.Random( 6 ) )
				{
					case 0:	DropItem( new StrangeRuby1(  ) );	break;
					case 1:	DropItem( new StrangeRuby2(  ) );	break;
					case 2:	DropItem( new StrangeRuby3(  ) );	break;
					case 3:	DropItem( new StrangeRuby4(  ) );	break;
					case 4:	DropItem( new StrangeRuby5(  ) );	break;
					case 5:	DropItem( new StrangeRuby6(  ) );	break;
				}
			
			
				switch ( Utility.Random( 4 ) )
				{
					case 0:	DropItem( new StrangeSapphire1(  ) );	break;
					case 1:	DropItem( new StrangeSapphire2(  ) );	break;
					case 2:	DropItem( new StrangeSapphire3(  ) );	break;
					case 3:	DropItem( new StrangeSapphire4(  ) );	break;
				}
			
			
				switch ( Utility.Random( 2 ) )
				{
					case 0:	DropItem( new StrangeStarSapphire1(  ) );	break;
					case 1:	DropItem( new StrangeStarSapphire2(  ) );	break;
				}
			
			
				switch ( Utility.Random( 3 ) )
				{
					case 0:	DropItem( new StrangeTourmaline1(  ) );	break;
					case 1:	DropItem( new StrangeTourmaline2(  ) );	break;
					case 2:	DropItem( new StrangeTourmaline3(  ) );	break;
				}
			
//Strange Gems End
			
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );
			DropItem( new Gold( 100, 150 ) );

           			if (Utility.RandomDouble() < 0.05)
                			DropItem( new CrimsonCincture() );
                			
                	if (Utility.RandomDouble() < 0.1)
                			DropItem( new ClothingBlessDeed() );
                			
                	if (Utility.RandomDouble() < 0.05)
                			DropItem( new AddLuckDeed() );
                			
                	if (Utility.RandomDouble() < 0.05)
                			DropItem( new TangleApron() );

			//TODO:  Add in high level magic items and strange gems and make them spray all over the ground when opened
                                                    
		}
		
                                   
                                                                                     
		public MagicalLockbox2( Serial serial ) : base( serial )
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