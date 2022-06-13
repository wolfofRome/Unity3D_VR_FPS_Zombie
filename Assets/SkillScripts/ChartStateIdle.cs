using UnityEngine;
using System.Collections;

public class ChartStateIdle : ChartStateBase {
	

	public override void Enter ()
	{
		base.Enter ();
		_strAnimation = _cscParent.m_strAnimationNameIdle;
		_cscParent.m_animation.Play (_strAnimation);
	}


}
