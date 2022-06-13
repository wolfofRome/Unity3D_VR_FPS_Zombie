using UnityEngine;
using System.Collections;

public class ChartStateOtherWalk : ChartStateBase {
	
	private int _nCurTarget =-1;

	// Use this for initialization
	void Start () {
				
	}
	
	// Update is called once per frame
	void Update () {
		if (_cscParent.m_nmaAgent) 
		{
				if (_cscParent.m_nmaAgent.pathStatus == NavMeshPathStatus.PathComplete && _cscParent.m_nmaAgent.remainingDistance <= 1f) 
				{
					if (!_cscParent.m_bAIWalk)
					{
						_cscParent.ChangeState (ChartStateBase.eCHART_STATE.IDLE);
					} 
					else 
					{
						if (_cscParent.m_bRandomWalk) 
						{
							int _nNewTarget = backRandomNum(_nCurTarget,1,_cscParent.m_listTargetV3.Count);
							_nCurTarget = _nNewTarget;
						} 
						else 
						{
							_nCurTarget++;
							if (_nCurTarget >= _cscParent.m_listTargetV3.Count) 
							{
								_nCurTarget = 1;
							}
						}
						_cscParent.m_nmaAgent.SetDestination (_cscParent.m_listTargetV3[_nCurTarget]);
					}
				}
		}
	}
	public void Enter(int TargetNum)
	{
		_nCurTarget = TargetNum;
		Enter();
	}
	public override void Enter ()
	{
		base.Enter ();
		_strAnimation = _cscParent.m_strAnimationNameWalk;
		_cscParent.m_animation.Play (_strAnimation);
		if (_nCurTarget < 0)
		{
			_nCurTarget =backRandomNum (0,1,_cscParent.m_listTargetV3.Count);
		}
		if (!_cscParent.m_bAIWalk) 
		{
			_nCurTarget = 0;
		}
		_cscParent.m_nmaAgent.SetDestination (_cscParent.m_listTargetV3[_nCurTarget]);
        _cscParent.m_nmaAgent.speed = _cscParent.m_fSpeed;
        _cscParent.m_nmaAgent.Resume ();
	}
	
	public override void Exit ()
	{
        _cscParent.m_nmaAgent.Stop ();
		_cscParent.m_iExWalkTarget = _nCurTarget;
		base.Exit ();
	}

	private int backRandomNum(int outNum,int miniNum,int maxNum)
	{
		int _newNum = Random.Range (miniNum,maxNum);
		if (_newNum == outNum) 
		{
				backRandomNum (outNum,miniNum,maxNum);
		}
		return _newNum;
	}
	
}
