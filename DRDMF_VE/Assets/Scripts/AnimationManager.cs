using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // the box
    public GameObject theBox;
    // color for the box
    public Material greenBox;
    public Material greyBox;
    public Material redBox;
    // animator correspondant aux mouvement aller et retour du cube
    public Animator MooveAnim;
    // variable indiquant quand le mode pause (aucune action possible) est activé
    public bool playerPauseState;
    // variable indiquant quand le mode passif (simple observation du mouvement sans interaction) est activé
    public bool playerPassivState;
    // varriable permettant de compter le nombre d'animation joué lors du mode passif
    private int passivmoov_counter;
    // var indiquant que l'action de pousser le cube est attendu (passage du cube en couleur verte)
    private bool activMode_Action;
    // Variable permettant d'enregistrer et récupérer la position de l'animation lors d'un nouvel input
    float animPosition= 0;
    // Var recceuillant les logs
    string mylogs = "";
    

    void Start()
    {
        //
        playerPauseState = true;
        theBox.GetComponent<Renderer>().material = greyBox;
        writeLogs("Initialisation de session");
    }

    void Update()
    {
        // Si appui sur la touche p >> changement entre 3 modes (passif/pause/actif/pause/passif...)
        if (Input.GetKeyDown("p"))
        {
            if (playerPauseState)
            {
                if (playerPassivState)
                {
                    writeLogs("__ActivMode__");
                    playerPauseState = false;
                    playerPassivState = false;
                    theBox.GetComponent<Renderer>().material = redBox;
                    writeLogs(" :: > Inhibition");
                    activMode_Action = false;
                }
                else
                {
                    writeLogs("__PassiveMode__");
                    playerPauseState = false;
                    playerPassivState = true;
                    passivmoov_counter = 0;
                }
            }
            else
            {
                writeLogs("__PauseMode__");
                playerPauseState = true;
                theBox.GetComponent<Renderer>().material = greyBox;
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
        }

        // Si le mode pause n'est pas actif
        if (!playerPauseState)
        {
            // accéder aux info de l'animator
            AnimatorStateInfo animationState = MooveAnim.GetCurrentAnimatorStateInfo(0);

            // si le mode passif est activé
            if (playerPassivState)
            {

                if (animationState.IsName("New State"))
                {
                    if (passivmoov_counter < 20)
                    {
                        // Jouer l'animation Forward en initialisant animPosition
                        GetComponent<Animator>().Play("PassivAnimation", 0);
                        passivmoov_counter += 1;
                    }
                    else
                    {
                        playerPauseState = true;
                    }
                }
            }
                
            // sinon si le mode actif est activé
            else
            {
                if (Input.GetKeyDown("c"))
                {
                    if (activMode_Action)
                    {
                        theBox.GetComponent<Renderer>().material = redBox;
                        activMode_Action = false;
                        writeLogs(" :: > Inhibition");
                    }
                    else
                    {
                        theBox.GetComponent<Renderer>().material = greenBox;
                        activMode_Action = true;
                        writeLogs(" :: > Action");
                    }
                }
                // si un input sur 'A' est rencontré
                if (Input.GetKeyDown("a"))
                {
                    writeLogs(" < Trigger activated >");
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

                // Si la touche 'A' est relaché (structure idem que précédemment)
                if (Input.GetKeyUp("a"))
                {
                    writeLogs(" < Trigger desactivated >");
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
            }



        }
        
        

        
    }


    /// <summary>
    /// ICI faire la fonction qui réduit et rend le code propre.
    /// </summary>
    /// <param name="animationInProgress"></param>
    /// <returns></returns>
    private int findAnimPosition (AnimatorStateInfo animationInProgress)
    {
        return 0;
    }

    // MAJ du fichier LOG
    public void writeLogs(string newlog)
    {
        System.DateTime currentTime = DateTime.Now;
        
        int hour = currentTime.Hour;
        int minute = currentTime.Minute;
        int second = currentTime.Second;
        int millisecond = currentTime.Millisecond;
        string recordedEntry = hour.ToString() + ":" + minute.ToString() + ":" + second.ToString() + ":" + millisecond.ToString();
        mylogs = mylogs + "\n" + recordedEntry + " ----> " + newlog;
        System.IO.File.WriteAllText("MyLogsFile.txt", mylogs);
    }
}