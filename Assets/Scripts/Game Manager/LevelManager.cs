using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] Animator transition;
    //[SerializeField] RuntimeAnimatorController[] transitions;
    [SerializeField] float transitionTime = 1f;
   public void Play()
    {
        SceneManager.LoadScene(1);
    }

    // Close the application
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //transition.runtimeAnimatorController = transitions[Random.Range(0,
        //    transitions.Length)];

        transition.SetTrigger("Start");
        AudioManager.instance.FadeOut("Theme", transitionTime);
        yield return new WaitForSeconds(transitionTime);

        AudioManager.instance.FadeIn("Theme", transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
