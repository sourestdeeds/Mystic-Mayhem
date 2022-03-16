using System; 
using System.Collections; 
using Server; 
using Server.Commands;
using Server.Mobiles; 
using Server.Network; 
using Server.Targeting;


namespace Server.Mobiles
{ 
	public class BondTime
	{ 

		public static void Initialize()
		{
			CommandSystem.Register( "BT", AccessLevel.Player, new CommandEventHandler( BT_OnCommand ) );  
			CommandSystem.Register( "BondTime", AccessLevel.Player, new CommandEventHandler( BT_OnCommand ) );   
		} 

		public static void BT_OnCommand( CommandEventArgs args )
		{ 
			Mobile m = args.Mobile; 
			PlayerMobile from = m as PlayerMobile; 
          
			if( from != null ) 
			{  
				from.SendMessage ( "Target your tame pet to view bond time." );
				m.Target = new InternalTarget();
			} 
		} 

		private class InternalTarget : Target
		{
			public InternalTarget() : base( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object obj )
			{
				if ( !from.Alive )
				{
					from.SendMessage( "You may not do that while dead." );
				}
                           	else if ( obj is BaseCreature ) 
                           	{ 
					BaseCreature bc = (BaseCreature)obj;
					//EvolutionDragon pet = (EvolutionDragon)obj;

					if ( bc.Controlled == true && bc.ControlMaster == from )
					{

						if (bc.IsBonded == true)
						{
							from.SendMessage("This pet is already bonded.");
						}
						else if ( bc.BondingBegin == DateTime.MinValue )
						{
							from.SendMessage("Bonding has not started!");
							//BondingBegin = DateTime.Now;
						}
						else
						{

							TimeSpan BondingDelay = TimeSpan.FromDays( 7.0 );
							DateTime TimeEnd = (bc.BondingBegin + BondingDelay);
							TimeSpan TimeLeft = TimeEnd - DateTime.Now;

							if ( (int)(TimeLeft.TotalSeconds) <= 0 )
							{
								from.SendMessage("This pet can be bonded now!");
							}
							else
							{
							int days, hours, minutes, totalMinutes;
							totalMinutes = (int)(TimeLeft.TotalSeconds / 60);
							days = (totalMinutes / 1440 );
							hours = (totalMinutes / 60) % 24;
							minutes = totalMinutes % 60;
							string exactTime = String.Format( "{0} days {1}:{2:D2}", days, hours, minutes );
								from.SendMessage("Time remaining to bond: {0}", exactTime);
							}

										
						}




					}
					else
					{
						from.SendMessage( "You do not control this pet!" );
					}
                           	} 
                           	else 
                           	{ 
                              		from.SendMessage( "That is not a pet!" );
			   	}
			}
		}
	} 
} 
