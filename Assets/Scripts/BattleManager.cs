using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public List<UnitData> playerUnitsData;
    public List<UnitData> enemyUnitsData;

    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;

    public List<Transform> enemySlots;
    public List<Transform> playerSlots;
    public List<Unit> turnUnits;  
    public GameObject unitPrefab;  

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GenerateEnemies();
        GeneratePlayers();
        StartBattle();
    }
    public void GenerateEnemies()
    {       
        for(int i =0; i < enemyUnitsData.Count; i++)
        {
            GameObject uGameObject = Instantiate(unitPrefab, enemySlots[i]);
            Unit unit = uGameObject.GetComponent<Unit>();
            unit.isPlayer = false;
            unit.unitData = enemyUnitsData[i];
            unit.Init(); // ← inicializa antes de StartBattle
            enemyUnits.Add(unit);
        }       
    }
    public void GeneratePlayers()
    {       
        for(int i =0; i < playerUnitsData.Count; i++)
        {
            GameObject uGameObject = Instantiate(unitPrefab, playerSlots[i]);
            Unit unit =  uGameObject.GetComponent<Unit>();
            unit.isPlayer  = true;
            unit.unitData = playerUnitsData[i];
            unit.Init();
            playerUnits.Add(unit);
        }       
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
                    //Unit enemyTarget = enemyUnits[UnityEngine.Random.Range(0, enemyUnits.Count)];
                    switch(turnUnits[0].arquetypes)
                    {
                        case Arquetypes.RANDOM:
                            turnUnits[0].Attack(GetRandomUnit(enemyUnits), Battle);
                            break;
                        case Arquetypes.LOOKING_FOR_WEAKEST:
                            turnUnits[0].Attack(GetUnitWithLessHP(enemyUnits), Battle);
                            break;
                        case Arquetypes.LOOKING_FOR_STRONGEST:
                            turnUnits[0].Attack(GetUnitWithMoreHP(enemyUnits), Battle);
                            break;
                    }
                    
                }
                else if(enemyUnits.Contains(turnUnits[0]))
                {
                    switch(turnUnits[0].arquetypes)
                    {
                        case Arquetypes.RANDOM:
                            turnUnits[0].Attack(GetRandomUnit(playerUnits), Battle);
                            break;
                        case Arquetypes.LOOKING_FOR_WEAKEST:
                            turnUnits[0].Attack(GetUnitWithLessHP(playerUnits), Battle);
                            break;
                        case Arquetypes.LOOKING_FOR_STRONGEST:
                            turnUnits[0].Attack(GetUnitWithMoreHP(playerUnits), Battle);
                            break;
                    }
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

    public Unit GetUnitWithMoreHP(List<Unit> targetList)
    {
        float hp = 0;
        Unit unit = null;
        foreach(Unit u in targetList)
        {
            if(u.health > hp)
            {
                unit = u;
                hp = u.health;
            }
        }
        return unit;
    }
    public Unit GetUnitWithLessHP(List<Unit> targetList)
    {
        float hp = float.MaxValue;
        Unit unit = null;
        foreach(Unit u in targetList)
        {
            if(u.health < hp)
            {
                unit = u;
                hp = u.health;
            }
        }
        return unit;

    }
    public Unit GetRandomUnit(List<Unit> targetList)
    {
        int rng = UnityEngine.Random.Range(0, targetList.Count);
        return targetList[rng];
    }





}
