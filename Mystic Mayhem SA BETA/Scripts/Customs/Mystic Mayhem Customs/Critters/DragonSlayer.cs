using System; 
using System.Collections; 
using Server.Items; 
using Server.ContextMenus; 
using Server.Misc; 
using Server.Network; 

namespace Server.Mobiles 
{ 
    public class DragonSlayer : BaseCreature 
    { 
        [Constructable] 
        public DragonSlayer() : base( AIType.AI_Mage, FightMode.Strongest, 10, 1, 0.2, 0.4 )
        { 
            SpeechHue = Utility.RandomDyedHue(); 
            Hue = Utility.RandomSkinHue(); 

            if ( this.Female = Utility.RandomBool() ) 
            { 
                Body = 0x191; 
                Name = NameList.RandomName( "female" ); 
		AddItem( new FancyDress(  ) );
            } 
            else 
            { 
                Body = 0x190; 
                Name = NameList.RandomName( "male" );
		AddItem( new FancyShirt( ) ); 
                AddItem( new LongPants( Utility.RandomNeutralHue() ) ); 
            } 
		Title = "the dragon slayer";

            HairItemID = Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ); 
            HairHue = Utility.RandomNeutralHue(); 
 

            if( Utility.RandomBool() && !this.Female )
            {
                FacialHairItemID = Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D );
                FacialHairHue = HairHue;

            }

            	SetStr( 200, 220 ); 
            	SetDex( 400, 440 ); 
            	SetInt( 400, 440 ); 
		SetHits( 15000 );
		Kills = 10;
            	SetDamage( 60, 81 );
 
		SetDamageType( ResistanceType.Physical, 80 );
		SetDamageType( ResistanceType.Energy, 20 );

		SetResistance( ResistanceType.Physical, 50, 60 );
		SetResistance( ResistanceType.Fire, 100, 100 );
		SetResistance( ResistanceType.Cold, 30, 40 );
		SetResistance( ResistanceType.Poison, 100, 100 );
		SetResistance( ResistanceType.Energy, 50, 60 );


		SetSkill( SkillName.EvalInt, 120.0 );
		SetSkill( SkillName.Magery, 200.0 );
		SetSkill( SkillName.Meditation, 200.0 );
		SetSkill( SkillName.Anatomy, 300.0 );
		SetSkill( SkillName.Fencing, 200.0 );
		SetSkill( SkillName.MagicResist, 500.0 ); //1000
		SetSkill( SkillName.Tactics, 300.0 );
            

            	Fame = 10000; 
            	Karma = -10000;
		VirtualArmor = 67; 

            	AddItem( new Shoes( Utility.RandomNeutralHue() ) ); 
            	//AddItem( new Shirt());
		//AddItem( new LongPants()); 

		Kryss weapon = new Kryss();
		//weapon.Skill = SkillName.Fencing;
		//weapon.Hue = 38;
		weapon.Movable = false;
		AddItem( weapon );
 
		//AddItem( new Kryss() );

            PackGold( 10000, 15000 ); 
        } 
	public override bool ClickTitle{ get{ return true; } }
		public override bool BardImmune{ get{ return true; } }
        public DragonSlayer( Serial serial ) : base( serial ) 
        { 
        } 
		public override void OnDeath( Container c )
		{

		//	if ( Utility.RandomDouble() <= 0.25 )
		//		c.DropItem( new EvoMercDeed() );
			base.OnDeath( c );
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

// new abilities
		public override void OnGotMeleeAttack( Mobile attacker )
		{

			base.OnGotMeleeAttack( attacker );
			
			if ( 0.05 >= Utility.RandomDouble() )
				SpawnDrake( attacker );

		}

		public void SpawnDrake( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int newDrakes = Utility.RandomMinMax( 1, 1 ); //3 6

			for ( int i = 0; i < newDrakes; ++i )
			{
				Drake drake = new Drake();

				drake.Team = this.Team;

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 3 ) - 1;
					int y = Y + Utility.Random( 3 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				drake.MoveToWorld( loc, map );
				drake.Combatant = target;
			}
		}
// end new abilities

    } 
} 
