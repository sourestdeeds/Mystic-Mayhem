using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a rend corpse" )]	
	public class Pyre : BaseCreature
	{
		[Constructable]
		public Pyre() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.05, 0.2 )
		{
			Name = "a pyre";
			Body = 0x5;
			Hue = 0x489;

			SetStr( 605, 611 );
			SetDex( 391, 519 );
			SetInt( 669, 818 );

			SetHits( 1783, 1939 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 65 );
			SetResistance( ResistanceType.Fire, 72, 74 );
			SetResistance( ResistanceType.Poison, 36, 41 );
			SetResistance( ResistanceType.Energy, 50, 51 );

			SetSkill( SkillName.Wrestling, 121.9, 130.6 );
			SetSkill( SkillName.Tactics, 114.9, 117.4 );
			SetSkill( SkillName.MagicResist, 147.7, 153.0 );
			SetSkill( SkillName.Poisoning, 122.8, 124.0 );
			SetSkill( SkillName.Magery, 121.8, 127.8 );
			SetSkill( SkillName.EvalInt, 103.6, 117.0 );
		}
				
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			if ( Utility.RandomDouble() < 0.01 )
			{
				switch ( Utility.Random( 37 ) )
				{
					case 0: c.DropItem( new AssassinChest() ); break;
					case 1: c.DropItem( new AssassinArms() ); break;
					case 2: c.DropItem( new DeathChest() );	break;		
					case 3: c.DropItem( new MyrmidonArms() ); break;
					case 4: c.DropItem( new MyrmidonLegs() ); break;
					case 5: c.DropItem( new MyrmidonGorget() ); break;
					case 6: c.DropItem( new LeafweaveGloves() ); break;
					case 7: c.DropItem( new LeafweaveLegs() ); break;
					case 8: c.DropItem( new LeafweavePauldrons() ); break;
					case 9: c.DropItem( new PaladinGloves() ); break;
					case 10: c.DropItem( new PaladinGorget() ); break;
					case 11: c.DropItem( new PaladinArms() ); break;
					case 12: c.DropItem( new HunterArms() ); break;
					case 13: c.DropItem( new HunterGloves() ); break;
					case 14: c.DropItem( new HunterLegs() ); break;
					case 15: c.DropItem( new HunterChest() ); break;
					case 16: c.DropItem( new GreymistArms() ); break;
					case 17: c.DropItem( new GreymistGloves() ); break;
					case 18: c.DropItem( new GreymistChest() ); break;
					case 19: c.DropItem( new GreymistLegs() ); break;
					case 20: c.DropItem( new AssassinGloves() ); break;
					case 21: c.DropItem( new AssassinLegs() ); break;
					case 22: c.DropItem( new Evocaricus() ); break;
					case 23: c.DropItem( new MalekisHonor() ); break;
					case 24: c.DropItem( new LeafweaveChest() ); break;
					case 25: c.DropItem( new Feathernock() ); break;
					case 26: c.DropItem( new Swiftflight() ); break;
					case 27: c.DropItem( new MyrmidonChest() ); break;
					case 28: c.DropItem( new MyrmidonCloseHelm() ); break;
					case 29: c.DropItem( new MyrmidonGloves() ); break;
					case 30: c.DropItem( new DeathGloves() );	break;
					case 31: c.DropItem( new DeathArms() );	break;	
					case 32: c.DropItem( new DeathLegs() );	break;	
					case 33: c.DropItem( new DeathBoneHelm() );	break;
					case 34: c.DropItem( new PaladinChest() ); break;
					case 35: c.DropItem( new PaladinHelm() ); break;
					case 36: c.DropItem( new PaladinLegs() ); break;
				}
			}
			
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 2 ) )
			{
				case 0: return WeaponAbility.ParalyzingBlow;
				case 1: return WeaponAbility.BleedAttack;
			}
		
			return null;
		}
		
		bool tick = false;
		
		public override void OnThink()
		{
			tick = !tick;
		
			if ( tick )
				return;
		
			List<Mobile> targets = new List<Mobile>();

			if ( Map != null )
				foreach ( Mobile m in GetMobilesInRange( 2 ) )
					if ( this != m && SpellHelper.ValidIndirectTarget( this, m ) && CanBeHarmful( m, false ) && ( !Core.AOS || InLOS( m ) ) )
					{
						if ( m is BaseCreature && ((BaseCreature) m).Controlled )
							targets.Add( m );
						else if ( m.Player )
							targets.Add( m );
					}
					
			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile m = targets[ i ];
				
				AOS.Damage( m, this, 5, 0, 100, 0, 0, 0 );
				
				if ( m.Player )
					m.SendLocalizedMessage( 1008112, Name ); // : The intense heat is damaging you!
			}			
		}
		
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Feathers{ get{ return 36; } }
		public override bool GivesMinorArtifact{ get{ return true; } }		
		
		public override int GetIdleSound() { return 0x2EF; }
		public override int GetAttackSound() { return 0x2EE; }
		public override int GetAngerSound() { return 0x2EF; }
		public override int GetHurtSound() { return 0x2F1; }
		public override int GetDeathSound()	{ return 0x2F2; }

		public Pyre( Serial serial ) : base( serial )
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
	}
}