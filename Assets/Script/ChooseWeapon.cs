using UnityEngine;
using System.Collections;

public class ChooseWeapon : MonoBehaviour {

    public GameObject m_Gun;
    public GameObject m_Knife;


	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (GameStateController.instant.m_nCurState > 0)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)this.GetComponent<SteamVR_TrackedObject>().index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
            {
                if (m_Gun.activeSelf)
                {
                    m_Gun.SetActive(false);
                    m_Knife.SetActive(true);
                }
                else
                {
                    m_Gun.SetActive(true);
                    m_Knife.SetActive(false);
                }
            }
        }
        else
		{
            if (!m_Gun.activeSelf)
            {
                m_Gun.SetActive(true);
                m_Knife.SetActive(false);
            }
        }
    }
}
