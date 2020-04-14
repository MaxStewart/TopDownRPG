using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy {

    public float m_ChaseRadius;
    public float m_AttackRadius;

    public Rigidbody2D m_RigidBody;
    public Transform m_Target;
    public Animator m_Anim;

    public Vector2 m_HomePosition;

	// Use this for initialization
	void Start () {
        enemyState = EnemyState.idle;
        m_HomePosition = transform.position;
        m_Target = GameObject.FindGameObjectWithTag("Player").transform;
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_Anim.SetBool("wakeUp", true);
	}

    // Update is called once per frame
    void FixedUpdate () {
        CheckDistance();
	}

    public virtual void CheckDistance()
    {
        float distanceToTarget = Vector2.Distance(transform.position, m_Target.position);
        if (distanceToTarget <= m_ChaseRadius && distanceToTarget > m_AttackRadius)
        {
            if (enemyState == EnemyState.idle || enemyState == EnemyState.walk && enemyState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, m_Target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                m_Anim.SetBool("wakeUp", true);
            }
        } else if (Vector2.Distance(transform.position, m_Target.position) > m_ChaseRadius)
        {
            ChangeState(EnemyState.idle);
            m_Anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        m_Anim.SetFloat("moveX", setVector.x);
        m_Anim.SetFloat("moveY", setVector.y);
    }

    public void ChangeAnim(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x >  0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if(enemyState != newState)
        {
            enemyState = newState;
        }
    }
}
