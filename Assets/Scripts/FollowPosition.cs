using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    private Transform followObj;
    public Transform FollowObj { get => followObj; set => followObj = value; }

    private void Start()
    {
        StartCoroutine(following());
    }

    private IEnumerator following()
    {
        while (true)
        {
            if(followObj != null)
            {
                gameObject.transform.localRotation = followObj.transform.rotation;
                gameObject.transform.position = followObj.transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
