using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHold
{
    public GameObject CurrentlyHoldingObj { get; set; }
    public Transform[] HoldPositions { get ; }
    public GameObject HolderObj { get; }

    public void StartHolding(IHold oldHolder, IPickup obj);

    public void StopHolding(IPickup obj);

}
