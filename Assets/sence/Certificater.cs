using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Certificater : MonoBehaviour
{
    private const string URL_PLAYREQUEST = @"www.flwhcm.com/VRke/API/selectionplay.php";
    private string[] commandLines = null;
    private string reply = "";
	
    void Start ()
    {
        commandLines = Environment.GetCommandLineArgs();
        foreach (string commandLine in commandLines)
        {
            Debug.Log(commandLine);
        }

        string processPath = commandLines[0];
        if (commandLines.Length > 1)
        {
            string arguments = commandLines[1];
            string[] args = arguments.Split(',');
            string username = args[0];
            string password = args[1];
            string gamename = args[2];
            StartCoroutine(SendPlayRequest(username, password, gamename));
        }
	}

    private IEnumerator SendPlayRequest(string usr, string pwd, string game)
    {
        Debug.Log(string.Format("SendPlayRequest({0}, {1})", usr, game));

        WWWForm form = new WWWForm();
        form.AddField("username", usr);
        form.AddField("password", pwd);
        form.AddField("gamename", game);
        form.AddField("realplay", "true");//false：测试播放，true：真实播放

        WWW www = new WWW(URL_PLAYREQUEST, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("request play VR Movie fialed, " + www.error);
            yield break;
        }

        OnPlayReply(www.text);
    }

    private void OnPlayReply(string msg)
    {
        Debug.Log(string.Format("OnPlayReply():{0}", msg));

        reply = msg;

        if (msg == "true")
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        for(int i = 0; i < commandLines.Length; ++i)
        {
            GUILayout.Label(string.Format("[{0}]:{1}", i, commandLines[i]));
        }

        GUILayout.Label("reply:" + reply);
    }
}
