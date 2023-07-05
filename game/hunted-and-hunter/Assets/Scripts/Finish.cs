using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Fox" || other.gameObject.name == "Rabbit")
        {
            SceneManager.LoadScene("EndScene");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
