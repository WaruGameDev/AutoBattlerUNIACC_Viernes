using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public float attack= 10;
    public float defense = 5;
    public float speed = 2;
    Tween hurtTween;
    public Transform posFront;
    public Image bar;
    

    public void Start()
    {
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
