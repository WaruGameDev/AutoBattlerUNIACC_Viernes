using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorManager : MonoBehaviour
{
    public static SelectorManager instance;
    public List<UnitData> selectedUnits = new List<UnitData>(3);

    void Awake()
    {
        instance = this;
    }
    public void SelectBattlers()
    {
        DataManager.selectedUnitsData.Clear();
        DataManager.selectedUnitsData.AddRange(selectedUnits);
        SceneManager.LoadScene("SampleScene");
    }
    
}
