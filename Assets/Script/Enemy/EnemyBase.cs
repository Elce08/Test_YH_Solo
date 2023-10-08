using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : PooledObject
{
    protected Animator anim;

    public float speed;
    public float attackDamage = 5.0f;
    public float def = 1.0f;

    public float startHp = 50.0f;

    public float hp;

    public bool isAlive = true;

    public virtual float Hp
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                    Die();
                }
            }
        }
    }

    public float ReMaxHp;

    protected virtual void Start()
    {
        Hp = startHp;
    }

    public virtual void Hitted(float damage)
    {
        StartCoroutine(HittedCoroutine());
        Hp -= damage / def;
    }

    protected virtual void Die()
    {
        isAlive = false;
        anim.SetTrigger("IsDie");
    }

    protected virtual IEnumerator HittedCoroutine()
    {
        anim.SetTrigger("IsHitted");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.SetTrigger("IsIdle");
        StopAllCoroutines();
    }
}
