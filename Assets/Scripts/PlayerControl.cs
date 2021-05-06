using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayerControl : MonoBehaviour
{
    public static int speed;
    private AudioSource _audio;
    [SerializeField] private AudioClip carCrash;
    public bool isCrashed;
    private VideoPlayer videoPlayer;
    private string path;
    private AudioSource myAudioSource;
    private string activeScene;

    public GameObject Canvas;

    public float timer = 5;

    public void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        SetUpVideo();
        myAudioSource = GetComponent<AudioSource>();
        isCrashed = false;
        _audio = GetComponent<AudioSource>();
        setUpSpeed();
    }

    private void setUpSpeed()
    {
        if (activeScene.Equals("Level1"))
        {
            speed = 15;
        }
        else
        {
            speed = 20;
        }
    }

    private void SetUpVideo()
    {
        path = Application.dataPath;

        GameObject camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.url = path + "/Video/crash.mp4";
        videoPlayer.Prepare();
    }

    void Slowmo(){
        Time.timeScale = Mathf.Lerp(1, 0.6f, 5);
    }

    void Normal(){
        Time.timeScale = Mathf.Lerp(0.1f, 1, 5);
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

            if(Input.GetKey(KeyCode.E) && timer > 0){
                timer -= Time.deltaTime;
                Slowmo();
            }
                else{
                Normal();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            myAudioSource.Stop();
            videoPlayer.Play();
            Debug.Log("collision");
            _audio.PlayOneShot(carCrash, 100);
            isCrashed = true;
            Destroy(Canvas);
            StartCoroutine(Crash());
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
        yield return new WaitForSeconds(6);
        if (activeScene.Equals("Level1"))
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