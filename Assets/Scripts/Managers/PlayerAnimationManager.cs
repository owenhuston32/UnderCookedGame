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

    public void SetAnimTrigger(string animTrigger)
    {
        anim.SetTrigger(animTrigger);
    }

    public void SetAnimBool(string animBool, bool val)
    {
        anim.SetBool(animBool, val);
    }

    public void SetHoldingObjAnim(string objTag, bool val)
    {
        anim.SetBool(holdObjTagToAnimParam.GetValueOrDefault(objTag), val);
    }


}
