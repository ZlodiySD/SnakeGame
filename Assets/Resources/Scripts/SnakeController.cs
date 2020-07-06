using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    #region Variables

    private GameObject[] tailArray = new GameObject [11];

    private float speed;

    private Vector3 direction;

    private Vector3 destPos;

    private Vector3 prevPosStorage;

    private Vector3 prevTailPos;

    private bool canMoving;

    private float angle = 0;

    private bool canRotate;

    private int tailLenght = 1;
    #endregion

    void Start()
    {
        canMoving = true;

        speed = 2f;

        canRotate = true;

        StartCoroutine(Coroutine());

        //tailArray[0] = this.gameObject;

        tailArray[0] = (GameObject)Instantiate(GameConroller.Insnatce.obj[4], new Vector3(10, 9, 0), Quaternion.identity);
    }
    
    void Update()
    {
        if (tailLenght >= 10)
        {
            canMoving = false;

            canRotate = false;

            HUD.Instance.ShowWonWindow(true);
        }

        SnakeRotation();
    }

    private void SnakeRotation()
    {
        if (HUD.Instance.changeDir == -1 && canRotate)
        {
            angle += 90;

            transform.localRotation = Quaternion.Euler(0, 0, angle);

            canRotate = false;

            HUD.Instance.changeDir = 0;
        }

        if (HUD.Instance.changeDir == 1 && canRotate)
        {
            angle -= 90f;

            transform.localRotation = Quaternion.Euler(0, 0, angle); 

            canRotate = false;

            HUD.Instance.changeDir = 0;
        }
    }

    private void SnakeMove()
    {
        prevPosStorage = transform.position;

        prevTailPos = prevPosStorage;

        transform.position = transform.up + transform.position;
        
        for (int i = 0; i < tailLenght; i++)
        {
            if (i == 0)
            {
                prevTailPos = tailArray[i].transform.position;

                tailArray[i].transform.position = prevPosStorage;
            }
            else if (i>0)
            {
                prevPosStorage = tailArray[i].transform.position;

                tailArray[i].transform.position = prevTailPos;

                prevTailPos = prevPosStorage;
            }

            if (GameConroller.Insnatce.appleBonus)
            {
                AddTail(prevPosStorage);

                tailLenght++;

                speed += 0.1f;

                GameConroller.Insnatce.appleBonus = false;
            }
            if (tailArray[i].transform.position == transform.position)
                GameConroller.Insnatce.RestartLevel();
        }
    }

    private void AddTail(Vector3 pos)
    {
        tailArray[tailLenght] = (GameObject)Instantiate(GameConroller.Insnatce.obj[4], pos, Quaternion.identity);
    }

    IEnumerator Coroutine()
    {
        while (canMoving)
        {

            GameConroller.Insnatce.CheckSnakePosition();

            yield return new WaitForSeconds(1/speed);

            SnakeMove();

            canRotate = true;
        }
    }
}
