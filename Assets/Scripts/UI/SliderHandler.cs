using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text textElement;
    [SerializeField] private Slider sliderElement;
    
    // Sets the value of sliderElement to textElement.text
    public void DisplaySliderValue()
    {
        if (textElement != null && sliderElement != null)
        {
            int value = (int)sliderElement.value;
            textElement.text = value.ToString();
        }
    }

    public int GetValue()
    {
        return (int)sliderElement.value;
    }
}
