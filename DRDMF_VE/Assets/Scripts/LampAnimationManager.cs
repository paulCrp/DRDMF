using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampAnimationManager : MonoBehaviour
{
    public GameObject gameManager;
    private string redLogs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            gameManager.GetComponent<GameManager>().writeLogs("RED");
            //writeLogs("RED");
            GetComponent<Animator>().Play("LampAnimation", 0);
        }
    }

    public void writeLogs(string newlog)
    {
        System.DateTime currentTime = DateTime.Now;

        int hour = currentTime.Hour;
        int minute = currentTime.Minute;
        int second = currentTime.Second;
        int millisecond = currentTime.Millisecond;
        string recordedEntry = hour.ToString() + ":" + minute.ToString() + ":" + second.ToString() + ":" + millisecond.ToString();
        redLogs = redLogs + "\n" + recordedEntry + " ----> " + newlog;
        System.IO.File.WriteAllText("MyredLogsFile.txt", redLogs);
    }


}
