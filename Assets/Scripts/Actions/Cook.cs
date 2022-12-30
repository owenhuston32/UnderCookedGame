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
        DisableCookBar();
    }
    public int CurrentScore()
    {
        return Mathf.CeilToInt(slider.value * 10) - 2;

    }
    public void DisableCookBar()
    {
        cookbarGameObject.SetActive(false);
    }
    public void StartCooking()
    {
        Debug.Log("cooking");

        regularFoodMesh.SetActive(false);
        cookedFoodMesh.SetActive(true);
        isCooking = true;
        cookbarGameObject.SetActive(true);
        StartCoroutine(Cooking());
        cookingAudio.Play();
    }
    public void StopCook()
    {
        isCooking = false;
        cookingAudio.Stop();
    }
    
    private IEnumerator Cooking()
    {
        Color initialColor = Color.red;
        Color finalColor = Color.green;
        while(isCooking && !isFinished)
        {
            Debug.Log(slider.value);
            slider.value += cookingSpeed;
            float sliderVal = slider.value;
            sliderFillImage.color = Color.Lerp(initialColor, finalColor, sliderVal);

            if (slider.value >= 1)
            {
                isFinished = true;
            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
