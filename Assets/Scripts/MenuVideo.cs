using UnityEngine;
using UnityEngine.Video;

public class MenuVideo : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    private string _path;

    void Start()
    {
        SetUpVideo();
    }

    private void SetUpVideo()
    {
        _path = Application.dataPath;

        GameObject camera = GameObject.Find("DummyCamera");
        _videoPlayer = camera.AddComponent<VideoPlayer>();
        _videoPlayer.playOnAwake = false;
        _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        _videoPlayer.url = _path + "/Video/2FAST2FURIOUS.mp4";
        _videoPlayer.isLooping = true;
        _videoPlayer.Play();
    }
}