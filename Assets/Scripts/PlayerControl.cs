using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerControl : MonoBehaviour
{
    public static int speed;
    private AudioSource _audio;
    [SerializeField] private AudioClip carCrash;
    private bool isCrashed;
    private VideoPlayer videoPlayer;

    public void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        // Set the video to play. URL supports local absolute or relative paths.
        // Here, using absolute.
        videoPlayer.url = "/Users/Jaymu/Downloads/crash.mp4";

        // Skip the first 100 frames.
        //     videoPlayer.frame = 100;

        // Restart from beginning when done.
        videoPlayer.isLooping = true;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Prepare();


        isCrashed = false;
        _audio = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
        {
            speed = 15;
        }
        else
        {
            speed = 25;
        }
    }

    public void Update()
    {
        if (!isCrashed)
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !transform.position.x.Equals(-20))
            {
                transform.position = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !transform.position.x.Equals(-5))
            {
                transform.position = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // print(other.gameObject);
        if (other.gameObject.CompareTag("NPC"))
        {
            videoPlayer.Play();
            Debug.Log("collision");
            _audio.PlayOneShot(carCrash, 100);
            isCrashed = true;

            //   StartCoroutine(Crash());
        }

        if (other.gameObject.CompareTag("END"))
        {
            Debug.Log("END LEVEL");
            StartCoroutine(EndLevel());
        }

        if (other.gameObject.CompareTag("FINISH"))
        {
            Debug.Log("END GAME");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator Crash()
    {
        yield return new WaitForSeconds(0.15f);
        if (SceneManager.GetActiveScene().name.Equals("Level1"))
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