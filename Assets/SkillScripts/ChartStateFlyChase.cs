using UnityEngine;
using System.Collections;

public class ChartStateFlyChase: ChartStateBase {
	// Use this for initialization
	void Start () {
		_v3lastPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update () {
		flyToTaget ();
		faceTaget ();
	}

	public override void Enter ()
	{
		base.Enter ();
		_strAnimation = _cscParent.m_strAnimationNameWalk;
		_cscParent.m_animation.Play (_strAnimation);

	}

	public override void Exit ()
	{
		
		base.Exit ();
	}

	private void flyToTaget()
	{
		if (Mathf.Abs (Vector3.Distance (this.transform.position, _cscParent.m_objPlayer.transform.position)) < _cscParent.m_fMyWith) 
		{
			return;
		}
		else 
		{
			this.transform.position = Vector3.MoveTowards (this.transform.position,_cscParent.m_objPlayer.transform.position,Time.deltaTime*_cscParent.m_fChaseSpeed);
		}
	}

}
