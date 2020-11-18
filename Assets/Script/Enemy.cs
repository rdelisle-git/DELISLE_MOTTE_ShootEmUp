using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Entity
{
    [SerializeField]
    float m_ennemySpeed = 0.0f;
    [SerializeField]
    EnemyBullet Bullet;

    float ROF;
    Camera m_mainCamera;
    public float m_respawnDelay;

    public event Action onEnemyBulletHit;
    public event Action onEnemyCrash;
    // Start is called before the first frame update
    void Start()
    {
        m_respawnDelay = 1.0f;
        m_ennemySpeed = 0.015f;
        m_actualHP = 1;
        StartCoroutine(BulletControl(m_respawnDelay));
    }

    private void Awake()
    {
        Bullet.OnEnemyBulletHit += isEnemyBulletHit;
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
        {
            onEnemyCrash?.Invoke();
            Destroy(gameObject);
        }
            


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            m_actualHP = gestionHP(m_actualHP);
            if (m_actualHP == 0)
            {
                Destroy(gameObject);
            }
        }
        else if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator BulletControl(float respawnDelay)
    {
        while (true)
        {
            if (Application.isPlaying)
            {
                EnemyBullet newEnemyBullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
                newEnemyBullet.OnEnemyBulletHit += isEnemyBulletHit;
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    void isEnemyBulletHit()
    {
        onEnemyBulletHit?.Invoke();
    }
}
