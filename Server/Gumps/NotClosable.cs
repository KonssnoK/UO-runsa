/***************************************************************************
 *                                GumpPage.cs
 *                            -------------------
 *   begin                : May 1, 2002
 *   copyright            : (C) The RunUO Software Team
 *   email                : info@runuo.com
 *
 *   $Id: GumpPage.cs 4 2006-06-15 04:28:39Z mark $
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
using System.Text;

namespace Server.Gumps
{
	public class GumpNotClosable : GumpEntry
	{
		public static byte[] StringToBuffer( string str )
		{
			return Encoding.ASCII.GetBytes( str );
		}
		private static byte[] m_NoClose = StringToBuffer( "{ noclose }" );

		public GumpNotClosable()
		{
		}

		public override string Compile()
		{
			return   "{ noclose }" ;
		}

		public override void AppendTo( IGumpWriter disp )
		{
			disp.AppendLayout( m_NoClose );
		}
	}
}