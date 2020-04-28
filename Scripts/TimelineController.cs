using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineController : MonoBehaviour
{
    public Transform character;
    public PlayableDirector director;
    public TimelineAsset timelineAsset;


    private void Start()
    {
        InitDirector();
        AddClipToTrack();
    }
    private void InitDirector() {
        director = character.GetComponent<PlayableDirector>();
        if (director == null)
            director = character.gameObject.AddComponent<PlayableDirector>();
        timelineAsset = Resources.Load<TimelineAsset>("playable/TempTimeline");
        director.playableAsset = timelineAsset as PlayableAsset;
        AnimationTrack actorAnimTrack = timelineAsset.CreateTrack<AnimationTrack>("actorAnimTrack");
        foreach (PlayableBinding bind in timelineAsset.outputs) {
            SetTrackDynamic(bind, director.gameObject);
        }
    }

    public void SetTrackDynamic(PlayableBinding bind, Object obj) {
        director.SetGenericBinding(bind.sourceObject, obj);
    }
    public AnimationClip animClip;
    public void AddClipToTrack() {
        List<TrackAsset> trackList = new List<TrackAsset>();
        foreach (TrackAsset asset in timelineAsset.GetOutputTracks()) {
            trackList.Add(asset);
        }
        //TimelineClip timelineClip = trackList[0].CreateClip<TrackAsset>();
        TimelineClip timelineClip = trackList[0].CreateDefaultClip();
        AnimationPlayableAsset animAsset = timelineClip.asset as AnimationPlayableAsset;
        animAsset.clip = animClip;
        timelineClip.displayName = animClip.name;
    }

    private void OnDestroy()
    {
        foreach (TrackAsset asset in timelineAsset.GetOutputTracks())
            timelineAsset.DeleteTrack(asset);
    }
}
