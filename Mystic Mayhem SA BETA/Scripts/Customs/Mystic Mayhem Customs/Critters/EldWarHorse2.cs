using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a corpse of an Over-worked Horse" )]
	public class ElderWarHorse2 : BaseMount
	{
	[Constructable]
		public ElderWarHorse2() : this( "an Elder War Horse" )
		{
		}

		[Constructable]
		public ElderWarHorse2( string name ) : base( name, 0x77, 0x3EB1, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) // AI_ melee or mage.
		{
			BaseSoundID = 362;

		    SetStr( 250, 600 );
			SetDex( 200, 450 );
			SetInt( 5, 20 );

			SetHits( 450, 800 );

			SetDamage( 10, 30 );

			switch ( Utility.Random( 5 ) )
			{
				case 0:
				{	
			SetDamageType( ResistanceType.Physical, 100 );
			break;
				}
				case 1:
				{
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );
			break;
				}
				case 2:
				{
			SetDamageType( ResistanceType.Physical, 50);
			SetDamageType( ResistanceType.Fire, 50 );
			break;
				}
				case 3:
				{
			SetDamageType( ResistanceType.Physical, 50);
			SetDamageType( ResistanceType.Energy, 50 );
			break;
				}
				case 4:
				{
			SetDamageType( ResistanceType.Physical, 50);
			SetDamageType( ResistanceType.Poison, 50 );
			break;
				}
			}
			
			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 0, 50 );
			SetResistance( ResistanceType.Cold, 0, 50 );
			SetResistance( ResistanceType.Poison, 0, 50 );
			SetResistance( ResistanceType.Energy, 0, 50 );

			//SetSkill( SkillName.EvalInt, 0, 120 );
			//SetSkill( SkillName.Magery, 0, 120 );
			SetSkill( SkillName.MagicResist, 50, 120 );
			SetSkill( SkillName.Tactics, 50, 120 );
			SetSkill( SkillName.Wrestling, 50, 120 );
			SetSkill( SkillName.Anatomy, 50, 120 );
			SetSkill( SkillName.Poisoning, 50, 120 );
			//SetSkill( SkillName.Meditation, 0, 120 );
			SetSkill( SkillName.Parry, 50, 120 );

			Fame = 50000;
			Karma = 50000;

			VirtualArmor = 70;

			Tamable = true; 
         		ControlSlots = 3; 
         		MinTameSkill = 115;

			switch ( Utility.Random( 4 ) )
			{
				case 0:
				{
					BodyValue = 118;
					ItemID = 16050;
					break;
				}
				case 1:
				{
					BodyValue = 119;
					ItemID = 16049;
					break;
				}
				case 2:
				{
					BodyValue = 120;
					ItemID = 16047;
					break;
				}
				case 3:
				{
					BodyValue = 121;
					ItemID = 16048;
					break;
				}
			}

			PackGem();
			PackGold( 300, 1500 );
			PackMagicItems( 1, 5 );
			PackSlayer();
		}	
		
                                    
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool HasBreath{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 5; } }
		public override int Hides{ get{ return 10; } } 
      	public override HideType HideType{ get{ return HideType.Barbed; } }

		public ElderWarHorse2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
