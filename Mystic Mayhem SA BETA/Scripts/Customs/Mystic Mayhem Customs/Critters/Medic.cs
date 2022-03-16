//Written by Haazen Jul 2005
using System; 
using Server; 
using System.Collections; 
using System.Collections.Generic;
using Server.Items; 
using Server.Spells;
using Server.Spells.First;
using Server.Spells.Second;
using Server.Spells.Sixth;
using Server.ContextMenus; 
using Server.Misc; 
using Server.Mobiles; 
using Server.Network; 

namespace Server.Mobiles 
{ 
	[CorpseName( "a kilt medical worker" )]
	public class Medic : BaseCreature 
	{ 
		private int m_Pay = 32; 
		private bool m_IsHired; 
		private int m_HoldGold = 8; 
		private Timer m_PayTimer; 
		private DateTime m_TimeEnd;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime TimeEnd
		{
			get{ return m_TimeEnd; }
			set{ m_TimeEnd = value; }
		}
        
		[Constructable]
		public Medic(): base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.1, 4.0 ) 
		{ 
                	Name = NameList.RandomName( "female" );
			Title = " the Medic";
			Body = 0x191;
			Female = true;
			Hue = 0x83EF;
			Blessed = true;
			ControlSlots = 3;
		//	SpeechHue = 72;

			SetStr( 400 );
			SetDex( 200 );
			SetInt( 400 );

			SetHits( 2500 );
			SetDamage( 25, 35 );

			SetSkill( SkillName.EvalInt, 200.0 );
			SetSkill( SkillName.Magery, 200.0 );
			SetSkill( SkillName.Meditation, 200.0 );
			SetSkill( SkillName.Anatomy, 300.0 );
			SetSkill( SkillName.Fencing, 200.0 );
			SetSkill( SkillName.MagicResist, 200.0 );
			SetSkill( SkillName.Tactics, 200.0 );

			HairItemID = 0x203D;
			HairHue = Utility.RandomHairHue();
			int hue = Utility.RandomList ( 1153, 1167, 1154, 1168, 1278, 1378, 91, 86, 121, 146, 156, 176,191 );

			AddItem( new PlainDress( hue ) ); 


			AddItem( new ThighBoots() );

			AddItem( new Obi( 132 ) );
			AddItem( new TricorneHat( hue ) );
			m_TimeEnd = DateTime.Now + TimeSpan.FromMinutes( 120.0 );
		} 

//aa time
		public override bool HandlesOnSpeech(Mobile from)
		{
			base.HandlesOnSpeech( from );
			return true;
		}
 
		public override bool DeleteOnRelease{ get{ return true; } }  //aa

		public override void OnThink()
                {
			foreach ( Mobile m in this.GetMobilesInRange( 10 ) ) 
			{                                                   
				if ( this.ControlMaster == m )
				{
					if ( m.InRange( this, 10 ) ) 
					{ 

						if ( m.Hits < (m.HitsMax - 15) )
						{
							if ( !new AgilitySpell( this, null ).Cast() )
							{
								new AgilitySpell( this, null ).Cast();
								m.Hits += 12; // increase here if you want more healing
							}
						}
					
						if ( m.Poisoned )
						{
							if ( !new AgilitySpell( this, null ).Cast() )
							{
								new AgilitySpell( this, null ).Cast();
								if ( m.CurePoison( this ) )
								{
									m.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
								}
							}
						}
					}
				}
			}
		}

		public Medic( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
          
			writer.Write( (bool)m_IsHired ); 
			writer.Write( (int)m_HoldGold ); 
			writer.Write( m_TimeEnd );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader );

			int version = reader.ReadInt(); 
          
			m_IsHired = reader.ReadBool(); 
			m_HoldGold = reader.ReadInt(); 
			m_TimeEnd = reader.ReadDateTime();
          
			m_PayTimer = new PayTimer( this ); 
			m_PayTimer.Start(); 
		} 

		private static Hashtable m_HireTable = new Hashtable(); 

		public static Hashtable HireTable 
		{ 
			get{ return m_HireTable; } 
		} 

		public override bool KeepsItemsOnDeath{ get{ return true; } } 
		private int m_GoldOnDeath = 0; 
		public override bool OnBeforeDeath() 
		{ 
            // Stop the pay timer if its running 
			if( m_PayTimer != null ) 
			m_PayTimer.Stop(); 

			m_PayTimer = null; 

            // Get all of the gold on the hireling and add up the total amount 
			if( this.Backpack != null ) 
			{ 
				Item[] AllGold = this.Backpack.FindItemsByType( typeof(Gold), true ); 
				if( AllGold != null ) 
				{ 
					foreach( Gold g in AllGold ) 
						this.m_GoldOnDeath += g.Amount; 
				} 
			} 

			return base.OnBeforeDeath(); 

		} 

		public override void OnDeath( Container c ) 
		{ 

			if( this.m_GoldOnDeath > 0 ) 
				c.DropItem( new Gold( this.m_GoldOnDeath ) ); 
			base.OnDeath( c ); 
		} 

		[CommandProperty( AccessLevel.Administrator )] 
		public bool IsHired 
		{ 
			get 
			{ 
				return m_IsHired; 
			} 
			set 
			{ 
				if ( m_IsHired== value ) 
					return; 

				m_IsHired= value; 
				Delta( MobileDelta.Noto ); 
				InvalidateProperties(); 
			} 
		} 

		public virtual Mobile GetOwner() 
		{ 
			if( !Controlled ) 
				return null; 
			Mobile Owner = ControlMaster; 
          
			m_IsHired = true; 
          
			if( Owner == null ) 
				return null; 
          
			if( Owner.Deleted || Owner.Map != this.Map || !Owner.InRange( Location, 30 ) ) 
			{ 
				Say( 1005653 ); // Hmmm.  I seem to have lost my master. 
				Medic.HireTable.Remove( Owner ); 
				SetControlMaster( null ); 
				this.Delete();
				return null; 
			} 

			return Owner; 
		} 
 
		public virtual bool AddHire( Mobile m ) 
		{ 
			Mobile owner = GetOwner(); 

			if( owner != null ) 
			{ 
				m.SendLocalizedMessage( 1043283, owner.Name ); // I am following ~1_NAME~. 
				return false; 
			} 

			if( SetControlMaster( m ) ) 
			{ 
				m_IsHired = true; 
				return true; 
			} 
          
			return false; 
		} 

		public virtual bool Payday( Medic m ) 
		{ 

			m_Pay = 32;
			return true; 
		} 
 
		public override bool OnDragDrop( Mobile from, Item item ) 
		{ 
			if ( from is PlayerMobile )
			{
				PlayerMobile pm = from as PlayerMobile;
				if ( !pm.Young )
				{
					from.SendMessage("I can only help Young players.");
					return false;
				}
			}


			if( m_Pay != 0 ) 
			{ 
				if( Controlled == false ) 
				{ 
					if( item is Gold ) 
					{ 
						if( item.Amount == m_Pay || item.Amount == m_Pay * 2) //aa  was >=
						{ 
							int span = item.Amount / 32;
							Medic hire = (Medic)m_HireTable[from]; 

							if( hire != null && !hire.Deleted && hire.GetOwner() == from ) 
							{ 
								SayTo( from, 500896 ); // I see you already have an escort. 
								return false; 
							} 

							if( AddHire(from) == true ) 
							{ 
								Say( string.Format( "I thank thee for paying me. I will heal thee for {0} hour{1}.", span == 1 ? "one" : "two", span == 1 ? "" : "s")); 
								m_TimeEnd = DateTime.Now + TimeSpan.FromMinutes( span * 60 );
								m_HireTable[from] = this; 
							//	m_HoldGold += m_Pay; //item.Amount; 
								m_PayTimer = new PayTimer( this ); 
								m_PayTimer.Start(); 
								item.Delete(); //aa
								return true; 
							} 
							else 
								return false; 
						} 
						else 
						{ 
							this.SayHireCost(); 
							return false;
						} 
					} 
					else 
					{ 
						SayTo( from, 1043268 ); // Tis crass of me, but I want gold 
					} 
				} 
				else 
				{ 
					Say( 1042495 );// I have already been hired. 
				} 
			} 
			else 
			{ 
				SayTo( from, 500200 ); // I have no need for that. 
				return false; //aa
			} 

			return base.OnDragDrop( from, item ); 
		} 

		internal void SayHireCost() 
		{ 
			Say( string.Format( "I am available to hire for {0} gold coins an hour for max of two hours. If thou dost give me gold, I will heal thee.", m_Pay ) ); 
		} 

		public override void OnSpeech( SpeechEventArgs e ) 
		{    
			if( !e.Handled && e.Mobile.InRange( this, 6 ) ) 
			{ 
				int[] keywords = e.Keywords; 
				string speech = e.Speech; 

				if( ( e.HasKeyword( 0x003B ) == true ) || ( e.HasKeyword( 0x0162 ) == true ) ) 
				{ 
					e.Handled = Payday( this ); 
					this.SayHireCost(); 
				} 
			} 
//aa time
		        Mobile from = e.Mobile;
                	string keyword5 = this.Name +" time";

                	if ( keyword5 != null && e.Speech.ToLower().IndexOf( keyword5.ToLower() ) >= 0 ) 
 			{ // time
 
	
        	       		if ( ControlMaster != from )
                	   		return;
TimeSpan ts = this.TimeEnd - DateTime.Now;

string gt = String.Format( "{0:D2}:{1:D2}", ts.Hours % 24, ts.Minutes % 60 );			

		//	from.SendMessage ( "Time End {0} ", this.TimeEnd );
		//	Say( string.Format( "I will be leaveing {0}.", this.TimeEnd ) );
			Say( string.Format( "I will be leaveing in {0}.", gt ) );
	               		e.Handled = true;
			}
//aa

			base.OnSpeech( e ); 
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			Mobile Owner = GetOwner(); 
          
			if( Owner == null ) 
			{ 
				base.GetContextMenuEntries( from, list ); 
				list.Add( new HireEntry( from, this ) ); 
			} 
			else 
				base.GetContextMenuEntries( from, list ); 
		} 

		private class PayTimer : Timer 
		{ 
			private Medic di; 
          
			public PayTimer( Medic vend ) : base( TimeSpan.FromMinutes( 5.0 ), TimeSpan.FromMinutes( 5.0 ) ) //aa was 30
			{ 
				di = vend; 
				Priority = TimerPriority.OneMinute; 
			} 
          
			protected override void OnTick() 
			{ 
				try {
				if ( di.Deleted )
				{
					Stop();
					return;
				}

				if ( DateTime.Now >= di.m_TimeEnd )
				{
					di.Say ("Good Bye!");
					Stop();
					di.ControlMaster.PlaySound( 259 );
			Effects.SendLocationEffect( new Point3D( di.X + 1, di.Y + 1, di.Z + 11 ), di.Map, 0x3728, 13, 93, 0 );
			Effects.SendLocationEffect( new Point3D( di.X + 1, di.Y + 1, di.Z + 7 ), di.Map, 0x3728, 13, 93, 0 );
		//	Effects.SendLocationEffect( new Point3D( di.X + 1, di.Y + 1, di.Z + 3 ), di.Map, 0x3728, 13 );
		//	Effects.SendLocationEffect( new Point3D( di.X + 1, di.Y + 1, di.Z - 1 ), di.Map, 0x3728, 13 );
					di.Delete();
				}

				else if ( DateTime.Now + TimeSpan.FromMinutes( 5.0 ) >= di.m_TimeEnd )
				{
					di.SpeechHue = 32;
					di.ControlMaster.PlaySound( 784 );
					di.Say ("I have only a few more minutes to serve you!");
				}
				else if ( DateTime.Now + TimeSpan.FromMinutes( 10.0 ) >= di.m_TimeEnd )
				{
					di.SpeechHue = 71;
					di.Say ("I must be leaving you soon!");
				}
				}
				catch{}
			} 
		} 

		public class HireEntry : ContextMenuEntry 
		{ 
			private Mobile m_Mobile; 
			private Medic m_Hire; 

			public HireEntry( Mobile from, Medic hire ) : base( 6120, 3 )    
			{ 
				m_Hire = hire; 
				m_Mobile = from; 
			} 
          
			public override void OnClick()    
			{    
				m_Hire.Payday(m_Hire); 
				m_Hire.SayHireCost(); 
			} 
		} 
	}    
} 
