/***************************************************************************
 *                               Containers.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id$
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/

using System;
using Server.Network;

namespace Server.Items
{
	public class BankBox : Container
	{
		private Mobile m_Owner;
		private bool m_Open;

		public override int DefaultMaxWeight
		{
			get
			{
				return 0;
			}
		}
		//Kons 
		public override int DefaultMaxItems
		{
			get
			{
				return 150;
			}
		}
		//End

		public override bool IsVirtualItem
		{
			get { return true; }
		}

		public BankBox( Serial serial ) : base( serial )
		{
		}

		public Mobile Owner
		{
			get
			{
				return m_Owner;
			}
		}

		public bool Opened
		{
			get
			{
				return m_Open;
			}
		}

		public void Open()
		{
			m_Open = true;

			if ( m_Owner != null )
			{
				m_Owner.PrivateOverheadMessage( MessageType.Regular, 0x3B2, true, String.Format( "Bank container has {0} items, {1} stones", TotalItems, TotalWeight ), m_Owner.NetState );
				//m_Owner.Send( new EquipUpdate( this ) );
				m_Owner.Send( new FakeBank(this , m_Owner) );	
				DisplayTo( m_Owner );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (Mobile) m_Owner );
			writer.Write( (bool) m_Open );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Owner = reader.ReadMobile();
					m_Open = reader.ReadBool();

					if ( m_Owner == null )
						Delete();

					break;
				}
			}

			if ( this.ItemID == 0xE41 )
				this.ItemID = 0xE7C;
		}

		private static bool m_SendRemovePacket;

		public static bool SendDeleteOnClose{ get{ return m_SendRemovePacket; } set{ m_SendRemovePacket = value; } }

		public void Close()
		{
			m_Open = false;

			if ( m_Owner != null && m_SendRemovePacket )
				m_Owner.Send( this.RemovePacket );
		}

		/*public override void OnSingleClick( Mobile from )
		{
		}*/

		public override void OnDoubleClick( Mobile from )
		{
			if(from.AccessLevel >= AccessLevel.GameMaster)
				base.OnDoubleClick( from );
		}

		public override DeathMoveResult OnParentDeath( Mobile parent )
		{
			return DeathMoveResult.RemainEquiped;
		}

		public BankBox( Mobile owner ) : base( 0xE7C )
		{
			Layer = Layer.Bank;
			Movable = false;
			m_Owner = owner;
		}

		public override bool IsAccessibleTo(Mobile check)
		{
		 	if ( ( check == m_Owner && m_Open ) || check.AccessLevel >= AccessLevel.GameMaster )
		 		return base.IsAccessibleTo (check);
		 	else
		 		return false;
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
		 	if ( ( from == m_Owner && m_Open ) || from.AccessLevel >= AccessLevel.GameMaster )
		 		return base.OnDragDrop( from, dropped );
			else
		 		return false;
		}

		public override bool OnDragDropInto(Mobile from, Item item, Point3D p)
		{
		 	if ( ( from == m_Owner && m_Open ) || from.AccessLevel >= AccessLevel.GameMaster )
		 		return base.OnDragDropInto (from, item, p);
		 	else
		 		return false;
		}
	}
	public sealed class FakeBank : Packet
	{
		public FakeBank( Item item ,Mobile mob) : base( 0x1A )
		{
			this.EnsureCapacity( 20 );
			uint serial = (uint)item.Serial.Value;
			int itemID = item.ItemID;
			int x = mob.X;
			int y = mob.Y;
			serial &= 0x7FFFFFFF;
			m_Stream.Write( (uint) serial );
			m_Stream.Write( (short) (itemID & 0x7FFF) );
			x &= 0x7FFF;
			m_Stream.Write( (short) x );
			y &= 0x3FFF;
			m_Stream.Write( (short) y );
			m_Stream.Write( (sbyte) (mob.Z - 150 ) );
		}
	}
}