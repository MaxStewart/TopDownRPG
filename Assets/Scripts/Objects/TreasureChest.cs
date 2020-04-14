using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue storedOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("Opened", true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_PlayerInRange)
        {
            if (!isOpen)
            {
                // Open chest
                OpenChest();
            }
            else
            {
                // Chest already open
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        isOpen = true;
        contextOff.Raise();
        anim.SetBool("Opened", true);
        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger && !isOpen)
        {
            m_PlayerInRange = true;
            contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger && !isOpen)
        {
            contextOff.Raise();
            m_PlayerInRange = false;
        }
    }
}
