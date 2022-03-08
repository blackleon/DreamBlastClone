using Unity.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [ReadOnly] public string color;
    public Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    
    public void Spawn(string _color)
    {
        color = _color;
        spriteRenderer.color = color.ToColor();
        rig.velocity = Vector2.down * 5f;
    }
    
    public void Pool()
    {
        transform.position = Vector3.zero;
        rig.velocity = Vector2.zero;
        
        SpawnController.Instance.PoolBall(this);
    }
    
    private void OnMouseDown()
    {
        SpawnController.Instance.CheckScore(this);
    }
}