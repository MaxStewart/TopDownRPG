using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private Transform m_Target;
    [SerializeField] private float m_Smoothing;

    public Vector2 m_MaxPosition = new Vector2(23.7f, 14.74f);
    public Vector2 m_MinPosition = new Vector2(9.24f, -0.46f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(transform.position != m_Target.position)
        {
            Vector3 targetPos = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, m_MinPosition.x, m_MaxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, m_MinPosition.y, m_MaxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, m_Smoothing);
        }
	}
}
