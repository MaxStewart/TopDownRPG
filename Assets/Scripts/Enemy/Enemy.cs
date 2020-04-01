using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour {

    [SerializeField] protected string m_EnemyName;
    [SerializeField] protected int m_BaseAttack;
    [SerializeField] protected float m_MoveSpeed;

    protected float m_CurrentHealth;

    public EnemyState m_EnemyState;
    public FloatValue m_MaxHealth;
    public GameObject deathEffect;

    private void Awake()
    {
        m_CurrentHealth = m_MaxHealth.m_InitialValue;
    }

    private void TakeDamage(float damage)
    {
        m_CurrentHealth -= damage;
        if(m_CurrentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            DeathEffect();
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }        
    }

    public void Knock(Rigidbody2D hitRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockRoutine(hitRigidbody, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockRoutine(Rigidbody2D hitRigidbody, float knockTime)
    {
        if (hitRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            hitRigidbody.velocity = Vector2.zero;
            m_EnemyState = EnemyState.idle;
            hitRigidbody.velocity = Vector2.zero;
        }

    }
}
