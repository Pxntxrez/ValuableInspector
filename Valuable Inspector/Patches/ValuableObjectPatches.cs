using HarmonyLib;

[HarmonyPatch(typeof(ValuableObject))]
public static class ValuableObjectPatches
{
    [HarmonyPatch(nameof(ValuableObject.Awake))]
    [HarmonyPostfix]
    public static void AwakePatch(ValuableObject __instance)
    {
        ValuableRegistry.Register(__instance);

        if (__instance.GetComponent<ValuableTracker>() == null)
            __instance.gameObject.AddComponent<ValuableTracker>();
    }
}
