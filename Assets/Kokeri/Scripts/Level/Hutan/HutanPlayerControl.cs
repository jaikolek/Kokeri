using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutanPlayerControl : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    private BoxCollider2D playerBoxCollider;
    private CircleCollider2D playerCircleCollider;
    private bool isOnGround;
    private bool isCrouch;

    [SerializeField] float jumpForce;

    [Header("Sprites")]
    [SerializeField] private Transform playerRadiusTransform;

    public bool IsCrouch { get => isCrouch; set => isCrouch = value; }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        playerCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        HutanEventManager.Instance.OnJump += HutanEventManager_OnJump;
        HutanEventManager.Instance.OnCrouch += HutanEventManager_OnCrouch;
        HutanEventManager.Instance.OnStand += HutanEventManager_OnStand;
        HutanEventManager.Instance.OnCatch += HutanEventManager_OnCatch;
    }

    private void HutanEventManager_OnJump()
    {
        PlayerJump();
    }

    private void HutanEventManager_OnCrouch()
    {
        isCrouch = true;
    }

    private void HutanEventManager_OnStand()
    {
        isCrouch = false;
    }

    private void HutanEventManager_OnCatch()
    {
        PlayerCatch();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }

        if (Input.GetKey(KeyCode.LeftShift) || isCrouch)
        {
            PlayerCrouch();
        }
        else
        {
            PlayerStand();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            PlayerCatch();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isOnGround = true;
    }

    private void PlayerJump()
    {
        if (!isOnGround)
            return;

        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
    }

    private void PlayerCrouch()
    {
        playerBoxCollider.offset = new Vector2(0, 0.5f);
        playerBoxCollider.size = new Vector2(1, 1);

        playerCircleCollider.offset = new Vector3(0, 1, 0);
        playerRadiusTransform.position = transform.position + new Vector3(0, 1, 0);

        if (!isOnGround)
            playerRigidbody.AddForce(Vector2.down * jumpForce * 0.01f, ForceMode2D.Impulse);
    }

    private void PlayerStand()
    {
        playerBoxCollider.offset = new Vector2(0, 0.75f);
        playerBoxCollider.size = new Vector2(1, 1.5f);

        playerCircleCollider.offset = new Vector2(0, 1.5f);
        playerRadiusTransform.position = transform.position + new Vector3(0, 1.5f, 0);
    }

    private void PlayerCatch()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(0, 1f, 0), 2.5f);


        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bug"))
            {
                Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
                Destroy(collider.gameObject);

                // ini Kumbang awokawok
                HutanGameManager.Instance.IncrementBug();
            }
        }
    }
}
