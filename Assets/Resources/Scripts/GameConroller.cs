using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    [SerializeField] public GameObject[] obj;

    [SerializeField] private int mapSize;

    private GameObject[,] cordMap;

    private static GameConroller insnatce;

    public static GameConroller Insnatce { get => insnatce; set => insnatce = value; }

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
    }

    public void CheckSnakePosition()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("Snake");

        int xSnakePos = (int)snake.transform.position.x;

        int ySnakePos = (int)snake.transform.position.y;

        if (snake.transform.position == cordMap[xSnakePos, ySnakePos].transform.position)
        {
            if (cordMap[xSnakePos, ySnakePos].CompareTag("Wall"))
                GameOver();
        }


    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
