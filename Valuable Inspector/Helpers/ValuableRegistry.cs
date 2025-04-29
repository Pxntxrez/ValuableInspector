using System.Collections.Generic;
using UnityEngine;

public static class ValuableRegistry
{
    public static readonly List<ValuableObject> AllValuables = new List<ValuableObject>();

    public static void Register(ValuableObject vo)
    {
        if (vo != null && !AllValuables.Contains(vo))
            AllValuables.Add(vo);
    }

    public static void Unregister(ValuableObject vo)
    {
        if (vo != null)
            AllValuables.Remove(vo);
    }
}
