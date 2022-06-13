using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    public enum STYLE
    {
        NOMARL,
        SPECIAL,
        DOG,
    }
    public STYLE m_MyStyle;
    public enum STATE
    {
        WALK,
        ATTACK,
        RUN,
        ROAR,
        GETHIT,
        HEADSHOT,
        DEATH,
    }
    //normal animation
    public string m_sIdleName = "idle";
    public string m_sWalkName = "walk";
    public string m_sAttack1Name = "attack";
    public string m_sAttack2Name = "attack2";
    public string m_sHeadshotName = "headshot";
    public string m_sHitName = "hit";
    public string m_sHit2Name = "hit2";
    public string m_sDeathName = "death";

    //special animation
    public string m_sRunName = "run";
    public string m_sRoarName = "roar";

    //dog have run,only one Attack,one hit
    public string m_sDogPrefabName = "zdog";
    public string[] m_sNormalPrefabName = new string[2] {"FemaleZombie", "MaleZombie"};
    public string m_sSpecialPrefabName = "FatZombie";

    public float m_fChaseRadius = 6f;      //敌人进入此半径内，则开始追击模式，为0则无此模式
    public float m_fAttackRadius = 2f;      //敌人进入此半径内，则开始攻击模式
    public float m_fAttackDogRadius = 2f;      //敌人进入此半径内，则开始攻击模式
    public float m_fAttackNormalRadius = 2f;      //敌人进入此半径内，则开始攻击模式
    public float m_fAttackSpecailRadius = 2f;      //敌人进入此半径内，则开始攻击模式
    public float m_AttackfreezenTime = 1f;  //冷却时间，攻击后是否冷却返回，为0则继续进入备战

    public float m_fNormalWalkMinSpeed = 2f;
    public float m_fNormalWalkMaxSpeed = 2f;
    public float m_fSpecialWalkMinSpeed = 2f;
    public float m_fSpecialWalkMaxSpeed = 2f;
    public float m_fRunSpeed = 4f;
    public float m_fDogSpeed = 3f;

    public float m_fNormalZombieBlood = 100;
    public float m_fSpecialZombieBlood = 150;
    public float m_fDogZombieBlood = 50;

    public int m_nNormalZombieDropBullet = 20;
    public int m_nSpecialZombieDropBullet = 1;

    public GameObject m_ZombieWalkAudio;

    [HideInInspector]
    public GameObject m_objPlayer;
    [HideInInspector]
    public ZombieBase m_eCurState;
    [HideInInspector]
    public Animation m_animation;           //动画
    [HideInInspector]
    public NavMeshObstacle m_Obstacle;           
    [HideInInspector]
    public float m_nMyBlood;

    private float m_fMyHight = 1.6f;
    private float m_fMyWith = 0.5f;
    private float m_fHightCenter;
    private STATE _GetHitExState;
    private AnimationEvent aEvent;
    
    public float m_curWalkSpeed
    {
        get
        {
            if (m_MyStyle == STYLE.DOG)
            {
                return m_fDogSpeed;
            }
            else if (m_MyStyle == STYLE.NOMARL)
            {
                return Random.Range(m_fNormalWalkMinSpeed, m_fNormalWalkMaxSpeed);
            }
            else if (m_MyStyle == STYLE.SPECIAL)
            {
                return Random.Range(m_fSpecialWalkMinSpeed, m_fSpecialWalkMaxSpeed);
            }
            return 1f;
        }
    }
    private NavMeshAgent _nmaAgent;
    public NavMeshAgent m_nmaAgent
    {
        get
        {
            if (!_nmaAgent)
            {
                _nmaAgent = gameObject.GetComponent<NavMeshAgent>();
                if (!_nmaAgent)
                {
                    _nmaAgent = gameObject.AddComponent<NavMeshAgent>();
                    _nmaAgent.stoppingDistance = 0.01f;
                    _nmaAgent.angularSpeed = 50f;
                    _nmaAgent.height = m_fMyHight;
                    _nmaAgent.radius = m_fMyWith;
                    _nmaAgent.baseOffset = m_fHightCenter;
                    _nmaAgent.autoRepath = true;
                }
            }
            return _nmaAgent;
        }
    }
    // Use this for initialization
    void Start () {
        //init( STYLE.SPECIAL);

    }
	
	// Update is called once per frame
	void Update () {
        if (m_eCurState != null)
        {
            StateController();
        }
    }

    void OnDrawGizmosSelected()
    {
        DrawRound(this.transform.localToWorldMatrix, Color.green, m_fChaseRadius);
        DrawRound(this.transform.localToWorldMatrix, Color.red, m_fAttackRadius);
    }

    void DrawRound(Matrix4x4 m4x4, Color drawColor, float drawRadius)
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

    public void init(STYLE mystyle)
    {
        m_MyStyle = mystyle;
        m_objPlayer = PlayerController.instant.gameObject;
        string myObjName = "";
        if (m_MyStyle == STYLE.DOG)
        {
            myObjName = m_sDogPrefabName;
            m_nMyBlood = m_fDogZombieBlood;
            m_fAttackRadius = m_fAttackDogRadius;
        }
        else if (m_MyStyle == STYLE.NOMARL)
        {
            int num = Random.Range(0, m_sNormalPrefabName.Length);
            myObjName = m_sNormalPrefabName[num];
            m_nMyBlood = m_fNormalZombieBlood;
			this.gameObject.transform.localScale = new Vector3 (1.3f,1.3f,1.3f);
            m_fAttackRadius = m_fAttackNormalRadius;
        }
        else if (m_MyStyle == STYLE.SPECIAL)
        {
            myObjName = m_sSpecialPrefabName;
            m_nMyBlood = m_fSpecialZombieBlood;
			this.gameObject.transform.localScale = new Vector3 (1.5f,1.5f,1.5f);
            m_fAttackRadius = m_fAttackSpecailRadius;
        }

        GameObject obj = Instantiate(Resources.Load(myObjName)) as GameObject;
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;

        m_animation = obj.GetComponent<Animation>();
        m_Obstacle = obj.GetComponent<NavMeshObstacle>();
        foreach (ZombieFindParent zparent in obj.GetComponentsInChildren<ZombieFindParent>())
        {
            zparent.m_MyParent = this;
        }
        ChangeState(STATE.WALK);
        aEvent = new AnimationEvent();
        AnimationClip attack1 = m_animation.GetClip(m_sAttack1Name);
        AnimationClip attack2 = m_animation.GetClip(m_sAttack2Name);
        if (attack1.events.Length > 0)
        {
            attack1.events = null;
			if (m_MyStyle != STYLE.DOG) {
				attack2.events = null;
			}
        }
        aEvent.time = attack1.length * 0.2f;
        aEvent.functionName = "AttackPlayr";
        aEvent.data = ((int)m_MyStyle).ToString();
        attack1.AddEvent(aEvent);
        if (attack2)
        {
            attack2.AddEvent(aEvent);
        }
    }
    
    void StateController()
    {
        switch (m_eCurState.m_eMyState)
        {
            case STATE.WALK:

                if (m_nmaAgent)
                {
                    if (m_MyStyle == ZombieController.STYLE.SPECIAL)
                    {
                        if (m_nmaAgent.pathStatus == NavMeshPathStatus.PathComplete && m_nmaAgent.remainingDistance <= m_fChaseRadius)
                        {
                            ChangeState( STATE.ROAR);
                        }
                        
                    }
                    else 
                    {
                        if (m_nmaAgent.pathStatus == NavMeshPathStatus.PathComplete && m_nmaAgent.remainingDistance <= m_fAttackRadius)
                        {
                            if (m_eCurState.isInView())
                            {
                                ChangeState(STATE.ATTACK);
                            }
                        }
                        else if (m_nmaAgent.pathStatus != NavMeshPathStatus.PathComplete)
                        {
                            if (Mathf.Abs(Vector3.Distance(m_nmaAgent.transform.position, m_objPlayer.transform.position)) <= m_fAttackRadius)
                            {
                                if (m_eCurState.isInView())
                                {
                                    ChangeState(STATE.ATTACK);
                                }
                            }
                        }
                    }
                }
                break;
            case STATE.RUN:
                if (m_nmaAgent.pathStatus == NavMeshPathStatus.PathComplete && m_nmaAgent.remainingDistance <= m_fAttackRadius)
                {
                    if (m_eCurState.isInView())
                    {
                        ChangeState(STATE.ATTACK);
                    }
                }
                else if (m_nmaAgent.pathStatus != NavMeshPathStatus.PathComplete)
                {
                    if (Mathf.Abs(Vector3.Distance(m_nmaAgent.transform.position, m_objPlayer.transform.position)) <= m_fAttackRadius)
                    {
                        if (m_eCurState.isInView())
                        {
                            ChangeState(STATE.ATTACK);
                        }
                    }
                }
                break;
            case STATE.ROAR:
                if (m_animation[m_sRoarName].normalizedTime >= 1)
                {
                    ChangeState(STATE.RUN);
                }
                break;
            case STATE.GETHIT:
                if (m_animation[m_sHitName].normalizedTime >= 1|| m_animation[m_sHit2Name].normalizedTime >= 1)
                {
                    ChangeState(_GetHitExState);
                }
                break;
            case STATE.ATTACK:
                if (!m_eCurState.isInView())
                {
                    if (m_MyStyle == ZombieController.STYLE.SPECIAL)
                    {
                        ChangeState(STATE.RUN);
                    }
                    else
                    {
                        ChangeState(STATE.WALK);
                    } 
                }
                break;
            case STATE.DEATH:
                if (m_animation[m_sDeathName].normalizedTime >= 1)
                {
                    toDie();
                }
                break;
            case STATE.HEADSHOT:
                if (m_animation[m_sHeadshotName].normalizedTime >= 1)
                {
                    if (m_MyStyle == ZombieController.STYLE.SPECIAL)
                    {
                        if (m_nMyBlood <= 0)
                        {
                            ChangeState(STATE.DEATH);
                        }
                        else
                        {
                            ChangeState(_GetHitExState);
                        }
                    }
                    else
                    {
                        toDie();
                       
                    }
                }
                break;
        }
        if(m_nMyBlood <=0)
        {
            if (m_eCurState != null)
            {
                if (m_eCurState.m_eMyState != STATE.HEADSHOT && m_eCurState.m_eMyState != STATE.DEATH)
                {
                    ChangeState(STATE.DEATH);
                }
            }
        }
    }

    private void toDie()
    {
        if (m_MyStyle == STYLE.NOMARL)
        {
            GameStateController.instant.AddScore(PlayerController.instant.m_nNormalZombieCoin);
            int rangnum = Random.Range(0, m_nNormalZombieDropBullet); 
            if (rangnum == 0)
            {
                PlayerController.instant.AddBullet();
                DropObj("Clip1");
            }
        }
        else if (m_MyStyle == STYLE.SPECIAL)
        {
            GameStateController.instant.AddScore(PlayerController.instant.m_nSpecialZombieCoin);
            int rangnum = Random.Range(0,m_nSpecialZombieDropBullet); 
            if (rangnum == 0)
            {
                PlayerController.instant.AddBullet();
                DropObj("Clip1");
            }
        }
        else if (m_MyStyle == STYLE.DOG)
        {
            GameStateController.instant.AddScore(PlayerController.instant.m_nDogZombieCoin);
        }
        DropObj("Coin");
        m_eCurState = null;
        Destroy(this.gameObject);
    }

    private void DropObj(string objname)
    {
        GameObject go = Instantiate(Resources.Load(objname)) as GameObject;
        go.transform.position = this.transform.position;
        go.transform.localEulerAngles = Vector3.zero;
        if (objname.Equals("Clip1"))
        {
            go.transform.position = new Vector3(go.transform.position.x+1f, go.transform.position.y, go.transform.position.z);
        }
    }

    public void ChangeState(STATE entityType)
    {

        if (m_eCurState)
        {
            m_eCurState.Exit();
        }
        switch (entityType)
        {
            case STATE.WALK:
                m_Obstacle.enabled = false;
                m_nmaAgent.enabled = true;
                m_ZombieWalkAudio.SetActive(true);
                m_eCurState = gameObject.AddComponent<ZombieStateWalk>();
                break;
            case STATE.RUN:
                m_Obstacle.enabled = false;
                m_nmaAgent.enabled = true;
                m_ZombieWalkAudio.SetActive(true);
                m_eCurState = gameObject.AddComponent<ZombieStateRun>();
                break;
            case STATE.ROAR:
                m_Obstacle.enabled = false;
                if (_nmaAgent)
                {
                    m_nmaAgent.enabled = false;
                }
                m_ZombieWalkAudio.SetActive(false);
                m_eCurState = gameObject.AddComponent<ZombieStateRoar>();
                break;
            case STATE.ATTACK:
                m_Obstacle.enabled = true;
                if (_nmaAgent)
                {
                    m_nmaAgent.enabled = false;
                }
                m_ZombieWalkAudio.SetActive(false);
                m_eCurState = gameObject.AddComponent<ZombieStateAttack>();
                MapCotroller.instant.addZombie(this);
                break;
            case STATE.GETHIT:
                if (_nmaAgent)
                {
                    m_nmaAgent.enabled = false;
                }
                m_ZombieWalkAudio.SetActive(false);
                m_eCurState = gameObject.AddComponent<ZombieStateGetHit>();
                break;
            case STATE.HEADSHOT:
                if (_nmaAgent)
                {
                    m_nmaAgent.enabled = false;
                }
                m_ZombieWalkAudio.SetActive(false);
                m_eCurState = gameObject.AddComponent<ZombieStateHeadShot>();
                break;
            case STATE.DEATH:
                m_Obstacle.enabled = false;
                if (_nmaAgent)
                {
                    m_nmaAgent.enabled = false;
                }
                m_ZombieWalkAudio.SetActive(false);
                m_eCurState = gameObject.AddComponent<ZombieStateDeath>();
                break;
        }
        m_eCurState.m_eMyState = entityType;
        m_eCurState.Enter();
    }

    public void GetHit(bool ishead,float cutBlood)
    {
        if (m_nMyBlood <= 0)
        {
            return;
        }
        m_nMyBlood -= cutBlood;
        if (m_eCurState.m_eMyState != STATE.DEATH)
        {
            if (ishead)
            {
                ChangeState(STATE.HEADSHOT);
            }
            else
            {
                ChangeState(STATE.GETHIT);
            }
        }
    }
}
