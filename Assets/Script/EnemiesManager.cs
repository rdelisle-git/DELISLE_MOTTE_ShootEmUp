using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

    [SerializeField]
    Enemy Enemy;
    [SerializeField]
    EnemyBullet EnemyBullet;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    Boss boss;
    [SerializeField]
    float m_respawnDelay = 0.0f;
    Camera m_mainCamera;

    public event Action onEnemyBulletHit;
    public event Action<int> onBulletHitBoss;
    public event Action onBossDone;
    public event Action onEnemyCrash;
    public event Action onBossInvoke;

    public bool b_BossLevel = false;
    bool b_BossInvoke = false;
    int nBoss = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_respawnDelay = 1.5f;
        m_mainCamera = FindObjectOfType<Camera>();
        StartCoroutine(EnnemyControl(m_respawnDelay));
    }

    private void Awake()
    {
        EnemyBullet.OnEnemyBulletHit += isEnemyBulletHit;
        gameManager.onBossLevel += isBossLevel;
        Enemy.onEnemyCrash += inEnemyCrash;
        boss.onBulletHitBoss += inBulletHitBoss;
    }

    private void Update()
    {
    }
    IEnumerator EnnemyControl(float respawnDelay)
    {
        while(true)
        {
            if (!b_BossLevel)
            {
                m_respawnDelay = (1 / gameManager.level) + 0.75f;
                Vector3 point = m_mainCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(40,1880), 900, 10));
                Enemy newEnemy = Instantiate(Enemy, point, Quaternion.identity);
                newEnemy.onEnemyBulletHit += isEnemyBulletHit;
                newEnemy.m_respawnDelay = (1 / gameManager.level) + 0.25f;
            }
            else
            {
                if (!b_BossInvoke)
                {
                    nBoss += 1;
                    Vector3 point = m_mainCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Random.Range(40, 1880), 900, 10));
                    Boss newBoss = Instantiate(boss, point, Quaternion.identity);
                    newBoss.ROF = (1/(nBoss*3))+0.25f;
                    newBoss.m_ennemySpeed = 0.004f * nBoss;
                    Debug.Log(newBoss.ROF);
                    newBoss.onEnemyBulletHit += isEnemyBulletHit;
                    newBoss.onBossDone += isBossDone;
                    b_BossInvoke = true;
                    onBossInvoke?.Invoke();
                }
            }
            yield return new WaitForSeconds(m_respawnDelay);
        }
    }

    void isEnemyBulletHit()
    {
        onEnemyBulletHit?.Invoke();
    }

    void isBossLevel()
    {
        b_BossLevel = true;
    }

    void isBossDone()
    {
        b_BossLevel = false;
        b_BossInvoke = false;
        onBossDone?.Invoke();
    }

    void inEnemyCrash()
    {
        onEnemyCrash?.Invoke();
    }
    
    void inBulletHitBoss(int i)
    {
        onBulletHitBoss?.Invoke(i);
    }
}
