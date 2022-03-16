using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "lifeless tentacles" )] // TODO: Corpse name?
	public class CreepingTentacles : BaseCreature
	{

		private DrainTimer m_Timer;

		[Constructable]
		public CreepingTentacles( ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "creeping tentacles";
			Body = 129;
			BaseSoundID = 352;

			SetStr( 151, 200 );
			SetDex( 62, 70 );
			SetInt( 501, 600 );

			SetHits( 271, 300 );

			SetDamage( 28, 30 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 35, 45 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.Meditation, 70.0 );
			SetSkill( SkillName.MagicResist, 60.1, 70.0 );
			SetSkill( SkillName.Swords, 70.1, 80.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 70.1, 80.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 40;

			m_Timer = new DrainTimer( this );
			m_Timer.Start();

			PackReg( 30 );
			PackNecroReg( 15, 25 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool Unprovokable{ get{ return true; } }
		//public override Poison PoisonImmune{ get{ return Poison.Greater; } }

		public CreepingTentacles( Serial serial ) : base( serial )
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

			switch ( version )
			{
				case 0:
				{
					m_Timer = new DrainTimer( this );
					m_Timer.Start();

					break;
				}
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;

			base.OnAfterDelete();
		}

		private class DrainTimer : Timer
		{
			private CreepingTentacles m_Owner;

			public DrainTimer( CreepingTentacles owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Owner = owner;
				Priority = TimerPriority.TwoFiftyMS;
			}

			private static ArrayList m_ToDrain = new ArrayList();

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				if ( 0.1 < Utility.RandomDouble() )  //.2
					return;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 6 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) )
						m_ToDrain.Add( m );
				}

				foreach ( Mobile m in m_ToDrain )
				{
					m_Owner.DoHarmful( m );

					m.FixedParticles( 0x374A, 10, 15, 5013, 0x455, 0, EffectLayer.Waist );
					m.PlaySound( 0x231 );

					m.SendMessage( "You feel the life drain out of you!" );

					m.Damage( 10, m_Owner );
				}

				m_ToDrain.Clear();
			}
		}
	}
}