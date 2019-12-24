using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    private Dictionary<EventName, GameEvent> eventDictionary;
    private static EventManager eventManager;

    private static EventManager Instance
    {
        get
        {
            if (eventManager == null)
            {
                eventManager = new EventManager();
            }
            return eventManager;
        }
    }

    /// <summary>
    ///   private constructor to initialize eventDictionary
    /// </summary>
    EventManager()
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<EventName, GameEvent>();
    }


    /// <summary>
    ///   Add listener to the event specified or create new event then add the listener
    /// </summary>
    public static void StartListening(EventName eventName, UnityAction<EventArgs> listener)
    {
        GameEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    ///   remove listener from the event specified if exists
    /// </summary>
    public static void StopListening(EventName eventName, UnityAction<EventArgs> listener)
    {
        if (eventManager == null) return;

        GameEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
    }

    /// <summary>
    ///   Trigger event specified by event name with arguments args
    /// </summary>
    public static void TriggerEvent(EventName eventName, EventArgs args)
    {
        GameEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(args);
        }
    }

}

public enum EventName
{
    AsteroidDestroyed, ShipDestroyed,
    ScoreChanged,
    PlayerDestroyed,
    GameOver,
    EnemyShipDestroyed,
    PowerupCollected
}
