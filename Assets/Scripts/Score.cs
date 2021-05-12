using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _scoreText;
    private float _timer;
    private int _score;

    void Start()
    {
        _scoreText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1f)
        {
            _score += 5;
            _scoreText.text = "Score : " + _score;
            _timer = 0;
        }
    }
}