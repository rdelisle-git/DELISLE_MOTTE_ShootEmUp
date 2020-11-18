using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button gameOverButton;
    [SerializeField]
    EnemiesManager enemiesManager;
    float time;
    public event Action onGameStart;
    public float level = 1.0f;
    public float levelBeforeBoss = 1.0f;
    public bool BossDone = false;
    public event Action<float> onNextLevel;
    public event Action onBossLevel;

    void Start()
    {
        onGameStart?.Invoke();
    }

    private void Awake()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        BossDone = enemiesManager.b_BossLevel;
        if (time >= 5 && !BossDone)
        {
            level += 1;
            levelBeforeBoss += 1;
            time = 0;
            onNextLevel?.Invoke(level);
           if(levelBeforeBoss == 5)
            {
                levelBeforeBoss = 0;
                onBossLevel?.Invoke();

            }
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
