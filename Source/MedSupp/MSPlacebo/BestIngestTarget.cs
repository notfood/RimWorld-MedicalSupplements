﻿using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace MSPlacebo
{
	// Token: 0x0200000F RID: 15
	[HarmonyPatch(typeof(JobGiver_BingeDrug), "BestIngestTarget")]
	public class BestIngestTarget
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000045D4 File Offset: 0x000027D4
		[HarmonyPrefix]
		public static bool Prefix(ref Thing __result, Pawn pawn)
		{
			Predicate<Thing> predicate = (Thing t) => (pawn.InMentalState || !t.IsForbidden(pawn)) && pawn.CanReserve(t, 1, -1, null, false) && (pawn.Position.InHorDistOf(t.Position, 100f) || t.Position.Roofed(t.Map) || pawn.Map.areaManager.Home[t.Position] || t.GetSlotGroup() != null);
			IntVec3 position = pawn.Position;
			Map map = pawn.Map;
			PathEndMode peMode = PathEndMode.OnCell;
			TraverseParms traverseParams = TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false);
			Predicate<Thing> validator = predicate;
			ThingRequest PlaceboReq = ThingRequest.ForDef(ThingDef.Named("MSPlacebo"));
			__result = GenClosest.ClosestThingReachable(position, map, PlaceboReq, peMode, traverseParams, 9999f, validator, null, 0, -1, false, RegionType.Set_Passable, false);
			return __result == null;
		}
	}
}
