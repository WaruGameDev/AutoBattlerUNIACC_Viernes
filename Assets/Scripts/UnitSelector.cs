using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public UnitData unitData;
    public Slot currentSlot;
    public Vector3 originalPos;
    public Transform visual;

    void Start()
    {
        originalPos = transform.position;
        Instantiate(unitData.model, visual);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Slot"))
        {
            if(currentSlot == null && other.gameObject.GetComponent<Slot>().currentUnitSelector == null)
            {               
                currentSlot = other.gameObject.GetComponent<Slot>();     
                
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Slot"))
        {
            currentSlot = null;
        }
    }

    public void StartDrag()
    {
        if(currentSlot != null)
        {
            currentSlot.CleanUnit();
        }

    }


    public void Drop()
    {
        if(currentSlot != null)
        {
            currentSlot.SetUnit(this);
            Vector3 slotPos = currentSlot.transform.position;
            slotPos.y = transform.position.y;        
            transform.position = slotPos;
        }
        else
        {
            transform.position = originalPos;
        }
    }





}
