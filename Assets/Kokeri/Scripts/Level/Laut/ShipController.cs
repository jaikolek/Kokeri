using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region singleton
    private static ShipController instance;
    public static ShipController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShipController>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion singleton

    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float offset = 5f;
    [SerializeField] private float time = 3f;
    [SerializeField] private bool rotateRight = false;
    private bool shake = true;
    private bool isRight = false;
    private float value;

    private Transform defaultTransform;

    private void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        defaultTransform = this.transform;
        value = this.transform.rotation.z;
    }

    private void Update()
    {
        //ShakeController();
    }

    private void ShakeController()
    {
        if (shake)
        {
            if (transform.rotation.z > offset)
            {
                isRight = false;
            }
                
            if (transform.rotation.z < -offset)
            {
                isRight = true;
            }

            if (isRight)
            {
                value -= speed;
                this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, value, this.transform.rotation.w);
            }

            if (!isRight)
            {
                value += speed;
                this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, value, this.transform.rotation.w);
            }  
        }
    }

    private IEnumerator PlayShake()
    {
        yield return new WaitForSeconds(time);
        shake = false;
    }

    public void Shake()
    {
        shake = true;
        StartCoroutine(PlayShake());
        this.transform.position = defaultTransform.position;
        this.transform.rotation = defaultTransform.rotation;
    }   
    
    public void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
    }
}
