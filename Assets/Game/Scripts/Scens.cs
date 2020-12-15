using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scens : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ScensChange(string name)
    {
        SceneManager.LoadScene(name);
    }
}
