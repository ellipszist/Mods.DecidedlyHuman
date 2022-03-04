﻿using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace SmartBuilding
{
	public class ModConfig
	{
		// TODO: Add an "instantly build" toggle.
		public KeybindList EngageBuildMode = KeybindList.Parse("LeftShift+B");
		public KeybindList HoldToDraw = KeybindList.Parse("MouseRight");
		public KeybindList HoldToErase = KeybindList.Parse("LeftShift");
		public KeybindList ConfirmBuild = KeybindList.Parse("MouseLeft");
		public bool CrabPotsInAnyWaterTile = false;
	}
}