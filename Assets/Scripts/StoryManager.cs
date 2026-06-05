using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public static StoryManager instance;
    public CanvasGroup panel;
    public TextMeshProUGUI storyText;

    void Awake()
    {
        instance = this;
    }

    public void SetStory(string storyToText)
    {
        panel.alpha =1;
        panel.blocksRaycasts = true;
        panel.interactable = true;
        storyText.text = storyToText;
    }

    public void HideStory()
    {
        panel.alpha =0;
        panel.blocksRaycasts = false;
        panel.interactable = false;
        DungeonManager.instance.NextEvent();
    }


}
