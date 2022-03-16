//ST
using System;
using System.Reflection;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Necromancy;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Ninjitsu
{
	public class MirrorImage : NinjaSpell
	{
		private static Dictionary<Mobile, int> m_CloneCount = new Dictionary<Mobile, int>();

		public static bool HasClone( Mobile m )
		{
			return m_CloneCount.ContainsKey( m );
		}

		public static void AddClone( Mobile m )
		{
			if ( m == null )
				return;

			if ( m_CloneCount.ContainsKey( m ) )
				m_CloneCount[m]++;
			else
				m_CloneCount[m] = 1;
		}

		public static void RemoveClone( Mobile m )
		{
			if ( m == null )
				return;

			if ( m_CloneCount.ContainsKey( m ) )
			{
				m_CloneCount[m]--;

				if ( m_CloneCount[m] == 0 )
					m_CloneCount.Remove( m );
			}
		}

		private static SpellInfo m_Info = new SpellInfo(
			"Mirror Image", null,
			266,
			9002
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }

		public override double RequiredSkill{ get{ return 40.0; } }
		public override int RequiredMana{ get{ return 10; } }
		public override bool ShowHandMovement{ get{ return true; } }

		public override bool BlockedByAnimalForm{ get{ return false; } }

		public MirrorImage( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
				return false;
			}
			else if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1063133 ); // You cannot summon a mirror image because you have too many followers.
				return false;
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1061091 ); // You cannot cast that spell in this form.
				return false;
			}

			return base.CheckCast();
		}

		public override bool CheckDisturb( DisturbType type, bool firstCircle, bool resistable )
		{
			return false;
		}

		public override void OnBeginCast()
		{
			base.OnBeginCast();

			Caster.SendLocalizedMessage( 1063134 ); // You begin to summon a mirror image of yourself.
		}

		public override void OnCast()
		{
			if ( Caster.Mounted )
			{
				Caster.SendLocalizedMessage( 1063132 ); // You cannot use this ability while mounted.
			}
			else if ( (Caster.Followers + 2) > Caster.FollowersMax )
			{
				Caster.SendLocalizedMessage( 1063133 ); // You cannot summon a mirror image because you have too many followers.
			}
			else if( TransformationSpellHelper.UnderTransformation( Caster, typeof( HorrificBeastSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1061091 ); // You cannot cast that spell in this form.
			}
			else if ( CheckSequence() )
			{
				Caster.FixedParticles( 0x376A, 1, 14, 0x13B5, EffectLayer.Waist );
				Caster.PlaySound( 0x511 );

				new Clone( Caster ).MoveToWorld( Caster.Location, Caster.Map );
				
			}

			FinishSequence();
		}
//
		public override TimeSpan GetCastDelay()
		{

			return TimeSpan.FromSeconds( 2.0 );
		}
//
	}
}

namespace Server.Mobiles
{
	public class Clone : BaseCreature
	{
		private Mobile m_Caster;

		public Clone( Mobile caster ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_Caster = caster;

			Body = caster.Body;

			Hue = caster.Hue;
			Female = caster.Female;

			Name = caster.Name;
			NameHue = caster.NameHue;

			Title = caster.Title;
			Kills = caster.Kills;

			HairItemID = caster.HairItemID;
			HairHue = caster.HairHue;

			FacialHairItemID = caster.FacialHairItemID;
			FacialHairHue = caster.FacialHairHue;

			for ( int i = 0; i < caster.Skills.Length; ++i )
			{
				Skills[i].Base = caster.Skills[i].Base;
				Skills[i].Cap = caster.Skills[i].Cap;
			}

			for( int i = 0; i < caster.Items.Count; i++ )
			{
				if ( !(caster.Items[i] is Backpack) && !(caster.Items[i] is BankBox ) )
					AddItem( CloneItem( caster.Items[i] ) );
			}


			double scaler = ( 960 / ( caster.Skills.Ninjitsu.Base + 120 ) ); // 6 at 40 - 4 at 120


			SetStr( (int)(caster.RawStr / scaler) );
			SetDex( (int)(caster.RawDex / scaler) );
			SetInt( (int)(caster.RawInt / scaler) );
			SetHits( (int)(caster.Hits / scaler) );
			SetStam( (int)(caster.Stam / scaler) );
			SetMana( (int)(caster.Mana / scaler) );


			ControlSlots = 2;
			Summoned = true;
			SummonMaster = caster;

			Controlled = true;
			ControlMaster = caster;
			ControlOrder = OrderType.Guard;
			ControlTarget = null;  // null = all guard caster = all guard me

			TimeSpan duration = TimeSpan.FromSeconds( 30 + caster.Skills.Ninjitsu.Fixed / 40 );

			new UnsummonTimer( caster, this, duration ).Start();
			SummonEnd = DateTime.Now + duration;

			MirrorImage.AddClone( m_Caster );
		}

		public override bool IsHumanInTown() { return false; }

		private Item CloneItem( Item item )
		{
			Item newItem = null;
			Item copy = (Item)item;
			Type t = copy.GetType();
			ConstructorInfo c = t.GetConstructor( Type.EmptyTypes );

			if ( c != null )
			{
				try
				{
					object o = c.Invoke( null );
					if ( o != null && o is Item )
					{
						newItem = (Item)o;

						CopyProps( newItem, copy );
						item.OnAfterDuped( newItem );
					}
					newItem.Layer = item.Layer;
				}
				catch{return null;}
			}
			return newItem;
		}

		public override bool DeleteCorpseOnDeath { get { return true; } }

		public override void OnDelete()
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3728, 10, 15, 5042 );

			base.OnDelete();
		}

		public override void OnAfterDelete()
		{
			MirrorImage.RemoveClone( m_Caster );
			base.OnAfterDelete();
		}

		public override bool IsDispellable { get { return ( Utility.RandomBool() ? true : false ); } }
		public override bool Commandable { get { return true; } }

// resists override
 public override int GetMaxResistance( ResistanceType type )
 {
  int max = base.GetMaxResistance( type );

			if ( max > 95 )
				max = 95;

  return max;
 }

		public Clone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( m_Caster );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Caster = reader.ReadMobile();

			MirrorImage.AddClone( m_Caster );
		}

		private static void CopyProps( Item dest, Item src )
		{
			PropertyInfo[] props = src.GetType().GetProperties();
			for ( int i = 0; i < props.Length; i++ )
			{
				try
				{
					if ( props[i].CanRead && props[i].CanWrite )
					{
						props[i].SetValue( dest, props[i].GetValue( src, null ), null );
					}
				}
				catch
				{
				}
			}
		}
	}
}