using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {

    [SerializeField] private Image[] m_Hearts;
    [SerializeField] private Sprite m_FullHeart;
    [SerializeField] private Sprite m_HalfHeart;
    [SerializeField] private Sprite m_EmptyHeart;
    [SerializeField] private FloatValue m_HeartContainers;
    [SerializeField] private FloatValue m_PlayerCurrentHealth;

    // Use this for initialization
    void Start () {
        InitHearts();
	}

    public void InitHearts()
    {
        for (int i = 0; i < m_HeartContainers.initialValue; i++)
        {
            m_Hearts[i].gameObject.SetActive(true);
            m_Hearts[i].sprite = m_FullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = m_PlayerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i <  m_HeartContainers.initialValue; i++)
        {
            if(i <= tempHealth-1)
            {
                // Full heart
                m_Hearts[i].sprite = m_FullHeart;
            }
            else if(i >= tempHealth)
            {
                // Empty heart
                m_Hearts[i].sprite = m_EmptyHeart;
            }
            else
            {
                // Half heart
                m_Hearts[i].sprite = m_HalfHeart;
            }
        }
    }
}
