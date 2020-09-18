using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator MooveAnim;
    float animPosition= 0; // position de l'animation lors d'un nouvel input


    void Start()
    {
        //
    }


    void Update()
    {
        // accéder aux info de l'animator
        AnimatorStateInfo animationState = MooveAnim.GetCurrentAnimatorStateInfo(0);

        // si un input sur 'A' est rencontré
        if (Input.GetKeyDown("a"))
        {
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