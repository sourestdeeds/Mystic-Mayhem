using System; 
using Server.Misc; 
using Server.Network; 
using System.Collections; 
using Server.Items; 
using Server.Targeting; 

namespace Server.Mobiles 
	{ 
   	public class HordeEnforcer : BaseCreature 
   		{ 
      			[Constructable] 
      			public HordeEnforcer():base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
      			{ 
			Name = "a horde enforcer";
			Body = 795;
			Hue = 2118;
			BaseSoundID = 0xE5;

         		this.SetStr( 146, 170 );
			this.SetDex( 51, 60 );
			this.SetInt( 80, 100 );

			this.SetHits( 700, 1000 );

			this.SetDamage( 17, 20 );

			this.SetDamageType( ResistanceType.Physical, 100 );

			this.SetResistance( ResistanceType.Physical, 45, 55 );
			this.SetResistance( ResistanceType.Cold, 45, 55 );
			this.SetResistance( ResistanceType.Poison, 45, 55 );
			this.SetResistance( ResistanceType.Fire, 45, 55 );
			this.SetResistance( ResistanceType.Energy, 45, 55 );

			this.SetSkill( SkillName.MagicResist, 65.1, 70.0 );
			this.SetSkill( SkillName.Tactics, 65.1, 80.0 );
			this.SetSkill( SkillName.Wrestling, 75.1, 90.0 ); 

			this.Fame = 5000;
			this.Karma = -5000;

         		this.VirtualArmor = 40;
			PackGold( 100, 150 ); 
      			} 

      			public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
			public override Poison HitPoison{ get{ return Poison.Greater; } }
			public override bool BardImmune{ get{ return true; } }
			public override bool Unprovokable{ get{ return true; } }
			public override bool Uncalmable{ get{ return true; } } 


      			public HordeEnforcer( Serial serial ) : base( serial ) 
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
