using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Slider healthSlider;


    private void Awake()
    {
        Instance= this;
    }
}
