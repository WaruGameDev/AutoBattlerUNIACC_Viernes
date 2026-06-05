using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;
    public List<DungeonEvent> dungeonEvents;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dungeonEvents[0].TriggerEvent();
    }
    public void NextEvent()
    {
        dungeonEvents.RemoveAt(0);
        dungeonEvents[0].TriggerEvent();
    }
}
