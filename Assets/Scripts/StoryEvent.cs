using UnityEngine;
[CreateAssetMenu(fileName = "StoryDungeonEvent", menuName = "AutoBattler/DungeonEvent/Story")]
public class StoryEvent : DungeonEvent
{
    public string storyToWrite;

    public override void TriggerEvent()
    {
        StoryManager.instance.SetStory(storyToWrite);
    }
}
