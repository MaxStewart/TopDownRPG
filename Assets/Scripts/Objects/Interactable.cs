using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Signal contextOn;
    public Signal contextOff;

    protected bool m_PlayerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            m_PlayerInRange = true;
            contextOn.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && !other.isTrigger)
        {
            contextOff.Raise();
            m_PlayerInRange = false;
        }
    }
}
