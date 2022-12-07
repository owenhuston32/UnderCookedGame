using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    [SerializeField] Image sliderFillImage;
    [SerializeField] Slider slider;
    [SerializeField] GameObject cookbarGameObject;
    private float cookingSpeed = .0005f;
    private bool isCooking = false;
    private bool isFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        disableCookBar();
    }
    public int CurrentScore()
    {
        if(slider.value < .25f)
        {
            return 0;
        }
        else if(slider.value < .5f)
        {
            return 1;
        }
        else if (slider.value < .75f)
        {
            return 2;
        }
        else if(slider.value < .9f)
        {
            return 3;
        }
        else
        {
            return 4;
        }

    }
    public void disableCookBar()
    {
        cookbarGameObject.SetActive(false);
    }
    public void cook()
    {
        isCooking = true;
        cookbarGameObject.SetActive(true);
        StartCoroutine(cooking());
    }
    public void stopCook()
    {
        isCooking = false;
    }
    
    private IEnumerator cooking()
    {
        Color initialColor = Color.red;
        Color finalColor = Color.green;
        while(isCooking && !isFinished)
        {

            slider.value += cookingSpeed;
            sliderFillImage.color = Color.Lerp(initialColor, finalColor, slider.value);

            if (slider.value >= 1)
            {
                isFinished = true;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
