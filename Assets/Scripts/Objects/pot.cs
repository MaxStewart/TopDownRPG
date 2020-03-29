using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour {

    private Animator m_Anim;

	// Use this for initialization
	void Start () {
        m_Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Smash()
    {
        m_Anim.SetBool("smash", true);
        StartCoroutine(BreakRoutine());
    }

    IEnumerator BreakRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }
}
