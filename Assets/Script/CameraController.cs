using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraController : MonoBehaviour {
    public static CameraController instant;
    public Camera m_mainCamera;
    public VignetteAndChromaticAberration m_CameraVignette;
    public float m_fDurationTime = 2f;

    private float _fUseCutTime = 0f;
    private float _fCutNum;
    /// <summary>
    /// 0.stop 1.tweenDark 2.tweenlight
    /// </summary>
    private int _nTweenState = 0;

    // Use this for initialization
    void Start () {
        instant = this;
    }

    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(new Vector2(0,0),new Vector2(100f,50f)),"end"))
    //    {
    //        GameEnd();
    //    }
    //    if (GUI.Button(new Rect(new Vector2(120f, 0), new Vector2(100f, 50f)), "Restart"))
    //    {
    //        Restart();
    //    }
    //}

	// Update is called once per frame
	void Update () {
        if (_nTweenState != 0)
        {
            EndAnimation();
        }
	}

    public void GameEnd()
	{
        _fCutNum = 1 / (m_fDurationTime / Time.deltaTime);
        _fUseCutTime = 0f;
        m_CameraVignette.intensity = 0f;
        m_CameraVignette.blur = 0f;
        m_CameraVignette.enabled = true;
        _nTweenState = 1;
    }

    public void Restart()
    {
        _fCutNum = 1 / (m_fDurationTime / Time.deltaTime);
        _fUseCutTime = 0f;
        m_mainCamera.cullingMask = -1;
        m_CameraVignette.intensity = 1f;
        m_CameraVignette.blur = 1f;
        m_CameraVignette.enabled = true;
		m_mainCamera.clearFlags = CameraClearFlags.Skybox;
        _nTweenState = 2;
    }

    void EndAnimation()
    {
       
        if (_nTweenState == 1)
        {
            if ((m_CameraVignette.intensity+ _fCutNum) < 1)
            {
                m_CameraVignette.intensity += _fCutNum;
                m_CameraVignette.blur += _fCutNum;
            }
            if (_fUseCutTime > m_fDurationTime)
            {
                _nTweenState = 0;
                int UIlayer = LayerMask.NameToLayer("UI");
                m_mainCamera.cullingMask =(UIlayer<<UIlayer);
                m_CameraVignette.enabled = false;
				m_mainCamera.clearFlags = CameraClearFlags.Color;
            }
        }
        else if (_nTweenState == 2)
        {
            m_CameraVignette.intensity -= _fCutNum;
            m_CameraVignette.blur -= _fCutNum;
            if (_fUseCutTime > m_fDurationTime)
            {
                m_CameraVignette.enabled = false;
                _nTweenState = 0;
            }
         }
        _fUseCutTime += Time.deltaTime;
    }
}
