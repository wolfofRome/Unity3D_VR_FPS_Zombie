using UnityEngine;
using System.Collections;

public class UIChooseHighLight : MonoBehaviour
{

    public GameObject m_gHighBack;

    // Use this for initialization
    void Start()
    {
        m_gHighBack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        m_gHighBack.SetActive(true);
		UIController.instant.setChooseUIObj (this.gameObject,true);
    }

	void OnTriggerStay(Collider other)
	{
		m_gHighBack.SetActive(true);
		UIController.instant.setChooseUIObj (this.gameObject,true);
	}

    void OnTriggerExit(Collider other)
    {
        m_gHighBack.SetActive(false);
		UIController.instant.setChooseUIObj (this.gameObject,false);
    }
}
