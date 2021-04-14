using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    float delayLoadLevel = 3f;

    [SerializeField] AudioClip FinishLevel;
    [SerializeField] AudioClip Crash;

    [SerializeField] ParticleSystem FinishParticles;
    [SerializeField] ParticleSystem ExplosionParticles;
    [SerializeField] ParticleSystem FireParticles;

    AudioSource rocketAudio;
    MeshCollider CollisionDetection;

    bool isLoading = false;
    bool collisionDisable = false;

    void Start()
    {
        rocketAudio = GetComponent<AudioSource>();
        CollisionDetection = GetComponent<MeshCollider>();
    }

    void Update()
    {
        RespondToCheats();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isLoading || collisionDisable){return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You bumped into a friendly");
                break;

            case "Finish":
                StartNextLevel();
                break;

            case "Fuel":
                Debug.Log("You picked up fuel!");
                break;

            default:
                StartCrashSequence();
                break;

        }
    }

    void RespondToCheats()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
            Debug.Log("You loaded the next level.");
        }

        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //Toggle collision
            Debug.Log("Disbaled Collisions");

        }

    }


    void StartCrashSequence()
    {
        isLoading = true;
        GetComponent<Movement>().enabled = false;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(Crash);
        ExplosionParticles.Play();
        FireParticles.Play();
        Invoke("ReloadLevel", delayLoadLevel);
    }

    void StartNextLevel()
    {
        isLoading = true;
        GetComponent<Movement>().enabled = false;
        rocketAudio.Stop();
        rocketAudio.PlayOneShot(FinishLevel);
        FinishParticles.Play();
        Invoke("LoadNextLevel", delayLoadLevel);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
