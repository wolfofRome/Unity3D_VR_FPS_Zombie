using UnityEngine;
using System.Collections;

public class ChartStateChase : ChartStateBase {

	public override void Enter ()
	{
		base.Enter ();
		_strAnimation = _cscParent.m_strAnimationNameChase;
		_cscParent.m_animation.Play (_strAnimation);
	    _cscParent.m_nmaAgent.SetDestination (_cscParent.m_objPlayer.transform.position);
	    _cscParent.m_nmaAgent.speed = _cscParent.m_fChaseSpeed;
	    _cscParent.m_nmaAgent.Resume ();
	}

	public override void Exit ()
	{
	    _cscParent.m_nmaAgent.Stop ();
		base.Exit ();
	}


}
