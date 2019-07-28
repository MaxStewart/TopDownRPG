using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

    [SerializeField] GameObject m_DialogBox;
    [SerializeField] Text m_DialogText;
    [SerializeField] string m_DialogString;

    private bool m_PlayerInRange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && m_PlayerInRange)
        {
            if (m_DialogBox.activeInHierarchy)
            {
                m_DialogBox.SetActive(false);
            }
            else
            {
                m_DialogBox.SetActive(true);
                m_DialogText.text = m_DialogString;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            m_PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            m_PlayerInRange = false;
            if (m_DialogBox.activeInHierarchy)
            {
                m_DialogBox.SetActive(false);
            }
        }
    }
}
