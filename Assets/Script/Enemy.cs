using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    float m_ennemySpeed = 0.0f;
    Camera m_mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_ennemySpeed = 0.01f;
        m_actualHP = 2;
    }

    private void Awake()
    {
        m_mainCamera = FindObjectOfType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        EnnemyControl();
    }

    private void EnnemyControl()
    {
        Vector3 screenPos = m_mainCamera.WorldToScreenPoint(gameObject.transform.position);
        gameObject.transform.Translate(0, -m_ennemySpeed, 0);
        if (screenPos.y < 40)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            m_actualHP = gestionHP(m_actualHP);
            UnityEngine.Debug.Log(m_actualHP);
            if (m_actualHP == 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
           
        
        
    }
}
