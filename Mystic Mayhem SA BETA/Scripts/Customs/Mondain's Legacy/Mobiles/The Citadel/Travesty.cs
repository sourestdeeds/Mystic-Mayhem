using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "The Tragic Remains Of Travesty" )]
	public class Travesty : BasePeerless
	{
		public override bool AlwaysMurderer{ get{ return true; } }
		[Constructable]
		public Travesty() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a travesty";
			Body = 0x108;

			SetStr( 909, 949 );
			SetDex( 901, 948 );
			SetInt( 903, 947 );

			SetHits( 50000 );

			SetDamage( 25, 30 );
			
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 52, 67 );
			SetResistance( ResistanceType.Fire, 51, 68 );
			SetResistance( ResistanceType.Cold, 51, 69 );
			SetResistance( ResistanceType.Poison, 51, 70 );
			SetResistance( ResistanceType.Energy, 50, 68 );

			SetSkill( SkillName.Wrestling, 100.1, 119.7 );
			SetSkill( SkillName.Tactics, 102.3, 118.5 );
			SetSkill( SkillName.MagicResist, 101.2, 119.6 );
			SetSkill( SkillName.Anatomy, 100.1, 117.5 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 50;
			//PackTalismans( 5 );
			PackResources( 8 );
		}

		public Travesty( Serial serial ) : base( serial )
		{
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new EyeOfTheTravesty() );
			c.DropItem( new OrdersFromMinax() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: c.DropItem( new TravestysSushiPreparations() ); break;
				case 1: c.DropItem( new TravestysFineTeakwoodTray() ); break;
				case 2: c.DropItem( new TravestysCollectionOfShells() ); break;
			}

			if ( Utility.RandomDouble() < 0.6 )
				c.DropItem( new ParrotItem() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new TragicRemainsOfTravesty() );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new ImprisonedDog() );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new MarkOfTravesty() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.02 )
				c.DropItem( new TangleApron() );
				
			if ( Utility.RandomDouble() < 0.05 )
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
			
			if ( Utility.RandomDouble() < 0.5 )
			{
				switch ( Utility.Random( 16 ) )
				{
					case 0: c.DropItem( new ArcaneCircleScroll() ); break;
					case 1: c.DropItem( new GiftOfRenewalScroll() ); break;
					case 2: c.DropItem( new ImmolatingWeaponScroll() ); break;
					case 3: c.DropItem( new AttuneWeaponScroll() ); break;
					case 4: c.DropItem( new ThunderstormScroll() ); break;
					case 5: c.DropItem( new NatureFuryScroll() ); break;
					case 6: c.DropItem( new SummonFeyScroll() ); break;
					case 7: c.DropItem( new SummonFiendScroll() ); break;
					case 8: c.DropItem( new ReaperFormScroll() ); break;
					case 9: c.DropItem( new WildfireScroll() ); break;
					case 10: c.DropItem( new EssenceOfWindScroll() ); break;
					case 11: c.DropItem( new DryadAllureScroll() ); break;
					case 12: c.DropItem( new EtherealVoyageScroll() ); break;
					case 13: c.DropItem( new WordOfDeathScroll() ); break;
					case 14: c.DropItem( new GiftOfLifeScroll() ); break;
					case 15: c.DropItem( new ArcaneEmpowermentScroll() ); break;
				}
			}	
			
			if ( Utility.RandomDouble() < 0.03 )
			{
				switch ( Utility.Random( 21 ) )
				{
					case 0: c.DropItem( new BloodwoodSpirit() ); break;
					case 1: c.DropItem( new Boomstick() ); break;
					case 2: c.DropItem( new BrightsightLenses() ); break;
					case 3: c.DropItem( new HelmOfSwiftnessNonElf() ); break;
					case 4: c.DropItem( new QuiverOfElements() ); break;
					case 5: c.DropItem( new QuiverOfRage() ); break;
					case 6: c.DropItem( new TotemOfVoid() ); break;
					case 7: c.DropItem( new WildfireBow() ); break;
					case 8: c.DropItem( new Windsong() ); break;
					case 9: c.DropItem( new AegisOfGraceNonElf() ); break;
					case 10: c.DropItem( new BladeDance() ); break;
					case 11: c.DropItem( new Bonesmasher() ); break;
					case 12: c.DropItem( new FeyLeggingsNonElf() ); break;
					case 13: c.DropItem( new FleshRipper() ); break;
					case 14: c.DropItem( new PadsOfTheCuSidhe() ); break;
					case 15: c.DropItem( new RaedsGlory() ); break;
					case 16: c.DropItem( new RighteousAnger() ); break;
					case 17: c.DropItem( new RobeOfTheEclipse() ); break;
					case 18: c.DropItem( new RobeOfTheEquinox() ); break;
					case 19: c.DropItem( new SoulSeeker() ); break;
					case 20: c.DropItem( new TalonBite() ); break;
				}
			}	
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool CanAnimateDead{ get{ return true; } }
		public override BaseCreature Animates{ get{ return new DragonsFlameMage(); } }
		public override bool GivesMinorArtifact{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
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

		private bool m_SpawnedHelpers;
		private Timer m_Timer;
		private string m_Name;
		private int m_Hue;

		public override void OnThink()
		{
			base.OnThink();

			if ( Combatant == null )
				return;

			if ( Combatant.Player && Name != Combatant.Name )
				Morph();
		}

		public virtual void Morph()
		{
			m_Name = Name;
			m_Hue = Hue;

			Body = Combatant.Body; 
			Hue = Combatant.Hue; 
			Name = Combatant.Name;
			Female = Combatant.Female;
			Title = Combatant.Title;

			foreach ( Item item in Combatant.Items )
			{
				if ( item.Layer != Layer.Backpack && item.Layer != Layer.Mount && item.Layer != Layer.Bank )
					if ( FindItemOnLayer( item.Layer ) == null )
						AddItem( new ClonedItem( item ) );
			}

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = Timer.DelayCall( TimeSpan.FromSeconds( 5 ), TimeSpan.FromSeconds( 5 ), new TimerCallback( EndMorph ) );
		}

		public void DeleteItems()
		{
			for ( int i = Items.Count - 1; i >= 0; i -- )
				if ( Items[ i ] is ClonedItem )
					Items[ i ].Delete();

			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; i -- )
					if ( Backpack.Items[ i ] is ClonedItem )
						Backpack.Items[ i ].Delete();
			}
		}

		public virtual void EndMorph()
		{
			if ( Combatant != null && Name == Combatant.Name )
				return;

			DeleteItems();

			if ( m_Timer != null )
			{
				m_Timer.Stop();		
				m_Timer = null;	
			}

			if ( Combatant != null )
			{
				Morph();
				return;
			}

			Body = 264;
			Title = null;
			Name = m_Name;
			Hue = m_Hue;

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}

		public override bool OnBeforeDeath()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			return base.OnBeforeDeath();
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		#region Spawn Helpers
		public override bool CanSpawnHelpers{ get{ return true; } }
		public override int MaxHelpersWaves{ get{ return 1; } }

		public override bool CanSpawnWave()
		{
			if ( Hits > 1100 )
				m_SpawnedHelpers = false;
			

			return !m_SpawnedHelpers && Hits < 1000;
		}

		public override void SpawnHelpers()
		{
			m_SpawnedHelpers = true;
			Say( 1075121 ); // You shall pay for your insolence!  My delaying tactic has worked and my brethren are alerted to your presence.  Trifle with the Black Order at your own peril.

			for ( int i = 0; i < 10; i++ )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
				switch ( Utility.Random( 3 ) )
				{
					case 0: SpawnHelper( new DragonsFlameMage(), 25 ); break;
					case 1: SpawnHelper( new SerpentsFangAssassin(), 25 ); break;
					case 2: SpawnHelper( new TigersClawThief(), 25 ); break;
				}
			}
		}
		#endregion

		private class ClonedItem : Item
		{	
			public ClonedItem( Item oItem ) : base( oItem.ItemID )
			{
				Name = oItem.Name;
				Weight = oItem.Weight;
				Hue = oItem.Hue;
				Layer = oItem.Layer;
			}

			public override DeathMoveResult OnParentDeath( Mobile parent )
			{
				return DeathMoveResult.RemainEquiped;
			}

			public override DeathMoveResult OnInventoryDeath( Mobile parent )
			{
				Delete();
				return base.OnInventoryDeath( parent );
			}

			public ClonedItem( Serial serial ) : base( serial )
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
}