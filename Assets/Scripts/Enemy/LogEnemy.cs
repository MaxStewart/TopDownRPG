using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy {

    public float chaseRadius; // Default should be 3.5
    public float attackRadius; // Default should be 1

    [HideInInspector]
    public Rigidbody2D rigidBody;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Animator anim;

    private Vector2 m_HomePosition;

	// Use this for initialization
	void Start () {
        enemyState = EnemyState.idle;
        m_HomePosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("wakeUp", true);
	}

    // Update is called once per frame
    void FixedUpdate () {
        CheckDistance();
	}

    public virtual void CheckDistance()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            if (enemyState == EnemyState.idle || enemyState == EnemyState.walk && enemyState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rigidBody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wakeUp", true);
            }
        } else if (Vector2.Distance(transform.position, target.position) > chaseRadius)
        {
            ChangeState(EnemyState.idle);
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
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
