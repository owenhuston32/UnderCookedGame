using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    public bool CanPickup { get; set; }
    public IHold Holder { get; set; }
    public GameObject PickupObj { get; }

    public void pickup(IHold newHolder);
    public void setDown(GameObject obj, IHold newHolder);
    public void drop();

}
