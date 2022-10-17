using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFinishPanel;
    [SerializeField] private GameObject gameWinPanel;
    private bool levelFinished = false;
    private Target playerHealth;
    public bool GetLevelFinished
    {
        get { return levelFinished; }
    }
    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
        levelFinishPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if ( playerHealth.GetHealth <= 0)
        {
            levelFinishPanel.SetActive(true);
            levelFinished = true;
        }else if (enemyCount <= 0)
        {
            gameWinPanel.SetActive(true);
            levelFinishPanel.SetActive(false);
        }
        else
        {
            levelFinishPanel.SetActive(false);
            gameWinPanel.SetActive(false);
            levelFinished = false;

        }

        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        levelFinishPanel.SetActive(false);
        

    }
}
