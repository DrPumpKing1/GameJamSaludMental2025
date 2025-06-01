using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float overlapRatio;
    public float left => transform.position.x;
    public float width => box.size.x;

    private void Update()
    {
        if(VisibilityChecker.Instance.PassedViewport(left, width)) Unload();
    }

    public float Paint(float left, float height, Sprite sprite)
    {
        this.sprite.sprite = sprite;

        float newWidth = (sprite.texture.width / sprite.pixelsPerUnit) * overlapRatio;
        float newHeight = sprite.texture.height / sprite.pixelsPerUnit;

        box.size = new Vector2(newWidth, newHeight);
        transform.position = new(left + newWidth/2, height + newHeight /2);

        return newWidth;
    }

    private void Unload()
    {
        BackgroundTiler.Instance.Unload(this);
    }
}
