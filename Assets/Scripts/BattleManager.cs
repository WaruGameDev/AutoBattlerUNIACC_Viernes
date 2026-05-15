using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;
    public List<Unit> turnUnits;    

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartBattle();
    }
    public void StartBattle()
    {
        SortTurn();
        Battle();
    }
    public void SortTurn()
    {
        turnUnits.AddRange(playerUnits);
        turnUnits.AddRange(enemyUnits);
        turnUnits.Sort((a, b)=>b.speed.CompareTo(a.speed));
    }

    public void Battle()
    {
        if(turnUnits.Count > 0)
        {
            if(playerUnits.Count > 0 && enemyUnits.Count > 0)
            {
                if(playerUnits.Contains(turnUnits[0]))
                {
                    Unit enemyTarget = enemyUnits[UnityEngine.Random.Range(0, enemyUnits.Count)];
                    turnUnits[0].Attack(enemyTarget, Battle);
                }
                else if(enemyUnits.Contains(turnUnits[0]))
                {
                    Unit playerTarget = playerUnits[UnityEngine.Random.Range(0, enemyUnits.Count)];
                    turnUnits[0].Attack(playerTarget, Battle);
                }
                turnUnits.RemoveAt(0);
            }
            else
            {
                Debug.Log("Termino la batalla");
                return;
            }
            
        }
        else
        {
            StartBattle();
        }
    }





}
