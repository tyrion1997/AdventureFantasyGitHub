using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 localscale;
    private BoxCollider2D playerBoxCollider;
    private int scores;
    //private Vector2 colliderSize;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Text score;


    private bool isJump = false;
    private bool isGround = true;
    private bool isFall = false;
    private bool isDeath = false;
    //private Vector2 lastGroundedPosition;
    //private int groundLayer;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        localscale = transform.localScale;
        playerBoxCollider = GetComponent<BoxCollider2D>();
        scores = 0;
        //colliderSize = playerBoxCollider.size;
        //groundLayer = LayerMask.NameToLayer("Ground");
    }

    // Update is called once per frame
    private void Update()
    {
        score.text = "" + scores;
        animator.SetBool("isGround", true);
        if(isDeath == false)
        {
            float dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

            animator.SetBool("Moving", dirX != 0);

            if (dirX != 0) transform.localScale = new Vector3(Mathf.Sign(dirX), localscale.y, localscale.z);
            //Mathf.Sign(dirX) to nic innego jak warto�ci 1/0/-1 w zale�no�ci czy idzie sie w prawo/stoi/lewo. Pozwala na szybka rotacje postaci wzgledem osi X (pionowo)

            // Ustaw pr�dko�� poruszania si� postaci
            rb.velocity = new Vector2(dirX, rb.velocity.y);

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && !isJump && isGround)
            {
                isGround = false;
                animator.SetBool("isGround", false);
                isJump = true;
                animator.SetBool("isJump", true);
                //Dodaj si�� skoku do postaci
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                //rb odnosi si� do komponentu Rigidbody2D przypisanego do postaci.Rigidbody2D jest odpowiedzialne za symulacj� fizyki i kontrol� nad ruchem obiektu.
                //AddForce jest funkcj�, kt�ra dodaje si�� do obiektu.W tym przypadku, dodajemy si�� skoku.
                //new Vector2(0f, jumpForce) tworzy wektor si�y, kt�ry zostanie zastosowany.Wektor ten ma warto��(0, jumpForce), co oznacza, �e si�a zostanie zastosowana tylko wzd�u� osi pionowej, aby wywo�a� ruch skoku. Warto�� jumpForce okre�la si�� skoku.
                //ForceMode2D.Impulse to tryb si�y. W tym przypadku, u�ywamy trybu impulsowego.Tryb impulsowy dodaje jednorazow�, natychmiastow� zmian� pr�dko�ci obiektu, co jest odpowiednie dla skoku.Dzi�ki temu posta� b�dzie natychmiastowo przyspiesza� w g�r�.

            }

            if (rb.velocity.y < -0.1f)
            {
                isJump = false;
                isFall = true;
                isGround = false;
                animator.SetBool("isJump", false);
                animator.SetBool("isFall", true);
            }
        }
    }

    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sprawd�, czy posta� dotyka ziemi
        if (collision.gameObject.CompareTag("Ground"))
        {
            isFall = false;
            isGround = true;
            animator.SetBool("isFall", false);
            animator.SetBool("isGround", true);
            //Debug.Log("Na ziemi");
        }
        if (collision.gameObject.CompareTag("spikes"))
        {
            isFall = false;
            isGround = true;
            animator.SetBool("isFall", false);
            animator.SetBool("isGround", true);
            animator.SetBool("isDeath", true);
            isDeath = true;
            StartCoroutine(WaitForDie());            
            //Debug.Log("smierc");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("cherry"))
        {
            scores += 1;
            Destroy(collision.gameObject);
            //Debug.Log("Wisienka");
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Koniec Gry");
            StartCoroutine(WaitForDie());
        }
    }
}
