using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour
{
    [SerializeField] Material finishedCookingMaterial;
    [SerializeField] GameObject fullCookBar;
    [SerializeField] GameObject cookBarParent;
    [SerializeField] GameObject currentCookBar;
    private float cookingSpeed = .0005f;
    private bool isCooking = false;
    private bool isFinished = false;
    private float maxXScale = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        disableCookBar();
        if (gameObject.CompareTag("Egg"))
            cookingSpeed = .0005f;
    }
    public int CurrentScore()
    {
        if(cookBarParent.transform.localScale.x / maxXScale < .25f)
        {
            return 0;
        }
        else if(cookBarParent.transform.localScale.x / maxXScale < .5f)
        {
            return 1;
        }
        else if (cookBarParent.transform.localScale.x / maxXScale < .75f)
        {
            return 2;
        }
        else if(cookBarParent.transform.localScale.x / maxXScale < .9f)
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
        fullCookBar.SetActive(false);
    }
    public void cook()
    {
        isCooking = true;
        fullCookBar.SetActive(true);
        StartCoroutine(cooking());
    }
    public void stopCook()
    {
        isCooking = false;
    }
    
    private IEnumerator cooking()
    {
        while(isCooking && !isFinished)
        {
            cookBarParent.transform.localScale = new Vector3(cookBarParent.transform.localScale.x + cookingSpeed,
    cookBarParent.transform.localScale.y, cookBarParent.transform.localScale.z);

            if (cookBarParent.transform.localScale.x >= maxXScale)
            {
                isFinished = true;
                currentCookBar.GetComponent<MeshRenderer>().material = finishedCookingMaterial;

            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
