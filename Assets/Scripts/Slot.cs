using UnityEngine;

public class Slot : MonoBehaviour
{
    public int indexSlot = 0;
    public UnitSelector currentUnitSelector;

    public void SetUnit(UnitSelector unitSelector)
    {
        if(unitSelector!= null)
        {
            currentUnitSelector = unitSelector;
            SelectorManager.instance.selectedUnits[indexSlot] = currentUnitSelector.unitData;
        }       
    }
    public void CleanUnit()
    {
        currentUnitSelector = null;
        SelectorManager.instance.selectedUnits[indexSlot] = null;
    }
   
}
