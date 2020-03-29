using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject {

    public List<SignalListener> m_Listeners = new List<SignalListener>();

    public void Raise()
    {
        for (int i = m_Listeners.Count-1; i >= 0; i--)
        {
            m_Listeners[i].OnSignalRaised(); // TODO this has a bug where theres a null SignalListener
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        m_Listeners.Add(listener);
    }

    public void DeregisterListener(SignalListener listener)
    {
        m_Listeners.Remove(listener);
    }
}
