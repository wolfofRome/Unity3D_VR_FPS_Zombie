using UnityEngine;
using System.Collections;

public class ZombieStateGetHit : ZombieBase
{
    public override void Enter()
    {
        base.Enter();
        _strAnimation = getHitName;
        _cscParent.m_animation.Play(_strAnimation);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private string getHitName
    {
        get
        {
            int num = Random.Range(0, 2);
            if (num == 0)
            {
                return _cscParent.m_sHitName;
            }
            else
            {
                return _cscParent.m_sHit2Name;
            }
        }
    }
}
