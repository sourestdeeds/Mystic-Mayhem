using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class CueBall : Item
    {



        [Constructable]
        public CueBall() : base(3878)
        {
            Weight = 1.0;
            Name = "CueBall";
		Hue = 1153;
        }

        public CueBall(Serial serial) : base( serial )
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


    public class PoolBall : Item
    {



        [Constructable]
        public PoolBall() : base(3878)
        {
            Weight = 1.0;
            Name = "ball";
		Hue = 133;
        }

        public PoolBall (Serial serial) : base( serial )
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




        public class SnookerBalls : Bag
        {
            [Constructable]
            public SnookerBalls() : this (50)
            {
                Name = "snooker balls";
		Hue = 373;
            }

            [Constructable]
            public SnookerBalls(int amount)
            {
                DropItem(new CueBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());
                DropItem(new PoolBall());

            }

            public SnookerBalls(Serial serial) : base( serial )
            {
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version 
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
            }
        }
    }


