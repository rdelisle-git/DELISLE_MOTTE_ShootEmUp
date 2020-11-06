using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Text Score;
    [SerializeField]
    Slider HP;
    [SerializeField]
    Text txtGameOver;
    [SerializeField]
    Button btnGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnDestroy()
    {
        player.OnHPChange -= updateHP;
    }
    void updateHP(int hp)
    {
        HP.value = hp;
    }

    void isGameOver()
    {
        txtGameOver.gameObject.SetActive(true);
        btnGameOver.gameObject.SetActive(true);
    }
}
