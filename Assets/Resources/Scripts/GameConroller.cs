using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;

    [SerializeField] private int mapSize;

    private GameObject[,] cordMap;

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
    }
}
