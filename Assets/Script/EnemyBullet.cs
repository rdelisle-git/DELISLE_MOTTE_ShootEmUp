using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    Camera m_mainCamera;

    float m_bulletSpeedX;
    float m_bulletSpeedY;

    public event Action OnEnemyBulletHit;
    // Start is called before the first frame update
    void Start()
    {
        m_bulletSpeedX = UnityEngine.Random.Range(-0.005f, 0.005f);
        m_bulletSpeedY = 0.030f;
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
        gameObject.transform.Translate(m_bulletSpeedX, -m_bulletSpeedY, 0);
        if (screenPos.y < -100)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnEnemyBulletHit?.Invoke();
            Destroy(gameObject);
        }


    }
}
