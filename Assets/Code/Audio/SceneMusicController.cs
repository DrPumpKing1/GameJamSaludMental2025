using UnityEngine;

public class SceneMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private bool loopMusic = true; // Permite decidir si la música se repite o no

    void Start()
    {
        // Asegúrate de que el AudioManagerSingleton esté disponible y luego reproduce la música.
        if (AudioManagerSingleton.audioManager != null)
        {
            AudioManagerSingleton.audioManager.PlayMusic(sceneMusic, loopMusic);
        }
    }
}
