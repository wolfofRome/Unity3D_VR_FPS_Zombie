using UnityEngine;
using System.Collections;

public class ZombieStateRoar : ZombieBase
{
    public override void Enter()
    {
        base.Enter();
        _strAnimation = _cscParent.m_sRoarName;
        _cscParent.m_animation.Play(_strAnimation);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
