    //////////////////////////////////
   //			           //
  //      Scripted by Raelis      //
 //		             	 //
//////////////////////////////////
using System;
using System.Collections; 
using Server.Mobiles;
using Server.Items;
using Server.Network; 
using Server.Targeting;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a dragon hatchling corpse" )]
	public class EvolutionDragon : BaseCreature
	{
		private Timer m_BreatheTimer;
		private DateTime m_EndBreathe;
		private Timer m_MatingTimer;
		private DateTime m_EndMating;

		private Timer m_PetLoyaltyTimer;
		private DateTime m_EndPetLoyalty;

		public DateTime EndMating{ get{ return m_EndMating; } set{ m_EndMating = value; } }

		public DateTime EndPetLoyalty{ get{ return m_EndPetLoyalty; } set{ m_EndPetLoyalty = value; } }

		public int m_Stage;
		public int m_KP;
		public bool m_AllowMating;
		public bool m_HasEgg;
		public bool m_Pregnant;

		public bool m_S1;
		public bool m_S2;
		public bool m_S3;
		public bool m_S4;
		public bool m_S5;
		public bool m_S6;

		public bool S1
		{
			get{ return m_S1; }
			set{ m_S1 = value; }
		}
		public bool S2
		{
			get{ return m_S2; }
			set{ m_S2 = value; }
		}
		public bool S3
		{
			get{ return m_S3; }
			set{ m_S3 = value; }
		}
		public bool S4
		{
			get{ return m_S4; }
			set{ m_S4 = value; }
		}
		public bool S5
		{
			get{ return m_S5; }
			set{ m_S5 = value; }
		}
		public bool S6
		{
			get{ return m_S6; }
			set{ m_S6 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowMating
		{
			get{ return m_AllowMating; }
			set{ m_AllowMating = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasEgg
		{
			get{ return m_HasEgg; }
			set{ m_HasEgg = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Pregnant
		{
			get{ return m_Pregnant; }
			set{ m_Pregnant = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int KP
		{
			get{ return m_KP; }
			set{ m_KP = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage
		{
			get{ return m_Stage; }
			set{ m_Stage = value; }
		}
		
		public override bool HasBreath{ get{ return true; } }

		[Constructable]
		public EvolutionDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Female = Utility.RandomBool();
			Name = "a dragon hatchling";
			Body = 52;
			Hue = Utility.RandomList( 2419, 2406, 2413, 2418, 2213, 2425, 2207, 2219, 2220, 2117, 2129, 1150, 1153, 1161 );
			BaseSoundID = 0xDB;
			Stage = 1;

			S1 = true;
			S2 = true;
			S3 = true;
			S4 = true;
			S5 = true;
			S6 = true;

			SetStr( 100, 200 );
			SetDex( 25, 75 );
			SetInt( 40, 100 );

			SetHits( 150, 300 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15 );

			SetSkill( SkillName.Magery, 50.1, 70.0 );
			SetSkill( SkillName.Meditation, 50.1, 70.0 );
			SetSkill( SkillName.EvalInt, 50.1, 70.0 );
			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );
			SetSkill( SkillName.Wrestling, 19.3, 34.0 );
			SetSkill( SkillName.Anatomy, 19.3, 34.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 20;

			ControlSlots = 1;
			MinTameSkill = 50.0;


			m_PetLoyaltyTimer = new PetLoyaltyTimer( this, TimeSpan.FromSeconds( 5.0 ) );
			m_PetLoyaltyTimer.Start();
			m_EndPetLoyalty = DateTime.Now + TimeSpan.FromSeconds( 5.0 );
		}

		public EvolutionDragon(Serial serial) : base(serial)
		{
		}
//aa stats
		public override bool HandlesOnSpeech(Mobile from)
		{
			base.HandlesOnSpeech( from );
			return true;
		}
 
	        public override void OnSpeech( SpeechEventArgs e ) 
        	{ 
		        bool isMatch = false;
		        Mobile from = e.Mobile;
                	string keyword5 = this.Name +" stats";

                	if ( keyword5 != null && e.Speech.ToLower().IndexOf( keyword5.ToLower() ) >= 0 ) 
 			{ // stats
				isMatch = true; 
 
 	              		if ( !isMatch ) 
        	            		return; 
	
        	       		if ( ControlMaster != from )
                	   		return;
			
				from.SendGump( new EvoStatGump( this, this.Stage, this.KP ) );
	               		e.Handled = true;
			}

			base.OnSpeech( e );
          	}

//aa
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			int kpgainmin, kpgainmax;

			if ( this.Stage == 1 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 10;
						kpgainmax = 50 + ( bc.HitsMax ) / 5;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 25000 )
				{
					if ( this.S1 == true )
					{
						this.S1 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 300 );

						va = ( this.VirtualArmor + 15 );

						mindamage = this.DamageMin + ( 2 );
						maxdamage = this.DamageMax + ( 2 );

						this.Warmode = false;
						this.Say( "*"+ this.Name +" evolves*");
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						this.BodyValue = 89;
						this.BaseSoundID = 219;
						this.VirtualArmor = va;
						this.Stage = 2;

						this.SetDamageType( ResistanceType.Physical, 40 );
						this.SetDamageType( ResistanceType.Fire, 0 );
						this.SetDamageType( ResistanceType.Cold, 0 );
						this.SetDamageType( ResistanceType.Poison, 60 );
						this.SetDamageType( ResistanceType.Energy, 0 );

						this.SetResistance( ResistanceType.Physical, 20 );
						this.SetResistance( ResistanceType.Fire, 20 );
						this.SetResistance( ResistanceType.Cold, 20 );
						this.SetResistance( ResistanceType.Poison, 20 );
						this.SetResistance( ResistanceType.Energy, 20 );

						this.RawStr += 300;
						this.RawInt += 100;
						this.RawDex += 50;
						
						this.ControlSlots = 1;
						this.MinTameSkill = 75.0;
					}
				}
			}

			else if ( this.Stage == 2 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 20;
						kpgainmax = 50 + ( bc.HitsMax ) / 10;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 75000 )
				{
					if ( this.S2 == true )
					{
						this.S2 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 250 );

						va = ( this.VirtualArmor + 10 );

						mindamage = this.DamageMin + ( 2 );
						maxdamage = this.DamageMax + ( 2 );

						this.Warmode = false;
						this.Say( "*"+ this.Name +" evolves*");
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						this.BodyValue = 0xCE;
						this.BaseSoundID = 0x5A;
						this.VirtualArmor = va;
						this.Stage = 3;

						this.SetDamageType( ResistanceType.Physical, 100 );
						this.SetDamageType( ResistanceType.Fire, 0 );
						this.SetDamageType( ResistanceType.Cold, 0 );
						this.SetDamageType( ResistanceType.Poison, 0 );
						this.SetDamageType( ResistanceType.Energy, 0 );

						this.SetResistance( ResistanceType.Physical, 40 );
						this.SetResistance( ResistanceType.Fire, 40 );
						this.SetResistance( ResistanceType.Cold, 40 );
						this.SetResistance( ResistanceType.Poison, 40 );
						this.SetResistance( ResistanceType.Energy, 40 );

						this.RawStr += 300;
						this.RawInt += 200;
						this.RawDex += 50;
						this.ControlSlots = 2;
						this.MinTameSkill = 75.0;
					}
				}
			}

			else if ( this.Stage == 3 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 30;
						kpgainmax = 50 + ( bc.HitsMax ) / 20;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 175000 )
				{
					if ( this.S3 == true )
					{
						this.S3 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 250 );

						va = ( this.VirtualArmor + 5 );

						mindamage = this.DamageMin + ( 2 );
						maxdamage = this.DamageMax + ( 2 );

						this.Warmode = false;
						this.Say( "*"+ this.Name +" evolves*");
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						//this.BodyValue = Utility.RandomList( 60, 61 );
						this.BodyValue = 794;
						this.BaseSoundID = 362;
						this.VirtualArmor = va;
						this.Stage = 4;
						this.SetDamageType( ResistanceType.Physical, 80 );
						this.SetDamageType( ResistanceType.Fire, 20 );
						this.SetDamageType( ResistanceType.Cold, 0 );
						this.SetDamageType( ResistanceType.Poison, 0 );
						this.SetDamageType( ResistanceType.Energy, 0 );

						this.SetResistance( ResistanceType.Physical, 50 );
						this.SetResistance( ResistanceType.Fire, 50 );
						this.SetResistance( ResistanceType.Cold, 50 );
						this.SetResistance( ResistanceType.Poison, 50 );
						this.SetResistance( ResistanceType.Energy, 50 );

						this.RawStr += 275;
						this.RawInt += 225;
						this.RawDex += 35;
						this.ControlSlots = 2;
						this.MinTameSkill = 85.0;
					}
				}
			}

			else if ( this.Stage == 4 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 140;
						kpgainmax = 50 + ( bc.HitsMax ) / 120;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 3750000 )
				{
					if ( this.S4 == true )
					{
						this.S4 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 375 );

						va = ( this.VirtualArmor + 15 );

						mindamage = this.DamageMin + ( 5 );
						maxdamage = this.DamageMax + ( 5 );

						this.Warmode = false;
						this.Say( "*"+ this.Name +" evolves*");
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						this.BodyValue = Utility.RandomList( 60, 61 );
						//this.BodyValue = 59;
						this.VirtualArmor = va;
						this.Stage = 5;
						this.SetDamageType( ResistanceType.Physical, 100 );
						this.SetDamageType( ResistanceType.Fire, 0 );
						this.SetDamageType( ResistanceType.Cold, 0 );
						this.SetDamageType( ResistanceType.Poison, 0 );
						this.SetDamageType( ResistanceType.Energy, 0 );

						this.SetResistance( ResistanceType.Physical, 60 );
						this.SetResistance( ResistanceType.Fire, 60 );
						this.SetResistance( ResistanceType.Cold, 60 );
						this.SetResistance( ResistanceType.Poison, 60 );
						this.SetResistance( ResistanceType.Energy, 60 );

						this.RawStr += 325;
						this.RawInt += 185;
						this.RawDex += 55;
						this.ControlSlots = 3;
						this.MinTameSkill = 90.0;
					}
				}
			}

			else if ( this.Stage == 5 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 340;
						kpgainmax = 50 + ( bc.HitsMax ) / 290;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 7750000 )
				{
					if ( this.S5 == true )
					{
						this.S5 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 375 );

						va = ( this.VirtualArmor + 10 );

						mindamage = this.DamageMin + ( 10 );
						maxdamage = this.DamageMax + ( 10 );

						this.AllowMating = true;
						this.Warmode = false;
						this.Say( "*"+ this.Name +" evolves*");
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						this.BodyValue = 12;
						//this.BodyValue = 46;
						this.VirtualArmor = va;
						this.Stage = 6;

						this.SetDamageType( ResistanceType.Physical, 35 );
						this.SetDamageType( ResistanceType.Fire, 35 );
						this.SetDamageType( ResistanceType.Cold, 0 );
						this.SetDamageType( ResistanceType.Poison, 0 );
						this.SetDamageType( ResistanceType.Energy, 30 );

						this.SetResistance( ResistanceType.Physical, 70 );
						this.SetResistance( ResistanceType.Fire, 70 );
						this.SetResistance( ResistanceType.Cold, 70 );
						this.SetResistance( ResistanceType.Poison, 70 );
						this.SetResistance( ResistanceType.Energy, 70 );
						
						this.SetSkill( SkillName.Magery, 100.0, 130.0 );
						this.SetSkill( SkillName.Meditation, 100.0, 130.0 );
						this.SetSkill( SkillName.EvalInt, 100.0, 130.0 );
						this.SetSkill( SkillName.MagicResist, 100.0, 130.0 );
						this.SetSkill( SkillName.Tactics, 100.0, 130.0 );
						this.SetSkill( SkillName.Wrestling, 100.0, 130.0 );
						this.SetSkill( SkillName.Anatomy, 100.0, 130.0 );

						this.RawStr += 325;
						this.RawInt += 220;
						this.RawDex += 75;
						this.ControlSlots = 3;
						this.MinTameSkill = 100.0;
					}
				}
			}

			else if ( this.Stage == 6 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.HitsMax ) / 540;
						kpgainmax = 50 + ( bc.HitsMax ) / 480;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}

				if ( this.KP >= 15000000 )
				{
					if ( this.S6 == true )
					{
						this.S6 = false;
						int hits, va, mindamage, maxdamage;

						hits = ( this.HitsMax + 500 );

						va = ( this.VirtualArmor + 80 );

						mindamage = this.DamageMin + ( 25 );
						maxdamage = this.DamageMax + ( 25 );

						this.Warmode = false;
						this.Say( "*"+ this.Name +" is now an ancient dragon*");
						this.Title = "the Ancient Dragon";
						this.SetDamage( mindamage, maxdamage );
						this.SetHits( hits );
						this.BodyValue = 46;
						//this.BodyValue = 172;
						this.VirtualArmor = va;
						this.Stage = 7;

						this.SetDamageType( ResistanceType.Physical, 20 );
						this.SetDamageType( ResistanceType.Fire, 20 );
						this.SetDamageType( ResistanceType.Cold, 20 );
						this.SetDamageType( ResistanceType.Poison, 20 );
						this.SetDamageType( ResistanceType.Energy, 20 );
						this.SetResistance( ResistanceType.Physical, 80 );
						this.SetResistance( ResistanceType.Fire, 80 );
						this.SetResistance( ResistanceType.Cold, 80 );
						this.SetResistance( ResistanceType.Poison, 80 );
						this.SetResistance( ResistanceType.Energy, 80 );
						
						this.SetSkill( SkillName.Magery, 150.0, 175.0 );
						this.SetSkill( SkillName.Meditation, 150.0, 175.0 );
						this.SetSkill( SkillName.EvalInt, 150.0, 175.0 );
						this.SetSkill( SkillName.MagicResist, 150.0, 175.0 );
						this.SetSkill( SkillName.Tactics, 150.0, 175.0 );
						this.SetSkill( SkillName.Wrestling, 150.0, 175.0 );
						this.SetSkill( SkillName.Anatomy, 150.0, 175.0 );

						this.RawStr += 300;
						this.RawInt += 275;
						this.RawDex += 75;
						this.ControlSlots = 5;
						this.MinTameSkill = 110.0;
					}
				}
			}

			else if ( this.Stage == 7 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						kpgainmin = 50 + ( bc.Hits ) / 740;
						kpgainmax = 50 + ( bc.Hits ) / 660;

						this.KP += Utility.RandomList( kpgainmin, kpgainmax );
					}
				}
			}

			base.OnGaveMeleeAttack( defender );
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			PlayerMobile player = from as PlayerMobile;

			if ( player != null )
			{
				if ( dropped is EvoBoost )
				{
					EvoBoost boost = ( EvoBoost )dropped;

					int amount = ( boost.Amount * 100000 );

					this.PlaySound( 665 );
					this.KP += amount;
					boost.Delete();
					this.Say( "*"+ this.Name +" absorbs the Boost*" );

					return false;
				}

				else if ( dropped is DragonDust )
				{
					DragonDust dust = ( DragonDust )dropped;

					int amount = ( dust.Amount * 5 );

					this.PlaySound( 665 );
					this.KP += amount;
					dust.Delete();
					this.Say( "*"+ this.Name +" absorbs the dragon dust*" );

					return false;
				}
				else
				{
				}
			}
			return base.OnDragDrop( from, dropped );
		}


                private void MatingTarget_Callback( Mobile from, object obj ) 
                { 
                           	if ( obj is EvolutionDragon && obj is BaseCreature ) 
                           	{ 
					BaseCreature bc = (BaseCreature)obj;
					EvolutionDragon ed = (EvolutionDragon)obj;

					if ( ed.Controlled == true && ed.ControlMaster == from )
					{
						if ( ed.Female == false )
						{
							if ( ed.AllowMating == true )
							{
								this.Blessed = true;
								this.Pregnant = true;

								m_MatingTimer = new MatingTimer( this, TimeSpan.FromDays( 3.0 ) );
								m_MatingTimer.Start();

								m_EndMating = DateTime.Now + TimeSpan.FromDays( 3.0 );
							}
							else
							{
								from.SendMessage( "This male dragon is not old enough to mate!" );
							}
						}
						else
						{
							from.SendMessage( "This dragon is not male!" );
						}
					}
					else if ( ed.Controlled == true )
					{
						if ( ed.Female == false )
						{
							if ( ed.AllowMating == true )
							{
								if ( ed.ControlMaster != null )
								{
									ed.ControlMaster.SendGump( new MatingGump( from, ed.ControlMaster, this, ed ) );
									from.SendMessage( "You ask the owner of the dragon if they will let your female mate with their male." );
								}
                           					else
                           					{
                              						from.SendMessage( "This dragon is wild." );
			   					}
							}
							else
							{
								from.SendMessage( "This male dragon is not old enough to mate!" );
							}
						}
						else
						{
							from.SendMessage( "This dragon is not male!" );
						}
					}
                           		else 
                           		{ 
                              			from.SendMessage( "This dragon is wild." );
			   		}
                           	} 
                           	else 
                           	{ 
                              		from.SendMessage( "That is not a dragon!" );
			   	}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Controlled == true && this.ControlMaster == from )
			{
				if ( this.Female == true )
				{
					if ( this.AllowMating == true )
					{
					    if ( this.Blessed == false )
					    {
						if ( this.Pregnant == true )
						{
							if ( this.HasEgg == true )
							{
								this.HasEgg = false;
								this.Pregnant = false;
							//	this.Blessed = false;
								from.AddToBackpack( new DragonEgg() );
								from.SendMessage( "A dragon's egg has been placed in your backpack" );
							}
							else
							{
								from.SendMessage( "The dragon has not yet produced an egg." );
							}
						}
						else
						{
							from.SendMessage( "Target a male dragon to mate with this female." );
                                			from.BeginTarget( -1, false, TargetFlags.Harmful, new TargetCallback( MatingTarget_Callback ) );
						}
					    }
					    else
					    {
						from.SendMessage( "The dragon has not yet produced an egg." );
					    } 
					}
					else
					{
						from.SendMessage( "This female dragon is not old enough to mate!" );
					}	
				}
			}
		}

		private DateTime m_NextBreathe;

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.Now >= m_NextBreathe )
			{
				Breathe( combatant );

				m_NextBreathe = DateTime.Now + TimeSpan.FromSeconds( 12.0 + (3.0 * Utility.RandomDouble()) ); // 12-15 seconds
			}
		}

		public void Breathe( Mobile m )
		{
			DoHarmful( m );

			m_BreatheTimer = new BreatheTimer( m, this, this, TimeSpan.FromSeconds( 1.0 ) );
			m_BreatheTimer.Start();
			m_EndBreathe = DateTime.Now + TimeSpan.FromSeconds( 1.0 );

			this.Frozen = true;

			if ( this.Stage == 1 )
			{
				this.MovingParticles( m, 0x1FA8, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 2 )
			{
				this.MovingParticles( m, 0x1FA9, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 3 )
			{
				this.MovingParticles( m, 0x1FAB, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 4 )
			{
				this.MovingParticles( m, 0x1FBC, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 5 )
			{
				this.MovingParticles( m, 0x1FBD, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 6 )
			{
				this.MovingParticles( m, 0x1FBF, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else if ( this.Stage == 7 )
			{
				this.MovingParticles( m, 0x1FBE, 1, 0, false, true, ( this.Hue - 1 ), 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );
			}
			else
			{
				
				this.PublicOverheadMessage( MessageType.Regular, this.SpeechHue, true, "Please call a GM if you are getting this message, they will fix the breathe, thank you :)", false );
			}
		}

		private class BreatheTimer : Timer
		{
			private EvolutionDragon ed;
			private Mobile m_Mobile, m_From;

			public BreatheTimer( Mobile m, EvolutionDragon owner, Mobile from, TimeSpan duration ) : base( duration ) 
			{
				ed = owner;
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				int damagemin = ed.Hits / 20;
				int damagemax = ed.Hits / 25;
				ed.Frozen = false;

				m_Mobile.PlaySound( 0x11D );
				AOS.Damage( m_Mobile, m_From, Utility.RandomMinMax( damagemin, damagemax ), 0, 100, 0, 0, 0 );
				Stop();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 1);
                        writer.Write( m_AllowMating ); 
                        writer.Write( m_HasEgg ); 
                        writer.Write( m_Pregnant ); 
                        writer.Write( m_S1 ); 
                        writer.Write( m_S2 ); 
                        writer.Write( m_S3 ); 
                        writer.Write( m_S4 ); 
                        writer.Write( m_S5 ); 
                        writer.Write( m_S6 ); 
			writer.Write( (int) m_KP );
			writer.Write( (int) m_Stage );
			writer.WriteDeltaTime( m_EndMating );
			writer.WriteDeltaTime( m_EndBreathe );
			writer.WriteDeltaTime( m_EndPetLoyalty );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
                        		m_AllowMating = reader.ReadBool();
                        		m_HasEgg = reader.ReadBool(); 
                        		m_Pregnant = reader.ReadBool();  
                        		m_S1 = reader.ReadBool(); 
                        		m_S2 = reader.ReadBool(); 
                        		m_S3 = reader.ReadBool(); 
                        		m_S4 = reader.ReadBool(); 
                        		m_S5 = reader.ReadBool(); 
                        		m_S6 = reader.ReadBool(); 
					m_KP = reader.ReadInt();
					m_Stage = reader.ReadInt();

					m_EndMating = reader.ReadDeltaTime();
					m_MatingTimer = new MatingTimer( this, m_EndMating - DateTime.Now );
					m_MatingTimer.Start();

					m_EndBreathe = reader.ReadDeltaTime();
					m_BreatheTimer = new BreatheTimer( this, this, this, m_EndBreathe - DateTime.Now );
					m_BreatheTimer.Start();

					m_EndPetLoyalty = reader.ReadDeltaTime();
					m_PetLoyaltyTimer = new PetLoyaltyTimer( this, m_EndPetLoyalty - DateTime.Now );
					m_PetLoyaltyTimer.Start();

					break;
				}
				case 0:
				{
					TimeSpan durationbreathe = TimeSpan.FromSeconds( 1.0 );
					TimeSpan durationmating = TimeSpan.FromDays( 3.0 );
					TimeSpan durationloyalty = TimeSpan.FromSeconds( 5.0 );

					m_BreatheTimer = new BreatheTimer( this, this, this, durationbreathe );
					m_BreatheTimer.Start();
					m_EndBreathe = DateTime.Now + durationbreathe;

					m_MatingTimer = new MatingTimer( this, durationmating );
					m_MatingTimer.Start();
					m_EndMating = DateTime.Now + durationmating;

					m_PetLoyaltyTimer = new PetLoyaltyTimer( this, durationloyalty );
					m_PetLoyaltyTimer.Start();
					m_EndPetLoyalty = DateTime.Now + durationloyalty;

					break;
				}
			}
		}
	}

	public class MatingTimer : Timer
	{ 
		private EvolutionDragon ed;

		public MatingTimer( EvolutionDragon owner, TimeSpan duration ) : base( duration ) 
		{ 
			Priority = TimerPriority.OneSecond;
			ed = owner;
		}

		protected override void OnTick() 
		{
			ed.Blessed = false;
			ed.HasEgg = true;
		//	ed.Pregnant = false;
			Stop();
		}
	}
	public class PetLoyaltyTimer : Timer
	{ 
		private EvolutionDragon ed;

		public PetLoyaltyTimer( EvolutionDragon owner, TimeSpan duration ) : base( duration ) 
		{ 
			Priority = TimerPriority.OneSecond;
			ed = owner;
		}

		protected override void OnTick() 
		{
			ed.Loyalty = 100;

			PetLoyaltyTimer lt = new PetLoyaltyTimer( ed, TimeSpan.FromSeconds( 5.0 ) );
			lt.Start();
			ed.EndPetLoyalty = DateTime.Now + TimeSpan.FromSeconds( 5.0 );

			Stop();
		}
	}
	public class MatingGump : Gump
	{
		private Mobile m_From;
		private Mobile m_Mobile;
		private EvolutionDragon m_ED1;
		private EvolutionDragon m_ED2;

		public MatingGump( Mobile from, Mobile mobile, EvolutionDragon ed1, EvolutionDragon ed2 ) : base( 25, 50 )
		{
			Closable = false; 
			Dragable = false; 

			m_From = from;
			m_Mobile = mobile;
			m_ED1 = ed1;
			m_ED2 = ed2;

			AddPage( 0 );

			AddBackground( 25, 10, 420, 200, 5054 );

			AddImageTiled( 33, 20, 401, 181, 2624 );
			AddAlphaRegion( 33, 20, 401, 181 );

			AddLabel( 125, 148, 1152, m_From.Name +" would like to mate "+ m_ED1.Name +" with" );
			AddLabel( 125, 158, 1152, m_ED2.Name +"." );

			AddButton( 100, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 130, 50, 1152, "Allow them to mate." );
			AddButton( 100, 75, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 130, 75, 1152, "Do not allow them to mate." );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				m_From.SendMessage( m_Mobile.Name +" declines your request to mate the two dragons." );
				m_Mobile.SendMessage( "You decline "+ m_From.Name +"'s request to mate the two dragons." );
			}
			if ( info.ButtonID == 1 )
			{
				m_ED1.Blessed = true;
				m_ED1.Pregnant = true;

				MatingTimer mt = new MatingTimer( m_ED1, TimeSpan.FromDays( 3.0 ) );
				mt.Start();
				m_ED1.EndMating = DateTime.Now + TimeSpan.FromDays( 3.0 );

				m_From.SendMessage( m_Mobile.Name +" accepts your request to mate the two dragons." );
				m_Mobile.SendMessage( "You accept "+ m_From.Name +"'s request to mate the two dragons." );
			}
		}
	}
//aa Stats
	public class EvoStatGump : Gump
	{
		private static string FormatSkill( BaseCreature c, SkillName name )
		{
			Skill skill = c.Skills[name];

			if ( skill.Base < 10.0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", skill.Base );
		}

		private static string FormatAttributes( int cur, int max )
		{
			if ( max == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}/{1}</div>", cur, max );
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}

		private static string FormatElement( int val )
		{
			if ( val <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}%</div>", val );
		}
		public string Color( string text, int color ) 
		{ 
			return String.Format( "<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text ); //  :X6
		}

		private const int LabelColor = 0x24E5;


		public EvoStatGump( BaseCreature c, int stage, int kp ) : base( 250, 50 )
		{
			AddPage( 0 );
	string header = String.Format( "<center><i>{0}   stage {1}</i></center>", c.Name, stage );
			AddImage( 100, 100, 2080 );
			AddImage( 118, 137, 2081 );
			AddImage( 118, 207, 2081 );
			AddImage( 118, 277, 2081 );
			AddImage( 118, 347, 2083 );

			AddHtml( 147, 108, 210, 18, Color( header, 200 ), false, false );

			AddButton( 240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0 );

			AddImage( 140, 138, 2091 );
			AddImage( 140, 335, 2091 );

			int pages = 4;
			int page = 0;


			#region Attributes
			AddPage( ++page );

			AddImage( 128, 152, 2086 );
			AddHtmlLocalized( 147, 150, 160, 18, 1049593, 200, false, false ); // Attributes

			AddHtmlLocalized( 153, 168, 160, 18, 1049578, LabelColor, false, false ); // Hits
			AddHtml( 280, 168, 75, 18, FormatAttributes( c.Hits, c.HitsMax ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 1049579, LabelColor, false, false ); // Stamina
			AddHtml( 280, 186, 75, 18, FormatAttributes( c.Stam, c.StamMax ), false, false );

			AddHtmlLocalized( 153, 204, 160, 18, 1049580, LabelColor, false, false ); // Mana
			AddHtml( 280, 204, 75, 18, FormatAttributes( c.Mana, c.ManaMax ), false, false );

			AddHtmlLocalized( 153, 222, 160, 18, 1028335, LabelColor, false, false ); // Strength
			AddHtml( 320, 222, 35, 18, FormatStat( c.Str ), false, false );

			AddHtmlLocalized( 153, 240, 160, 18, 3000113, LabelColor, false, false ); // Dexterity
			AddHtml( 320, 240, 35, 18, FormatStat( c.Dex ), false, false );

			AddHtmlLocalized( 153, 258, 160, 18, 3000112, LabelColor, false, false ); // Intelligence
			AddHtml( 320, 258, 35, 18, FormatStat( c.Int ), false, false );
	string skp = String.Format( "<left>Kill Points</left>" );
AddImage( 128, 278, 2086 );
AddHtml( 147, 276, 160, 18, Color( skp, 33 ), false, false ); // 0xC8
AddHtml( 153, 294, 160, 18, String.Format( "<left>{0}</left>", kp ), false, false );

			AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
			AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, pages );
			#endregion

			#region Resistances
			if ( Core.AOS )
			{
				AddPage( ++page );

				AddImage( 128, 152, 2086 );
				AddHtmlLocalized( 147, 150, 160, 18, 1061645, 200, false, false ); // Resistances

				AddHtmlLocalized( 153, 168, 160, 18, 1061646, LabelColor, false, false ); // Physical
				AddHtml( 320, 168, 35, 18, FormatElement( c.PhysicalResistance ), false, false );

				AddHtmlLocalized( 153, 186, 160, 18, 1061647, LabelColor, false, false ); // Fire
				AddHtml( 320, 186, 35, 18, FormatElement( c.FireResistance ), false, false );

				AddHtmlLocalized( 153, 204, 160, 18, 1061648, LabelColor, false, false ); // Cold
				AddHtml( 320, 204, 35, 18, FormatElement( c.ColdResistance ), false, false );

				AddHtmlLocalized( 153, 222, 160, 18, 1061649, LabelColor, false, false ); // Poison
				AddHtml( 320, 222, 35, 18, FormatElement( c.PoisonResistance ), false, false );

				AddHtmlLocalized( 153, 240, 160, 18, 1061650, LabelColor, false, false ); // Energy
				AddHtml( 320, 240, 35, 18, FormatElement( c.EnergyResistance ), false, false );

				AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
				AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			}
			#endregion

			#region Damage
			if ( Core.AOS )
			{
				AddPage( ++page );

				AddImage( 128, 152, 2086 );
				AddHtmlLocalized( 147, 150, 160, 18, 1017319, 200, false, false ); // Damage

				AddHtmlLocalized( 153, 168, 160, 18, 1061646, LabelColor, false, false ); // Physical
				AddHtml( 320, 168, 35, 18, FormatElement( c.PhysicalDamage ), false, false );

				AddHtmlLocalized( 153, 186, 160, 18, 1061647, LabelColor, false, false ); // Fire
				AddHtml( 320, 186, 35, 18, FormatElement( c.FireDamage ), false, false );

				AddHtmlLocalized( 153, 204, 160, 18, 1061648, LabelColor, false, false ); // Cold
				AddHtml( 320, 204, 35, 18, FormatElement( c.ColdDamage ), false, false );

				AddHtmlLocalized( 153, 222, 160, 18, 1061649, LabelColor, false, false ); // Poison
				AddHtml( 320, 222, 35, 18, FormatElement( c.PoisonDamage ), false, false );

				AddHtmlLocalized( 153, 240, 160, 18, 1061650, LabelColor, false, false ); // Energy
				AddHtml( 320, 240, 35, 18, FormatElement( c.EnergyDamage ), false, false );

				AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
				AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			}
			#endregion

			#region Skills
			AddPage( ++page );
			AddImage( 128, 152, 2086 );
			AddHtmlLocalized( 147, 150, 160, 18, 3001030, 200, false, false ); // Combat Ratings

			AddHtmlLocalized( 153, 168, 160, 18, 1044103, LabelColor, false, false ); // Wrestling
			AddHtml( 320, 168, 35, 18, FormatSkill( c, SkillName.Wrestling ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 1044087, LabelColor, false, false ); // Tactics
			AddHtml( 320, 186, 35, 18, FormatSkill( c, SkillName.Tactics ), false, false );

			AddHtmlLocalized( 153, 204, 160, 18, 1044086, LabelColor, false, false ); // Magic Resistance
			AddHtml( 320, 204, 35, 18, FormatSkill( c, SkillName.MagicResist ), false, false );

			AddHtmlLocalized( 153, 222, 160, 18, 1044061, LabelColor, false, false ); // Anatomy
			AddHtml( 320, 222, 35, 18, FormatSkill( c, SkillName.Anatomy ), false, false );


			AddImage( 128, 260, 2086 );
			AddHtmlLocalized( 147, 258, 160, 18, 3001032, 200, false, false ); // Lore & Knowledge

			AddHtmlLocalized( 153, 276, 160, 18, 1044085, LabelColor, false, false ); // Magery
			AddHtml( 320, 276, 35, 18, FormatSkill( c, SkillName.Magery ), false, false );

			AddHtmlLocalized( 153, 294, 160, 18, 1044076, LabelColor, false, false ); // Evaluating Intelligence
			AddHtml( 320, 294, 35, 18,FormatSkill( c, SkillName.EvalInt ), false, false );

			AddHtmlLocalized( 153, 312, 160, 18, 1044106, LabelColor, false, false ); // Meditation
			AddHtml( 320, 312, 35, 18, FormatSkill( c, SkillName.Meditation ), false, false );

/*
			AddHtmlLocalized( 153, 168, 160, 18, 1044061, LabelColor, false, false ); // Anatomy
			AddHtml( 320, 168, 35, 18, FormatSkill( c, SkillName.Anatomy ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 1044076, LabelColor, false, false ); // Eval Int
			AddHtml( 320, 186, 35, 18, FormatSkill( c, SkillName.EvalInt ), false, false );

			AddHtmlLocalized( 153, 204, 160, 18, 1044085, LabelColor, false, false ); // Magery
			AddHtml( 320, 204, 35, 18, FormatSkill( c, SkillName.Magery ), false, false );

			AddHtmlLocalized( 153, 222, 160, 18, 1044106, LabelColor, false, false ); // Meditation
			AddHtml( 320, 222, 35, 18, FormatSkill( c, SkillName.Meditation ), false, false );

			AddHtmlLocalized( 153, 240, 160, 18, 1044086, LabelColor, false, false ); // Magic Resistance
			AddHtml( 320, 240, 35, 18, FormatSkill( c, SkillName.MagicResist ), false, false );


			AddHtmlLocalized( 153, 258, 160, 18, 1044087, LabelColor, false, false ); // Tactics
			AddHtml( 320, 258, 35, 18, FormatSkill( c, SkillName.Tactics ), false, false );

			AddHtmlLocalized( 153, 276, 160, 18, 1044103, LabelColor, false, false ); // Wrestling
			AddHtml( 320, 276, 35, 18,FormatSkill( c, SkillName.Wrestling ), false, false );

*/
			AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, 1 );
			AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			#endregion

		}
	}
}