using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour {

    [SerializeField] private Vector2 m_MaxPosition;
    [SerializeField] private Vector2 m_MinPosition;
    [SerializeField] private Vector3 m_PlayerChange;
    [SerializeField] private bool m_IsNameNeeded;
    [SerializeField] private string m_PlaceName;
    [SerializeField] private GameObject m_TextObject;

    private CameraMovement m_CameraMovement;


	// Use this for initialization
	void Start () {
        m_CameraMovement = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_CameraMovement.m_MinPosition = m_MinPosition;
            m_CameraMovement.m_MaxPosition = m_MaxPosition;
            collision.transform.position += m_PlayerChange;

            if (m_IsNameNeeded)
            {
                StartCoroutine(ShowPlaceText());
            }
        }
    }

    IEnumerator ShowPlaceText()
    {
        m_TextObject.SetActive(true);
        m_TextObject.GetComponent<Text>().text = m_PlaceName;
        yield return new WaitForSeconds(4f);
        m_TextObject.SetActive(false);
    }
}
