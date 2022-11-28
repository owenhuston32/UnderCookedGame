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
    public void enableCookBar()
    {
        fullCookBar.SetActive(true);
    }
    public void disableCookBar()
    {
        fullCookBar.SetActive(false);
    }
    public void cook()
    {
        isCooking = true;
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
            cookBarParent.transform.localScale = new Vector3(cookBarParent.transform.localScale.x + 0.001f,
    cookBarParent.transform.localScale.y, cookBarParent.transform.localScale.z);

            if (cookBarParent.transform.localScale.x >= maxXScale)
            {
                Debug.Log("finished");
                isFinished = true;
                currentCookBar.GetComponent<MeshRenderer>().material = finishedCookingMaterial;

            }

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
