using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objek : MonoBehaviour
{ 
    private Tarik tarik;
    private TextUI textUi;


    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private GameObject tali;
    private GameObject kail;
    private GameObject objectHolder;
    private CircleCollider2D cirColl;

    public float berat, kecepatan;
    public int skor;
    public bool isKanan, isTarik, isSampah;

    public virtual void Start()
    {
        tarik = FindObjectOfType<Tarik>();
        textUi = FindObjectOfType<TextUI>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();


        player = GameObject.Find("Player");
        tali = player.transform.GetChild(0).gameObject;
        kail = tali.transform.GetChild(0).gameObject;
        objectHolder = kail.transform.GetChild(0).gameObject;
        cirColl = objectHolder.GetComponent<CircleCollider2D>();

        isTarik = false;

        if (transform.position.x < 16f)
        {
            isKanan = true;
        }

        if (transform.position.x < -12f)
        {
            isKanan = false;
        }


    }

    private void Update()
    {

        if(textUi.convertScore >= 10)
        {
            textUi.koin += 2;
            textUi.convertScore -= 10; 
        }

        if (!tarik.moveDown)
            cirColl.enabled = false;

        if (tarik.canRotate)
            cirColl.enabled = true;

        if (isTarik)
        {
            transform.position = objectHolder.transform.position;
            tarik.moveDown = false;
            
            if (tarik.canRotate)
            {
                Object.Destroy(gameObject);
                isTarik = false;

                tarik.move_speed = tarik.initialMoveSpeed;
                textUi.TotalSkor += skor;
                textUi.convertScore += skor;
                cirColl.enabled = true;

                if (isSampah)
                {
                    textUi.nyawaPlayer -= 1;
                    if (textUi.minNyawa < 0)
                    {
                        textUi.nyawaPlayer = 0;
                    }
                    if (textUi.nyawaPlayer >= 0)
                        textUi.iconNyawa[textUi.nyawaPlayer].enabled = false;
                }

            }

        }

        if (isKanan == false)
        {
            transform.Translate(Vector3.right * kecepatan * Time.deltaTime);
            spriteRenderer.flipX = true;
            if(transform.tag == "IkanKebalik")
            {
                spriteRenderer.flipX = false;
            }
        }

        if (isKanan == true)
        {
            transform.Translate(Vector3.left * kecepatan * Time.deltaTime);
            spriteRenderer.flipX = false;
            if (transform.tag == "IkanKebalik")
            {
                spriteRenderer.flipX = true;
            }
        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Penghapus")
        {
            Object.Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Kail")
        {
           tarik.move_speed -= berat;
           isTarik = true;
           cirColl.enabled = false;
        }
        

    }


}
