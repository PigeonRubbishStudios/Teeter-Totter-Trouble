using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine("EndGame");
        }

        if (other.gameObject.GetComponent<PaperWeight>())
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
