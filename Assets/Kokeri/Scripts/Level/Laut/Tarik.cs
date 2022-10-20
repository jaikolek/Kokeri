using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarik : MonoBehaviour
{
    private TaliRenderer taliRenderer;
    public GameObject medan;

    public float rotationMinZ = -55f;
    public float rotationMaxZ = 55f;
    public float rotateSpeed = 50f;
    public float move_speed = 3f;
    public float maxY = -6.8f;

    private float rotateAngle;
    public float initialMoveSpeed;
    private float initialY;

    public bool moveDown;
    private bool rotateRight;
    public bool canRotate;

    private void Awake()
    {
        taliRenderer = GetComponent<TaliRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
        initialMoveSpeed = move_speed;
        canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        PlayerInput();
        MoveRope();

        if (move_speed <= 0)
        {
            move_speed = 0.7f;
        }

    }

    void Rotate() // untuk rotasi alat pancing
    {
        if (!canRotate)
            return;

        if (rotateRight)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
        }else{
            rotateAngle -= rotateSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);

        if (rotateAngle >= rotationMaxZ) 
            rotateRight = false;
        else if (rotateAngle <= rotationMinZ) 
            rotateRight = true;
    }

    void PlayerInput() // ketika player tap
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canRotate)
            {
                canRotate = false;
                moveDown = true;
            }
        }
    }

    void MoveRope() // untuk menggerakkan kail ke bawah
    {
        if (canRotate)
            return;

        if (!canRotate)
        {
            // play sound
            Vector3 temp = transform.position;

            if (moveDown)
            {
                temp -= transform.up * Time.deltaTime * move_speed;
                taliRenderer.RenderLine(temp, true);
            }
            else
            {
                temp += transform.up * Time.deltaTime * move_speed;
                taliRenderer.RenderLine(temp, true);
            }

            transform.position = temp;

            if (temp.y <= maxY)
                moveDown = false;

            if (temp.y >= initialY)
            {
                canRotate = true;
                move_speed = initialMoveSpeed;
                taliRenderer.RenderLine(temp, false);
            }

        }
    }
}
