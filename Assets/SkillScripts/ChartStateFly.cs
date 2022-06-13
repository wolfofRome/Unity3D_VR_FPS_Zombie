using UnityEngine;
using System.Collections;

public class ChartStateFly : ChartStateBase {

	private int _nCurTarget = -1;
	// Use this for initialization
	void Start () {
		_v3lastPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (_nCurTarget > 0) 
		{
			flyToTaget ();
			faceTaget ();
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
		if (_nCurTarget < 0) {
			_nCurTarget = 1;
		}
	}

	public override void Exit ()
	{
		_cscParent.m_iExWalkTarget = _nCurTarget+6;
		base.Exit ();
	}

	private void flyToTaget()
	{
		if (Mathf.Abs (Vector3.Distance (this.transform.position, _cscParent.m_listTargetV3ToFly [_nCurTarget])) <= 0.1f) 
		{
			_nCurTarget++;
			if (_nCurTarget+2 >= _cscParent.m_listTargetV3ToFly.Count) {
				_nCurTarget = 1;
			}
		}
		else 
		{
			this.transform.position = Vector3.MoveTowards (this.transform.position,_cscParent.m_listTargetV3ToFly[_nCurTarget],Time.deltaTime*_cscParent.m_fSpeed);
		}
	}
}
