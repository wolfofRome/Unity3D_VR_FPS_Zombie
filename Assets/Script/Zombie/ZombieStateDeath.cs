using UnityEngine;
using System.Collections;

public class ZombieStateDeath : ZombieBase
{
    public override void Enter()
    {
        base.Enter();
        _strAnimation = _cscParent.m_sDeathName;
        _cscParent.m_animation.Play(_strAnimation);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
