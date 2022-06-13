using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HandActionCatch : MonoBehaviour {
    public enum CATCH_STATE
    {
        WAVE,
        HANDDOWN,
    }

    public bool m_bCatch = false;

    private GameObject _trackedObj;

    private float _fMathIntervalTime = 0.001f;
    private float _fMathIntervalCutTime = 0f;

    private float _fWaveMathMinAX = 200f;
    private float _fWaveMathMaxAX = 340f;
    

    private Dictionary<CATCH_STATE, Action> CallBackDic = new Dictionary<CATCH_STATE, Action>();
    private List<baseCatch> ArmsForCatchList = new List<baseCatch>();
    private List<baseCatch> nullCatchs = new List<baseCatch>();

    // Use this for initialization
    void Start () {
        _fMathIntervalCutTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        Catch();

    }

    public static HandActionCatch instant(GameObject obj)
    {
       
        GameObject go = new GameObject();
	    HandActionCatch _instant = go.AddComponent<HandActionCatch>();
        _instant._trackedObj = obj;
        return _instant;
    }

    public void AddCatch(CATCH_STATE state,Action action)
    {
        CallBackDic.Add(state,action);
    }

    void Catch()
    {
        if (!m_bCatch)
        {
            return;
        }
       
        if (_fMathIntervalCutTime <= 0f)
        {
            if (CallBackDic.ContainsKey(CATCH_STATE.WAVE))
            {
                if (_trackedObj.transform.eulerAngles.x > _fWaveMathMinAX && _trackedObj.transform.eulerAngles.x < _fWaveMathMaxAX)
                {
                    WaveCatch wtch = new WaveCatch();
                    wtch.m_myState = CATCH_STATE.WAVE;
                    wtch.init(_trackedObj);
                    ArmsForCatchList.Add(wtch);
                }
                if (_trackedObj.transform.eulerAngles.x > -80f && _trackedObj.transform.eulerAngles.x < 17f)
                {
                    WaveCatch wtch = new WaveCatch();
                    wtch.m_myState = CATCH_STATE.WAVE;
                    wtch.init(_trackedObj);
                    ArmsForCatchList.Add(wtch);
                }

            }
            else if (CallBackDic.ContainsKey(CATCH_STATE.HANDDOWN))
            {
                HandDownCatch wtch = new HandDownCatch();
                wtch.m_myState = CATCH_STATE.HANDDOWN;
                wtch.init(_trackedObj);
                ArmsForCatchList.Add(wtch);
            }
            _fMathIntervalCutTime = _fMathIntervalTime;
        }
        else
        {
            _fMathIntervalCutTime -= Time.deltaTime;
        }

        if (ArmsForCatchList.Count > 0)
        {
            for (int i =0;i< ArmsForCatchList.Count;i++)
            {
                baseCatch bcth = ArmsForCatchList[i];
                if (bcth.m_nIAmAchieve == 0)
                {
                    bcth.timeGone(_trackedObj);
                }
                else if (bcth.m_nIAmAchieve == 1)
                {
                    if (CallBackDic.ContainsKey(bcth.m_myState))
                    {
                        CallBackDic[bcth.m_myState]();
						m_bCatch = false;
                        nullCatchs.AddRange(ArmsForCatchList);
                        ArmsForCatchList.Clear();
                    }
                }
                else if (bcth.m_nIAmAchieve == 2)
                {
                    ArmsForCatchList.Remove(bcth);
                    nullCatchs.Add(bcth);
                }
            }
        }

        if (nullCatchs.Count > 0)
        {
            for (int i = 0; i < nullCatchs.Count; i++)
            {
                nullCatchs[i] = null;
            }
            nullCatchs.Clear();
        }
       
    }

}

public class WaveCatch:baseCatch
{
    private float _fWaveIntervalTime = 0.8f;
    private float _fWaveDistance = 0.001f;
    private float _fWaveAngle = 0.85f;
    private Vector3 _V3StartPosition;
    private Quaternion _V3StartRotation;

    public void init(GameObject obj)
    {
        _V3StartPosition = obj.transform.position;
        _V3StartRotation = obj.transform.rotation;
    }

    public override void timeGone(GameObject obj)
    {
        if (m_nIAmAchieve == 0)
        {
            _waitTime += Time.deltaTime;
            if (_waitTime >= _fWaveIntervalTime)
            {
                float _distance = Mathf.Abs(Vector3.Distance(_V3StartPosition, obj.transform.position));
                float _Angle = Math.Abs(Quaternion.Dot(_V3StartRotation, obj.transform.rotation));
                if (_distance >= _fWaveDistance)
                {
                    if (_Angle <= _fWaveAngle)
                    {
                        m_nIAmAchieve = 1;
                        Debug.LogError("---------I-Achieve----------");
                    }
                }
                if (m_nIAmAchieve != 1)
                {
                   // Debug.Log("-----------distance--------" + _distance+ "  _Angle:" + _Angle);
                    m_nIAmAchieve = 2;
                }
            }
        }
    }
}
public class HandDownCatch : baseCatch
{
    private float _fWaveIntervalTime = 0.2f;
    private float _fWaveDistance = 0.002f;
    private Vector3 _V3StartPosition;

    public void init(GameObject obj)
    {
		_V3StartPosition = obj.transform.localPosition;
    }

    public override void timeGone(GameObject obj)
    {
        if (m_nIAmAchieve == 0)
        {
            float _distance = Mathf.Abs(Vector3.Distance(_V3StartPosition, obj.transform.localPosition));
            float startY = _V3StartPosition.y;
            float curY = obj.transform.localPosition.y;
            if (_distance >= _fWaveDistance)
            {
                if (startY > curY)
                {
                    m_nIAmAchieve = 1;
                  //  Debug.LogError("---------I-Achieve----------");
                }
            }
            _waitTime += Time.deltaTime;
            if (_waitTime >= _fWaveIntervalTime)
            {
                if (m_nIAmAchieve != 1)
                {
				//	Debug.Log("-----------startY--------:" + startY+"  curY:"+curY+"  "+_distance);
                    m_nIAmAchieve = 2;
                }
            }
        }
    }
}
public class baseCatch
{
    /// <summary>
    /// 0.start 1.Achieve 2.fail
    /// </summary>
    public int m_nIAmAchieve = 0;
    protected float _waitTime = 0f;
    public HandActionCatch.CATCH_STATE m_myState;

    public virtual void timeGone(GameObject obj)
    {

    }
}
