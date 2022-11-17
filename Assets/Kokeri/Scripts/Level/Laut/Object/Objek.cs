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
    // private bool playSFXForOnce = true;

    private void Start()
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
        UbahSpeed();
        if(textUi.convertScore >= 10)
        {
            textUi.koin += 2;
            textUi.convertScore -= 10; 
        }

        if (!tarik.moveDown)
        {
            cirColl.enabled = false;
            /*if (playSFXForOnce && !tarik.canRotate)
            {
                AudioManager.Instance.PlaySFX("Tarik");
                playSFXForOnce = false;
            }*/
        }

        if (tarik.canRotate)
        {
            cirColl.enabled = true;
          /*  if (!playSFXForOnce)
            {
                playSFXForOnce = true;
                AudioManager.Instance.StopSFX();
            }*/
        }

        if (isTarik)
        {
            transform.position = objectHolder.transform.position;
            tarik.moveDown = false;
            
            if (tarik.canRotate)
            {
            AudioManager.Instance.StopSFX();
                Object.Destroy(gameObject);
                isTarik = false;

                tarik.move_speed = tarik.initialMoveSpeed;
                textUi.TotalSkor += skor;
                textUi.convertScore += skor;
                cirColl.enabled = true;

                if (isSampah)
                {
                    AudioManager.Instance.PlaySFX("Feedback Sampah");
                    textUi.nyawaPlayer -= 1;
                    if (textUi.minNyawa < 0)
                    {
                        textUi.nyawaPlayer = 0;
                    }
                    if (textUi.nyawaPlayer >= 0)
                        textUi.iconNyawa[textUi.nyawaPlayer].enabled = false;
                }
                else
                {
                    AudioManager.Instance.PlaySFX("Feedback Ikan");
                    textUi.ikanCounter += 1;
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

    public virtual void UbahSpeed()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Penghapus")
        {
            Object.Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Kail")
        {
           AudioManager.Instance.PlaySFX("Tarik");
           isTarik = true;
           cirColl.enabled = false;
           tarik.move_speed -= berat;
        }
        

    }


}
