using UnityEngine;

public class BallController : MonoBehaviour
{
    public string color;
    public Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    
    public void Spawn(string _color)
    {
        color = _color;
        spriteRenderer.color = color.ToColor();
        rig.velocity = Vector2.down * 5f;
    }
    
    private void OnMouseDown()
    {
        SpawnController.Instance.CheckScore(this);
    }
}