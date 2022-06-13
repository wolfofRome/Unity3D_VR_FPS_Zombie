using UnityEngine;
using System.Collections;

public class ZombieStateWalk : ZombieBase
{
    public override void Enter()
    {
        base.Enter();
        _strAnimation = _cscParent.m_sWalkName;
        _cscParent.m_animation.Play(_strAnimation);
        _cscParent.m_nmaAgent.SetDestination(_cscParent.m_objPlayer.transform.position);
        _cscParent.m_nmaAgent.speed = _cscParent.m_curWalkSpeed;
        _cscParent.m_animation[_strAnimation].speed = _cscParent.m_nmaAgent.speed / 2f;
        _cscParent.m_nmaAgent.angularSpeed = 80;
        _cscParent.m_nmaAgent.Resume();
    }

    public override void Exit()
    {
        _cscParent.m_nmaAgent.Stop();
        base.Exit();
    }
}
