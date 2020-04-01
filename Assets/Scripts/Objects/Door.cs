using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemyCleared,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool isOpen;
    public GameObject door;
    public BoxCollider2D triggerArea;
    public GameObject RoomTransfer;

    public Inventory playerInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_PlayerInRange && thisDoorType == DoorType.key)
            {
                // Does player have key
                if(playerInventory.numberOfKeys > 0)
                {
                    OpenDoor();
                }
            }
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        door.SetActive(false);
        triggerArea.enabled = false;
        RoomTransfer.SetActive(true);
    }

    public void CloseDoor()
    {

    }
}
