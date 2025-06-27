using UnityEngine;
using TMPro; // Importa el namespace de TextMeshPro
using System.Collections.Generic; // Necesario para List

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI gameOverText; // Asigna tu TextMeshProUGUI desde el Inspector
    public List<string> gameOverPhrases = new List<string>(); // Lista de frases para el Game Over

    void Start()
    {
        // Aseg�rate de que la lista no est� vac�a antes de intentar mostrar una frase
        if (gameOverPhrases.Count > 0)
        {
            DisplayRandomPhrase();
        }
        else
        {
            Debug.LogWarning("La lista de frases de Game Over est� vac�a. Agrega algunas frases en el Inspector.");
        }
    }

    void DisplayRandomPhrase()
    {
        // Genera un �ndice aleatorio dentro del rango de la lista de frases
        int randomIndex = Random.Range(0, gameOverPhrases.Count);

        // Asigna la frase aleatoria al componente TextMeshPro
        gameOverText.text = gameOverPhrases[randomIndex];
    }

    // Puedes llamar a esta funci�n desde cualquier otro script
    // cuando necesites activar la pantalla de Game Over y mostrar una frase.
    public void ShowGameOver()
    {
        // Aseg�rate de que el objeto de texto est� activo si lo desactivas normalmente
        gameOverText.gameObject.SetActive(true);
        DisplayRandomPhrase();
    }
}
