using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip deathSound;

    [SerializeField] ParticleSystem successParticals;
    [SerializeField] ParticleSystem deathParticals;
    [SerializeField] float delaySeconds = 1f;
    AudioSource audioSource;
    bool isTransitioning = false;
 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning != true)
        {
            if (other.gameObject.tag == "Fuel")
            {
                Debug.Log("This gave me fuel, lets goo!");
            }
            if (other.gameObject.tag == "Untagged")
            {
                StartCrashSequence();
            }
            if (other.gameObject.tag == "Finish")
            {
                StartSuccessSequence();
            }
            else 
            {
                Debug.Log("This thing is friendly");
            }
        }
    }
    
    void StartSuccessSequence()
    {
        // audioSource.Stop();
        isTransitioning = true;
        successParticals.Play();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delaySeconds);
    }
    void StartCrashSequence()
    {
        // audioSource.Stop();
        isTransitioning = true;
        deathParticals.Play(true);
        audioSource.PlayOneShot(deathSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delaySeconds);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
