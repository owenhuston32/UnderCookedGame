using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHold
{
    public GameObject CurrentlyHoldingObj { get; set; }
    public Transform HoldPosition { get ; set; }

}
