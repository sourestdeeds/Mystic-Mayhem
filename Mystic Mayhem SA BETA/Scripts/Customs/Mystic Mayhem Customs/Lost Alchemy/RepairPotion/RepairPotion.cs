using System;
using Server.Network;
using Server;
using Server.Targets;
using Server.Targeting;
namespace Server.Items
{
	public class RepairPotion : BasePotion
	{
		[Constructable]
		public RepairPotion() : base( 0xF0B, PotionEffect.Repair )
		{
			Weight = 1.0;
			Movable = true;
			Hue = 407;
			Name = "Repair Potion";
		}

		public RepairPotion( Serial serial ) : base( serial )
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
	  
	  	public override void Drink( Mobile m )
      	        {
         	if ( m.InRange( this.GetWorldLocation(), 1 ) ) 
         	{ 
                        m.Target = new RepairTarget( this );
           		m.SendMessage( "What would you like to pour this on!" );
                       // this.Delete();
                       // m.AddToBackpack( new Bottle() );
         	} 
         	else 
         	{ 
            	m.LocalOverheadMessage( MessageType.Regular, 906, 1019045 ); // I can't reach that. 
         	} 
		}
  public class RepairTarget : Target
		{
			private RepairPotion m_Potion;

			public RepairTarget( Item i) :  base ( 1, false, TargetFlags.None )
			{
				m_Potion=(RepairPotion)i;
			}
			protected override void OnTarget( Mobile from, object targeted )
			{
				//int number;
				if ( targeted is BaseArmor )
				{
					BaseArmor repairing = (BaseArmor)targeted;
					if ( !repairing.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1044275 ); // The item must be in your backpack to repair it.
					}
					else if ( repairing.MaxHitPoints <= 0 || repairing.HitPoints == repairing.MaxHitPoints )
					{
						from.SendLocalizedMessage( 1044281 );// That item is in full repair
					}
					else
					{
						from.SendLocalizedMessage( 1044279 ); // You repair the item.
						repairing.MaxHitPoints -= 2;
						repairing.HitPoints = repairing.MaxHitPoints;
                       m_Potion.Delete();
                       from.AddToBackpack( new Bottle() );
					}
				}
				else if ( targeted is BaseWeapon )
				{
					BaseWeapon repairing2 = (BaseWeapon)targeted;
					if ( !repairing2.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1044275 ); // The item must be in your backpack to repair it.
					}
					else if ( repairing2.MaxHitPoints <= 0 || repairing2.HitPoints == repairing2.MaxHitPoints )
					{
						from.SendLocalizedMessage( 1044281 );// That item is in full repair
					}
					else
					{
						from.SendLocalizedMessage( 1044279 ); // You repair the item.
						repairing2.MaxHitPoints -= 2;
						repairing2.HitPoints = repairing2.MaxHitPoints;
                       m_Potion.Delete();
                       from.AddToBackpack( new Bottle() );
					}
				}

				else if ( targeted is DivineCountenance )
				{
					DivineCountenance repairing3 = (DivineCountenance)targeted;
					if ( !repairing3.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1044275 ); // The item must be in your backpack to repair it.
					}
					else if ( repairing3.MaxHitPoints <= 0 || repairing3.HitPoints == repairing3.MaxHitPoints )
					{
						from.SendLocalizedMessage( 1044281 );// That item is in full repair
					}
					else
					{
						from.SendLocalizedMessage( 1044279 ); // You repair the item.
						repairing3.MaxHitPoints -= 2;
						repairing3.HitPoints = repairing3.MaxHitPoints;
                       m_Potion.Delete();
                       from.AddToBackpack( new Bottle() );
					}
				}

				else if ( targeted is Item )
				{
					from.SendLocalizedMessage( 1044277 ); // That item cannot be repaired.
				}
				else
				{
				   from.SendLocalizedMessage( 500426 ); // You can't repair that.
				}
				}
		}
	}
}
