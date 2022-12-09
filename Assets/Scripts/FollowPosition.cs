using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    private Transform followTransform;
    private bool isFollowing = false;
    public void setPosition(Transform followTransform)
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        isFollowing = false;
        StartCoroutine(waitFrameThenSetPosition(followTransform));
    }
    private IEnumerator waitFrameThenSetPosition(Transform followTransform)
    {
        yield return new WaitForEndOfFrame();
        gameObject.transform.position = followTransform.transform.position;
    }

    public void startFollowing(Transform followTransform)
    {
        this.followTransform = followTransform;

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        isFollowing = true;
        StartCoroutine(following());

    }

    public void stopFollowing()
    {
        isFollowing = false;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    private IEnumerator following()
    {
        while (isFollowing)
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
