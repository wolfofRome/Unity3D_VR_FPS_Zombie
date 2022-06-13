using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public static UIController instant;

    public GameObject m_gStartUI;
    public GameObject m_gLoseUI;
    public GameObject m_gRenameUI;
    public GameObject m_gRankingUI;
    public GameObject m_gWinUI;
    

	public GameObject _CurUIChooseObj;

	void Awake()
	{
		instant = this;
	}


    // Use this for initialization
    void Start () {

		for (int i = 0; i < 8; i++)
		{
			//PlayerPrefs.SetString(i.ToString(), "-1");
		}
    }


	// Update is called once per frame
	void Update () {
	
	}
   public void addCutDown()
    {
        GameObject go = Instantiate(Resources.Load("PanelCutDown")) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
    }

    public void addLevelUp()
    {
        GameObject go = Instantiate(Resources.Load("PanelLevelUp")) as GameObject;
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.transform.localEulerAngles = Vector3.zero;
    }


    public void init()
    {
        m_gStartUI.SetActive(true);
        m_gLoseUI.SetActive(false);
        m_gRenameUI.SetActive(false);
        m_gRankingUI.SetActive(false);
        m_gWinUI.SetActive(false);
    }

    public void fromRenameToRang()
    {
        m_gRenameUI.SetActive(false);
        m_gRankingUI.SetActive(true);
    }

    public void ShowGameOver()
    {
        m_gLoseUI.SetActive(true);
        Invoke("showRange", 2f);
    }

    public void ShowGameWin()
    {
        m_gWinUI.SetActive(true);
        Invoke("showRange", 2f);
    }

    void showRange()
    {
        m_gLoseUI.SetActive(false);
        m_gWinUI.SetActive(false);
        int num = -1;
        for (int i = 0; i < 8; i++)
        {
            if (PlayerPrefs.GetString(i.ToString(), "-1").Equals("-1"))
            {
                if (num < 0)
                {
                    num = i;
                }
            }
            else
            {
                string[] infos = PlayerPrefs.GetString(i.ToString(), "-1").Split('_');
                int otherScore = 0;
                int.TryParse(infos[1],out otherScore);
                Debug.Log("------otherScore:"+ otherScore);
                if (GameStateController.instant.m_nCurScore > otherScore)
                {
                    if (num > i|| num < 0)
                    {
                        num = i;
                    }
                }
            }
        }
        if (num < 0)
        {
            m_gRankingUI.SetActive(true);
        }
        else
        {
			Debug.Log("--2----otherScore:"+ num);
            GameStateController.instant.m_nCurRang = 8;
            m_gRenameUI.SetActive(true);
        }
    }

	public void setChooseUIObj(GameObject go,bool setOrGet)
	{
		if (!setOrGet)
		{
			if (_CurUIChooseObj == go)
			{
				_CurUIChooseObj = null;
			}
		}
		else
		{
			_CurUIChooseObj = go;
		}
	}

}
