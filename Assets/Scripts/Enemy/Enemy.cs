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
