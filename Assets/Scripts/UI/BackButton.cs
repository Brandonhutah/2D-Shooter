using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructionMenu;

    public void BackToMenu()
    {
        instructionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
