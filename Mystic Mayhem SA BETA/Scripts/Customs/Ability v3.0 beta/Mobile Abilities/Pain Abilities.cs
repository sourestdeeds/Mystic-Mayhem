//Created by Peoharen for the Mobile Abilities Package.
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Engines.PartySystem;
using Server.Guilds;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server
{
	public partial class Ability
	{

		#region Push
		public static void ShinraTensei( Mobile from, Mobile to )
		{
			if ( !CanUse( from ) || to == null || !from.CanSee( to ) )
				return;

			from.Say( "Kal Vas Tal [Wing Buffet]" );
			new PushTimer( from, to ).Start();
		}

		private class PushTimer : Timer
		{
			private Mobile m_From;
			private Mobile m_To;
			private int m_Count;

			public PushTimer( Mobile from, Mobile to ) : base( TimeSpan.FromMilliseconds( 100.0 ), TimeSpan.FromMilliseconds( 100.0 ) )
			{
				m_From = from;
				m_To = to;
				m_Count = 0;

				m_To.CantWalk = true;
			}

			protected override void OnTick()
			{
				m_Count++;

				if ( m_From == null || m_To == null )
				{
					if ( m_To != null )
						m_To.CantWalk = false;

					Stop();
				}
				else if ( m_Count > 11 || !m_To.InRange( m_From, 11 ) )
				{
					if ( m_To != null )
					{
						m_From.Combatant = m_To;
						m_To.CantWalk = false;
						Stop();
					}
				}
				else if ( m_To.Map != null )
				{
					Direction d = m_To.GetDirectionTo( m_From.Location );
					Point3D point = new Point3D( m_To.X, m_To.Y, m_To.Z );

					switch( d )
					{
						case (Direction)0x0: case (Direction)0x80: point.Y++; break; //North
						case (Direction)0x1: case (Direction)0x81: { point.X--; point.Y++; break; } //Right
						case (Direction)0x2: case (Direction)0x82: point.X--; break; //East
						case (Direction)0x3: case (Direction)0x83: { point.X--; point.Y--; break; } //Down
						case (Direction)0x4: case (Direction)0x84: point.Y--; break; //South
						case (Direction)0x5: case (Direction)0x85: { point.X++; point.Y--; break; } //Left
						case (Direction)0x6: case (Direction)0x86: point.X++; break; //West
						case (Direction)0x7: case (Direction)0x87: { point.X++; point.Y++; break; } //Up
						default: { break; }
					}

					switch( d )
					{
						case (Direction)0x0: case (Direction)0x80: { m_To.Direction = Direction.South; break; } //North
						case (Direction)0x1: case (Direction)0x81: { m_To.Direction = Direction.Left; break; } //Right
						case (Direction)0x2: case (Direction)0x82: { m_To.Direction = Direction.West; break; } //East
						case (Direction)0x3: case (Direction)0x83: { m_To.Direction = Direction.Up; break; } //Down
						case (Direction)0x4: case (Direction)0x84: { m_To.Direction = Direction.North; break; } //South
						case (Direction)0x5: case (Direction)0x85: { m_To.Direction = Direction.Right; break; } //Left
						case (Direction)0x6: case (Direction)0x86: { m_To.Direction = Direction.East; break; } //West
						case (Direction)0x7: case (Direction)0x87: { m_To.Direction = Direction.Down; break; } //Up
						default: { break; }
					}


					if ( m_To.Map.CanFit( point.X, point.Y, m_To.Map.GetAverageZ( point.X, point.Y ), 16, false, false ) )
						m_To.Location = point;
					else
					{
						m_To.CantWalk = false;
						AOS.Damage( m_To, m_From, ((15 - m_Count) * 10), 100, 0, 0, 0, 0 );
						//m_To.Damage( 10 - m_Count * 10 );
						Stop();
					}
				}
			}
		}
		#endregion

		#region Pull
		public static void BanshoTenin( Mobile from, Mobile to )
		{
			if ( !CanUse( from ) || to == null || !from.CanSee( to ) )
				return;

			from.Say( "Bansho Ten'in" );
			new PullTimer( from, to ).Start();
		}

		private class PullTimer : Timer
		{
			private Mobile m_From;
			private Mobile m_To;
			private int m_Count;

			public PullTimer( Mobile from, Mobile to ) : base( TimeSpan.FromMilliseconds( 100.0 ), TimeSpan.FromMilliseconds( 100.0 ) )
			{
				m_From = from;
				m_To = to;
				m_Count = 0;

				m_To.CantWalk = true;
			}

			protected override void OnTick()
			{
				m_Count++;

				if ( m_From == null || m_To == null )
				{
					if ( m_To != null )
						m_To.CantWalk = false;

					Stop();
				}
				else if ( m_Count > 11 || m_To.InRange( m_From, 1 ) )
				{
					if ( m_To != null )
					{
						m_From.Combatant = m_To;

						if ( m_From.Weapon != null )
							if ( m_From.Weapon is BaseWeapon )
							{
								BaseWeapon bw = (BaseWeapon) m_From.Weapon;
								bw.OnHit( m_From, m_To, 2.0 ); //int damage = bw.ComputeDamage( m_From, m_To ) * 2;
								//Ability.Strike( m_From, 1 );
							}

						m_To.CantWalk = false;
						Stop();
					}
				}
				else
				{
					Direction d = m_To.GetDirectionTo( m_From.Location );
					m_To.Direction = d;

					switch( d )
					{
						case (Direction)0x0: case (Direction)0x80: m_To.Y--; break; //North
						case (Direction)0x1: case (Direction)0x81: { m_To.X++; m_To.Y--; break; } //Right
						case (Direction)0x2: case (Direction)0x82: m_To.X++; break; //East
						case (Direction)0x3: case (Direction)0x83: { m_To.X++; m_To.Y++; break; } //Down
						case (Direction)0x4: case (Direction)0x84: m_To.Y++; break; //South
						case (Direction)0x5: case (Direction)0x85: { m_To.X--; m_To.Y++; break; } //Left
						case (Direction)0x6: case (Direction)0x86: m_To.X--; break; //West
						case (Direction)0x7: case (Direction)0x87: { m_To.X--; m_To.Y--; break; } //Up
						default: { break; }
					}
				}
			}
		}
		#endregion

		#region Ninjitsu

		public static void GrandFireball( Mobile from )
		{
			from.Say("Katon!");
			new GrandFireballTimer( from ).Start();
		}

		private class GrandFireballTimer : Timer
		{
			private Mobile m_From;
			private int m_Count;
			private Point3D m_Point;
			private Direction m_Direction;

			public GrandFireballTimer( Mobile from ) : base( TimeSpan.FromMilliseconds( 250.0 ), TimeSpan.FromMilliseconds( 250.0 ) )
			{
				m_From = from;
				m_Direction = from.Direction;
				m_Count = 0;

				/*  4 3 2 1 0 1 2 3 4
				  4|_|_|_|_|_|_|_|_|_|
				  3|_|_|_|O|O|O|_|_|_|
				  2|_|_|O|O|O|O|O|_|_|
				  1|_|O|O|O|O|O|O|O|_|
				  0|_|O|O|O|X|O|O|O|_|
				  1|_|O|O|O|O|O|O|O|_|
				  2|_|_|O|O|O|O|O|_|_|
				  3|_|_|_|O|O|O|_|_|_|
				  4|_|_|_|_|_|_|_|_|_|
				*/

				m_Point = from.Location;
			}

			protected override void OnTick()
			{
				m_Count++;

				if ( m_From == null || m_Count > 7 )
					Stop();

				if ( m_Count == 1 )
				{
					m_From.Say("Grand Fire Ball");
					MoveFire();
					MoveFire();
					MoveFire();
					MoveFire();
				}

				// Effects
				for ( int i = -3; i < 4; i++ )
					for ( int j = -3; j < 4; j++ )
					{
						Point3D point = new Point3D( m_Point.X + i, m_Point.Y + j, m_Point.Z );
						if ( Ability.GetDist( m_Point, point ) < 3.1 )
							Effects.SendLocationParticles( EffectItem.Create( point, m_From.Map, EffectItem.DefaultDuration ), 
								0x36CB/*ItemID*/, 1/*Speed*/, 7/*Duration*/, 0/*Hue*/, 4/*RenderMode*/, 0/*Effect*/, 0/*Unknown*/ );
					}

				// Mobile Finder
				List<Mobile> targets = new List<Mobile>();

				foreach ( Mobile m in Map.AllMaps[m_From.Map.MapID].GetMobilesInRange( new Point3D( m_Point.X, m_Point.Y, m_Point.Z ), 4 ) )
				{
					if ( CanTarget( m_From, m, true, false, false ) )
						if ( Ability.GetDist( m_Point, m.Location ) < 3.1 )
							targets.Add( m );
				}

				if ( targets.Count > 0 )
					for ( int i = 0; i < targets.Count; i++ )
						AOS.Damage( targets[i], m_From, (int)(m_From.Skills[SkillName.Magery].Value * 0.2 + 0.10 * 0.5), 0, 100, 0, 0, 0 );

				MoveFire();
			}

			private void MoveFire()
			{
				IncreaseByDirection( ref m_Point, m_Direction );
			}
		}
		#endregion





	}
}

namespace Server.Commands
{
	public class NarutoCommands
	{
		public static void Initialize()
		{
			CommandSystem.Register( "NPush", AccessLevel.Seer, new CommandEventHandler( ShinraTensei_OnCommand ) );
			CommandSystem.Register( "NPull", AccessLevel.Seer, new CommandEventHandler( BanshoTenin_OnCommand ) );
			CommandSystem.Register( "NFire", AccessLevel.Seer, new CommandEventHandler( GrandFireball_OnCommand ) );
		}

		[Description( "Shove a foe away from you" )]
		public static void ShinraTensei_OnCommand( CommandEventArgs e )
		{
			e.Mobile.BeginTarget( 10, false, TargetFlags.Harmful, new TargetCallback( ShinraTensei_CallBack ) );
		}

		public static void ShinraTensei_CallBack( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
				Ability.ShinraTensei( from, (Mobile)targeted );
			else
				from.SendMessage("That is not a mobile");
		}


		[Description( "Pull a foe to you" )]
		public static void BanshoTenin_OnCommand( CommandEventArgs e )
		{
			e.Mobile.BeginTarget( 10, false, TargetFlags.Harmful, new TargetCallback( BanshoTenin_CallBack ) );
		}

		public static void BanshoTenin_CallBack( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
				Ability.BanshoTenin( from, (Mobile)targeted );
			else
				from.SendMessage("That is not a mobile");
		}


		[Description( "Cast a huge fireball" )]
		public static void GrandFireball_OnCommand( CommandEventArgs e )
		{
			Ability.GrandFireball( e.Mobile );
		}

	}
}












