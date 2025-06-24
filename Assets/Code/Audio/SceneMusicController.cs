using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private bool loopMusic = true; // Permite decidir si la m�sica se repite o no

    void Start()
    {
        // Aseg�rate de que el AudioManagerSingleton est� disponible y luego reproduce la m�sica.
        if (AudioManagerSingleton.audioManager != null)
        {
            AudioManagerSingleton.audioManager.PlayMusic(sceneMusic, loopMusic);
        }
    }
}
