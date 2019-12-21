using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static void Invoke(EventName eventName)
    {

    }
}

public enum EventName
{
    AsteroidDestroyed, ShipDestroyed
}
