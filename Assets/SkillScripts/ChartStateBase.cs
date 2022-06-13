using UnityEngine;
using System.Collections;

public class ChartStateBase : MonoBehaviour {
	
		public enum eCHART_STATE
		{
				IDLE = 1,
				WALK =2,
				RUN = 3,
				JUMP = 4,
				ATTACK = 5,
				CHASE = 6,
				GETHIT = 7,
				FLY = 8,
				FLY_CHASE = 9,			
		}

		public eCHART_STATE m_eMyState; 
		protected ChartStateController _cscParent; 
		protected string _strAnimation = "";
		protected Vector3 _v3lastPosition = Vector3.zero;
		private Vector3 _v3lastDirection = Vector3.zero;
		private Quaternion _qlastQuaternion = Quaternion.identity;
	//进入状态
		public virtual void Enter ()
		{
			_cscParent = this.gameObject.GetComponent<ChartStateController>();	
		}

	//状态正常执行
		public virtual void Execute ()
		{

		}

	//退出状态
		public virtual void Exit ()
		{
				Destroy (this);
		}


	public Hashtable Hash(params object[] args){
		Hashtable hashTable = new Hashtable(args.Length/2);
		if (args.Length %2 != 0) {
			Debug.LogError("Tween Error: Hash requires an even number of arguments!"); 
			return null;
		}else{
			int i = 0;
			while(i < args.Length - 1) {
				hashTable.Add(args[i], args[i+1]);
				i += 2;
			}
			return hashTable;
		}
	}	
	public void RotateUpdate(GameObject target, Hashtable args){
		CleanArgs(args);

		bool isLocal;
		float time;
		Vector3[] vector3s = new Vector3[4];
		Vector3 preUpdate = target.transform.eulerAngles;

		//set smooth time:
		if(args.Contains("time")){
			time=(float)args["time"];
			time*= 0.05f;
		}else{
			time=0.05f;
		}

		//set isLocal:
		if(args.Contains("islocal")){
			isLocal = (bool)args["islocal"];
		}else{
			isLocal = false;	
		}

		//from values:
		if(isLocal){
			vector3s[0] = target.transform.localEulerAngles;
		}else{
			vector3s[0] = target.transform.eulerAngles;	
		}

		//set to:
		if(args.Contains("rotation")){
			if (args["rotation"].GetType() == typeof(Transform)){
				Transform trans = (Transform)args["rotation"];
				vector3s[1]=trans.eulerAngles;
			}else if(args["rotation"].GetType() == typeof(Vector3)){
				vector3s[1]=(Vector3)args["rotation"];
			}	
		}

		//calculate:
		vector3s[3].x=Mathf.SmoothDampAngle(vector3s[0].x,vector3s[1].x,ref vector3s[2].x,time);
		vector3s[3].y=Mathf.SmoothDampAngle(vector3s[0].y,vector3s[1].y,ref vector3s[2].y,time);
		vector3s[3].z=Mathf.SmoothDampAngle(vector3s[0].z,vector3s[1].z,ref vector3s[2].z,time);

		//apply:
		if(isLocal){
			target.transform.localEulerAngles=vector3s[3];
		}else{
			target.transform.eulerAngles=vector3s[3];
		}

		Vector3 postUpdate=target.transform.eulerAngles;
		target.transform.eulerAngles=preUpdate;
		target.transform.rotation = Quaternion.Euler (postUpdate);
	}

	Hashtable CleanArgs(Hashtable args){
		Hashtable argsCopy = new Hashtable(args.Count);
		Hashtable argsCaseUnified = new Hashtable(args.Count);

		foreach (DictionaryEntry item in args) {
			argsCopy.Add(item.Key, item.Value);
		}

		foreach (DictionaryEntry item in argsCopy) {
			if(item.Value.GetType() == typeof(System.Int32)){
				int original = (int)item.Value;
				float casted = (float)original;
				args[item.Key] = casted;
			}
			if(item.Value.GetType() == typeof(System.Double)){
				double original = (double)item.Value;
				float casted = (float)original;
				args[item.Key] = casted;
			}
		}	

		//unify parameter case:
		foreach (DictionaryEntry item in args) {
			argsCaseUnified.Add(item.Key.ToString().ToLower(), item.Value);
		}	

		//swap back case unification:
		args = argsCaseUnified;

		return args;
	}

	protected void faceTaget()
	{
		if (_v3lastPosition == gameObject.transform.position)
		{
			return;
		}
		_v3lastDirection = gameObject.transform.position - _v3lastPosition;
		_qlastQuaternion = Quaternion.LookRotation(1f*_v3lastDirection, new Vector3(0, 1, 0));
		RotateUpdate (gameObject,Hash("rotation",_qlastQuaternion.eulerAngles,"time",1f));
		_v3lastPosition = gameObject.transform.position;
	}
}
