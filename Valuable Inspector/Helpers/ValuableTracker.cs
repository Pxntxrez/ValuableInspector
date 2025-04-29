using UnityEngine;

public class ValuableTracker : MonoBehaviour
{
    private ValuableObject valuable;

    private void Awake()
    {
        valuable = GetComponent<ValuableObject>();
    }

    private void OnDestroy()
    {
        ValuableRegistry.Unregister(valuable);
    }
}
