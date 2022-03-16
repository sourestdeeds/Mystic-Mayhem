using System;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server;
namespace Server.Items
{
	public class ShrinkPotion : BasePotion
	{

		#region Constructors
		public ShrinkPotion( Serial serial ) : base( serial )
		{
		}
		[Constructable]
		public ShrinkPotion() : base( 0xF09, PotionEffect.Shrink )
		{
			Name="shrink potion";
			Hue = 2315;
		}
		#endregion

		public override void Drink( Mobile from )  //OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;
			else if( from.InRange( this.GetWorldLocation(), 2 ) == false )
			{
				from.SendLocalizedMessage( 500486 );	//That is too far away.
				return;
			}

			Container pack = from.Backpack;

			if ( !(Parent == from || ( pack != null && Parent == pack )) ) //If not in pack.
			{
				from.SendLocalizedMessage( 1042001 );	//That must be in your pack to use it.
				return;
			}
			from.Target=new ShrinkPotionTarget( this );
			from.SendMessage( "What do you wish to shrink?" );
		}


		private class ShrinkPotionTarget : Target
		{
			private ShrinkPotion m_Potion;

			public ShrinkPotionTarget( Item i ) : base( 3, false, TargetFlags.None )
			{
				m_Potion=(ShrinkPotion)i;
			}
			
			protected override void OnTarget( Mobile from, object targ )
			{
				if ( !(m_Potion.Deleted) )
				{
					if ( ShrinkFunctions.Shrink( from, targ ) )
					{
BasePotion.PlayDrinkEffect( from );
						m_Potion.Consume();
					}
				}

				return;
			}
		}


		#region Serialization
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
		#endregion
	}
}
