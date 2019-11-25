using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void Restarts()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Restarts();
        }
    }
}