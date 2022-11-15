using UnityEngine;

public class HutanPlayerSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRendererComponent;

    private void Awake()
    {
        spriteRendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
    }
}