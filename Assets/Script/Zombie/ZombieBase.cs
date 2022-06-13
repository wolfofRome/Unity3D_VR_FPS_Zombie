using UnityEngine;
using System.Collections;

public class ZombieBase : MonoBehaviour {
    public ZombieController.STATE m_eMyState;
    protected ZombieController _cscParent;
    protected string _strAnimation = "";
    private float viewAngle = 60f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //进入状态
    public virtual void Enter()
    {
        _cscParent = this.gameObject.GetComponent<ZombieController>();
    }

    //状态正常执行
    public virtual void Execute()
    {

    }

    //退出状态
    public virtual void Exit()
    {
        Destroy(this);
    }

    public bool isInView()
    {
        Quaternion r = transform.rotation;
        Vector3 f0 = transform.position + (r * Vector3.forward) * _cscParent.m_fAttackRadius;
        Debug.DrawLine(transform.position, f0, Color.red);

        Quaternion r0 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - viewAngle, transform.rotation.eulerAngles.z);
        Quaternion r1 = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + viewAngle, transform.rotation.eulerAngles.z);
        
        Vector3 f1 = transform.position + ((r0 * Vector3.forward) * _cscParent.m_fAttackRadius);
        Debug.DrawLine(transform.position, f1, Color.red);

        Vector3 f2 = transform.position + ((r1 * Vector3.forward) * _cscParent.m_fAttackRadius);
        Debug.DrawLine(transform.position, f2, Color.red);

        Vector3 point = _cscParent.m_objPlayer.transform.position;
        if (isINTriangle(point, transform.position, f1, f0) || isINTriangle(point, transform.position, f2, f0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool isINTriangle(Vector3 point, Vector3 v0, Vector3 v1, Vector3 v2)
    {
        float x = point.x;
        float y = point.z;

        float v0x = v0.x;
        float v0y = v0.z;

        float v1x = v1.x;
        float v1y = v1.z;

        float v2x = v2.x;
        float v2y = v2.z;

        float t = triangleArea(v0x, v0y, v1x, v1y, v2x, v2y);
        float a = triangleArea(v0x, v0y, v1x, v1y, x, y) + triangleArea(v0x, v0y, x, y, v2x, v2y) + triangleArea(x, y, v1x, v1y, v2x, v2y);

        if (Mathf.Abs(t - a) <= 0.01f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private float triangleArea(float v0x, float v0y, float v1x, float v1y, float v2x, float v2y)
    {
        return Mathf.Abs((v0x * v1y + v1x * v2y + v2x * v0y
            - v1x * v0y - v2x * v1y - v0x * v2y) / 2f);
    }
}
