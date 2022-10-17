using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructionMenu;

    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        instructionMenu.SetActive(true);
    }
}
