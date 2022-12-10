using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHold
{
    public GameObject CurrentlyHoldingObj { get; set; }
    public GameObject HolderObj { get; }

    public void StartHolding(IHold oldHolder, IPickup obj, Transform followTransform);

    public void StopHolding(IPickup obj);

}
