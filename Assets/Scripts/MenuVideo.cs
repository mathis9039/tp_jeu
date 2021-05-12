using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private string path;
    // Start is called before the first frame update
    void Start()
    {
        SetUpVideo();
    }
    
    private void SetUpVideo()
    {
        path = Application.dataPath;

        GameObject camera = GameObject.Find("DummyCamera");
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        //videoPlayer.targetCameraAlpha = 0.5F;
       // videoPlayer.SetDirectAudioMute(0,true);
        videoPlayer.url = path + "/Video/2FAST2FURIOUS.mp4";
        videoPlayer.isLooping = true;
       // videoPlayer.Prepare();
        videoPlayer.Play();
    }
}
