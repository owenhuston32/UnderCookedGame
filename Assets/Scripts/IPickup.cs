using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    GameObject Holder { get; set; }

    public void pickup(GameObject holder);
    public void setDown(GameObject obj, GameObject holder);
    public void drop();

}
