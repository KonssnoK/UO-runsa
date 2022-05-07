using System;

namespace Server
{
	public enum AnimationKR
	{
		Attack = 0, // subs : 0 1 
		//1 Parry not working while riding
		Parry = 2,		// Block
		Unk3 = 3,		// Should be death, ? on ASM
		PassiveHit = 4,	// Impact
		Idle = 5,		// Fidget for NPC - Idle
		Eat = 6,		// ? on ASM
		Emote = 7,		//7.0 Bow 7.1 Salute
		Alert = 8,		// Switch WarModePosition
		Takeoff = 9,	//9 Start Flying
		Land = 10,		//10 Stop Flying
		Spell = 11,		//Casting of spells
		StartCombat = 12,	//Never Used, till now
		EndCombat = 13,		// Never Used, till now
		Pillage = 14,		// Looting
		Spawn = 15			// Never Used, till now
	}

	public enum WeatherTypes
	{
		Rain = 0x00,
		FStorm = 0x01,
		Snow = 0x02,
		Storm = 0x03,
		SetTemperature = 0xFE,
		None = 0xFF
	}

	public enum Seasons
	{
		Spring = 0x00,
		Summer = 0x01,
		Fall = 0x02,
		Winter = 0x03,
		Desolation = 0x04
	}
}