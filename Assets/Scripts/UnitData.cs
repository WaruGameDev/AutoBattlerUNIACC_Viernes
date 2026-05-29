using JetBrains.Annotations;
using UnityEngine;
[CreateAssetMenu(fileName = "Unit", menuName = "AutoBattler/Unit")]
public class UnitData : ScriptableObject
{
   
    public float maxHealth = 100;
    public float attack= 10;
    public float defense = 5;
    public float speed = 2;
    public GameObject model;
    public Arquetypes arquetypes = Arquetypes.RANDOM;
}
