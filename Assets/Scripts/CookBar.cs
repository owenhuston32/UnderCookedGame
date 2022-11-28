using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookBar : MonoBehaviour
{
    [SerializeField] GameObject cookBarParent;
    private float maxXScale = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cookBarParent.transform.localScale = new Vector3(cookBarParent.transform.localScale.x + 0.001f,
            cookBarParent.transform.localScale.y, cookBarParent.transform.localScale.z);
    }
}
