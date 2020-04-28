using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Button triggerBtn;
    public bool isTrigger;
    public PlayableDirector playableDirector;

	// Use this for initialization
	void Start () {
        triggerBtn.onClick.AddListener(()=> {
            isTrigger = !isTrigger;
            if (isTrigger)
            {
                triggerBtn.transform.GetChild(0).GetComponent<Text>().text = "activing";
            }
            else {
                triggerBtn.transform.GetChild(0).GetComponent<Text>().text = "sleeping";
            }
        });	
	}

    private void Update()
    {
        if (isTrigger) {
            playableDirector.Play();
        }
    }
}
