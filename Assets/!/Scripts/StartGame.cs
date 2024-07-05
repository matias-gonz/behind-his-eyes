using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    

    private Transform UI, task, GunPanel;
    private Transform player;
    private Text taskName, taskContent,bulletNum;
    private bool up, down, right, left,isJump, isCrouched, isCrawl;
    public static bool isPickUp;
    private int taskIndex=0;
    private int bltNum = 30;


    // Start is called before the first frame update
    void Start()
    {
        InitValue();
        InitEvent();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTaskIsCompulish();
        CurrentTask();
        GameOver();
    }

    private void InitValue()
    {
        player = GameObject.Find("Player").transform;
        UI = GameObject.Find("Canvas").transform;
        task = UI.Find("MainPanel/Task");
        GunPanel = UI.Find("MainPanel/GunPanel");
        taskName = task.Find("TaskName").GetComponentInChildren<Text>();
        taskContent = task.Find("TaskContent").GetComponentInChildren<Text>();
        bulletNum = GunPanel.Find("Image/BulletNum").GetComponent<Text>();
    }

    private void InitEvent()
    {
        up = false;
        down = false;
        right = false;
        left = false;
        isJump = false;
        isCrouched = false;
        isCrawl = false;
        bulletNum.text = bltNum.ToString();
        GunPanel.gameObject.SetActive(false);
    }

    private void CurrentTask()
    {
        taskName.text = "Task Name" + taskIndex + "/" + 6;
        switch (taskIndex)
        {
            case 0:
                player.GetComponent<TwoDimensionalAnimationStateController>().enabled = false;
                taskContent.text = "Task Content";
                break;
            case 1:
                player.GetComponent<TwoDimensionalAnimationStateController>().enabled = true;
                taskContent.text = "Task1";
                break;
            case 2:
                taskContent.text = "Task2";
                break;
            case 3:
                taskContent.text = "Task3";
                break;
            case 4:
                taskContent.text = "Task4";
                break;
            case 5:
                taskContent.text = "Task5";
                break;
            default:
                break;
        }
    }

    private void CurrentTaskIsCompulish()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (taskIndex==0)
        {
            if (mouseX>2)
            {
                right = true;
            }
            if (mouseX < -2)
            {
                left = true;
            }
            if (mouseY < -2)
            {
                down = true;
            }
            if (mouseY > 2)
            {
                up = true;
            }
            if (right&&left&&up&&down)
            {
                Debug.Log(taskIndex);
                right = false; left = false; up = false; down = false;
                   taskIndex++;
            }
        }
        else if (taskIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                right = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                left = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                down = true;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                up = true;
            }
            if (right && left && up && down)
            {
                Debug.Log(taskIndex);
                right = false; left = false; up = false; down = false;
                taskIndex++;
            }
        }
        else if (taskIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space)) isJump = true;
            Debug.Log(isJump);
            Debug.Log(player.transform.position.z);
            if (player.transform.localPosition.x>64&&isJump)
            {
                Debug.Log("jump=" + isJump);
                taskIndex++;
            }
        }
        else if (taskIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.C)) isCrouched = true;
            if (player.transform.localPosition.x > 104 && isCrouched)
            {
                taskIndex++;
            }
        }
        else if (taskIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.X)) isCrawl = true;
            if (player.transform.localPosition.x > 141 && isCrawl)
            {
                taskIndex++;
            }
        }
        else if (taskIndex == 5)
        {
            taskIndex++;
        }
    }

    private void GameOver()
    {
        if (taskIndex==6)
        {

        }
    }
}
