using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame updat

    public int m_maxHP;
    public int m_actualHP;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        m_actualHP = m_maxHP;
    }

    public int gestionHP(int actualHP)
    {
        actualHP = actualHP - 1;
        return actualHP;
    }
}
