using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N���󂯕t����
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MapScene");
        }
            
    }
}
