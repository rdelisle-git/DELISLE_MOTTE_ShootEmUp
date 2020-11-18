using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField]
    float m_VerticalSpeed = 0.0f;
    [SerializeField]
    float m_HorizontalSpeed = 0.0f;
    [SerializeField]
    Camera m_mainCamera;
    [SerializeField]
    Bullet Bullet;
    float rateOfFire = 0.0f;
    

    public Stopwatch stopWatch = new Stopwatch();

    public event Action<int> OnHPChange;

    public event Action onGameOver;

    public event Action onBulletHit;

    float m_time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_VerticalSpeed = 0.13f;
        m_HorizontalSpeed = 0.13f;
        rateOfFire = 0.3f;
        stopWatch.Start();
    }
    private void Awake()
    {
        Bullet.OnBulletHit += isBulletHit;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        float ROF;
        Vector3 screenPos = m_mainCamera.WorldToScreenPoint(gameObject.transform.position);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(screenPos.x > 55)
                gameObject.transform.Translate(-m_HorizontalSpeed, 0, 0);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            if(screenPos.x <= 1900)
                gameObject.transform.Translate(m_HorizontalSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(screenPos.y <= 1000)
                gameObject.transform.Translate(0, m_VerticalSpeed, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {   
            if(screenPos.y > 40)
                gameObject.transform.Translate(0, -m_VerticalSpeed, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_time = stopWatch.ElapsedMilliseconds;
            ROF = rateOfFire * 1000;
            if (m_time > ROF)
            {
                Bullet newBullet = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
                newBullet.OnBulletHit += isBulletHit;
                stopWatch.Reset();
                stopWatch.Start();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_actualHP = gestionHP(m_actualHP);
            OnHPChange?.Invoke(m_actualHP);
        }
    }

    void isBulletHit()
    {
        onBulletHit?.Invoke();
    }
}
