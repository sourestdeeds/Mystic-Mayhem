
using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Spells;
using Server.Misc;
using Server.Engines.CannedEvil;
using Server.Targeting;


namespace Server.Mobiles
{
	public class Hvergel : AuraCreature
	{
        private DateTime m_NextAbility = DateTime.Now + TimeSpan.FromSeconds(5);
        private DateTime m_NextWall = DateTime.Now + TimeSpan.FromSeconds(20);

        [Constructable]
		public Hvergel() : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.1, 0.4 )
		{
			Name = "Hvergelmirunnr";
			Title = "the Stagnant";
			Body = 780;
			BaseSoundID = 357;
            Hue = 1196;

			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 15;
			MaxAuraDamage = 25;
			AuraRange = 5;

			SetStr( 1232, 1400 );
			SetDex( 150, 175 );
			SetInt( 200, 250 );

			SetHits( 100000 );

			SetDamage( 27, 31 );

			SetSkill( SkillName.EvalInt, 140.1, 150.0 );
			SetSkill( SkillName.Magery, 110.1, 120.0 );
			SetSkill( SkillName.MagicResist, 175.1, 185.0 );
			SetSkill( SkillName.Tactics, 180.1, 190.0 );
			SetSkill( SkillName.Wrestling, 100.1, 120.0 );

            SetDamageType(ResistanceType.Physical, 0);
            SetDamageType(ResistanceType.Cold, 100);

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			Fame = 88000;
			Karma = -88000;

			VirtualArmor = 60;
		}

        public override void OnThink()
        {
            if (Alive && !Blessed && Map != Map.Internal && Map != null && Combatant != null)
            {
                if (m_NextAbility <= DateTime.Now /*&& Hits > HitsMax * 0.03 */)
                {
                    switch (Utility.Random(3))
                    {
                        case 1: DoBlizzard();
                            break;
                        case 2: DoSummon();
                            break;
                        case 0: DoIceNova();
                            break;
                    }
                    m_NextAbility = DateTime.Now + TimeSpan.FromSeconds(Utility.Random(11) + 12);
                }
                if (m_NextWall <= DateTime.Now)
                {
                    if (Utility.RandomBool())
                    {
                        DoCircleOfIce();
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(40);
                    }
                    else
                        m_NextWall = DateTime.Now + TimeSpan.FromSeconds(5);
                }
            }
        }

        #region CircleOfIce

        public void DoCircleOfIce()
        {
            for (int x = -8; x <= 8; ++x)
            {
                for (int y = -8; y <= 8; ++y)
                {
                    double dist = Math.Sqrt(x * x + y * y);

                    if (dist > 7 && dist < 9)
                    {
                        Point3D loc = new Point3D(X + x, Y + y, Z);
                        if (Map.CanFit(loc, 0, true))
                        {
                            Item item = new InternalItem();
                            item.MoveToWorld(loc, Map);
                            Effects.SendLocationParticles(EffectItem.Create(loc, Map, EffectItem.DefaultDuration), 0x376A, 9, 10, 5029);
                        }
                    }
                }
            }
        }

        [DispellableField]
        private class InternalItem : Item
        {            
            public override bool BlocksFit { get { return true; } }
            private Timer m_Timer;

            public InternalItem()
                : base(0x08E2)
            {
                Movable = false;
                ItemID = Utility.RandomList(2274, 2275, 2272, 2273, 2279, 2280);
                Name = "A Stagnant Rock";
                Hue = 1196;
                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(30.0));
                m_Timer.Start();
            }

            public InternalItem(Serial serial)
                : base(serial)
            {
            }
            
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)1); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                switch (version)
                {
                    case 1:
                        {
                            break;
                        }
                    case 0:
                        {
                            break;
                        }
                }
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }
        }

        private class InternalTimer : Timer
        {
            private InternalItem m_Item;

            public InternalTimer(InternalItem item, TimeSpan duration)
                : base(duration)
            {
                m_Item = item;
            }

            protected override void OnTick()
            {
                m_Item.Delete();
            }
        }
        #endregion

        #region IceNova

        public void DoIceNova()
        {
            AbilityCollection.AreaEffect(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1), 0.1, Map, Location, 14000, 13, 569, 4, 12, 3, false, true, false);
            Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(IceNova_Callback), new object[]{this, Location});
        }

        public static void IceNova_Callback(object state)
        {
            object[] states = ((object[])state);
            Hvergel ag = states[0] as Hvergel;
            Point3D loc = ((Point3D)states[1]);
            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            Effects.PlaySound(ag.Location, ag.Map, 534);

            IPooledEnumerable eable = ag.Map.GetMobilesInRange(loc, 12);

            if (eable == null)
                return;

            foreach (Mobile m in eable)
            {
                if (m is BaseCreature)
                {
                    BaseCreature b = m as BaseCreature;

                    if (b == null)
                        return;

                    if (b.Alive && !b.Blessed && !b.IsDeadBondedPet && b.CanBeHarmful(ag) && ((BaseCreature)ag).IsEnemy(b) && b.Map != null && b.Map != Map.Internal && b != null)
                    {
                        AOS.Damage(b, ag, Utility.RandomMinMax(100, 150), 0, 0, 100, 0, 0);
                        ag.DoFreeze(b, 20);
                    }
                }
                else if (m is PlayerMobile)
                {
                    PlayerMobile p = m as PlayerMobile;

                    if (p == null)
                        return;

                    if (p.Alive && !p.Blessed && p.AccessLevel == AccessLevel.Player && p.Map != null && p.Map != Map.Internal && p != null)
                    {
                        AOS.Damage(p, ag, Utility.RandomMinMax(50, 100), 0, 0, 100, 0, 0);
                        ag.DoFreeze(p, 3);
                    }
                }
            }
            eable.Free();
        }

        #endregion

        #region Blizzard

        public void DoBlizzard()
        {
            for (int i = 0; i < 30; ++i)
            {
                int x = X + Utility.Random(25) - 12;
                int y = Y + Utility.Random(25) - 12;
                int z = Map.GetAverageZ(x, y);

                Point3D loc = new Point3D(x, y, z);

                if (Map.CanFit(loc, 0, true))
                {
                    double delay = 5 * Utility.RandomDouble();
                    Timer.DelayCall(TimeSpan.FromSeconds(delay), new TimerStateCallback(BlizzardEffect_Callback), new object[]{this, loc});
                    Timer.DelayCall(TimeSpan.FromSeconds(delay + 2.5), new TimerStateCallback(BlizzardDamage_Callback), new object[] { this, loc });
                }
            }
        }

        public static void BlizzardEffect_Callback(object state)
        {
            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            IEntity to = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z), ag.Map);
            IEntity from = new Entity(Serial.Zero, new Point3D(loc.X, loc.Y, loc.Z + 50), ag.Map);
            Effects.SendMovingEffect(from, to, 14036, 2, 16, false, true, 569, 4);
            Effects.PlaySound(loc, ag.Map, 0x1E5);
        }

        public static void BlizzardDamage_Callback(object state)
        {
            //Point3D loc = (Point3D)state;

            object[] states = ((object[])state);
            Mobile ag = states[0] as Mobile;
            Point3D loc = ((Point3D)states[1]);

            if (ag == null || !ag.Alive || ag.Map == Map.Internal)
                return;

            IPooledEnumerable eable = ag.Map.GetMobilesInRange(loc, 0);
            foreach (Mobile m in eable)
            {
                if (m.Blessed || m == null || m.Map == Map.Internal || m.Map == null || !m.Alive || !ag.CanBeHarmful(m))                
                    return;

                else if (m is PlayerMobile)
                {
                    PlayerMobile p = m as PlayerMobile;

                    if (p.AccessLevel == AccessLevel.Player)
                        AOS.Damage(p, ag, 100, 0, 0, 0, 100, 0);
                    else
                        p.SendMessage("With your godly powers you avoid the damage");
                }

                else if (m is BaseCreature)
                {
                    BaseCreature b = m as BaseCreature;

                    if (b.IsEnemy(ag))
                        AOS.Damage(b, ag, 300, 0, 0, 0, 100, 0);
                }
            }
	        eable.Free();
        }
        #endregion

        #region Summon

        public void DoSummon()
        {
	        //Summon Effect
	        Effects.SendLocationEffect( new Point3D( X + 1, Y, Z + 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y, Z ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y, Z - 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z + 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z - 4 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 11 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 7 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z + 3 ), Map, 0x3728, 13 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z - 1 ), Map, 0x3728, 13 );
	        Effects.SendLocationEffect( new Point3D( X , Y + 1, Z ), Map, 0x374A, 15, 569, 0 );
	        Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3709, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x208);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3709, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z + 6),Map, 0x3709, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z + 6), Map, 0x3709, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x208);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x376A, 15, 569, 10); //0x47D );
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 492);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3709, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x208);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1),Map, 0x375A, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x213);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map, 0x373A, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map, 0x373A, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y, Z - 1), Map, 0x373A, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x213);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x36BD, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map, 0x36BD, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z), Map, 0x36BD, 15, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map, 0x36BD, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x307);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z + 4), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z - 4), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z + 4), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z - 4), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 11), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 7), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 3), Map, 0x3728, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3728, 13, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x228);
			Effects.SendLocationEffect(new Point3D(X, Y, Z), Map, 0x37C4, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x1E2);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z + 6), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y, Z + 6), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z + 8), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 1, Z + 6), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 11), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 8), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X + 1, Y + 1, Z + 10), Map, 0x36D4, 13, 569, 10);
			Effects.SendLocationEffect(new Point3D(X, Y, Z + 1), Map, 0x3709, 15, 569, 10);
			Effects.PlaySound(new Point3D(X, Y, Z), Map, 0x15E);
			PlaySound(520);
			PlaySound(525);
			Effects.SendLocationEffect(new Point3D(X + 5, Y, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.SendLocationEffect(new Point3D(X - 5, Y, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.SendLocationEffect(new Point3D(X, Y + 5, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.SendLocationEffect(new Point3D(X, Y - 5, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.SendLocationEffect(new Point3D(X + 5, Y - 5, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.SendLocationEffect(new Point3D(X - 5, Y + 5, Z), Map, 0x3709, 17, 0x55C, 10);
			Effects.PlaySound ( new Point3D( X, Y, Z ), Map, 0x1f7 );
			Effects.SendLocationEffect( new Point3D( X, Y, Z + 1), Map, 0x374A, 15, 569, 0 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y, Z ), Map, 0x374A, 15, 569, 0 );
			Effects.SendLocationEffect( new Point3D( X + 1, Y + 1, Z ), Map, 0x374A, 15, 569, 0 );
			Effects.SendLocationEffect( new Point3D( X, Y + 1, Z ), Map, 0x374A, 15, 569, 0 );
			Effects.PlaySound ( new Point3D( X, Y, Z ), Map, 0x1E0 );
			Say( "Let their blood fester once more!" );
            
			if (this.Map != null)
            {
                Map map = this.Map;
                int amount = Utility.RandomMinMax(2, 8);

                for (int l = 0; l < amount; ++l)
                {
                    for (int k = 0; k < 1; ++k)
                    {
                        bool validLocation = false;
                        Point3D loc = this.Location;
                        for (int j = 0; !validLocation && j < 10; ++j)
                        {
                            int x = X + Utility.Random(11) - 5;
                            int y = Y + Utility.Random(11) - 5;
                            int z = map.GetAverageZ(x, y);

                            if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                                loc = new Point3D(x, y, Z);
                            else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                                loc = new Point3D(x, y, z);
                        }
                        switch (Utility.Random(2))
                        {
                            case 0: BulbousPutrifaction bulbous = new BulbousPutrifaction();
                                bulbous.MoveToWorld(loc, map);
                                break;
                            case 1: Fester corpses = new Fester();
                                corpses.MoveToWorld(loc, map);
                                break;
                		}   
            		}
                }
            }
        }
        #endregion

        #region Teleport

        public void DoTeleport(Mobile target)
        {
            if ((GetDistanceToSqrt(target) <= 1 || GetDistanceToSqrt(target) >= 12) || !CanSee(target))
                return;

            bool validLocation = false;
            Point3D loc = target.Location;
            for (int j = 0; !validLocation && j < 10; ++j)
            {
                int x = target.X + Utility.Random(3) - 1;
                int y = target.Y + Utility.Random(3) - 1;
                int z = Map.GetAverageZ(x, y);

                if (validLocation = Map.CanFit(x, y, target.Z, 16, false, false))
                    loc = new Point3D(x, y, Z);
                else if (validLocation = Map.CanFit(x, y, z, 16, false, false))
                    loc = new Point3D(x, y, z);
            }
            Effects.SendLocationParticles(EffectItem.Create(Location, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
            Effects.SendLocationParticles(EffectItem.Create(loc, Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);
            MoveToWorld(loc, Map);

            Combatant = target;
        }
        #endregion

        public override void OnDamagedBySpell(Mobile from)
        {
            if (Utility.Random(5) == 1 && from != null && InRange(from, 12) && from.Map == Map)
            {
                DoTeleport(from);
            }
        }

        #region Freeze
        public override void OnGaveMeleeAttack(Mobile defender)
        {
            if (!AFrozen(defender) && Utility.RandomBool())
                DoFreeze(defender, 5);
            base.OnGaveMeleeAttack(defender);
        }

        public void DoFreeze(Mobile from, int duration)
        {
            ExpireTimer timer = (ExpireTimer)m_Table[from];

            if (timer != null )
            {
                timer.DoExpire();
                from.SendLocalizedMessage(1072274); // The freezing wind continues to blow!
            }
            else
                from.SendLocalizedMessage(1072072); // An icy wind surrounds you, freezing your lungs as you breathe!

            timer = new ExpireTimer(from, this, duration);
            timer.Start();
            m_Table[from] = timer;
        }

        public bool AFrozen(Mobile m)
        {
            ExpireTimer timer = (ExpireTimer)m_Table[m];
            return timer != null;
        }

        private static Hashtable m_Table = new Hashtable();

        private class FrozenItem : Item 
        {
            public override bool BlocksFit { get { return false; } }

            public FrozenItem()
                : base(Utility.RandomList(2279, 2281, 2276, 2272))
            {
                Hue = 1196;
                Name = "Remains";
            }

            public FrozenItem(Serial serial)
                : base(serial)
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)1); // version
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                switch (version)
                {
                    case 1:
                        {
                            break;
                        }
                }
            }
        }

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private Mobile m_From;
            private int m_Count;
            private int m_MaxCount;
            private FrozenItem m_Item;

            public ExpireTimer(Mobile m, Mobile from, int maxCount)
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
            {
                m_Mobile = m;
                m_From = from;
                m_MaxCount = maxCount;
                m_Mobile.CantWalk = true;
                m_Item = new FrozenItem();
                m_Item.MoveToWorld(m_Mobile.Location, m_Mobile.Map);
                Priority = TimerPriority.TwoFiftyMS;
            }

            public void DoExpire()
            {
                Stop();
                m_Mobile.CantWalk = false;
                m_Item.Delete();
                m_Table.Remove(m_Mobile);
            }

            public void DrainLife()
            {
                if (m_Mobile.Alive)
                    m_Mobile.Damage(Utility.RandomMinMax(5,11), m_From);
                else
                    DoExpire();
            }

            protected override void OnTick()
            {
                DrainLife();

                if (++m_Count >= m_MaxCount)
                {
                    DoExpire();
                    m_Mobile.SendLocalizedMessage(502136); // The icy wind dissipates.
                }
            }
        }
        #endregion

        public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8);
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
			c.DropItem( new Ruby( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Amber( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Amethyst( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Citrine( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Diamond( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Emerald( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Sapphire( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new StarSapphire( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Tourmaline( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new MagicJewel( Utility.RandomMinMax( 16, 250 ) ) );
			c.DropItem( new Platinum( Utility.RandomMinMax( 16, 5000 ) ) );
			c.DropItem( new CompassionStone() );
			c.DropItem( new HandGrenade() );
			
			switch ( Utility.Random( 110 )) 
			{ 
				case 0: 				c.DropItem( new PowerScroll( SkillName.Blacksmith, 120 ) ); break;  
				case 1: case 2: 			c.DropItem( new PowerScroll( SkillName.Blacksmith, 115 ) ); break; 
				case 3: case 4: case 5: 		c.DropItem( new PowerScroll( SkillName.Blacksmith, 110 ) ); break; 
				case 6: case 7: case 8: case 9: 	c.DropItem( new PowerScroll( SkillName.Blacksmith, 105 ) ); break; 
  
				case 10: 				c.DropItem( new PowerScroll( SkillName.Tailoring, 120 ) ); break;  
				case 11: case 12: 			c.DropItem( new PowerScroll( SkillName.Tailoring, 115 ) ); break; 
				case 13: case 14: case 15: 		c.DropItem( new PowerScroll( SkillName.Tailoring, 110 ) ); break; 
				case 16: case 17: case 18: case 19: 	c.DropItem( new PowerScroll( SkillName.Tailoring, 105 ) ); break;  

				case 20: 				c.DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 21: case 22: 			c.DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 23: case 24: case 25: 		c.DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 26: case 27: case 28: case 29: 	c.DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break; 
  
				case 30: 				c.DropItem( new PowerScroll( SkillName.Mining, 120 ) ); break;  
				case 31: case 32: 			c.DropItem( new PowerScroll( SkillName.Mining, 115 ) ); break; 
				case 33: case 34: case 35: 		c.DropItem( new PowerScroll( SkillName.Mining, 110 ) ); break; 
				case 36: case 37: case 38: case 39: 	c.DropItem( new PowerScroll( SkillName.Mining, 105 ) ); break;
 
				case 40: 				c.DropItem( new PowerScroll( SkillName.Carpentry, 120 ) ); break;  
				case 41: case 42: 			c.DropItem( new PowerScroll( SkillName.Carpentry, 115 ) ); break; 
				case 43: case 44: case 45: 		c.DropItem( new PowerScroll( SkillName.Carpentry, 110 ) ); break; 
				case 46: case 47: case 48: case 49: 	c.DropItem( new PowerScroll( SkillName.Carpentry, 105 ) ); break; 

				case 50: 				c.DropItem( new PowerScroll( SkillName.Alchemy, 120 ) ); break;  
				case 51: case 52: 			c.DropItem( new PowerScroll( SkillName.Alchemy, 115 ) ); break; 
				case 53: case 54: case 55: 		c.DropItem( new PowerScroll( SkillName.Alchemy, 110 ) ); break; 
				case 56: case 57: case 58: case 59: 	c.DropItem( new PowerScroll( SkillName.Alchemy, 105 ) ); break; 

				case 60: 				c.DropItem( new PowerScroll( SkillName.Fletching, 120 ) ); break;  
				case 61: case 62: 			c.DropItem( new PowerScroll( SkillName.Fletching, 115 ) ); break; 
				case 63: case 64: case 65: 		c.DropItem( new PowerScroll( SkillName.Fletching, 110 ) ); break; 
				case 66: case 67: case 68: case 69: 	c.DropItem( new PowerScroll( SkillName.Fletching, 105 ) ); break;

				case 70: 				c.DropItem( new PowerScroll( SkillName.Inscribe, 120 ) ); break;  
				case 71: case 72: 			c.DropItem( new PowerScroll( SkillName.Inscribe, 115 ) ); break; 
				case 73: case 74: case 75: 		c.DropItem( new PowerScroll( SkillName.Inscribe, 110 ) ); break; 
				case 76: case 77: case 78: case 79: 	c.DropItem( new PowerScroll( SkillName.Inscribe, 105 ) ); break;
  
				case 80: 				c.DropItem( new PowerScroll( SkillName.Cartography, 120 ) ); break;  
				case 81: case 82: 			c.DropItem( new PowerScroll( SkillName.Cartography, 115 ) ); break; 
				case 83: case 84: case 85: 		c.DropItem( new PowerScroll( SkillName.Cartography, 110 ) ); break; 
				case 86: case 87: case 88: case 89: 	c.DropItem( new PowerScroll( SkillName.Cartography, 105 ) ); break;

				case 90: 				c.DropItem( new PowerScroll( SkillName.Tinkering, 120 ) ); break;  
				case 91: case 92: 			c.DropItem( new PowerScroll( SkillName.Tinkering, 115 ) ); break; 
				case 93: case 94: case 95: 		c.DropItem( new PowerScroll( SkillName.Tinkering, 110 ) ); break; 
				case 96: case 97: case 98: case 99: 	c.DropItem( new PowerScroll( SkillName.Tinkering, 105 ) ); break;

				case 100: 				c.DropItem( new PowerScroll( SkillName.Cooking, 120 ) ); break;  
				case 101: case 102: 			c.DropItem( new PowerScroll( SkillName.Cooking, 115 ) ); break; 
				case 103: case 104: case 105: 		c.DropItem( new PowerScroll( SkillName.Cooking, 110 ) ); break; 
				case 106: case 107: case 108: case 109: c.DropItem( new PowerScroll( SkillName.Cooking, 105 ) ); break; 
      			}
  			}
      			private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 500, 1000 );
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
		
		public override bool OnBeforeDeath()
		{

				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -12; x <= 12; ++x )
					{
						for ( int y = -12; y <= 12; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 12 )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			
					
			return base.OnBeforeDeath();
		}
		
		

        public override bool AlwaysMurderer { get { return true; } }
        public override bool AutoDispel { get { return true; } }
        public override double AutoDispelChance { get { return 1.0; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Deadly; } }

        public Hvergel(Serial serial)
            : base(serial)
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