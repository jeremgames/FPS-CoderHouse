using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Slider healthSlider;
    public Slider ammoSlider;

    public GameObject pauseScreen;

    private void Awake()
    {
        Instance= this;
    }
}
