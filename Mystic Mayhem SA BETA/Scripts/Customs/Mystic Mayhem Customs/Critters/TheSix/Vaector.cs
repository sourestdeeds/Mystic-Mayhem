/*
	Jon		24/05/10 	- Ln 14: Custom Effects Delays Added
						- Ln 177: Custom Effects Triggers Added
*/

using System; 
using System.Collections; 
using Server; 
using Server.Items; 
using Server.Engines.CannedEvil;
using System.Collections.Generic;
namespace Server.Mobiles 
{ 
   [CorpseName( "the remains of Vaector" )] 
   public class Vaector : BaseCreature 
   {
//Jon
		private DateTime m_Delay;
		private DateTime m_DelayOne;
		private DateTime m_DelayTwo;
		private DateTime m_DelayThree;
		private DateTime m_DelayFour;
		
      public override bool AlwaysMurderer { get { return true; } }
      public override bool ShowFameTitle{ get{ return false; } }
	public override bool DeleteCorpseOnDeath{ get{ return true; } }

     public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
     public DateTime m_NextTalk;

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( DateTime.Now >= m_NextTalk && InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) ) // check if its time to talk + Player in range.
			{
				m_NextTalk = DateTime.Now + TalkDelay;
				switch ( Utility.Random( 5 ))		   
				{
					case 0: Say("Pathetic Mortals!"); break;
					case 1: Say("You cannot comprehend my power!"); break;
					case 2: Say("Sweet Death, embrace mine enemies!"); break;
					case 3: Say("You shall all face my wrath!"); break;
					case 4: Say("Behold true terror!"); break;
				};
		
			}
		}

      [Constructable]
      public Vaector()
          : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
      {
          Name = "Vaector";
	Body = 0x190;

	SpeechHue = 44;

	Hue = 1175;

          BaseSoundID = 362;

          	SetStr( 3000 );
          	SetDex( 300 );
          	SetInt( 1100 );

          	SetHits( 35000 );

          	SetDamage( 20, 25 );

          	SetDamageType( ResistanceType.Physical, 50 );
          	SetDamageType( ResistanceType.Fire, 50 );

          	SetResistance( ResistanceType.Physical, 80, 85 );
          	SetResistance( ResistanceType.Fire, 100 );
          	SetResistance( ResistanceType.Cold, 60, 70 );
          	SetResistance( ResistanceType.Poison, 80, 85 );
          	SetResistance( ResistanceType.Energy, 80, 85 );

          	SetSkill( SkillName.EvalInt, 125.3 );
          	SetSkill( SkillName.Magery, 125.0 );
          	SetSkill( SkillName.MagicResist, 125.0 );
          	SetSkill( SkillName.Tactics, 130.0 );
          	SetSkill( SkillName.Wrestling, 130.0 );
          	SetSkill( SkillName.Meditation, 130.0 );
          	SetSkill( SkillName.Anatomy, 125.0 );
          	SetSkill(SkillName.DetectHidden, 120.0 );
          
          Fame = 20000;
          Karma = -20000;

          VirtualArmor = 70;

			AddItem( new ThighBoots( 1175 ) ); //black

			RuneBlade blade = new RuneBlade();
			blade.Movable = false;
			AddItem( blade );

			DragonHelm helm = new DragonHelm();
			helm.Resource = CraftResource.BlackScales;
			helm.Movable = false;
			AddItem( helm );

			DragonChest chest = new DragonChest();
			chest.Resource = CraftResource.BlackScales;
			chest.Movable = false;
			AddItem( chest );

			DragonArms arms = new DragonArms();
			arms.Resource = CraftResource.BlackScales;
			arms.Movable = false;
			AddItem( arms );

			DragonGloves gloves = new DragonGloves();
			gloves.Resource = CraftResource.BlackScales;
			gloves.Movable = false;
			AddItem( gloves );

			DragonLegs legs = new DragonLegs();
			legs.Resource = CraftResource.BlackScales;
			legs.Movable = false;
			AddItem( legs );

        } 
//Jon
public override void OnActionCombat()
		{
			if ( Combatant == null || Frozen || Paralyzed || Map == null || Map == Map.Internal )
				base.OnActionCombat();
			else if ( DateTime.Now > m_Delay )
			{
				double chance = Utility.RandomDouble();

				if ( DateTime.Now > m_DelayOne && chance < 0.5 ) // 50%
				{
					// It looked like it delt 67 damage, presuming 70% fire res thats about 223 damage delt before resistance.
					Ability.MultiFireball( this, Combatant, 223 );
					m_DelayOne = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				else if ( DateTime.Now > m_DelayTwo && chance < 0.8 ) // 30%
				{
					Ability.FlameWave( this );
					m_DelayTwo = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 25 ) );
				}
				else if ( DateTime.Now > m_DelayThree && chance < 0.9 ) // 10%
				{
					Ability.FlameCross( this );
					m_DelayThree = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}
				else if ( DateTime.Now > m_DelayFour && chance < 0.9 ) // 10%
				{
					Ability.SoulDrain( this );
					m_DelayFour = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 60, 120 ) );
				}
				
				m_Delay = DateTime.Now + TimeSpan.FromSeconds( 5 );
			}

			base.OnActionCombat();
		}

        public override int GetIdleSound()
        {
            return 0x2D3;
        }
        public override int GetHurtSound()
        {
            return 0x2D1;
        }
        		public override bool Unprovokable { get { return true; } }
        		public override bool BardImmune { get { return true; } }
        		public override bool GivesMinorArtifact{ get{ return false; } }
        		public override bool ReacquireOnMovement{ get{ return true; } }
        		public override bool Uncalmable{ get{ return true; } }
        		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
        		public override int TreasureMapLevel{ get{ return 5; } }


        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (this.Map != null && attacker != this && 0.1 > Utility.RandomDouble())
            {
                if (attacker is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)attacker;
                    if (pet.ControlMaster != null && (attacker is Dragon || attacker is GreaterDragon || attacker is SkeletalDragon || attacker is WhiteWyrm || attacker is Drake))
                    {
                        Combatant = null;
                        pet.Combatant = null;
                        Combatant = null;
                        pet.ControlMaster = null;
                        pet.Controlled = false;
                        attacker.Emote(String.Format("* {0} decided to go wild *", attacker.Name));
                    }

                    if (pet.ControlMaster != null && 0.1 > Utility.RandomDouble())
                    {
                        Combatant = null;
                        pet.Combatant = pet.ControlMaster;
                        Combatant = null;
                        attacker.Emote(String.Format("* {0} is being angered *", attacker.Name));
                    }

                }
            }

            base.OnGotMeleeAttack(attacker); //TODO:  Add fire diamond attack
        }

		public override bool OnBeforeDeath()
		{
			MysteriousImage rm = new MysteriousImage();
			rm.Team = this.Team;
			rm.CantWalk = true;
			Effects.PlaySound(this, Map, GetDeathSound());
			Effects.SendLocationEffect( Location, Map, 0x3709, 30, 10, 0, 0 );
			rm.MoveToWorld( Location, Map );

			if ( !base.OnBeforeDeath() )
				return false;

			MagicalLockbox magicallockbox = new MagicalLockbox();
			magicallockbox.MoveToWorld( Location, Map );

			Effects.SendLocationEffect( Location, Map, 0x376A, 10, 1 );
			return true;
		}
    
	public Vaector( Serial serial ) : base( serial )
	{
	}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
      
   }

}

      
      