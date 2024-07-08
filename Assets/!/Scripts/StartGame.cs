using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Transform UI, task;
    private Transform player;
    private Text taskName, taskContent;
    private bool up, down, right, left,isJump, isCrouched, isCrawl;
    public static bool isPickUp;
    private int taskIndex;


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
    }

    private void InitValue()
    {
        player = GameObject.Find("Player").transform;
        UI = GameObject.Find("Canvas").transform;
        task = UI.Find("MainPanel/Task");
        taskName = task.Find("TaskName").GetComponentInChildren<Text>();
        taskContent = task.Find("TaskContent").GetComponentInChildren<Text>();
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
        taskIndex = 0;
    }

    private void CurrentTask()
    {
        taskName.text = "Task not completed" + taskIndex + "/" + 7;
        switch (taskIndex)
        {
            case 0:
                taskContent.text = "Move your mouse to look around";
                break;
            case 1:
                taskContent.text = "Now press WASD to move around";
                break;
            case 2:
                taskContent.text = "Press Space to jump";
                break;
            case 3:
                taskContent.text = "Press left shift to crouch";
                break;
            case 4:
                taskContent.text = "Press shift+space to jump across a pit,only walk and jump is not enough ";
                break;
            case 5:
                taskContent.text = "press C and WASD to crawl";
                break;
            case 6:
                taskContent.text = "shoot";
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
                right = false; left = false; up = false; down = false;
                taskIndex++;
            }
        }
        else if (taskIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space)) isJump = true;
            if (player.transform.localPosition.x>57&&isJump)
            {
                taskIndex++;
            }
        }
        else if (taskIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.X)) isCrawl = true;
            if (player.transform.localPosition.x > 90 && isCrawl)
            {
                taskIndex++;
            }
        }
        else if (taskIndex == 4)
        {
            if (player.transform.localPosition.x > 117.5f)
            {
                taskIndex++;
            }
            else if (player.transform.localPosition.y < 47)
            {
                UI.Find("MainPanel/Black").gameObject.SetActive(true);
                player.localPosition = new Vector3(100, 55, 85);
                player.Rotate(new Vector3(0, 90, 0));
                StartCoroutine(ReStart());
            }
        }
        else if (taskIndex == 5)
        {
            if (Input.GetKeyDown(KeyCode.C)) isCrouched = true;
            if (player.transform.localPosition.x > 140 && isCrouched)
            {
                taskIndex++;
            }
            if (player.transform.localPosition.y < 46.5f)
            {
                UI.Find("MainPanel/Black").gameObject.SetActive(true);
                player.localPosition = new Vector3(120, 55, 85);
                player.Rotate(new Vector3(0, 90, 0));
                StartCoroutine(ReStart());
            }
        }
        
        else if (taskIndex == 6)
        {
            taskIndex++;
        }
    }


    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(1);
        UI.Find("MainPanel/Black").gameObject.SetActive(false);
    }
}
