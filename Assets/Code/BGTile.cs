using UnityEngine;

public class BGTile : MonoBehaviour
{
    private Material mat;
    private Vector2 offset;
    public float x;

    void Start()
    {
        // Obtener el material del Renderer del mismo GameObject
        mat = GetComponent<Renderer>().material;
        offset = mat.mainTextureOffset;
    }

    void Update()
    {
        // Incrementar el offset en el eje X con el tiempo
        offset.x += Time.deltaTime/x;
        mat.mainTextureOffset = offset;
    }
}
