using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrans : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //������
    public void LoadTo(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    //���}�C��
    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
