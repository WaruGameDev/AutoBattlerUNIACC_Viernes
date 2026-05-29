using System.Collections.Generic;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public static SelectorManager instance;
    public List<UnitData> selectedUnits = new List<UnitData>(3);

    void Awake()
    {
        instance = this;
    }
    
}
