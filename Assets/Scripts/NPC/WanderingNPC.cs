using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingNPC : MonoBehaviour
{
    public Transform[] path;
    public string name;

    private Transform currentGoal;
    private float roundingDistance = 0.2f;
    private float moveSpeed = 2f;
    private Rigidbody2D rigidbody;
    private int currentPoint = 0;
    private Animator anim;
    private bool canMove = true;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        if (currentGoal == null)
        {
            currentGoal = path[currentPoint];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MoveNPC();
        }
    }

    private void MoveNPC()
    {
        if (!currentGoal)
        {
            ChangeGoal();
        }

        if (Vector3.Distance(currentGoal.position, transform.position) > roundingDistance)
        {
            anim.SetBool("isMoving", true);
            Vector3 temp = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            rigidbody.MovePosition(temp);
        }
        else
        {
            StartCoroutine(SitIdleCo());
        }
    }

    IEnumerator SitIdleCo()
    {
        canMove = false;
        float timeToWait = Random.Range(3, 10);

        anim.SetBool("isMoving", false);
        yield return new WaitForSeconds(timeToWait);
        ChangeGoal();
        canMove = true;
    }

    private void ChangeGoal()
    {
        if (currentGoal == null)
        {
            currentGoal = path[currentPoint];
        }

        if (currentPoint == path.Length - 1)
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

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
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
}
