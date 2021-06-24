# DRDMF
Virtual environment with Emotiv Epoq interaction for phantom limb pain remediation 

## Info
In this repository you can find a virtual environment (VE) for remediation of phantom limb pain. This work is based on the mirror box therapy in VE and the association of virtual reality and BCI. This project is carried by Paul Crépin and the UBO OpenFactory. Is also supported by the master of psychology CAER (Cognition, learning, evaluation and remediation : virtual reality & simulation) of the UBO (University of Occidental Brittany).

## Arch

- Branch master : VE version 1.3
- Branch develop : VE version 2.0 in progress
- Branch experimental : VE for experimentation in lab

## Setup

- Unity 3D
- Emotiv Epoq
- Xavier-EmotivControlPanel software
- Xavier-Emokey software

## Use

First of all you need to calibrate a mental command in the Xavier-EmotivControlPanel software and train you to trigger it. Select the "push" command for a more natural feeling with the VE.
After, you need to use the Xavier-Emokey software to send the command to unity.
- Create a new rule by clicking "Add rule"

![Screenshot](https://github.com/paulcrepin/DRDMF/blob/master/_readme/XEK1.PNG)

- Then double click on it to attribute a key to triger the mental command (! even with an AZERTY keyboard the software stay in QWERTY mode). The key by default in the VE to launch the animation is "e". Check the checkbox "Hold the key" and click on "Apply" button.

![Screenshot](https://github.com/paulcrepin/DRDMF/blob/master/_readme/XEK2.PNG)
 
- After, you need to set a condition by clicking on "Add condition" button of the Xavier-Emokey interface. Double click on the condition and in the new window select the mental command you perform.

![Screenshot](https://github.com/paulcrepin/DRDMF/blob/master/_readme/XEK3.PNG)

Go to unity, and click on start button. You're on, you can trigger the animation with you're neural activity.

## Releases

A version with calibration and training directly in VE and interaction with Emotiv Cortex API V2 will be released soon.

## Note

We advise to not use Oculus Rift S VR headset because combining Emotiv Epoq with it is not  easy. For exemple, we use an old Oculus DK2.

## Experimental version

The GameManager script allow you to manage some variables :
  - You can choose a subject number ([int] number only)
  - You can choose the experimental condition (change the point of view from internal to external)
  - You can select the number of trial the participant had to do (a trial is composed of an alternance of 1 activ session and 1 passiv session)
  - You can select the time of a session (in seconds)
    For exemple : if you choose 3 trial and 10 seconds per sessions, the participant need to do 6 session of 10 seconds each.
 
 The commands are :
  - "spacebar" : start the trial
  - "f" on keyboard : the desk lamp fall on the right arm of the avatar
  - "e" on keyboard : play box push animation (This keys need to be use with Xavier-Emokey interface to trigger animation with mental command)
 
 
