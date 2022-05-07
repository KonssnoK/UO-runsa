using Server.Network;
using System;

namespace Server
{
	[Flags]
	public enum RunningFlags
	{
		None = 0x00000000,
		Flying = 0x00000001,
		Sleep = 0x00000002,
		ReaperForm = 0x00000004,
		//WalkAnimalForm = 0x00000008,
		StoneForm = 0x00000010,
		TwistedWeald = 0x00000020,
		Grizzle = 0x00000040,
		SpeedBoost = 0x00000080,
		AnimalForm = 0x00000100,
		//Requiem
		Bola = 0x00000200,
		TPBlocked = 0x00000400,
		GuerrieroStile = 0x00000800,
	}

	public class SpeedHandler
	{
		public static Packet GetSpeedType(RunningFlags flag)
		{
			RunningFlags fl = flag;
			Packet modspeed = null;
			switch (fl)
			{
				case RunningFlags.Flying:
				case RunningFlags.AnimalForm:
				case RunningFlags.SpeedBoost:
					modspeed = SpeedControl.MountSpeed; break;
				case RunningFlags.Grizzle:
				case RunningFlags.ReaperForm:
				case RunningFlags.Sleep:
				case RunningFlags.StoneForm:
				case RunningFlags.TwistedWeald:
				//case RunningFlags.WalkAnimalForm:
				//Requiem
				case RunningFlags.Bola:
				case RunningFlags.GuerrieroStile:
				case RunningFlags.TPBlocked:
					modspeed = SpeedControl.WalkSpeed; break;
				default: break;
			}
			return modspeed;
		}
	}
}