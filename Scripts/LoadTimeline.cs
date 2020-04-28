using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LoadTimeline : MonoBehaviour
{
    public Transform character;
    public PlayableDirector director;
    public TimelineAsset asset;

    public readonly Dictionary<string, PlayableBinding> bindDict = new Dictionary<string, PlayableBinding>();

    void Start()
    {
        InitTimeline();
        SetTrackClip();
    }

    private void InitTimeline()
    {
        director = character.GetComponent<PlayableDirector>();
        if (director == null)
            director = character.gameObject.AddComponent<PlayableDirector>();
        asset = Resources.Load("playable/unitychanTimeline") as TimelineAsset;
        director.playableAsset = asset as PlayableAsset;
        //可创建一个名为tt的PlayableTrack
        //asset.CreateTrack<PlayableTrack>(null, "tt"); 
        //asset.CreateTrack<AnimationTrack>();
        foreach (PlayableBinding i in director.playableAsset.outputs)
        {
            //sourceObject => 该行轨道(track, AnimationTrack、PlayableTrack)
            //outputTargetType => Animator
            Debug.Log(i.streamName + ":" + i.sourceObject + ":" + i.outputTargetType);
            if (i.streamName == "Animation Track0")
            {
                director.SetGenericBinding(i.sourceObject, director.gameObject);
            }
            if (!bindDict.ContainsKey(i.streamName))
            {
                bindDict.Add(i.streamName, i);
            }
        }
        //Debug.Log("Track rootCount:" + asset.rootTrackCount); //父节点
        //for (int i = 0; i < asset.rootTrackCount; i++) {
        //    Debug.Log("=>" + asset.GetRootTrack(i));
        //}
        //Debug.Log(asset.track)
        //Debug.Log("Track count:" + asset.outputTrackCount); //除开Group一行的节点
        //for (int i = 0; i < asset.rootTrackCount; i++)
        //{
        //    Debug.Log("=>" + asset.GetOutputTrack(i));
        //}
        //director.time = 5;
    }
    //动态设置轨道
    public void SetTrackDynamic(string trackName, GameObject obj)
    {
        if (bindDict.TryGetValue(trackName, out PlayableBinding value)) {
            director.SetGenericBinding(value.sourceObject, obj);
        }
    }
    //设置一行轨道的属性
    public AnimationClip animClip;
    public void SetTrackClip() {
        TrackAsset track = asset.GetOutputTrack(2);
        //修改
        foreach (TimelineClip clip in track.GetClips()) {
            Debug.Log("clip:" + clip.displayName); //clip name
            //CinemachineShot shot = clip.asset as CinemachineShot;
            //director.SetReferenceValue(shot.VirtualCamera.exposedName, cinemachineVirtualCam);
            AnimationPlayableAsset animAsset = clip.asset as AnimationPlayableAsset;
            animAsset.clip = animClip;
            clip.displayName = animClip.name;
            //director.SetReferenceValue(animAsset.clip.name, animClip); //没效果
        }
    }

}
