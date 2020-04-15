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

public class Enemy : MonoBehaviour
{

    public EnemyState enemyState;

    [Header("Stats")]
    [SerializeField] protected string enemyName;
    [SerializeField] protected int baseAttack;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private FloatValue maxHealth;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

    protected float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
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
            Destroy(effect, deathEffectDelay);
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
            enemyState = EnemyState.idle;
            hitRigidbody.velocity = Vector2.zero;
        }

    }
}
