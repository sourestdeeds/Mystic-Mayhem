using System;
using System.Data;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Gumps;
using Server.Targeting;
using System.Reflection;
using Server.Commands;
using CPA = Server.CommandPropertyAttribute;
using System.Xml;
using Server.Spells;
using System.Text;
using Server.Accounting;
using System.Diagnostics;



namespace Server.Mobiles
{
	public class MinaxCourt : TalkingBaseCreature
	{
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 60.0 );
     	public DateTime m_NextTalk;
     	
     	public override bool AlwaysMurderer{ get{ return true; } }
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 2 ) && !InRange( oldLocation, 2 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 21 ))		   
				{
					case 0: Say("Your actions affect so many others than yourself. You will come to realise how little choice you have."); break;
					case 1: Say("Follow, and receive the gift you are owed by the blood in your veins. Follow, if only to protect the weak that fell because of you."); break;
					case 2: Say("He resists. He clings to his old life as if it actually matters. He will learn."); break;
					case 3: Say("Life... is strength. That is not to be contested; it seems logical enough. You live; you affect your world. But is it what you want?"); break;
					case 4: Say("I wonder if you are destined to be forgotten. Will your life fade in the shadow of greater beings?"); break;
					case 5: Say("You walk as a mortal, taking no advantage from your heritage, from your talents within. So many things of flesh are greater than you."); break;
					case 6: Say("She resists. She clings to her old life as if it actually matters. She will learn."); break;
					case 7: Say("No, you'll warrant no villain's exposition from me."); break;
					case 8: Say("You will do what you must, become what you must, or others will pay for your cowardice."); break;
					case 9: Say("You *will* accept the gifts offered to you."); break;
					case 10: Say("You will wither, you will wane, and you will die."); break;
					case 11: Say("You are but a gnat, compared to our power."); break;
					case 12: Say("Will your life fade in the shadow of greater beings?"); break;
					case 13: Say("Your pathetic magics are useless. Let this end!"); break;
					case 14: Say("It is time for more...'experiments'... The pain will only be passing; you should survive the process..."); break;
					case 15: Say("Typical. If I had a sense of humor left I might find that funny. I do not, on both accounts."); break;
					case 16: Say("You are to be given a gift. It is a valuable prize, one you had better appreciate."); break;
					case 17: Say("Fool. Minax cannot die."); break;
					case 18: Say("You worry for your comrades, perhaps? Leave them, abandon them and become what you must."); break;
					case 19: Say("Become part of something greater."); break;
					case 20: Say("Welcome. I have watched your progress with great interest. For a lesser creature you are quite amusing."); break;
					
					
				};
		
			}
		}

        [Constructable]
        public MinaxCourt() : this(-1)
        {
        }

        [Constructable]
        public MinaxCourt(int gender) : base( AIType.AI_Melee, FightMode.None, 10, 1, 0.8, 3.0 )
        {
            SetStr( 10, 30 );
            SetDex( 10, 30 );
            SetInt( 10, 30 );
            Body = 970;

            Fame = 50;
            Karma = 50;
			//Blessed = true;
            CanHearGhosts = true;

            SpeechHue = Utility.RandomNeutralHue();
            Title = "The Disciple Of Minax";
            Hue = Utility.RandomNeutralHue();
            
            switch(gender)
            {
                case -1: this.Female = Utility.RandomBool(); break;
                case 0: this.Female = false; break;
                case 1: this.Female = true; break;
            }

            if ( this.Female)
            {
                this.Body = 970;
                this.Name = NameList.RandomName( "female" );
                Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2045, 0x204A, 0x2046 , 0x2049 ) );
                hair.Hue = Utility.RandomHairHue();
                hair.Layer = Layer.Hair;
                hair.Movable = false;
                AddItem( hair );
                Item hat = null;
                switch ( Utility.Random( 5 ) )//4 hats, one empty, for no hat
                {
                    case 0: hat = new FloppyHat( Utility.RandomNeutralHue() );		break;
                    case 1: hat = new FeatheredHat( Utility.RandomNeutralHue() );	break;
                    case 2: hat = new Bonnet();			break;
                    case 3: hat = new Cap( Utility.RandomNeutralHue() );			break;
                }
                AddItem( hat );
                Item pants = null;
                switch ( Utility.Random( 3 ) )
                {
                    case 0: pants = new ShortPants( GetRandomHue() );	break;
                    case 1: pants = new LongPants( GetRandomHue() );	break;
                    case 2: pants = new Skirt( GetRandomHue() );		break;
                }
                AddItem( pants );
                Item shirt = null;
                switch ( Utility.Random( 7 ) )
                {
                    case 0: shirt = new Doublet( GetRandomHue() );		break;
                    case 1: shirt = new Surcoat( GetRandomHue() );		break;
                    case 2: shirt = new Tunic( GetRandomHue() );		break;
                    case 3: shirt = new FancyDress( GetRandomHue() );	break;
                    case 4: shirt = new PlainDress( GetRandomHue() );	break;
                    case 5: shirt = new FancyShirt( GetRandomHue() );	break;
                    case 6: shirt = new Shirt( GetRandomHue() );		break;
                }
                AddItem( shirt );
            }
            else
            {
                this.Body = 970;
                this.Name = NameList.RandomName( "male" );
                Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2048 ) );
                hair.Hue = Utility.RandomHairHue();
                hair.Layer = Layer.Hair;
                hair.Movable = false;
                AddItem( hair );
                Item beard = new Item( Utility.RandomList( 0x0000, 0x203E, 0x203F, 0x2040, 0x2041, 0x2067, 0x2068, 0x2069 ) );
                beard.Hue = hair.Hue;
                beard.Layer = Layer.FacialHair;
                beard.Movable = false;
                AddItem( beard );
                Item hat = null;
                switch ( Utility.Random( 7 ) ) //6 hats, one empty, for no hat
                {
                    case 0: hat = new SkullCap( GetRandomHue() );					break;
                    case 1: hat = new Bandana( GetRandomHue() );					break;
                    case 2: hat = new WideBrimHat();								break;
                    case 3: hat = new TallStrawHat( Utility.RandomNeutralHue() );	break;
                    case 4: hat = new StrawHat( Utility.RandomNeutralHue() );		break;
                    case 5: hat = new TricorneHat( Utility.RandomNeutralHue() );	break;
                }
                AddItem( hat );
                Item pants = null;
                switch ( Utility.Random( 2 ) )
                {
                    case 0: pants = new ShortPants( GetRandomHue() );	break;
                    case 1: pants = new LongPants( GetRandomHue() );	break;
                }
                AddItem( pants );
                Item shirt = null;
                switch ( Utility.Random( 5 ) )
                {
                    case 0: shirt = new Doublet( GetRandomHue() );		break;
                    case 1: shirt = new Surcoat( GetRandomHue() );		break;
                    case 2: shirt = new Tunic( GetRandomHue() );		break;
                    case 3: shirt = new FancyShirt( GetRandomHue() );	break;
                    case 4: shirt = new Shirt( GetRandomHue() );		break;
                }
                AddItem( shirt );
            }

            Item feet = null;
            switch ( Utility.Random( 3 ) )
            {
                case 0: feet = new Boots( Utility.RandomNeutralHue() );	break;
                case 1: feet = new Shoes( Utility.RandomNeutralHue() );	break;
                case 2: feet = new Sandals( Utility.RandomNeutralHue() );		break;
            }
            AddItem( feet );
            Container pack = new Backpack();

            pack.DropItem( new Gold( 0, 50 ) );

            pack.Movable = false;

            AddItem( pack );
        }

        public MinaxCourt( Serial serial ) : base( serial )
        {
        }

		

        private static int GetRandomHue()
        {
            switch ( Utility.Random( 6 ) )
            {
                default:
                case 0: return 0;
                case 1: return Utility.RandomBlueHue();
                case 2: return Utility.RandomGreenHue();
                case 3: return Utility.RandomRedHue();
                case 4: return Utility.RandomYellowHue();
                case 5: return Utility.RandomNeutralHue();
            }
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
