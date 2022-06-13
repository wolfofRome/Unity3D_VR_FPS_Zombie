using UnityEngine;
using System.Collections;

public class ZombieStateRun : ZombieBase
{
	
    public override void Enter()
    {
        base.Enter();
        StartRun();
    }

    public override void Exit()
    {
        _cscParent.m_nmaAgent.Stop();
        base.Exit();
    }

    private void StartRun()
    {
        _strAnimation = _cscParent.m_sRunName;
        _cscParent.m_animation.Play(_strAnimation);
        _cscParent.m_nmaAgent.SetDestination(_cscParent.m_objPlayer.transform.position);
        _cscParent.m_nmaAgent.speed = _cscParent.m_fRunSpeed;
        _cscParent.m_nmaAgent.angularSpeed = 280;
        _cscParent.m_nmaAgent.Resume();
    }
}
