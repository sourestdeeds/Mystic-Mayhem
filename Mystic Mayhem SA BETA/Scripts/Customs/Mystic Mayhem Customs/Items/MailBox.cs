using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x9A8, 0xE80 )]
	public class MailBox : BaseContainer, ITelekinesisable
	{
		private PlayerMobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public PlayerMobile Owner
		{
			get{ return m_Owner; }
			set { m_Owner = value; }
		}

		[Constructable]
		public MailBox( ) : base( 0x9A8 )
		{
			Weight = 2.0;

			Movable = false;
			Name = "mailbox";
			LiftOverride = true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			return base.OnDragDropInto( from, item, p );
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			return base.OnDragDrop( from, dropped );
		}

		public override void OnDoubleClick(Mobile from)
		{
		try{
			if (( m_Owner != null && from.Account == m_Owner.Account ) || from.AccessLevel >= AccessLevel.GameMaster )
				base.OnDoubleClick (from);
		   }
		   catch{}
		}

		public virtual void OnTelekinesis( Mobile from )
		{
			return;
		}

		public MailBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( m_Owner );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_Owner = reader.ReadMobile() as PlayerMobile;
		}
	}
}