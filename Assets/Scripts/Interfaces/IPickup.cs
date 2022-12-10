using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    public void Initialize();
    public bool CanPickup { get; set; }
    public IHold PickupHolder { get; set; }
    public GameObject PickupObj { get; }
    public void pickup(IHold newHolder);
    public void setDown(IHold newHolder);
    public void drop();

}
