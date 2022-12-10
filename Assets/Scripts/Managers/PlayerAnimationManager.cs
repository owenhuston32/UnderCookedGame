using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager
{
    private Dictionary<string, string> holdObjTagToAnimParam = new Dictionary<string, string>
    {
        { StaticStrings.Food, StaticStrings.isHoldingInFront  },
        { StaticStrings.Pan, StaticStrings.isHoldingToSide },
        { StaticStrings.Plate, StaticStrings.isHoldingInFront }

    };


    private Animator anim;

    public PlayerAnimationManager(Animator anim) 
    {
        this.anim = anim;
    }

    public void SetAnimParam(string animParam, bool val)
    {
        anim.SetBool(animParam, val);
    }

    public void SetHoldingObjAnim(string objTag, bool val)
    {
        anim.SetBool(holdObjTagToAnimParam.GetValueOrDefault(objTag), val);
    }


}
