using System;
using Server;

namespace Server.Items
{
    public class BloodyWrist : SilverBracelet
    {
        public override int ArtifactRarity{ get{ return 15; } }
        
        [Constructable]
        public BloodyWrist()
        {
            Name = "Bloody Wrist";
            Hue = 1324;
            
            Attributes.BonusStr = 10;
            Attributes.BonusInt = 10;
            Attributes.BonusDex = 10;
            Attributes.BonusHits = 5;
            Attributes.BonusStam = 5;
            Attributes.BonusMana = 5;
            Attributes.RegenMana = 2;
            Attributes.AttackChance = 5;
            Attributes.DefendChance = 5;
            Attributes.LowerManaCost = 10;
            
        }

        public BloodyWrist(Serial serial) : base( serial )
        {
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    } 
}
