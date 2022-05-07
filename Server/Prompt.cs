/***************************************************************************
 *                                 Prompt.cs
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
using Server.Gumps;

namespace Server.Prompts
{
	#region KR
	public class PromptGump : Gump
	{
		public PromptGump( Prompt prompt ) : base( 0, 0 )
		{
			TypeID=686;
			Serial = prompt.Serial;
			AddBackground( 50, 50, 540, 350, 0xA28 );

			AddPage( 0 );

			AddHtmlLocalized( 264, 80, 200, 24, 1062524, false, false ); // Enter Description
			AddHtmlLocalized( 120, 108, 420, 48, 1062638, false, false ); // Please enter a brief description (up to 200 characters) of your problem:
			AddBackground( 100, 148, 440, 200, 0xDAC );
			AddTextEntry( 120, 168, 400, 200, 0x0, 21, "TEXTENTRY" );
			AddButton( 175, 355, 0x81A, 0x81B, 1, GumpButtonType.Reply, 0 );
			AddButton( 405, 355, 0x819, 0x818, 0, GumpButtonType.Reply, 0 );


			//from recal rune
			AddKRLabel("1079653906");
			AddKRLabel("845373");
			AddKRLabel("21");
			AddKRLabel( prompt.Message.ToString() );
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Console.WriteLine("Hvanato");
		}
	}
	#endregion
	public abstract class Prompt
	{
		private int m_Serial;
		private static int m_Serials;
		#region KR
		private int m_Message;

		public int Message
		{
			get
			{
				return m_Message;
			}
			set
			{
				m_Message = value;
			}
		}
		#endregion

		public int Serial
		{
			get
			{
				return m_Serial;
			}
			#region KR
			set
			{
				m_Serial = value;
			}
			#endregion
		}
		public Prompt() : this(0)
		{
		}

		public Prompt(int message)
		{
			#region KR
			m_Message = message;
			#endregion
			do
			{
				m_Serial = ++m_Serials;
			} while ( m_Serial == 0 );
		}

		public virtual void OnCancel( Mobile from )
		{
        }

		public virtual void OnResponse( Mobile from, string text )
		{
		}
	}
}