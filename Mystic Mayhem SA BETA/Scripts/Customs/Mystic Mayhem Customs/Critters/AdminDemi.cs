using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using System.Collections;


namespace Server.Mobiles
{

	[CorpseName( "a corpse" )]
	public class AdminDemi : BaseCreature
	{

	private static bool m_Talked;

        string[] kfcsay = new string[]
      	{ 
		 "Feeling lucky",
		"You're making a mistake",
		 "I'll turn you into a toad",
		 "Don't you love me anymore",
		"Please don't hurt me",
		"Have mercy on me",
		"I've never hurt you",

      	}; 


		[Constructable]
		public AdminDemi () : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "Admin Demi";
			NameHue = 1171;
			Body = 401;
			Female = true;
			Hue = 33770;
			Kills = 10;
			SetStr( 400 );
			SetDex( 200 );
			SetInt( 500 ); // 400

			SetHits( 15000 );
			SetDamage( 35, 45 );
		//	BardFactor = 40;

			SetSkill( SkillName.EvalInt, 250.0 ); // 200
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.Anatomy, 300.0 );
			SetSkill( SkillName.Wrestling, 200.0 );
			SetSkill( SkillName.MagicResist, 1000.0 );
			SetSkill( SkillName.Tactics, 300.0 );

			//PackItem( new Gold( 8000, 10000 ));
			//if ( Utility.RandomDouble() <= 0.05 )

			//PackItem( new AminPower() );
			
		//	AddItem( new LongHair( 0x483 ) );
			HairItemID = 0x203C;
			HairHue = 0x483;

		//	Item hair = new Item( 0x203C );
		//	hair.Hue = 0x483;
		//	hair.Layer = Layer.Hair;

			Item chest = new LeatherBustierArms();
			chest.Hue = 168;
			chest.Movable = false;
			AddItem( chest );

			Item feet = new Sandals();
			feet.Hue = 213;
			feet.Movable = false;
			AddItem( feet );

			Item legs = new LeatherSkirt();
			legs.Hue = 213;
			legs.Movable = false;
			AddItem( legs );

			
		}
	//	public override bool BardImmune{ get{ return true; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 1 );

			AddLoot( LootPack.Gems, Utility.Random( 5, 10 ) );
		}
		public override void OnDeath( Container c )
		{

			c.DropItem( new Gold( 8000, 10000 ) );
			if ( Utility.RandomDouble() <= 0.10 )
				c.DropItem( new AminPower() );
		//	if ( Utility.RandomDouble() <= 0.25 )
		//		c.DropItem( new EvoMercDeed() );
			base.OnDeath( c );
		}

		public void Polymorph( Mobile m )
		{
			if ( !m.CanBeginAction( typeof( PolymorphSpell ) ) || !m.CanBeginAction( typeof( IncognitoSpell ) ) || m.IsBodyMod )
				return;

			IMount mount = m.Mount;
			if ( m is Clone )
				return;

			if ( mount != null )
				mount.Rider = null;

			if ( m.Mounted )
				return;

			if ( m.BeginAction( typeof( PolymorphSpell ) ) )
			{
				Item disarm = m.FindItemOnLayer( Layer.OneHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				disarm = m.FindItemOnLayer( Layer.TwoHanded );

				if ( disarm != null && disarm.Movable )
					m.AddToBackpack( disarm );

				m.BodyMod = 80;
				m.HueMod = 0;

				new ExpirePolymorphTimer( m ).Start();
			}
		}

		private class ExpirePolymorphTimer : Timer
		{
			private Mobile m_Owner;

			public ExpirePolymorphTimer( Mobile owner ) : base( TimeSpan.FromMinutes( 2.0 ) )
			{
				m_Owner = owner;

				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( !m_Owner.CanBeginAction( typeof( PolymorphSpell ) ) )
				{
					m_Owner.BodyMod = 0;
					m_Owner.HueMod = -1;
					m_Owner.EndAction( typeof( PolymorphSpell ) );
				}
			}
		}
		public void DoSpecialAbility( Mobile target )
		{
			if ( 0.3 >= Utility.RandomDouble() ) // 30% chance to polymorph attacker into a toad
				Polymorph( target );
		}
		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );
		}

		public override void OnMovement( Mobile m, Point3D oldLocation ) 
                {                                                    
         		if( m_Talked == false ) 
         		{ 
           	 		if ( m.InRange( this, 4 ) ) 
            	 		{                
               	 			m_Talked = true; 
                 			SayRandom( kfcsay, this ); 
                 			this.Move( GetDirectionTo( m.Location ) ); 
                 			SpamTimer t = new SpamTimer(); 
                 			t.Start(); 
            	 		} 
			}
	  	}				

		
		public AdminDemi( Serial serial ) : base( serial )
		{
		}

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
