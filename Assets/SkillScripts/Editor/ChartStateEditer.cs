using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ChartStateController))]
public class ChartStateEditer : Editor {
	
	GUIStyle style = new GUIStyle();
	public static int count = 0;
	ChartStateController _chartState;

	void OnEnable(){
		//i like bold handle labels since I'm getting old:
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;
		_chartState = (ChartStateController)target;
	}

	public override void OnInspectorGUI(){		
		base.OnInspectorGUI ();

		//exploration segment count control:
		EditorGUILayout.BeginHorizontal();
		_chartState.m_iTargetCount = Mathf.Max(1, EditorGUILayout.IntField("I Target Count", _chartState.m_iTargetCount));
		//_target.nodeCount =  Mathf.Clamp(EditorGUILayout.IntSlider(_target.nodeCount, 0, 10), 2,100);
		EditorGUILayout.EndHorizontal();

		//add node?
		if(_chartState.m_iTargetCount > _chartState.m_listTargetV3.Count){
			for (int i = 0; i < _chartState.m_iTargetCount - _chartState.m_listTargetV3.Count; i++) {
				_chartState.m_listTargetV3.Add(_chartState.transform.position);	
			}
		}

		//remove node?
		if(_chartState.m_iTargetCount < _chartState.m_listTargetV3.Count){
				int removeCount = _chartState.m_listTargetV3.Count - _chartState.m_iTargetCount;
				_chartState.m_listTargetV3.RemoveRange(_chartState.m_listTargetV3.Count-removeCount,removeCount);
		}

        //node display:
        EditorGUI.indentLevel = 4;
        for (int i = 0; i < _chartState.m_listTargetV3.Count; i++)
        {
            _chartState.m_listTargetV3[i] = EditorGUILayout.Vector3Field("Target_" + (i + 1), _chartState.m_listTargetV3[i]);
        }

        //update and redraw:
        if (GUI.changed){
			EditorUtility.SetDirty(_chartState);			
		}
	}
	void OnSceneGUI(){
        if (_chartState.m_bMoveWithCenter)
        {
            if (_chartState.m_listTargetObj.Count > 1)
            {
                //allow path adjustment undo:
                Undo.SetSnapshotTarget(_chartState, "Adjust iTween Path");

                //path begin and end labels:
                if (_chartState.m_listTargetObj[1])
                {
                    Handles.Label(_chartState.m_listTargetObj[1].transform.position, "CenterTarget", style);
                }

                //node handle display:
                for (int i = 1; i < _chartState.m_listTargetObj.Count; i++)
                {
                    if (i != 1)
                    {
                        Handles.Label(_chartState.m_listTargetObj[i].transform.position, "Target_" + (i-1).ToString(), style);
                    }
                    if (_chartState.m_listTargetObj[i])
                    {
                        _chartState.m_listTargetObj[i].transform.position = Handles.PositionHandle(_chartState.m_listTargetObj[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }
        else
        {
            if (_chartState.m_listTargetV3.Count > 0)
            {
                //allow path adjustment undo:
                Undo.SetSnapshotTarget(_chartState, "Adjust iTween Path");

                //path begin and end labels:
                Handles.Label(_chartState.m_listTargetV3[0], "CenterTarget", style);

                //node handle display:
                for (int i = 0; i < _chartState.m_listTargetV3.Count; i++)
                {
                    if (i != 0)
                    {
                        Handles.Label(_chartState.m_listTargetV3[i], "Target_" + i.ToString(), style);
                    }
                    _chartState.m_listTargetV3[i] = Handles.PositionHandle(_chartState.m_listTargetV3[i], Quaternion.identity);
                }
            }
        }
	}
}
