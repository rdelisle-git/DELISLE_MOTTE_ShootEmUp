using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{

    [SerializeField]
    public GameObject Enemy;
    [SerializeField]
    float m_respawnDelay = 0.0f;
    Camera m_mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        m_respawnDelay = 1.0f;
        m_mainCamera = FindObjectOfType<Camera>();
        StartCoroutine(EnnemyControl(m_respawnDelay));
    }

    IEnumerator EnnemyControl(float respawnDelay)
    {
        while(true)
        {
            if (Application.isPlaying)
            {
                Vector3 point = m_mainCamera.ScreenToWorldPoint(new Vector3(Random.Range(50,1090), 500, 10));
                Instantiate(Enemy, point, Quaternion.identity);
            }
            yield return new WaitForSeconds(respawnDelay);
        }
    }
}
