using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Enemy enemy;
    [SerializeField]
    Boss boss;
    [SerializeField]
    EnemyBullet enemyBullet;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    GameManager gamemanager;
    [SerializeField]
    EnemiesManager enemiesManager;
    [SerializeField]
    Text lblScore;
    [SerializeField]
    Text Score;
    [SerializeField]
    Slider HP;
    [SerializeField]
    Text txtGameOver;
    [SerializeField]
    Button btnGameOver;
    [SerializeField]
    Text Level;

    float iScore;
    // Start is called before the first frame update
    void Start()
    {
        btnGameOver.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void Awake()
    {
        HP.maxValue = player.m_maxHP;
        HP.value = player.m_actualHP;

        player.OnHPChange += updateHP;
        player.onGameOver += isGameOver;
        player.onBulletHit += inBulletHit;

        enemiesManager.onEnemyCrash += inEnemyCrash;
        enemiesManager.onEnemyBulletHit += inEnemyBulletHit;

        gamemanager.onGameStart += isGameStart;
        gamemanager.onNextLevel += isNextLevel;
    }

    private void OnDestroy()
    {
        player.OnHPChange -= updateHP;
    }
    void updateHP(int hp)
    {
        HP.value = hp;
        if (player.m_actualHP == 0)
        {
            isGameOver();

        }
    }

    void isGameOver()
    {
        txtGameOver.gameObject.SetActive(true);
        btnGameOver.gameObject.SetActive(true);
    }

    void isGameStart()
    {
        HP.gameObject.SetActive(true);
        lblScore.gameObject.SetActive(true);
        Score.gameObject.SetActive(true);
        iScore = 0;
        Score.text = iScore.ToString();
    }

    void inBulletHit()
    {
        iScore = iScore + (100*(1.1f*gamemanager.level));
        Score.text = iScore.ToString();
    }

    void inEnemyBulletHit()
    {
        player.m_actualHP = player.gestionHP(player.m_actualHP);
        updateHP(player.m_actualHP);
    }

    void isNextLevel(float iLevel)
    {
        Level.text = iLevel.ToString();
    }

    void inEnemyCrash()
    {
        UnityEngine.Debug.Log(player.m_actualHP);
        player.m_actualHP = player.gestionHP(player.m_actualHP);
        updateHP(player.m_actualHP);
    }


    void Restart()
    {
        float highScore = PlayerPrefs.GetFloat("HighScore");
        if(highScore < iScore)
        {
            PlayerPrefs.SetFloat("HighScore", iScore);
        }
        SceneManager.LoadScene("menu");
    }
}
