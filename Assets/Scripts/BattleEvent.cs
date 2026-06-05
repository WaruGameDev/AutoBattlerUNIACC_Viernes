using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BattleDungeonEvent", menuName = "AutoBattler/DungeonEvent/Battle")]
public class BattleEvent : DungeonEvent
{
    public List<UnitData> enemies;

    public override void TriggerEvent()
    {
        BattleManager.instance.enemyUnitsData.Clear();
        BattleManager.instance.enemyUnitsData.AddRange(enemies);
        BattleManager.instance.InitializeBattle();
    }
}
