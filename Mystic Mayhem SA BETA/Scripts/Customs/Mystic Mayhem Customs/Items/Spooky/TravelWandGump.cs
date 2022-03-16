// changed felucca minoc entry point to behind barricade line 558

using System; 
using Server; 
using Server.Commands;
using Server.Gumps; 
using Server.Network; 
using Server.Mobiles;

namespace Server.Gumps 
{ 
	public class TravelWandGump : Gump 
	{ 
		public static void Initialize() 
		{ 
			CommandSystem.Register( "TravelWandGump", AccessLevel.GameMaster, new CommandEventHandler( TravelWandGump_OnCommand ) ); 
		} 

		private static void TravelWandGump_OnCommand( CommandEventArgs e ) 
		{ 
			e.Mobile.SendGump( new TravelWandGump( e.Mobile ) ); 
		} 

		public TravelWandGump( Mobile owner ) : base( 50,50 ) 
		{ 
			owner.CloseGump( typeof( TravelWandGump ) );

			AddPage( 0 ); 

		//	AddBackground( 0, 0, 320, 450, 5054);// 320 to 400
AddBackground( 0, 0, 320, 455, 9270);
AddBackground( 10, 58, 300, 250, 3000 );

			AddButton( 35, 323, 0x2C93, 0x2C93, 1, GumpButtonType.Page, 1 ); //0x2C89 green button
			AddLabel( 55, 320, 52, "Trammel Cities" );

			AddButton( 165, 323, 0x2C93, 0x2C93, 2, GumpButtonType.Page, 2 );
			AddLabel( 185, 320, 34, "Trammel Dungeons" );

	if ( !((PlayerMobile)owner).Young )
	{
			AddButton( 35, 348, 0x2C93, 0x2C93, 3, GumpButtonType.Page, 3 );
			AddLabel( 55, 345, 342, "Felucca Cities" );

			AddButton( 165, 348, 0x2C93, 0x2C93, 4, GumpButtonType.Page, 4 );
			AddLabel( 185, 345, 32, "Felucca Dungeons" );
	}
			AddButton( 35, 373, 0x2C93, 0x2C93, 5, GumpButtonType.Page, 5 );
			AddLabel( 55, 370, 22, "Ilshenar" );

			AddButton( 165, 373, 0x2C93, 0x2C93, 6, GumpButtonType.Page, 6 );
			AddLabel( 185, 370, 182, "Tokuno" );

			AddButton( 35, 398, 0x2C93, 0x2C93, 7, GumpButtonType.Page, 7 );
			AddLabel( 55, 395, 193, "Malas" );

			AddButton( 165, 398, 0x2C93, 0x2C93, 8, GumpButtonType.Page, 8 );
			AddLabel( 185, 395, 62, "Mondain Legacy" );

		//	AddButton( 35, 423, 0x2C93, 0x2C93, 9, GumpButtonType.Page, 9 );
		//	AddLabel( 55, 420, 139, "Special Paradise" );

		//	if (  ( (PlayerMobile)owner).AccessLevel >= AccessLevel.GameMaster )
		//	{
		//		AddButton( 165, 423, 0x2C93, 0x2C93, 10, GumpButtonType.Page, 10 );
		//		AddLabel( 185, 420, 32, "Purgatory" );
		//	}
		
		//	AddAlphaRegion( 10, 60, 300, 250 ); // 325
//   PAGE 1
			AddPage( 1 ); 
			AddHtml( 90, 20, 145, 27,"  TRAMMEL CITIES", true, false ); 
 

			AddButton( 20, 65, 9903, 9905, 30, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 52, "Britain" ); 

			AddButton( 20, 95, 9903, 9905, 31, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 52, "Cove" ); 

			AddButton( 20, 125, 9903, 9905, 32, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 52, "Jhelom" ); 

			AddButton( 20, 155, 9903, 9905, 33, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 52, "Magincia" );

			AddButton( 20, 185, 9903, 9905, 34, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 52, "Minoc" ); 

			AddButton( 20, 215, 9903, 9905, 35, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 52, "Moonglow " );

			AddButton( 20, 245, 9903, 9905, 36, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 245, 52, "Nujel'm " );

			AddButton( 160, 65, 9903, 9905, 37, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 52, "Serp hold" ); 

			AddButton( 160, 95, 9903, 9905, 38, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 52, "Skara Brae" ); 

			AddButton( 160, 125, 9903, 9905, 39, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 52, "Trinsic" ); 

			AddButton( 160, 155, 9903, 9905, 40, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 52, "Vesper" );

			AddButton( 160, 185, 9903, 9905, 41, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 52, "Wind" ); 

			AddButton( 160, 215, 9903, 9905, 42, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 52, "Yew" ); 

			AddButton( 160, 245, 9903, 9905, 43, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 245, 52, "New Haven" ); 

//// START PAGE 2

			AddPage( 2 ); 
			AddHtml( 90, 20, 145, 27," TRAMMEL DUNGEONS", true, false ); 


			AddButton( 20, 65, 9903, 9905, 45, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 34, "Covetous" ); 

			AddButton( 20, 95, 9903, 9905, 46, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 34, "Deceit" ); 

			AddButton( 20, 125, 9903, 9905, 47, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 34, "Despise" ); 

			AddButton( 20, 155, 9903, 9905, 48, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 34, "Destard" ); 

			AddButton( 160, 65, 9903, 9905, 49, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 34, "Fire" ); 

			AddButton( 160, 95, 9903, 9905, 50, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 90, 34, "Ice" );

			AddButton( 160, 125, 9903, 9905, 51, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 34, "Shame" ); 

			AddButton( 160, 155, 9903, 9905, 52, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 34, "Wrong" ); 

			AddButton( 160, 185, 9903, 9905, 53, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 34, "Hythloth" );

////PAGE 3
			AddPage( 3 ); 
			AddHtml( 90, 20, 145, 27,"  FELUCCA CITIES", true, false ); 
 

			AddButton( 20, 65, 9903, 9905, 1, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 342, "Britain" ); 

			AddButton( 20, 95, 9903, 9905, 2, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 342, "Cove" ); 

			AddButton( 20, 125, 9903, 9905, 3, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 342, "Jhelom" ); 

			AddButton( 20, 155, 9903, 9905, 4, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 342, "Magincia" );

			AddButton( 20, 185, 9903, 9905, 5, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 342, "Minoc" ); 

			AddButton( 20, 215, 9903, 9905, 6, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 342, "Moonglow " );

			AddButton( 20, 245, 9903, 9905, 7, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 245, 342, "Nujel'm " );

			AddButton( 20, 275, 9903, 9905, 8, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 275, 342, "Delucia" ); 

			AddButton( 160, 65, 9903, 9905, 9, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 342, "Serp hold" ); 

			AddButton( 160, 95, 9903, 9905, 10, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 342, "Skara Brae" ); 

			AddButton( 160, 125, 9903, 9905, 11, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 342, "Trinsic" ); 

			AddButton( 160, 155, 9903, 9905, 12, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 342, "Vesper" );

			AddButton( 160, 185, 9903, 9905, 13, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 342, "Wind" ); 

			AddButton( 160, 215, 9903, 9905, 14, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 342, "Yew" ); 

			AddButton( 160, 245, 9903, 9905, 15, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 245, 342, "Ocllo" );

			AddButton( 160, 275,9903, 9905, 16, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 275, 342, "Papua" ); 





//// START PAGE 4

			AddPage( 4 ); 
			AddHtml( 90, 20, 145, 27," FELUCCA DUNGEONS", true, false ); 


			AddButton( 20, 65, 9903, 9905, 17, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 32, "Covetous" ); 

			AddButton( 20, 95, 9903, 9905, 18, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 32, "Deceit" ); 

			AddButton( 20, 125, 9903, 9905, 19, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 32, "Despise" ); 

			AddButton( 20, 155, 9903, 9905, 20, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 32, "Destard" ); 

		//	AddButton( 20, 185, 9903, 9905, 21, GumpButtonType.Reply, 0 ); 
		//	AddLabel( 45, 185, 32, "Bones" ); 

		//	AddButton( 20, 215, 9903, 9905, 22, GumpButtonType.Reply, 0 ); 
		//	AddLabel( 45, 215, 32, "Demise" );

			AddButton( 20, 185, 9903, 9905, 23, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 32, "Khaldun" );

			AddButton( 160, 65, 9903, 9905, 24, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 32, "Fire" );

			AddButton( 160, 95, 9903, 9905, 25, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 32, "Ice" ); 

			AddButton( 160, 125, 9903, 9905, 26, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 32, "Shame" ); 

			AddButton( 160, 155, 9903, 9905, 27, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 32, "Wrong" );

			AddButton( 160, 185, 9903, 9905, 28, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 32, "Hythloth" );

			AddButton( 160, 215, 9903, 9905, 29, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 32, "Terathan Keep" ); 



 

//// START PAGE 5

			AddPage( 5 ); 
			AddHtml( 90, 20, 145, 27,"     ILSHENAR", true, false ); 


			AddButton( 20, 65, 9903, 9905, 60, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 22, "Rat Cave" ); 

			AddButton( 20, 95, 9903, 9905, 61, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 22, "Spider Cave" ); 

			AddButton( 20, 125, 9903, 9905, 62, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 22, "Wisp" ); 

			AddButton( 20, 155, 9903, 9905, 63, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 22, "Spectre" ); 

			AddButton( 20, 185, 9903, 9905, 64, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 22, "Mistas" ); 

			AddButton( 20, 215, 9903, 9905, 65, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 22, "Montor" ); 

			AddButton( 20, 245, 9903, 9905, 66, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 245, 22, "Sorcerer" ); 

			AddButton( 160, 65, 9903, 9905, 67, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 22, "Ankh" ); 

			AddButton( 160, 125, 9903, 9905, 68, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 22, "Blood" ); 

			AddButton( 160, 95, 9903, 9905, 69, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 22, "Rock" ); 

			AddButton( 160, 185, 9903, 9905, 70, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 22, "Savage Camp" ); 

			AddButton( 160, 155, 9903, 9905, 71, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 22, "Gargoyle City" ); 

			AddButton( 160, 245, 9903, 9905, 72, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 245, 22, "Ancient Citadel" ); 

			AddButton( 160, 215, 9903, 9905, 73, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 22, "Cyclops Temple" ); 

//// START PAGE 6

			AddPage( 6 ); 
			AddHtml( 90, 20, 145, 27," TOKUNO ISLANDS", true, false ); 


			AddButton( 20, 65, 9903, 9905, 80, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 182, "Zento" ); 

			AddButton( 20, 95, 9903, 9905, 81, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 182, "Waste" ); 

			AddButton( 20, 125, 9903, 9905, 82, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 182, "Bushido Dojo" ); 

			AddButton( 20, 155, 9903, 9905, 83, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 182, "Kitsune Woods" ); 

			AddButton( 20, 185, 9903, 9905, 84, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 182, "Yomotsu Mines" ); 

			AddButton( 20, 215, 9903, 9905, 85, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 182, "Crane Marsh" ); 

			AddButton( 160, 65, 9903, 9905, 86, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 182, "Fan Dancer Dojo" ); 

			AddButton( 160, 95, 9903, 9905, 87, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 182, "Sleeping Dragon" ); 

			AddButton( 160, 125, 9903, 9905, 88, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 182, "Central Shrine" ); 

			AddButton( 160, 155, 9903, 9905, 89, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 182, "Lotus Lakes" ); 

			AddButton( 160, 185, 9903, 9905, 90, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 182, "Storm Point" ); 

/////  PAGE 7

			AddPage( 7 ); 
			AddHtml( 90, 20, 145, 27,"      MALAS", true, false ); 


			AddButton( 20, 65, 9903, 9905, 100, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 193, "Luna" ); 

			AddButton( 20, 95, 9903, 9905, 101, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 193, "Umbra" ); 

			AddButton( 20, 125, 9903, 9905, 102, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 193, "Grimswind" ); 

			AddButton( 20, 155, 9903, 9905, 103, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 193, "Broken Mountains" ); 

			AddButton( 20, 185, 9903, 9905, 104, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 193, "Forgotten Pyramid" ); 

			AddButton( 160, 65, 9903, 9905, 105, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 193, "Doom Dungeon" ); 

		/*	AddButton( 160, 125, 9903, 9905, 106, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 62, "Painted Caves" ); 

			AddButton( 160, 155, 9903, 9905, 107, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 62, "Labyrinth" ); 

			AddButton( 160, 185, 9903, 9905, 108, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 62, "Sanctuary" ); */

/////  PAGE 8
			AddPage( 8 ); 
			AddHtml( 90, 20, 145, 27," MONDAIN DUNGEONS", true, false ); 


			AddButton( 20, 65, 9903, 9905, 110, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 62, "Palace of Paroxysmus" ); 

			AddButton( 20, 95, 9903, 9905, 111, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 62, "Twisted Weald" ); 

			AddButton( 20, 125, 9903, 9905, 112, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 62, "Blighted Grove" ); 

			AddButton( 20, 155, 9903, 9905, 113, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 62, "Bedlam" ); 

			AddButton( 20, 185, 9903, 9905, 114, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 62, "Prism of Light" ); 

			AddButton( 160, 95, 9903, 9905, 115, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 62, "The Citadel" ); 

			AddButton( 160, 125, 9903, 9905, 116, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 62, "Painted Caves" ); 

			AddButton( 160, 155, 9903, 9905, 117, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 62, "Labyrinth" ); 

			AddButton( 160, 185, 9903, 9905, 118, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 62, "Sanctuary" ); 

/////  PAGE 9
/*			AddPage( 9 ); 
			AddHtml( 90, 20, 145, 27," MYSTIC MAYHEM", true, false ); 


			AddButton( 20, 65, 9903, 9905, 120, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 139, "Training Oasis" ); 

			AddButton( 20, 95, 9903, 9905, 121, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 139, "Rune Library" ); 

			AddButton( 20, 125, 9903, 9905, 122, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 139, "Home Showcase" ); 

			AddButton( 20, 155, 9903, 9905, 123, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 139, "Paradise Mall" ); 

			AddButton( 20, 185, 9903, 9905, 124, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 139, "Player Vendors" ); 

			AddButton( 20, 215, 9903, 9905, 125, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 139, "Mini Mall" );

			AddButton( 20, 245, 9903, 9905, 126, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 245, 139, "North Willow Mall " );

			AddButton( 20, 275, 9903, 9905, 127, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 275, 139, "Heartwood" );

			AddButton( 160, 65, 9903, 9905, 128, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 139, "Silver Rewards" ); 

			AddButton( 160, 95, 9903, 9905, 129, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 139, "Taste of Paradise" ); 

			AddButton( 160, 125, 9903, 9905, 130, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 139, "Quest Rewards" ); 

			AddButton( 160, 155, 9903, 9905, 131, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 139, "Artifact House" ); 

			AddButton( 160, 215, 9903, 9905, 132, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 139, "Event Center" );
	*/
	//	} 

//// START PAGE 10

	/*		AddPage( 10 ); 
			AddHtml( 90, 20, 145, 27,"   PURGATORY", true, false ); 

			AddButton( 20, 65, 9903, 9905, 150, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 65, 22, "Purgatory" );

			AddButton( 20, 95, 9903, 9905, 151, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 95, 22, "Bandit Hamlet" );

			AddButton( 20, 125, 9903, 9905, 152, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 125, 22, "Lost City" ); 

			AddButton( 20, 155, 9903, 9905, 153, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 155, 22, "Recluse Fortress" );

			AddButton( 20, 185, 9903, 9905, 154, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 185, 22, "Arachnid Lair " );

			AddButton( 20, 215, 9903, 9905, 155, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 215, 22, "Outcast Covert" );

			AddButton( 20, 245, 9903, 9905, 156, GumpButtonType.Reply, 0 ); 
			AddLabel( 45, 245, 22, "Secret Asylum" ); 


			AddButton( 160, 65, 9903, 9905, 157, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 65, 22, "Deaths Staircase" ); 

			AddButton( 160, 95, 9903, 9905, 158, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 95, 22, "Devils Maze" ); 

			AddButton( 160, 125, 9903, 9905, 159, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 125, 22, "Inferno" ); 

			AddButton( 160, 155, 9903, 9905, 160, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 155, 22, "Infested Mine" ); 

			AddButton( 160, 185, 9903, 9905, 161, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 185, 22, "Forgotten Village" ); 

			AddButton( 160, 215, 9903, 9905, 162, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 215, 22, "Ancient Temple" ); 

			AddButton( 160, 245, 9903, 9905, 163, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 245, 22, "Island Sanctum" ); 


			AddButton( 160, 275, 9903, 9905, 164, GumpButtonType.Reply, 0 ); 
			AddLabel( 185, 275, 22, "Banshee Refugium" ); 
		*/



		}

		public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
		{ 
			Mobile from = state.Mobile; 

			Point3D dest = from.Location;
			Map map = from.Map;

			switch ( info.ButtonID ) 
			{ 
				case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
				{ 
					//Cancel 
					from.SendMessage( "You have chosen to stay here." ); 
					break; 
				} 
// page 1
				case 1: 
				{ 
					//britian 
					map = Map.Felucca;
					dest = new Point3D(1418,1698,0);
					break; 
				} 
 				case 2: 
				{ 
					//Cove 
					map = Map.Felucca;
					dest = new Point3D(2233,1197,0); 
					break; 
				} 

				case 3: 
				{ 
					//Jhelom 
					map = Map.Felucca;
					dest = new Point3D(1330,3780,0);
					break; 
				} 
				case 4: 
				{ 
					//Magincia 
					map = Map.Felucca;
					dest = new Point3D(3728,2164,20 );
					break; 
				}
				case 5: 
				{ 
					//Minoc //changed minoc spawn point to behind barricade, rag
					map = Map.Felucca;
					dest = new Point3D(2419,539,0);
					break; 
				} 
				case 6: 
				{ 
					//Moonglow 
					map = Map.Felucca;
					dest = new Point3D(4469,1177,0); 
					break; 
				} 
				case 7: 
				{ 
					//Nujel'm 
					map = Map.Felucca;
					dest = new Point3D(3770,1308,0); 
					break; 
				} 
				case 8:  
				{ 
					//Delucia 
					map = Map.Felucca;
					dest = new Point3D(5275,3989,37); 
					break; 
				}
				case 9: 
				{ 
					//Serp hold 
					map = Map.Felucca;
					dest = new Point3D(2895,3479,15); 
					break; 
				} 
				case 10: 
				{ 
					//Skara Brae 
					map = Map.Felucca;
					dest = new Point3D(599,2154,0); 
					break; 
				} 
				case 11: 
				{ 
					//Trinsic 
					map = Map.Felucca;
					dest = new Point3D(1821,2826,0); 
					break; 
				} 
				case 12: 
				{ 
					//Vesper 
					map = Map.Felucca;
					dest = new Point3D(2897,678,0); 
					break; 
				}
				case 13: 
				{ 
					//Wind 
					map = Map.Felucca;
					dest = new Point3D(1361,895,0); 
					break; 
				} 
				case 14: 
				{ 
					//Yew 
					map = Map.Felucca;
					dest = new Point3D(548,1006,0); 
					break; 
				} 
				case 15: 
				{ 
					//Ocllo 
					map = Map.Felucca;
					dest = new Point3D(3688,2521,0); 
					break; 
				}
				case 16: 
				{ 
					//Papua 
					map = Map.Felucca;
					dest = new Point3D(5676,3140,12); 
					break; 
				} 

// Page 2
				case 17: 
				{ 
					//Covetous 
					map = Map.Felucca;
					dest = new Point3D(2499,922,0); 
					break; 
				} 
				case 18: 
				{ 
					//Deceit 
					map = Map.Felucca;
					dest = new Point3D(4111,434,5 ); 
					break; 
				} 
				case 19: 
				{ 
					//Despise 
					map = Map.Felucca;
					dest = new Point3D(1301,1080,0); 
					break; 
				} 
				case 20: 
				{ 
					//Destard 
					map = Map.Felucca;
					dest = new Point3D(1176,2639,0 ); 
					break; 
				} 
				case 21: 
				{ 
					// Bones
					map = Map.Felucca;
					dest = new Point3D(5386,3969,-3); 
					break; 
				} 
				case 22: 
				{ 
					// Demise
					map = Map.Felucca;
					dest = new Point3D(1264,1256,0); 
					break; 
				} 
				case 23: 
				{ 
					// Khaldun
					map = Map.Felucca;
					dest = new Point3D(5881,3821,1); 
					break; 
				}
				case 24: 
				{ 
					// Fire 
					map = Map.Felucca;
					dest = new Point3D(5760,2907,17); 
					break; 
				}
				case 25: 
				{ 
					// Ice 
					map = Map.Felucca;
					dest = new Point3D(1999,81,5); 
					break; 
				}
				case 26: 
				{ 
					//Shame 
					map = Map.Felucca;
					dest = new Point3D(514,1561,0); 
					break; 
				} 
				case 27: 
				{ 
					//Wrong 
					map = Map.Felucca;
					dest = new Point3D(2044,238,10); 
					break; 
				} 
 				case 28: 
				{ 
					//Hythloth 
					map = Map.Felucca;
					dest = new Point3D(4722,3825,0); 
					break; 
				} 
 				case 29: 
				{ 
					// Terathan Keep
					map = Map.Felucca;
					dest = new Point3D(5499,3224,0); 
					break; 
				}
 

// page 3  Trammel cities
				case 30: 
				{ 
					//britian 
					map = Map.Trammel;
					dest = new Point3D(1418,1698,0); 
					break; 
				} 
 				case 31: 
				{ 
					//Cove 
					map = Map.Trammel;
					dest = new Point3D(2237,1214,0); 
					break; 
				} 
				case 32: 
				{ 
					//Jhelom 
					map = Map.Trammel;
					dest = new Point3D(1330,3780,0); 
					break; 
				}
				case 33: 
				{ 
					//Magincia 
					map = Map.Trammel;
					dest = new Point3D(3728,2164,20 ); 
					break; 
				} 
				case 34: 
				{ 
					//Minoc 
					map = Map.Trammel;
					dest = new Point3D(2503,563,0); 
					break; 
				} 
				case 35: 
				{ 
					//Moonglow 
					map = Map.Trammel;
					dest = new Point3D(4454,1170,0); 
					break; 
				} 
				case 36: 
				{ 
					//Nujel'm 
					map = Map.Trammel;
					dest = new Point3D(3770,1308,0); 
					break; 
				} 
				case 37: 
				{ 
					//Serp hold 
					map = Map.Trammel;
					dest = new Point3D(2892,3480,15); 
					break; 
				} 
				case 38: 
				{ 
					//Skara Brae 
					map = Map.Trammel;
					dest = new Point3D(593,2152,0); 
					break; 
				} 
				case 39: 
				{ 
					//Trinsic 
					map = Map.Trammel;
					dest = new Point3D(1821,2826,0); 
					break; 
				} 
				case 40: 
				{ 
					//Vesper 
					map = Map.Trammel;
					dest = new Point3D(2899,676,0); 
					break; 
				}
				case 41: 
				{ 
					//Wind 
					map = Map.Trammel;
					dest = new Point3D(1361,895,0); 
					break; 
				} 
				case 42: 
				{ 
					//Yew 
					map = Map.Trammel;
					dest = new Point3D(548,1006,0); 
					break; 
				} 
				case 43: 
				{ 
					//New Haven 
					map = Map.Trammel;
					dest = new Point3D(3496,2571,15); 
					break; 
				} 


// Page 4  Trammel Dungeons
				case 45: 
				{ 
					//Covetous 
					map = Map.Trammel;
					dest = new Point3D(2499,922,0); 
					break; 
				} 
				case 46: 
				{ 
					//Deceit 
					map = Map.Trammel;
					dest = new Point3D(4111,434,5 ); 
					break; 
				} 
				case 47: 
				{ 
					//Despise 
					map = Map.Trammel;
					dest = new Point3D(1301,1080,0); 
					break; 
				} 
				case 48: 
				{ 
					//Destard 
					map = Map.Trammel;
					dest = new Point3D(1176,2639,0 ); 
					break; 
				} 
 				case 49: 
				{ 
					// Fire 
					map = Map.Trammel;
					dest = new Point3D(5760,2907,17); 
					break; 
				} 
				case 50: 
				{ 
					// Ice 
					map = Map.Trammel;
					dest = new Point3D(1999,81,5); 
					break; 
				}
				case 51: 
				{ 
					//Shame 
					map = Map.Trammel;
					dest = new Point3D(514,1561,0); 
					break; 
				} 
				case 52: 
				{ 
					//Wrong 
					map = Map.Trammel;
					dest = new Point3D(2044,238,10); 
					break; 
				} 
 				case 53: 
				{ 
					//Hythloth 
					map = Map.Trammel;
					dest = new Point3D(4722,3825,0); 
					break; 
				}
  

////// START OF ILSHENAR CASES

				case 60: 
				{ 
					// Rat Cave
					map = Map.Ilshenar;
					dest = new Point3D(1034,1153,-22); 
					break; 
				} 
				case 61: 
				{ 
					//Spider Cave
					map = Map.Ilshenar;
					dest = new Point3D(1421,913,-16); 
					break; 
				} 
 
				case 62: 
				{ 
					// Wisp
					map = Map.Ilshenar;
					dest = new Point3D(651,1302,-58); 
					break; 
				}
				case 63: 
				{ 
					// Spectre
					map = Map.Ilshenar;
					dest = new Point3D(1363,1033,-8); 
					break; 
				}
				case 64: 
				{ 
					// Mistas
					map = Map.Ilshenar;
					dest = new Point3D(820,1073,-30); 
					break; 
				} 
				case 65: 
				{ 
					// Montor
					map = Map.Ilshenar;
					dest = new Point3D(1643,310,48); 
					break; 
				} 
				case 66: 
				{ 
					// Sorceror
					map = Map.Ilshenar;
					dest = new Point3D(548,462,-53); 
					break; 
				}
				case 67: 
				{ 
					// Ankh
					map = Map.Ilshenar;
					dest = new Point3D(576,1150,-100); 
					break; 
				} 
				case 68: 
				{ 
					// Blood
					map = Map.Ilshenar;
					dest = new Point3D(1747,1228,-1); 
					break; 
				} 
				case 69: 
				{ 
					// Rock
					map = Map.Ilshenar;
					dest = new Point3D(1788,573,71); 
					break; 
				}
				case 70: 
				{ 
					// Savage Camp
					map = Map.Ilshenar;
					dest = new Point3D(1251,743,-80); 
					break; 
				} 
 
				case 71: 
				{ 
					// Gargoyle City
					map = Map.Ilshenar;
					dest = new Point3D(836,641,-20); 
					break; 
				} 
				case 72: 
				{ 
					// Ancient Citadel
					map = Map.Ilshenar;
					dest = new Point3D(1518,568,-13); 
					break; 
				}
				case 73: 
				{ 
					// Cyclops Temple
					map = Map.Ilshenar;
					dest = new Point3D(861,1304,-71); 
					break; 
				} 


///// END OF ILSHENAR


////  START TOKUNO CASES

				case 80: 
				{ 
					// Zento
					map = Map.Tokuno;//Tokuno;
					dest = new Point3D(736,1256,30); 
					break; 
				} 
				case 81: 
				{ 
					// Waste
					map = Map.Tokuno;
					dest = new Point3D(725,1051,35); 
					break; 
				} 
				case 82: 
				{ 
					// Bushido Dojo
					map = Map.Tokuno;
					dest = new Point3D(338,400,32); 
					break; 
				} 
				case 83: 
				{ 
					// Kitsune Woods
					map = Map.Tokuno;
					dest = new Point3D(577,453,35); 
					break; 
				} 
				case 84: 
				{ 
					// Yomotsu Mines
					map = Map.Tokuno;
					dest = new Point3D(254,787,63); 
					break; 
				} 
				case 85: 
				{ 
					// Crane Marsh
					map = Map.Tokuno;
					dest = new Point3D(191,998,10); 
					break; 
				} 
				case 86: 
				{ 
					// Fan Dancer Dojo
					map = Map.Tokuno;
					dest = new Point3D(977,201,24); 
					break; 
				} 
				case 87: 
				{ 
					// Sleeping Dragon
					map = Map.Tokuno;
					dest = new Point3D(910,346,8); 
					break; 
				} 
				case 88: 
				{ 
					// Central Shrine
					map = Map.Tokuno;
					dest = new Point3D(1044,516,15); 
					break; 
				} 
				case 89: 
				{ 
					// Lotus Lakes
					map = Map.Tokuno;
					dest = new Point3D(1051,939,28); 
					break; 
				} 
				case 90: 
				{ 
					// Storm Point
					map = Map.Tokuno;
					dest = new Point3D(1170,999,36); 
					break; 
				} 

///// END OF TOKUNO

// MALAS

				case 100: 
				{ 
					// Luna
					map = Map.Malas;
					dest = new Point3D(993,519,-50); 
					break; 
				} 
				case 101: 
				{ 
					// Umbra
					map = Map.Malas;
					dest = new Point3D(2058,1342,-85); 
					break; 
				} 
				case 102: 
				{ 
					// Grimswind
					map = Map.Malas;
					dest = new Point3D(2192,330,-90); 
					break; 
				} 
				case 103: 
				{ 
					// Broken Mountains
					map = Map.Malas;
					dest = new Point3D(1111,1461,-90); 
					break; 
				} 
				case 104: 
				{ 
					// Forgotten Pyramid
					map = Map.Malas;
					dest = new Point3D(1852,1793,-110); 
					break; 
				} 
				case 105: 
				{ 
					// Doom
					map = Map.Malas;
					dest = new Point3D(2367,1268,-85); 
					break; 
				}

// Mundan Lunacy

				case 110: 
				{ 
					// Poroxysmus
					map = Map.Felucca;
					dest = new Point3D(5570,3020,29); 
					break; 
				} 
				case 111: 
				{ 
					// Twisted Weald
					map = Map.Ilshenar;
					dest = new Point3D(1461,1328,-23); 
					break; 
				} 
				case 112: 
				{ 
					// Blighted Grove
					map = Map.Felucca;
					dest = new Point3D(579,1655,0); 
					break; 
				} 
				case 113: 
				{ 
					// Bedlam
					map = Map.Malas;
					dest = new Point3D(2079,1375,-70); 
					break; 
				} 
				case 114: 
				{ 
					// Prizm of Light
					map = Map.Felucca;
					dest = new Point3D(3789,1094,20); 
					break; 
				} 
				case 115: 
				{ 
					// The Citadel
					map = Map.Tokuno;
					dest = new Point3D(1351,762,20); 
					break; 
				} 
				case 116: 
				{ 
					// Painted Caves
					map = Map.Felucca;
					dest = new Point3D(1715,2993,0); 
					break; 
				} 
				case 117: 
				{ 
					// Labyrinth
					map = Map.Malas;
					dest = new Point3D(1723,1158,-90); 
					break; 
				} 
				case 118: 
				{ 
					// Sanctuary
					map = Map.Felucca;
					dest = new Point3D(796,1607,0); 
					break; 
				}
//Extra

				case 120: 
				{ 
					// Training Oasis
					map = Map.Malas;
					dest = new Point3D(371,779,-1); 
					break; 
				}
				case 121: 
				{ 
					// Rune Library
					map = Map.Felucca;
					dest = new Point3D(4438,1097,0); 
					break; 
				}
				case 122: 
				{ 
					// Home Showcase
					map = Map.Felucca;
					dest = new Point3D(3662,1332,0); 
					break; 
				}
				case 123: 
				{ 
					// Paradise Mall
					map = Map.Felucca;
					dest = new Point3D(1364,1625,94); 
					break; 
				}
				case 124: 
				{ 
					// Vendor
					map = Map.Felucca;
					dest = new Point3D(2948, 611, 0); //3796,2260,20); 
					break; 
				}
				case 125: 
				{ 
					// Mini
					map = Map.Felucca;
					dest = new Point3D(2700,507,22); 
					break; 
				}
				case 126: 
				{ 
					// Willow
					map = Map.Felucca;
					dest = new Point3D(2180,605,0); 
					break; 
				}
				case 127: 
				{ 
					// Heartwood
					map = Map.Felucca;
					dest = new Point3D(7077,381,0); 
					break; 
				}
				case 128: 
				{ 
					// Silver
					map = Map.Felucca;
					dest = new Point3D(1352,1653,72); 
					break; 
				}

				case 129: 
				{ 
					// Taste
					map = Map.Felucca;
					dest = new Point3D(1503,1525,40); 
					break; 
				}

				case 130: 
				{ 
					// Quest
					map = Map.Felucca;
					dest = new Point3D(2040,2878,0); 
					break; 
				}

				case 131: 
				{ 
					// Arties
					map = Map.Felucca;
					dest = new Point3D(1224,1891,0); 
					break; 
				}

				case 132: 
				{ 
					// Events
					map = Map.Felucca;
					dest = new Point3D(4311,990,0); 
					break; 
				}

////// START OF PURGATORY CASES

	/*			case 150: 
				{ 
					// Purgatory
					map = Map.Maps[32];
					dest = new Point3D(836,641,-20); 
					break; 
				}
				case 151: 
				{ 
					// Bandit Hamlet
					map = Map.Maps[32];
					dest = new Point3D(820,1073,-30); 
					break; 
				} 
				case 152: 
				{ 
					// Lost City
					map = Map.Maps[32];
					dest = new Point3D(1622,261,78); 
					break; 
				} 
				case 153: 
				{ 
					// Recluse Fortress
					map = Map.Maps[32];
					dest = new Point3D(1000,300,52); 
					break; 
				}
				case 154: 
				{ 
					//Arachnid Lair 
					map = Map.Maps[32];
					dest = new Point3D(1421,913,-16); 
					break; 
				} 
 
				case 155: 
				{ 
					// Outcast Covert
					map = Map.Maps[32];
					dest = new Point3D(651,1302,-58); 
					break; 
				}
				case 156: 
				{ 
					// Secret Asylum
					map = Map.Maps[32];
					dest = new Point3D(548,462,-53); 
					break; 
				}
				case 157: 
				{ 
					// Deaths Staircase
					map = Map.Maps[32];
					dest = new Point3D(576,1150,-100); 
					break; 
				} 
				case 158: 
				{ 
					// Devils Maze
					map = Map.Maps[32];
					dest = new Point3D(1788,573,71); 
					break; 
				}
				case 159: 
				{ 
					// Inferno Blood
					map = Map.Maps[32];
					dest = new Point3D(1747,1228,-1); 
					break; 
				} 
				case 160: 
				{ 
					// Infested Mine
					map = Map.Maps[32];
					dest = new Point3D(1034,1153,-22); 
					break; 
				} 
				case 161: 
				{ 
					// Forgotten Village
					map = Map.Maps[32];
					dest = new Point3D(1251,743,-80); 
					break; 
				} 
				case 162: 
				{ 
					// Ancient Temple
					map = Map.Maps[32];
					dest = new Point3D(907,1283,-46); 
					break; 
				}
				case 163: 
				{ 
					// Island Sanctum
					map = Map.Maps[32];
					dest = new Point3D(1518,568,-13); 
					break; 
				}
				case 164: 
				{ 
					// Banshee Regugium
					map = Map.Maps[32];
					dest = new Point3D(1363,1033,-8); 
					break; 
				}
		*/

			} 
			BaseCreature.TeleportPets( from, dest, map );
			from.MoveToWorld( dest, map );
		} 
	} 
}