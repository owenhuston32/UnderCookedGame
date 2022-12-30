using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHold
{
    public GameObject CurrentlyHoldingObj { get; set; }
    public GameObject HolderObj { get; }

    public void StartHolding(IHold oldHolder, GameObject pickupObj, Transform followTransform);

    public void StopHolding(GameObject pickupObj);

}
