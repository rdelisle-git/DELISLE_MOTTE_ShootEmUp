using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        highScore.text = PlayerPrefs.GetFloat("HighScore").ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("game");
    }
}
