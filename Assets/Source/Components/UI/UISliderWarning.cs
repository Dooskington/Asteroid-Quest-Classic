using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderWarning : MonoBehaviour
{
    public GameObject warningObject;
    public float warningPercentage;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        warningObject.SetActive(slider.value <= (slider.maxValue * warningPercentage));
    }
}
