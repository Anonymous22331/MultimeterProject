using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultimeterTrigger : MonoBehaviour
{
    public MultimeterController multimeter; // Ссылка на другой объект

    private void OnMouseEnter()
    {
        multimeter.SetMultimeterWorking(true);
    }

    private void OnMouseExit()
    {
        multimeter.SetMultimeterWorking(false);
    }
}
