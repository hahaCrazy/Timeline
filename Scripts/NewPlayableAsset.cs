using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class NewPlayableAsset : PlayableAsset
{
    //This allows you to use GameObjects in your Scene
    public ExposedReference<GameObject> m_MySceneObject;
    //This variable allows you to decide the velocity of your GameObject
    public Vector3 m_SceneObjectVelocity;

    [Header("对话框")]
    public ExposedReference<Text> showText;
    [Multiline(3)]
    public string dialogStr;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject myGameObject)
    {
        NewPlayableBehaviour playableBehaviour = new NewPlayableBehaviour();

        playableBehaviour.m_MySceneObject = m_MySceneObject.Resolve(graph.GetResolver());
        playableBehaviour.m_SceneObjectVelocity = m_SceneObjectVelocity;

        playableBehaviour.showText = showText.Resolve(graph.GetResolver());
        playableBehaviour.strs = dialogStr;

        //Create a custom playable from this script using the Player Behaviour script
        return ScriptPlayable<NewPlayableBehaviour>.Create(graph, playableBehaviour);
    }
}
