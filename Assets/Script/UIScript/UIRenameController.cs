using UnityEngine;
using System.Collections;

public class UIRenameController : MonoBehaviour {

    public static UIRenameController instant; 

    public UISprite[] m_spName;

    private int _nNameNum;
	// Use this for initialization
	void Start () {
        instant = this;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        _nNameNum = 0;
		m_spName[0].spriteName = "";
		m_spName[1].spriteName = "";
		m_spName[2].spriteName = "";
    }

    public void setName(string spname)
    {
        if (_nNameNum < m_spName.Length)
        {
            m_spName[_nNameNum].spriteName = spname;
			if (_nNameNum+1 < m_spName.Length) {
				_nNameNum += 1;
			}
        }
    }

    public void deletName()
    {
		Debug.Log ("deletName:"+_nNameNum);
			if (_nNameNum >= 0) {
				if (_nNameNum < m_spName.Length)
				{
					m_spName[_nNameNum].spriteName = "";
					if (_nNameNum > 0) {
						_nNameNum -= 1;
					}

				}
			}
    }

    public void Sure()
    {
        string curname = "";
        for (int i = 0; i < m_spName.Length; i++)
        {
			curname += m_spName[i].spriteName;
        }
        if (curname.Length <= 0)
        {
            curname = " ";
        }
		Debug.Log ("GameStateController.instant.m_nCurRang:"+GameStateController.instant.m_nCurRang+"   m_nCurScore:"+GameStateController.instant.m_nCurScore+"   curname:"+curname);
        PlayerPrefs.SetString(GameStateController.instant.m_nCurRang.ToString(), curname+"_"+ GameStateController.instant.m_nCurScore.ToString());
        UIController.instant.fromRenameToRang();
    }

    public void ButtonOnClick(string clickspname)
    {
		Debug.Log ("ButtonOnClick:"+clickspname);
        if (clickspname.Contains("queren"))
        {
            Sure();
        }
        else if (clickspname.Contains("shanchu"))
        {
			Debug.Log ("deletName");
            deletName();
        }
        else
        {
            setName(clickspname);
        }
    }

}
