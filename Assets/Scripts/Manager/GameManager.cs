using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Start = 0,
    Map,
    Battle,
}

public class GameManager : MonoBehaviour
{
    public string EnemyName { get; set; }

    string[] greenEnemys = { "GreenEnemy1", "GreenEnemy2", "GreenEnemy3" };
    string[] redEnemys = { "RedEnemy1", "RedEnemy2", "RedEnemy3" };

    string[] bosssName = { "GreenBoss", "GrayBoss", "RedBoss" };

    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = GameObject.Find("Manager");
                if (gameObject == null)
                {
                    return null;
                }
                _instance = gameObject.GetComponent<GameManager>();
            }

            return _instance;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
        UIManager.Instance.UICanvas.gameObject.SetActive(true);
        UIManager.Instance.PushPanel(UIPanelType.Map);
    }

    public void GotoBattle(EnemyType enemyType)
    {
        UIManager.Instance.MapPanel?.gameObject.SetActive(false);
        UIManager.Instance.UICanvas.gameObject.SetActive(false);

        switch (enemyType)
        {
            case EnemyType.Normal:
                int i = UnityEngine.Random.Range(0, 2);
                int j = UnityEngine.Random.Range(0, 3);
                EnemyName = i > 0 ? redEnemys[j] : greenEnemys[j];
                break;
            case EnemyType.Boss:
                EnemyName = bosssName[UnityEngine.Random.Range(0, 3)];
                break;
        }
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu(bool vectory)
    {
        UIManager.Instance.ReSetPanel();
        Player.Instance.ResetPlayer();
        SceneManager.LoadScene(0);
        if (vectory)
            Debug.Log("Vectory!");
        else
            Debug.Log("You Lose!");

    }

    public void NextMap()
    {
        ReturnToMenu(true);
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene(1);
        UIManager.Instance.UICanvas.gameObject.SetActive(true);
        UIManager.Instance.MapPanel?.gameObject.SetActive(true);
    }


}
