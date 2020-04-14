using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : LogEnemy
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance = 0.2f;

    public override void CheckDistance()
    {
        if(currentGoal == null)
        {
            ChangeGoal();
        }

        if (Vector2.Distance(transform.position, m_Target.position) <= m_ChaseRadius
            && Vector2.Distance(transform.position, m_Target.position) > m_AttackRadius)
        {
            if (enemyState == EnemyState.idle || enemyState == EnemyState.walk && enemyState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, m_Target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
                m_Anim.SetBool("wakeUp", true);
            }
        }
        else if (Vector2.Distance(transform.position, m_Target.position) > m_ChaseRadius)
        {
            m_Anim.SetBool("wakeUp", true);
            if (Vector3.Distance(currentGoal.position, transform.position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(currentGoal == null)
        {
            currentGoal = path[currentPoint];
        }

        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
