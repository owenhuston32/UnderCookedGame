using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cook : MonoBehaviour
{
    [SerializeField] AudioSource cookingAudio;
    [SerializeField] GameObject cookedFoodMesh;
    [SerializeField] GameObject regularFoodMesh;
    [SerializeField] Image sliderFillImage;
    [SerializeField] Slider slider;
    [SerializeField] GameObject cookbarGameObject;
    [SerializeField] private float cookingSpeed = .0005f;
    private bool isCooking = false;
    private bool isFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        disableCookBar();
    }
    public int CurrentScore()
    {
        return Mathf.CeilToInt(slider.value * 10) - 2;

    }
    public void disableCookBar()
    {
        cookbarGameObject.SetActive(false);
    }
    public void cook()
    {
        regularFoodMesh.SetActive(false);
        cookedFoodMesh.SetActive(true);
        isCooking = true;
        cookbarGameObject.SetActive(true);
        StartCoroutine(cooking());
        cookingAudio.Play();
    }
    public void stopCook()
    {
        isCooking = false;
        cookingAudio.Stop();
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
