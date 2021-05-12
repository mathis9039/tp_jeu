using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerControl : MonoBehaviour
{
    private static int _speed;
    private AudioSource _audio;
    [SerializeField] private AudioClip carCrash;
    private bool _isCrashed;
    private VideoPlayer _videoPlayer;
    private string _path;
    private AudioSource _myAudioSource;
    private string _activeScene;
    public GameObject canvas;
    private float _timer = 5;

    public void Start()
    {
        _activeScene = SceneManager.GetActiveScene().name;
        SetUpVideo();
        _myAudioSource = GetComponent<AudioSource>();
        _isCrashed = false;
        _audio = GetComponent<AudioSource>();
        SetUpSpeed();
    }

    private void SetUpSpeed()
    {
        if (_activeScene.Equals("Level1"))
        {
            _speed = 15;
        }
        else
        {
            _speed = 20;
        }
    }

    private void SetUpVideo()
    {
        _path = Application.dataPath;

        GameObject camera = GameObject.Find("Main Camera");
        _videoPlayer = camera.AddComponent<VideoPlayer>();
        _videoPlayer.playOnAwake = false;
        _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        _videoPlayer.url = _path + "/Video/crash.mp4";
        _videoPlayer.Prepare();
    }

    void Slowmo()
    {
        Time.timeScale = Mathf.Lerp(1, 0.6f, 5);
    }

    void Normal()
    {
        Time.timeScale = Mathf.Lerp(0.1f, 1, 5);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        if (!_isCrashed)
        {
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !transform.position.x.Equals(-20))
            {
                transform.position = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !transform.position.x.Equals(-5))
            {
                transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
            }

            if (Input.GetKey(KeyCode.E) && _timer > 0)
            {
                _timer -= Time.deltaTime;
                Slowmo();
            }
            else
            {
                Normal();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            _myAudioSource.Stop();
            _videoPlayer.Play();
            _audio.PlayOneShot(carCrash, 100);
            _isCrashed = true;
            Destroy(canvas);
            StartCoroutine(Crash());
        }

        if (other.gameObject.CompareTag("END"))
        {
            StartCoroutine(EndLevel());
        }

        if (other.gameObject.CompareTag("FINISH"))
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator Crash()
    {
        yield return new WaitForSeconds(6);
        if (_activeScene.Equals("Level1"))
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        else
        {
            SceneManager.LoadSceneAsync("Level2");
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Level2");
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("Menu");
    }
}