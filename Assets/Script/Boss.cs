using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : Entity
{
    [SerializeField]
    EnemyBullet Bullet;

    public float m_ennemySpeed;
    public float ROF;

    Camera m_mainCamera;

    float m_respawnDelay;

    public event Action<int> onBulletHitBoss;
    public event Action onEnemyBulletHit;
    public event Action onBossDone;
    // Start is called before the first frame update
    void Start()
    {
        m_ennemySpeed = 0.025f;
        m_actualHP = 15;
        StartCoroutine(BulletControl(ROF));
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
        if(screenPos.x > 1700)
        {
            m_ennemySpeed = -0.025f;
        }
        if(screenPos.x < 300)
        {
            m_ennemySpeed = 0.025f;
        }
        gameObject.transform.Translate(m_ennemySpeed, 0, 0);
        if (screenPos.y < 40)
            Destroy(gameObject);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            m_actualHP = gestionHP(m_actualHP);
            onBulletHitBoss?.Invoke(m_actualHP);
            Debug.Log(m_actualHP);
            if (m_actualHP == 0)
            {
                Destroy(gameObject);
                onBossDone?.Invoke();
            }
        }
    }

    IEnumerator BulletControl(float ROF)
    {
        while (true)
        {
            if (Application.isPlaying)
            {
                EnemyBullet newEnemyBullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
                newEnemyBullet.OnEnemyBulletHit += isEnemyBulletHit;
            }
            yield return new WaitForSeconds(ROF);
        }
    }

    void isEnemyBulletHit()
    {
        onEnemyBulletHit?.Invoke();
    }
}
