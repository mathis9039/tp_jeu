using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _scoreText;
    private float _timer;
    private int _score;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText = gameObject.GetComponent<Text>();
        print(_scoreText);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 1f) {

            _score += 5;

            //We only need to update the text if the score changed.
            _scoreText.text = "Score : " + _score;

            //Reset the timer to 0.
            _timer = 0;
        }
    }
}

    
