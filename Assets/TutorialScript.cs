using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public class StartScript : MonoBehaviour
{

    public void StartTutorial()
    {
        SceneManager.LoadScene(1);
    }

}
}
