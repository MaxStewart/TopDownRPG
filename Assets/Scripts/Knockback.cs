using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    [SerializeField] private float m_Thrust;
    [SerializeField] private float m_KnockTime;
    [SerializeField] private float m_Damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hitRigidBody = other.GetComponent<Rigidbody2D>();
            if(hitRigidBody != null)
            {
                Vector2 difference = (hitRigidBody.transform.position - transform.position);
                difference = difference.normalized * m_Thrust;
                hitRigidBody.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    hitRigidBody.GetComponent<Enemy>().enemyState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hitRigidBody, m_KnockTime, m_Damage);
                }
                if (other.gameObject.CompareTag("Player") && other.isTrigger)
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hitRigidBody.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(m_KnockTime, m_Damage);
                    }
                }
            }
        }
        else if (other.gameObject.CompareTag("breakable") && gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().Smash();
        }
    }
}
