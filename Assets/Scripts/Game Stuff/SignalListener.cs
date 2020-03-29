using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour {

    public Signal m_Signal;
    public UnityEvent m_SignalEvent;

	public void OnSignalRaised()
    {
        m_SignalEvent.Invoke();
    }

    private void OnEnable()
    {
        m_Signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        m_Signal.DeregisterListener(this);
    }
}
