using UnityEngine;
using System.Collections;

public class ZombieStateHeadShot : ZombieBase
{
    public override void Enter()
    {
        base.Enter();
        _strAnimation = _cscParent.m_sHeadshotName;
        _cscParent.m_animation.Play(_strAnimation);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
