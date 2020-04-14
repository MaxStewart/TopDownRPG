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

    [SerializeField] private Signal playerHealthSignal;
    [SerializeField] private FloatValue currenHealth;

    private Rigidbody2D rigidBody;
    private Animator anim;
    private Vector3 change;
    private float speed = 5;

    public PlayerState currentState;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer collectedItemSprite;
    public Signal playerHit;

    // Use this for initialization
    void Start () {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        // Set the player to move down for hitbox bug
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1);

        transform.position = startingPosition.initialValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentState == PlayerState.interact) return;
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal"); // Raw gives either 0 or 1
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackRoutine());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null; // Wait 1 frame so animation can start but not repeat
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }        
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                anim.SetBool("CollectItem", true);
                currentState = PlayerState.interact;
                collectedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                anim.SetBool("CollectItem", false);
                currentState = PlayerState.idle;
                collectedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize(); // So diagonal movement isnt faster
        rigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        currenHealth.RuntimeValue -= damage;
        if (currenHealth.RuntimeValue > 0)
        {
            playerHealthSignal.Raise();
            StartCoroutine(KnockRoutine(knockTime));
        }
        else
        {
            playerHealthSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockRoutine(float knockTime)
    {
        playerHit.Raise();
        if (rigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rigidBody.velocity = Vector2.zero;
        }
    }
}
