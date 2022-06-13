using UnityEngine;
using System.Collections;

public class ChartStateGetHit : ChartStateBase {

	private bool _bPlaying = false;
	private float _fUseCutTime = 0f;
	private float _viewAngle = 50f;
	private Vector3 _newTarget;

	public override void Enter ()
	{
		base.Enter ();
		_v3lastPosition = gameObject.transform.position;
		_strAnimation = _cscParent.m_strAnimationNameGetHit;
		_cscParent.m_animation.Play (_strAnimation);
		setTarget ();
		_bPlaying = true;

		if (!_cscParent.m_bCanfly&&_cscParent.m_bGetHitMove) {
			_cscParent.m_nmaAgent.SetDestination (_newTarget);
			_cscParent.m_nmaAgent.speed = _cscParent.m_fChaseSpeed;
			_cscParent.m_nmaAgent.Resume ();
		}
	}

	public void Update()
	{
		if (!_bPlaying) {
			switch (_cscParent.m_eGetHitBack) {
			case ChartStateController.GETHIT_BACK.BACK_ATTACK:
				_cscParent.ChangeState (ChartStateBase.eCHART_STATE.ATTACK);
				break;
			case ChartStateController.GETHIT_BACK.BACK_BACK:
				_cscParent.ChangeState (1);
				break;
			case ChartStateController.GETHIT_BACK.BACK_WALK:
				_cscParent.ChangeState (_cscParent.m_iExWalkTarget);
				break;
			}
		}
		else
		{
			if (_cscParent.m_bCanfly&&_cscParent.m_bGetHitMove) {
				flyToTarget ();
				faceTaget ();		
			}

			_fUseCutTime += Time.deltaTime;
			if (_fUseCutTime >= _cscParent.m_fGetHitTime) {
				_bPlaying = false;
			}
		}
	}

	private void flyToTarget()
	{
		this.transform.position = Vector3.Slerp(this.transform.position,_newTarget,Time.deltaTime*_cscParent.m_fChaseSpeed);
		if (Mathf.Abs (Vector3.Distance (this.transform.position, _newTarget))<0.1f) {
			_bPlaying = false;
		}
	}

	private void setTarget()
	{
		int randomNum = Random.Range (0,2);
		Quaternion newR = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - ((randomNum==0)?-_viewAngle:_viewAngle), transform.rotation.eulerAngles.z);
		_newTarget = transform.position + ((newR * Vector3.forward) * _cscParent.m_fnewTargetDistance);
	}


}
