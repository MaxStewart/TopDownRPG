using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private Signal m_PlayerHealthSignal;
    [SerializeField] private FloatValue m_CurrenHealth;

    private Rigidbody2D m_RigidBody;
    private Animator m_Anim;
    private Vector3 m_Change;
    private float m_Speed = 5;

    public PlayerState m_CurrentState;
    public VectorValue startingPosition;

    // Use this for initialization
    void Start () {
        m_CurrentState = PlayerState.walk;
        m_Anim = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody2D>();
        // Set the player to move down for hitbox bug
        m_Anim.SetFloat("moveX", 0);
        m_Anim.SetFloat("moveY", -1);

        transform.position = startingPosition.initialValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_Change = Vector2.zero;
        m_Change.x = Input.GetAxisRaw("Horizontal"); // Raw gives either 0 or 1
        m_Change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && m_CurrentState != PlayerState.attack && m_CurrentState != PlayerState.stagger)
        {
            StartCoroutine(AttackRoutine());
        }
        else if(m_CurrentState == PlayerState.walk || m_CurrentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    IEnumerator AttackRoutine()
    {
        m_Anim.SetBool("attacking", true);
        m_CurrentState = PlayerState.attack;
        yield return null; // Wait 1 frame so animation can start but not repeat
        m_Anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        m_CurrentState = PlayerState.walk;
    }

    private void UpdateAnimationAndMove()
    {
        if (m_Change != Vector3.zero)
        {
            MoveCharacter();
            m_Anim.SetFloat("moveX", m_Change.x);
            m_Anim.SetFloat("moveY", m_Change.y);
            m_Anim.SetBool("moving", true);
        }
        else
        {
            m_Anim.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        m_Change.Normalize(); // So diagonal movement isnt faster
        m_RigidBody.MovePosition(transform.position + m_Change * m_Speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        m_CurrenHealth.RuntimeValue -= damage;
        if (m_CurrenHealth.RuntimeValue > 0)
        {
            m_PlayerHealthSignal.Raise();
            StartCoroutine(KnockRoutine(knockTime));
        }
        else
        {
            m_PlayerHealthSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockRoutine(float knockTime)
    {
        if (m_RigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            m_RigidBody.velocity = Vector2.zero;
            m_CurrentState = PlayerState.idle;
            m_RigidBody.velocity = Vector2.zero;
        }
    }
}
