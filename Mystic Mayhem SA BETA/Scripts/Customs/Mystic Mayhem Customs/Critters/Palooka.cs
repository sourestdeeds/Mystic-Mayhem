using System;
using Server;
using Server.Items;
using Server.Network;
using System.Collections;


namespace Server.Mobiles
{

	[CorpseName( "a corpse" )]
	public class Palooka : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

	private static bool m_Talked;

        string[] kfcsay = new string[]
      		{ 
		 "I'll thump your skull",
		 "Take your best shot",
		"I'm gonna bust you up",
		"You call yourself a fighter",
		"I am the champ",
		"I'm gonna knock somebody out",
		"T T   T T   T T   Taxi",
		"You can't hurt me",

      		}; 


		[Constructable]
		public Palooka () : base( AIType.AI_Melee, FightMode.Closest, 10, 2, 0.2, 0.4 )
		{
			Name = "Joe";
			Title = "the pugilist";
			Body = 0x190;
			Female = false;
			Hue = 0x83EF;
			Criminal = true;
			//Kills = 10;
			//Frozen = true;
			CantWalk = true;

			SetStr( 400 );
			SetDex( 200 );
			SetInt( 400 );

			SetHits( 5000 );
			SetDamage( 1, 2 );

			SetSkill( SkillName.EvalInt, 200.0 );
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.Anatomy, 300.0 );
			SetSkill( SkillName.Wrestling, 200.0 );
			SetSkill( SkillName.MagicResist, 1000.0 );
			SetSkill( SkillName.Tactics, 300.0 );


			AddItem( new ShortPants( 1172 ) );
			AddItem( new Shoes() );

			PlateGloves gloves = new PlateGloves();
			gloves.Hue = 132;
			AddItem( gloves );


			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 1 );

			AddLoot( LootPack.Gems, Utility.Random( 5, 10 ) );
		}
//aa
		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( attacker is BaseCreature )
			{

				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controlled && c.ControlMaster != null )
				{
					this.Say("Take your dog elsewhere!  I QUIT!" );

                 			DeleteTimer t = new DeleteTimer( this ); 
                 			t.Start();
				}
			}
		}
	/*	public override void OnDamagedBySpell( Mobile attacker )
		{
			base.OnDamagedBySpell( attacker );

			if ( attacker is BaseCreature )
			{
				BaseCreature c = (BaseCreature)attacker;

				if ( c.Controlled && c.ControlMaster != null )
				{
					this.Say("I will not walk your dog!  I QUIT!" );

                 			DeleteTimer t = new DeleteTimer( this ); 
                 			t.Start();
				}
			}
		} */
//aa
		public override void OnMovement( Mobile m, Point3D oldLocation ) 
                { 
                                                  
         	if( m_Talked == false ) 
         	{ 
           	 	if ( m.InRange( this, 4 ) && m is PlayerMobile ) 
            	 	{                
               	 	m_Talked = true; 
                 	SayRandom( kfcsay, this ); 
                 	this.Move( GetDirectionTo( m.Location ) ); 
                 	SpamTimer t = new SpamTimer(); 
                 	t.Start(); 
				Item disarm = m.FindItemOnLayer( Layer.OneHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				disarm = m.FindItemOnLayer( Layer.TwoHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm ); 
            	 	} 
		}
	  }				


		public Palooka( Serial serial ) : base( serial )
		{
		}
//aa
		private class DeleteTimer : Timer 
		{ 
			private BaseCreature m_Mob;
			public DeleteTimer( BaseCreature mob ) : base( TimeSpan.FromSeconds( 2 ) ) 
			{ 
				m_Mob = mob;
				Priority = TimerPriority.OneSecond;
			} 

			protected override void OnTick() 
			{ 
				m_Mob.Delete();
			} 
		}
//aa
		private class SpamTimer : Timer 
      { 
         public SpamTimer() : base( TimeSpan.FromSeconds( 3 ) ) 
         { 
			Priority = TimerPriority.OneSecond;
         } 

         protected override void OnTick() 
         { 
            m_Talked = false; 
         } 
	  }

	  private static void SayRandom( string[] say, Mobile m )
	  {
		// m.Say( say[] );
		 m.Say( say[Utility.Random( say.Length )] );
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
