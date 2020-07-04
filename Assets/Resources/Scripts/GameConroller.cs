using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConroller : MonoBehaviour
{
    #region Variables

    [SerializeField] public GameObject[] obj;

    [SerializeField] private int mapSize;

    private GameObject[,] cordMap;

    private static GameConroller insnatce;

    public static GameConroller Insnatce { get => insnatce; set => insnatce = value; }

    public bool appleBonus;

    #endregion 

    private void Awake()
    {
        insnatce = this;
    }

    private void MapGeterator()
    {
        for (int i = 0; i <= mapSize; i++)
        {
            for(int j = 0; j <= mapSize; j++)
            {
                if(i == 0 || i == mapSize || j == 0 || j == mapSize)
                {
                    cordMap[i, j] = (GameObject)Instantiate(obj[1], new Vector3(i, j, 0), Quaternion.identity);
                }
                else{
                    cordMap[i, j] = (GameObject)Instantiate(obj[0], new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }

        int wallCount = Random.Range(3, 5);

        for(int i = 0; i<=wallCount; i++)
        {
            int xRbdCord = Random.Range(0, mapSize);

            int yRbdCord = Random.Range(0, mapSize);

            if(cordMap[xRbdCord, yRbdCord] == obj[1])
            {
                i--;
            }
            else
            {
                Destroy(cordMap[xRbdCord, yRbdCord]);

                cordMap[xRbdCord, yRbdCord] = (GameObject)Instantiate(obj[1], new Vector3(xRbdCord, yRbdCord, 0), Quaternion.identity);
            }
        }
    }
    private void Start()
    {
        cordMap = new GameObject[mapSize+1, mapSize+1];

        MapGeterator();

        Instantiate(obj[3], new Vector3(mapSize/2, mapSize/2, 0), Quaternion.identity);

        AppleSpawn();
    }

    public void AppleSpawn()
    {
        int h = 0;

        while(h == 0)
        {
            int xRbdCord = Random.Range(0, mapSize);

            int yRbdCord = Random.Range(0, mapSize);

            if (cordMap[xRbdCord, yRbdCord].CompareTag("Ground"))
            {
                Destroy(cordMap[xRbdCord, yRbdCord]);

                cordMap[xRbdCord, yRbdCord] = (GameObject)Instantiate(obj[2], new Vector3(xRbdCord, yRbdCord), Quaternion.identity);

                h++;
            }
        }
    }

    public void CheckSnakePosition()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("Snake");

        int xSnakePos = (int)Mathf.Round(snake.transform.position.x/1);

        int ySnakePos = (int)Mathf.Round(snake.transform.position.y / 1);

        Vector3 snakePosV3 = new Vector3(xSnakePos, ySnakePos, 0);

        if (snakePosV3 == cordMap[xSnakePos, ySnakePos].transform.position)
        {
            if (cordMap[xSnakePos, ySnakePos].CompareTag("Wall"))
                GameOver();

            if (cordMap[xSnakePos, ySnakePos].CompareTag("Apple"))
            {
                AppleSpawn();

                Destroy(cordMap[xSnakePos, ySnakePos]);

                cordMap[xSnakePos, ySnakePos] = (GameObject)Instantiate(obj[0], new Vector3(xSnakePos, ySnakePos, 0), Quaternion.identity);

                appleBonus = true;
            }
        }
    }

    public void GameOver()
    {
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
