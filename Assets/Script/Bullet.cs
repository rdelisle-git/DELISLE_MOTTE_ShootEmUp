using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float m_bulletSpeed = 0.0f;
    [SerializeField]
    Camera m_mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_bulletSpeed = 0.03f;
    }

    void Awake()
    {
        m_mainCamera = FindObjectOfType<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        BulletControl();
    }

    private void BulletControl()
    {
        Vector3 screenPos = m_mainCamera.WorldToScreenPoint(gameObject.transform.position);
        gameObject.transform.Translate(0, m_bulletSpeed, 0);
        if (screenPos.y > 450)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {

            Destroy(gameObject);
        }

        
    }
}
