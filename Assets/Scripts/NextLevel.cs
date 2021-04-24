using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // print(other.gameObject);
        if (other.gameObject.CompareTag("END"))
        {
            Debug.Log("END LEVEL");
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadSceneAsync("SecondGame");
    }
    
}
