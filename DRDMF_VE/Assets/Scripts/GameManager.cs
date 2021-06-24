using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region VARIABLES

    // numéro de participant
    public int subjectNumber = 0;
    // Conditions
    public enum Conditions
    {
        Externe,
        Interne
    }
    public Conditions conditionExpe = Conditions.Externe;
    // nombre d'essai pendant les essais actifs
    public int numberOfTrials = 5;
    // temps de l'action requise en seconde
    public int timeActivModeInSeconde = 30;
    // the box
    public GameObject box;
    public GameObject PlayerHead;
    // color for the box
    public Material greenBox;
    public Material greyBox;
    public Material redBox;
    // animator correspondant aux mouvement aller et retour du cube
    public Animator MooveAnim;
    // variable indiquant quand le mode pause (aucune action possible) est activé
    public bool playerPauseState;
    // var indiquant que l'action de pousser le cube est attendu (passage du cube en couleur verte)
    private bool activMode_Action;
    // var indiquant que la variable à changé
    private long timeModeStarted;
    // Nombre d'essai en cour
    private int ActivTrialNumber = 0;
    // Variable permettant d'enregistrer et récupérer la position de l'animation lors d'un nouvel input
    float animPosition = 0;
    // Var recceuillant les logs
    string mylogs = "";

    #endregion

    #region UNITY FUNCTIONS

    // Start is called before the first frame update
    void Start()
    {
        if (conditionExpe == Conditions.Interne)
        {
            PlayerHead.SetActive(false);
        }
        playerPauseState = true;
        box.GetComponent<Renderer>().material = greyBox;
        writeLogs("Init");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!playerPauseState)
        {
            AnimatorStateInfo animationStateInfo = MooveAnim.GetCurrentAnimatorStateInfo(0);

            if (Input.GetKeyDown("e"))
            {
                Debug.Log("HERE");
                AnimIsTriggerd(animationStateInfo);
            }
            if (Input.GetKeyUp("e"))
            {
                AnimIsStopped(animationStateInfo);
            }
            
            if ((DateTimeOffset.Now.ToUnixTimeSeconds() - timeModeStarted) > timeActivModeInSeconde)
            {
                if (ActivTrialNumber < numberOfTrials*2)
                {
                    if (activMode_Action)
                    {
                        Debug.Log("Inhib");
                        setActivInhibitionMode();
                    }
                    else
                    {
                        Debug.Log("Action");
                        setActivActionMode();
                    }
                }
                else
                {
                    Debug.Log("Pause");
                    setPauseMode();
                }
            }
        }
        else if (Input.GetKeyDown("space"))
        {
            if (playerPauseState)
            {
                setActivMode();
            }
        }
    }

    #endregion

    #region FUNCTIONS  

    public void writeLogs(string newlog)
    {
        DateTime currentTime = DateTime.Now;

        int hour = currentTime.Hour;
        int minute = currentTime.Minute;
        int second = currentTime.Second;
        int millisecond = currentTime.Millisecond;
        string recordedEntry = hour.ToString() + ":" + minute.ToString() + ":" + second.ToString() + ":" + millisecond.ToString();
        mylogs = mylogs + "\n" + recordedEntry + "--" + DateTimeOffset.Now.ToUnixTimeSeconds() + "--" + newlog;
        System.IO.File.WriteAllText("MyLogsFile"+ subjectNumber +".txt", mylogs);
    }

    private void setActivMode()
    {
        ActivTrialNumber = 0;
        writeLogs("AM");
        playerPauseState = false;
        setActivActionMode();
    }
    private void setActivActionMode()
    {
        ActivTrialNumber ++;
        timeModeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();
        box.GetComponent<Renderer>().material = greenBox;
        writeLogs("AMA");
        activMode_Action = true;
    }
    private void setActivInhibitionMode()
    {
        ActivTrialNumber ++;
        timeModeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();
        box.GetComponent<Renderer>().material = redBox;
        writeLogs("AMI");
        activMode_Action = false;
    }
    private void setPauseMode()
    {
        ActivTrialNumber=0;
        writeLogs("PM");
        playerPauseState = true;
        box.GetComponent<Renderer>().material = greyBox;
        // accéder aux info de l'animator
        AnimatorStateInfo animationState = MooveAnim.GetCurrentAnimatorStateInfo(0);
        if (animationState.IsName("MoveForward"))
        {
            animPosition = 1 - animationState.normalizedTime;
            if (animPosition < 0)
                animPosition = 0;
            else
                animPosition = 0;
            GetComponent<Animator>().Play("MoveBackward", 0, animPosition);
        }
    }
    private void AnimIsTriggerd(AnimatorStateInfo animationState)
    {
        writeLogs("ta");
        if (animationState.IsName("MoveBackward"))
        {
            // identifier la position de l'animation précédente (nécessité d'inverser la position [avec le 1-position précédente] car les animation sont en miroir)
            animPosition = 1 - animationState.normalizedTime;
            // Si la position est inférieur à 0, initialiser la position à 0 (= position initiale de l'animation qui correspond à la position maximum de la précédente animation)
            if (animPosition < 0)
                animPosition = 0;
        }
        else
            animPosition = 0;
        // Jouer l'animation Forward en initialisant animPosition
        GetComponent<Animator>().Play("MoveForward", 0, animPosition);
    }
    private void AnimIsStopped(AnimatorStateInfo animationState)
    {
        writeLogs("td");
        if (animationState.IsName("MoveForward"))
        {
            animPosition = 1 - animationState.normalizedTime;
            if (animPosition < 0)
                animPosition = 0;
        }
        else
            animPosition = 0;
        GetComponent<Animator>().Play("MoveBackward", 0, animPosition);
    }

    #endregion





}
