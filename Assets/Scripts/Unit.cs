using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public enum Arquetypes
{
    LOOKING_FOR_STRONGEST, LOOKING_FOR_WEAKEST, RANDOM
}

public class Unit : MonoBehaviour
{
    public UnitData unitData;
    public float health;
    public float maxHealth = 100;
    public float attack= 10;
    public float defense = 5;
    public float speed = 2;
    Tween hurtTween;
    public Transform posFront;
    public Image bar;
    public Transform visual;
    public bool isPlayer;
    public Arquetypes arquetypes;
    

    public void Init()
    {
        maxHealth = unitData.maxHealth;
        attack = unitData.attack;
        defense = unitData.defense;
        speed = unitData.speed;
        arquetypes = unitData.arquetypes;

        GameObject visualUnit = Instantiate(unitData.model, visual);
        if(isPlayer)
        {
            visualUnit.transform.Rotate(Vector3.up * 90);
        }
        else
        {
            visualUnit.transform.Rotate(Vector3.up * -90);
        }
        health = maxHealth;
        hurtTween = transform.DOPunchScale(new Vector3(1.5f,-.5f, 1.5f), .2f).SetAutoKill(false).Pause();
        bar.fillAmount = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float effectiveDamage = Mathf.Max(damage- defense,0);
        health -= effectiveDamage;
        bar.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            Die();
        }
        else
        {
            hurtTween.Restart();
        }
    }
    public void Die()
    {
        BattleManager.instance.playerUnits.Remove(this);
        BattleManager.instance.enemyUnits.Remove(this);
        BattleManager.instance.turnUnits.Remove(this);
        Destroy(gameObject);
    }
    public void Attack(Unit target, Action onCompleteAttack = null)
    {
        Vector3 posOriginal = transform.position;
        transform.DOJump(target.posFront.position, 2,1,.2f).
        OnComplete(()=>
        {
            target.TakeDamage(attack);
            transform.DOMove(posOriginal,.2f).OnComplete(()=> onCompleteAttack?.Invoke());
        });
        
    }
}
