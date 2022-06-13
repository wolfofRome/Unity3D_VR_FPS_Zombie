using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChartStateController : MonoBehaviour {

	public Animation m_animation;			//动画
	public string m_strAnimationNameIdle = "idle";         //站立    填入动画名称
	public string m_strAnimationNameWalk = "Walk";         //行走----飞行动物则填入飞行动画
	public string m_strAnimationNameAttack = "Attack";     //攻击
	public string m_strAnimationNameChase = "Run";         //追捕
	public string m_strAnimationNameGetHit = "GetHit";     //受击

	public float m_fMyHight = 2f;			//角色高度
	public float m_fMyWith = 0.5f;			//角色宽度（半径）
	public float m_fHightCenter = 0f;		//高度中心点位移，0，中间，-往下
	public float m_fSpeed = 3f;				//运动速度
	public float m_fChaseSpeed = 6f;		//追逐速度
	public bool m_bCanfly = false;          //是否飞行
	public bool m_bRandomWalk = false;      //是否随机在几个巡视点行走，false则按路径从1到n行走
	public bool m_bAIWalk = true;			//是否有巡视功能
	public float m_fChaseRadius = 15f;      //敌人进入此半径内，则开始追击模式，为0则无此模式
	public float m_fMoveRadius = 30f;		//出此范围则放弃追击，如果为0则为无限追击
	public float m_fAttackDistance = 3f;    //敌人进入此范围进入攻击状态，为0则不主动攻击
    public bool m_bMoveWithCenter = false;  //是否跟随中心点的位移而位移
	public bool m_bGetHit = false;          //是否有受击状态
	public GETHIT_BACK m_eGetHitBack = GETHIT_BACK.BACK_WALK; //受击或被触摸后的状态
	public bool m_bGetHitMove = false;      //是否受击自动躲避
	public float m_fGetHitSpeed = 5f;       //受击躲避时速度
	public float m_AttackTime = 0f;			//攻击时间，为0则一直攻击，直到敌人离开攻击区域
	public float m_AttackfreezenTime = 0f;  //冷却时间，攻击后是否冷却返回，为0则继续进入备战
    public float m_fnewTargetDistance = 3f; //躲避时目标距离
    public float m_fGetHitTime = 5f;        //躲避时间
    public GameObject m_objPlayer;          //敌人

	[HideInInspector]
	public int m_iTargetCount = 1;			//几个运动目标点

	[HideInInspector]
	public List<Vector3> m_listTargetV3 = new List<Vector3>(); 
	[HideInInspector]
	public List<Vector3> m_listTargetV3ToFly = new List<Vector3> ();
    [HideInInspector]
    public List<GameObject> m_listTargetObj = new List<GameObject>();

	[HideInInspector]
	public ChartStateBase m_eCurState;
	private NavMeshAgent _nmaAgent;
    public NavMeshAgent m_nmaAgent
	{
		get
		{
			if (!_nmaAgent) 
			{
				_nmaAgent = gameObject.GetComponent<NavMeshAgent> ();
				if (!_nmaAgent)
				{
					_nmaAgent = gameObject.AddComponent<NavMeshAgent> ();
					_nmaAgent.stoppingDistance = 0.05f;
					_nmaAgent.angularSpeed = 200f;
					_nmaAgent.height = m_fMyHight;
					_nmaAgent.radius = m_fMyWith;
					_nmaAgent.baseOffset = m_fHightCenter;
					_nmaAgent.autoRepath = true;
				}
			}
			return _nmaAgent;
		}
	}
	public Vector3 m_v3Enemy
	{
		get
		{
			if (m_objPlayer) {
				if (m_bCanfly) {
					return m_objPlayer.transform.position;
				}
				else {
					
				}
			}
			return m_listTargetV3[0];
		}
	}
	public enum GETHIT_BACK
	{
		BACK_WALK,
		BACK_ATTACK,
		BACK_BACK,
	}
	[HideInInspector]
	public int m_iExWalkTarget = 1;

    void Awake()
    {
		/*
		if (m_bCanfly) {
			if (m_listTargetV3.Count > 1) {
				List<Vector3> tagetV3list = new List<Vector3> ();
				tagetV3list.Clear ();
				for (int i = 1; i < m_listTargetV3.Count; i++) {
					tagetV3list.Add (m_listTargetV3 [i]);
				}
				tagetV3list.Add (m_listTargetV3 [1]);
				//Line Draw:
				Vector3[] vector3s = PathControlPointGenerator (tagetV3list.ToArray ());
				Vector3 prevPt = Interp (vector3s, 0);
				//Gizmos.color = Color.blue;
				int SmoothAmount = tagetV3list.Count * 20;
				if (m_listTargetV3ToFly.Count > SmoothAmount) {
					int removeCount = m_listTargetV3ToFly.Count - SmoothAmount;
					m_listTargetV3ToFly.RemoveRange (m_listTargetV3ToFly.Count - removeCount, removeCount);
				}
				for (int i = 1; i <= SmoothAmount; i++) {
					float pm = (float)i / SmoothAmount;
					Vector3 currPt = Interp (vector3s, pm);
					//Gizmos.DrawLine (currPt, prevPt);
					prevPt = currPt;
					if (m_listTargetV3ToFly.Count > i) {
						m_listTargetV3ToFly [i - 1] = currPt;
					} else {
						m_listTargetV3ToFly.Add (currPt);
					}
				}
			}
		}*/
	}
	// Use this for initialization
	void Start () {
		init ();
	}
	void init()
	{
		if (m_bAIWalk) {
			ChangeState (ChartStateBase.eCHART_STATE.WALK);
		}
		else 
		{
			if (Mathf.Abs (Vector3.Distance (this.transform.position, m_listTargetV3 [0])) > 3f) 
			{
				ChangeState (ChartStateBase.eCHART_STATE.WALK);
			}
			else 
			{
				ChangeState (ChartStateBase.eCHART_STATE.IDLE);				
			}
		}	
	}
	// Update is called once per frame
	void Update () {
				
		if (m_eCurState != null) 
		{
			StateController ();
		}
		else 
		{
			init ();
		}

	}
		/*		
		void OnGUI()
		{
				if (GUI.Button (new Rect(10f,10f,100f,50f),"idle")) 
				{
						ChangeState (ChartStateBase.eCHART_STATE.IDLE);
				}

				if (GUI.Button (new Rect(10,60,100,50),"walk")) 
				{
						ChangeState (ChartStateBase.eCHART_STATE.WALK);
				}
		}*/

		public void ChangeState(ChartStateBase.eCHART_STATE entityType)
		{
               
				if (m_eCurState)
				{
                    m_eCurState.Exit ();
				}
				if (m_bCanfly) 
				{
					if (entityType == ChartStateBase.eCHART_STATE.WALK) 
					{
						entityType = ChartStateBase.eCHART_STATE.FLY;
					}
					else if(entityType == ChartStateBase.eCHART_STATE.CHASE) 
					{
						entityType = ChartStateBase.eCHART_STATE.FLY_CHASE;
					}
				}
				switch (entityType) 
				{
				case ChartStateBase.eCHART_STATE.IDLE:
					if (_nmaAgent) 
					{
						m_nmaAgent.enabled = false;
					}
					m_eCurState = gameObject.AddComponent<ChartStateIdle> ();
						break;
				case ChartStateBase.eCHART_STATE.WALK:
					m_nmaAgent.enabled = true;
						m_eCurState = gameObject.AddComponent<ChartStateOtherWalk> ();
						break;
				case ChartStateBase.eCHART_STATE.CHASE:
					m_nmaAgent.enabled = true;
						m_eCurState = gameObject.AddComponent<ChartStateChase> ();
						break;
				case ChartStateBase.eCHART_STATE.ATTACK:
					if (_nmaAgent) 
					{
						m_nmaAgent.enabled = false;
					}
						m_eCurState = gameObject.AddComponent<ChartStateAttack> ();
						break;
				case ChartStateBase.eCHART_STATE.GETHIT:
					if (_nmaAgent) 
					{
						m_nmaAgent.enabled = false;
					}
					m_eCurState = gameObject.AddComponent<ChartStateGetHit> ();
						break;
				case ChartStateBase.eCHART_STATE.FLY:
					if (_nmaAgent) 
					{
						m_nmaAgent.enabled = false;
					}
					m_eCurState = gameObject.AddComponent<ChartStateFly> ();
					break;
				case ChartStateBase.eCHART_STATE.FLY_CHASE:
					if (_nmaAgent) 
					{
						m_nmaAgent.enabled = false;
					}
					m_eCurState = gameObject.AddComponent<ChartStateFlyChase> ();
					break;
				}
				m_eCurState.m_eMyState = entityType;
				m_eCurState.Enter ();
		}

		public void ChangeState(int walkTarget)
		{
			ChartStateBase.eCHART_STATE entityType = ChartStateBase.eCHART_STATE.WALK;
			if (m_eCurState)
			{
				m_eCurState.Exit ();
			}
			if (m_bCanfly) 
			{
				if (entityType == ChartStateBase.eCHART_STATE.WALK) 
				{
					entityType = ChartStateBase.eCHART_STATE.FLY;
				}
			}
			if (entityType == ChartStateBase.eCHART_STATE.WALK) 
			{
				m_nmaAgent.enabled = true;
				m_eCurState = gameObject.AddComponent<ChartStateOtherWalk> ();
			} 
			else if (entityType == ChartStateBase.eCHART_STATE.FLY) 
			{
				if (_nmaAgent) 
				{
					m_nmaAgent.enabled = false;
				}
				m_eCurState = gameObject.AddComponent<ChartStateFly> ();
			}
			m_eCurState.m_eMyState = entityType;
			m_eCurState.Enter ();
			
		}
		void StateController()
		{
				switch (m_eCurState.m_eMyState) {
				case ChartStateBase.eCHART_STATE.IDLE:
				case ChartStateBase.eCHART_STATE.WALK:
				case ChartStateBase.eCHART_STATE.FLY:
					if (m_objPlayer) 
					{
							float distance = Vector3.Distance (this.transform.position, m_objPlayer.transform.position);
							if (m_fChaseRadius > 0) {
								if (Mathf.Abs (distance) < m_fChaseRadius) {
									if (m_fMoveRadius > 0) {
										if (Mathf.Abs (Vector3.Distance (this.transform.position, m_listTargetV3 [0])) < m_fMoveRadius) {
											ChangeState (ChartStateBase.eCHART_STATE.CHASE);
										}
									} else {
										ChangeState (ChartStateBase.eCHART_STATE.CHASE);
									}
								}
							} 
							else if (m_fAttackDistance > 0) 
							{
								if (Mathf.Abs (distance) < m_fAttackDistance) 
									{
										if (m_fMoveRadius > 0)
											{
											if (Mathf.Abs (Vector3.Distance (this.transform.position, m_listTargetV3 [0])) < m_fMoveRadius) {
												ChangeState (ChartStateBase.eCHART_STATE.ATTACK);
												//ChangeState (ChartStateBase.eCHART_STATE.GETHIT);
											}
										} 
										else
										{
											//ChangeState (ChartStateBase.eCHART_STATE.GETHIT);
											ChangeState (ChartStateBase.eCHART_STATE.ATTACK);
										}
									}
							}
				}
						break;
				case ChartStateBase.eCHART_STATE.CHASE:
				case ChartStateBase.eCHART_STATE.FLY_CHASE:
                if (m_objPlayer)
                {
                    if (m_fMoveRadius > 0)
                    {
						if (Mathf.Abs(Vector3.Distance(this.transform.position, m_listTargetV3[0])) > m_fMoveRadius)
                        {
                            ChangeState(ChartStateBase.eCHART_STATE.WALK);
							break;
                        }
                    }
					if(!m_bCanfly)
					{
						if (m_nmaAgent)
						{
							if (m_nmaAgent.pathStatus == NavMeshPathStatus.PathComplete && m_nmaAgent.remainingDistance < m_fAttackDistance)
							{
								ChangeState (ChartStateBase.eCHART_STATE.ATTACK);
								break;
							}
						}
					}
					else
					{
						if (Mathf.Abs (Vector3.Distance (this.transform.position, m_objPlayer.transform.position)) < m_fAttackDistance) 
						{
							ChangeState (ChartStateBase.eCHART_STATE.ATTACK);
							break;
						}
					}
                }
                else
                {
                    ChangeState(ChartStateBase.eCHART_STATE.WALK);
                }
						break;
				case ChartStateBase.eCHART_STATE.ATTACK:
			
				if (m_objPlayer)
				{
					if ((Mathf.Abs (Vector3.Distance (this.transform.position, m_objPlayer.transform.position)) > m_fAttackDistance+1f)) 
					{
							ChangeState (ChartStateBase.eCHART_STATE.CHASE);
					}
                }
				else
				{
						ChangeState (ChartStateBase.eCHART_STATE.WALK);
				}
						break;
				}

		}

	void OnDrawGizmosSelected()
	{
        if (m_bMoveWithCenter)
        {
            if (m_listTargetObj.Count > 0)
            {
                foreach (GameObject obj in m_listTargetObj)
                {
                    if (!obj)
                    {
                        m_listTargetObj.Clear();
                    }
                }
            }
            if (m_listTargetObj.Count <= 0)
            {
                GameObject newparent = new GameObject();
                newparent.transform.parent = this.transform.parent;
                newparent.transform.localPosition = Vector3.zero;
                this.transform.parent = newparent.transform;
                newparent.transform.localPosition = this.transform.localPosition;
                this.transform.localPosition = Vector3.zero;
                m_listTargetObj.Add(newparent);
                newparent.name = this.name + "_parent";
            }
            int tcount = m_iTargetCount + 1;
            //add node?
            if (tcount > m_listTargetObj.Count)
            {
                for (int i = 0; i < tcount - m_listTargetObj.Count; i++)
                {
                    GameObject newNode = new GameObject();
                    newNode.transform.parent = this.transform.parent;
                    newNode.transform.localPosition = this.transform.localPosition;
                    m_listTargetObj.Add(newNode);
                    if (m_listTargetObj.Count == 2)
                    {
                        newNode.name = "Center";
                    }
                    else
                    {
                        newNode.name = "Target_" + (m_listTargetObj.Count - 2).ToString();
                    }
                }
            }
            //remove node?
            if (tcount < m_listTargetObj.Count)
            {
                int removeCount = m_listTargetObj.Count - tcount;
                for (int i = m_listTargetObj.Count - removeCount; i < m_listTargetObj.Count; i++)
                {
                    DestroyImmediate(m_listTargetObj[i]);
                }
                m_listTargetObj.RemoveRange(m_listTargetObj.Count - removeCount, removeCount);
            }
            for (int i = 1; i < m_listTargetObj.Count; i++)
            {
                if (m_listTargetV3.Count > i-1)
                {
                    m_listTargetV3[i-1] = m_listTargetObj[i].transform.position;
                }
                else
                {
                    m_listTargetV3.Add(m_listTargetObj[i].transform.position);
                }
            }
        }
        else
        {
            if (m_listTargetObj.Count > 0)
            {
                if (m_listTargetObj[0])
                {
                    this.transform.parent = m_listTargetObj[0].transform.parent;
                }
            }
            foreach (GameObject obj in m_listTargetObj)
            {
                DestroyImmediate(obj);
            }
            m_listTargetObj.Clear();
        }
           
        if (m_listTargetV3.Count > 1)
        {
            List<Vector3> tagetV3list = new List<Vector3>();
            for (int i = 1; i < m_listTargetV3.Count; i++)
            {
                tagetV3list.Add(m_listTargetV3[i]);
            }
            tagetV3list.Add(m_listTargetV3[1]);

            if (m_bCanfly)
            {

                //Line Draw:
                Vector3[] vector3s = PathControlPointGenerator(tagetV3list.ToArray());
                Vector3 prevPt = Interp(vector3s, 0);
                Gizmos.color = Color.blue;
                int SmoothAmount = tagetV3list.Count * 20;
                if (m_listTargetV3ToFly.Count > SmoothAmount)
                {
                    int removeCount = m_listTargetV3ToFly.Count - SmoothAmount;
                    m_listTargetV3ToFly.RemoveRange(m_listTargetV3ToFly.Count - removeCount, removeCount);
                }
                for (int i = 1; i <= SmoothAmount; i++)
                {
                    float pm = (float)i / SmoothAmount;
                    Vector3 currPt = Interp(vector3s, pm);
                    Gizmos.DrawLine(currPt, prevPt);
                    prevPt = currPt;
                    if (m_listTargetV3ToFly.Count > i)
                    {
                        m_listTargetV3ToFly[i - 1] = currPt;
                    }
                    else
                    {
                        m_listTargetV3ToFly.Add(currPt);
                    }
                }
            }
            else
            {
                Vector3 prevPt = tagetV3list[0];
                Gizmos.color = Color.blue;
                for (int i = 1; i <= tagetV3list.Count; i++)
                {
                    Vector3 currPt = tagetV3list[i - 1];
                    Gizmos.DrawLine(currPt, prevPt);
                    prevPt = currPt;
                }
            }
		
		}
		if (m_listTargetV3.Count > 0) {
			DrawRound(Matrix4x4.TRS( m_listTargetV3[0], Quaternion.LookRotation(Vector3.forward), Vector3.one),Color.red,m_fMoveRadius);
		}
		DrawRound (this.transform.localToWorldMatrix,Color.green,m_fChaseRadius);
		DrawRound (this.transform.localToWorldMatrix,Color.black,m_fAttackDistance);
	}

	public static Vector3[] PathControlPointGenerator(Vector3[] path){
		Vector3[] suppliedPath;
		Vector3[] vector3s;
		suppliedPath = path;
		int offset = 2;
		vector3s = new Vector3[suppliedPath.Length+offset];
		System.Array.Copy(suppliedPath,0,vector3s,1,suppliedPath.Length);
		vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
		vector3s[vector3s.Length-1] = vector3s[vector3s.Length-2] + (vector3s[vector3s.Length-2] - vector3s[vector3s.Length-3]);
		if(vector3s[1] == vector3s[vector3s.Length-2]){
			Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
			System.Array.Copy(vector3s,tmpLoopSpline,vector3s.Length);
			tmpLoopSpline[0]=tmpLoopSpline[tmpLoopSpline.Length-3];
			tmpLoopSpline[tmpLoopSpline.Length-1]=tmpLoopSpline[2];
			vector3s=new Vector3[tmpLoopSpline.Length];
			System.Array.Copy(tmpLoopSpline,vector3s,tmpLoopSpline.Length);
		}	

		return(vector3s);
	}

	public static Vector3 Interp(Vector3[] pts, float t){
		int numSections = pts.Length - 3;
		int currPt = Mathf.Min(Mathf.FloorToInt(t * (float) numSections), numSections - 1);
		float u = t * (float) numSections - (float) currPt;

		Vector3 a = pts[currPt];
		Vector3 b = pts[currPt + 1];
		Vector3 c = pts[currPt + 2];
		Vector3 d = pts[currPt + 3];

		return .5f * (
			(-a + 3f * b - 3f * c + d) * (u * u * u)
			+ (2f * a - 5f * b + 4f * c - d) * (u * u)
			+ (-a + c) * u
			+ 2f * b
		);
	}

	void DrawRound(Matrix4x4 m4x4,Color drawColor,float drawRadius)
	{
		float m_Theta = 0.1f;
		if (m_Theta < 0.0001f) m_Theta = 0.0001f;

		Matrix4x4 defaultMatrix = Gizmos.matrix;
		Gizmos.matrix = m4x4;

		Color defaultColor = Gizmos.color;
		Gizmos.color = drawColor;

		Vector3 beginPoint = Vector3.zero;
		Vector3 firstPoint = Vector3.zero;
		for (float theta = 0; theta < 2 * Mathf.PI; theta += m_Theta)
		{
			float x = drawRadius * Mathf.Cos(theta);
			float z = drawRadius * Mathf.Sin(theta);
			Vector3 endPoint = new Vector3(x, 0, z);
			if (theta == 0)
			{
				firstPoint = endPoint;
			}
			else
			{
				Gizmos.DrawLine(beginPoint, endPoint);
			}
			beginPoint = endPoint;
		}
		Gizmos.DrawLine(firstPoint, beginPoint);
		Gizmos.color = defaultColor;
		Gizmos.matrix = defaultMatrix;
	}
}
