using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayMovie : MonoBehaviour {

    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    // Use this for initialization
    void Start () {
        videoPlayer = this.GetComponent<VideoPlayer>();
        rawImage = this.GetComponent<RawImage>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (videoPlayer.texture == null)
        {
            return;
        }

        rawImage.texture = videoPlayer.texture;

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("startMenu");
        }
	}
}
