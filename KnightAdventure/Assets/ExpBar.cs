using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    public static Slider slider;
    public TextMeshPro textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
        slider.value = 0;
        BotController.OnBotDeath += increaseExp;
    }

    

    public void increaseExp()
    {
        slider.value += 10;
        if (slider.value == 100)
        {
            playerController.maxHealth += 20;
            playerController.attackDamage += 2;
            //playerController.defend += 2;
            slider.value = 0;
            textMeshPro.text = (int.Parse(textMeshPro.text) + 1).ToString();
        }
    }

    
}
