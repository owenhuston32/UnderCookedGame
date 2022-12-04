using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    private Transform followTransform;
    public Transform FollowTransform { get => followTransform; set => followTransform = value; }

    private void Start()
    {
        StartCoroutine(following());
    }

    private IEnumerator following()
    {
        while (true)
        {
            if(followTransform != null)
            {
                gameObject.transform.localRotation = followTransform.transform.rotation;
                gameObject.transform.position = followTransform.transform.position;
            }
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
