using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class Interactor : MonoBehaviour
{

    [SerializeField]
    private GameObject visualIndicator;
    private GameObject upgradeMenu;



    private void OnMouseDown()
    {
        visualIndicator.SetActive(true);
        upgradeMenu.SetActive(true);
    }


}
