using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
public class NewPlayableBehaviour : PlayableBehaviour
{
    public GameObject m_MySceneObject;
    public Vector3 m_SceneObjectVelocity;

    public Text showText;
    public string strs;

    public override void OnGraphStart(Playable playable)
    {
        //_dialog = dialog.Resolve(playable.GetGraph().GetResolver());
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {

        showText.gameObject.SetActive(true);
        showText.text = strs;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (showText) {
            showText.gameObject.SetActive(false);
        }
    }

    public override void PrepareFrame(Playable playable, FrameData frameData)
    {
        Debug.Log("Obj:" + m_MySceneObject.name);
        Debug.Log("speed:" + m_SceneObjectVelocity);
        if (m_MySceneObject != null)
            m_MySceneObject.transform.Translate(m_SceneObjectVelocity);
    }
}
