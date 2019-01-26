using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string m_name;
    public bool m_isActive;

    public Item(string m_name, bool m_isActive)
    {
        this.m_name = m_name;
        this.m_isActive = m_isActive;
    }

    void Start()
    {
        m_isActive = false;
    }

    bool GetItemState()
    {
        if(m_isActive)
        {
            return true;
        }

        return false;
    }
}
