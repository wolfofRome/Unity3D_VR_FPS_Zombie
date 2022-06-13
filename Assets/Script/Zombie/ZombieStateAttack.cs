using UnityEngine;
using System.Collections;

public class ZombieStateAttack : ZombieBase
{
    private float _fColdTime = 0f;
	// Update is called once per frame
	void Update () {
        if (!_strAnimation.Equals(_cscParent.m_sIdleName))
        {
            if (_cscParent.m_animation[_strAnimation].normalizedTime >=1)
            {
                _fColdTime = _cscParent.m_AttackfreezenTime;
                _strAnimation = _cscParent.m_sIdleName;
                _cscParent.m_animation.Play(_strAnimation);
            }
        }
        else
        {
            if (_fColdTime > 0f)
            {
                _fColdTime -= Time.deltaTime;
            }
            else
            {
                _fColdTime = 0f;
                _strAnimation = getAttackName;
                _cscParent.m_animation.Play(_strAnimation);
            }
        }
	}

    public override void Enter()
    {
        base.Enter();
        _strAnimation = getAttackName;
        _cscParent.m_animation.Play(_strAnimation);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private string getAttackName
    {
        get
        {
            int num = Random.Range(0,2);
			if (_cscParent.m_MyStyle == ZombieController.STYLE.DOG) {
				num = 0;
			}
            if (num == 0)
            {
                return _cscParent.m_sAttack1Name;
            }
            else
            {
                return _cscParent.m_sAttack2Name;
            }
        }
    }


}
