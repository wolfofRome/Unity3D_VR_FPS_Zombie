using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapCotroller : MonoBehaviour {

    public static MapCotroller instant;

    public GameObject m_gSkullDown;
    public GameObject m_gSkullRight;
    public GameObject m_gSkullLeft;
    public GameObject m_gPlayerObj;

    private List<ZombieController> AttackZombieList = new List<ZombieController>();
    private float _fRadius = 4f;
    // Use this for initialization
    void Start () {
        instant = this;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameStateController.instant.m_nCurState > 0)
        {
            showSkulls();
        }
        else
        {
            if (AttackZombieList.Count > 0)
            {
                AttackZombieList.Clear();
            }
            if (m_gSkullDown.activeSelf)
            {
                m_gSkullDown.SetActive(false);
            }
            if (m_gSkullRight.activeSelf)
            {
                m_gSkullRight.SetActive(false);
            }
            if (m_gSkullLeft.activeSelf)
            {
                m_gSkullLeft.SetActive(false);
            }
        }

	}

    public void addZombie(ZombieController zb)
    {
        AttackZombieList.Add(zb);
    }

    void showSkulls()
    {
        bool backflg = false;
        bool righflg = false;
        bool leftflg = false;
        if (AttackZombieList.Count > 0)
        {
            foreach (ZombieController zc in AttackZombieList)
            {
                if (zc != null)
                {
                    if (zc.m_eCurState.m_eMyState == ZombieController.STATE.ATTACK)
                    {
                        if (isInViewRight(zc.gameObject))
                        {
                            righflg = true;
                        }
                        if (isInViewLeft(zc.gameObject))
                        {
                            leftflg = true;
                        }
                        if (isInViewBack(zc.gameObject))
                        {
                            backflg = true;
                        }
                    }
                }
            }
           
        }
        m_gSkullDown.SetActive(backflg);
        m_gSkullRight.SetActive(righflg);
        m_gSkullLeft.SetActive(leftflg);
    }


    public bool isInViewRight(GameObject zb)
    {
		Quaternion r = m_gPlayerObj.transform.rotation;
		Vector3 f0 = m_gPlayerObj.transform.position + (r * Vector3.right) * _fRadius;
        //Debug.DrawLine(transform.position, f0, Color.red);

		Quaternion r0 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y - 45f, m_gPlayerObj.transform.rotation.eulerAngles.z);
		Quaternion r1 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y + 45f, m_gPlayerObj.transform.rotation.eulerAngles.z);

		Vector3 f1 = m_gPlayerObj.transform.position + ((r0 * Vector3.right) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f1, Color.red);

		Vector3 f2 = m_gPlayerObj.transform.position + ((r1 * Vector3.right) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f2, Color.red);

        Vector3 point = zb.transform.position;
		if (isINTriangle(point, m_gPlayerObj.transform.position, f1, f0) || isINTriangle(point, m_gPlayerObj.transform.position, f2, f0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isInViewLeft(GameObject zb)
    {
		Quaternion r = m_gPlayerObj.transform.rotation;
		Vector3 f0 = m_gPlayerObj.transform.position + (r * Vector3.left) * _fRadius;
       // Debug.DrawLine(transform.position, f0, Color.red);

		Quaternion r0 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y - 45f, m_gPlayerObj.transform.rotation.eulerAngles.z);
		Quaternion r1 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y + 45f, m_gPlayerObj.transform.rotation.eulerAngles.z);

		Vector3 f1 = m_gPlayerObj.transform.position + ((r0 * Vector3.left) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f1, Color.blue);

		Vector3 f2 = m_gPlayerObj.transform.position + ((r1 * Vector3.left) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f2, Color.blue);

        Vector3 point = zb.transform.position;
		if (isINTriangle(point, m_gPlayerObj.transform.position, f1, f0) || isINTriangle(point, m_gPlayerObj.transform.position, f2, f0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isInViewBack(GameObject zb)
    {
		Quaternion r = m_gPlayerObj.transform.rotation;
		Vector3 f0 = m_gPlayerObj.transform.position + (r * Vector3.back) * _fRadius;
       // Debug.DrawLine(transform.position, f0, Color.red);

		Quaternion r0 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y - 45f,m_gPlayerObj. transform.rotation.eulerAngles.z);
		Quaternion r1 = Quaternion.Euler(m_gPlayerObj.transform.rotation.eulerAngles.x, m_gPlayerObj.transform.rotation.eulerAngles.y + 45f,m_gPlayerObj. transform.rotation.eulerAngles.z);

		Vector3 f1 = m_gPlayerObj.transform.position + ((r0 * Vector3.back) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f1, Color.green);

		Vector3 f2 = m_gPlayerObj.transform.position + ((r1 * Vector3.back) * _fRadius);
		Debug.DrawLine(m_gPlayerObj.transform.position, f2, Color.green);

        Vector3 point = zb.transform.position;
		if (isINTriangle(point, m_gPlayerObj.transform.position, f1, f0) || isINTriangle(point, m_gPlayerObj.transform.position, f2, f0))
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
