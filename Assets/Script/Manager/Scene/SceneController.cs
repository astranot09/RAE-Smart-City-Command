using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private Animator transition;
    public float transitionTime = 1f;

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadLevelRoutine(sceneName));
    }
    public void LoadSceneByIndexPlus()
    {
        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        int nextBuildIndex = currentBuildIndex + 1;

        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadLevelRoutine(nextBuildIndex));
        }
        else
        {
            Debug.LogWarning("Already at the last scene in Build Settings!");
        }
    }

    private IEnumerator LoadLevelRoutine(string sceneName)
    {
        if(transition == null)
        {
            transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        }


        if (transition != null)
        {
            transition.SetTrigger("Start");
        }

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadLevelRoutine(int sceneIndex)
    {
        if (transition == null)
        {
            transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
        }

        if (transition != null)
        {
            transition.SetTrigger("Start");
        }

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

}
