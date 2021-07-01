using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;

    //public static float mySpeed
    //{
    //    get 
    //    {
    //        return speed;
    //    }
    //    set
    //    {
    //        speed = value;
    //    }
    //}

    public float laneSpeed;
    public float jumpLength;
    public float JumpSpeed;

    public float jumpHeight;
    public float slideLength;
    public int maxLife = 3;
    public float minSpeed = 5f;
	//public float maxSpeed = 10f;
    public float invincibleTime;
    public GameObject model;

    public static bool isMagnet;
    public bool isSpeedUp=false;



    private Animator anim;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    private Vector3 boxColliderSize;
    private bool jumping = false;
    private float jumpStart;
    private bool sliding = false;
    private float slideStart;
    private bool invincible = false;
    public static int currentLife;
    static int blinkingValue;
    private UIManager uiManager;
    public int coins;
    public float score;
    public int asilar;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        anim.Play("runStart");
        currentLife = maxLife;
        speed = minSpeed;
        blinkingValue = Shader.PropertyToID("_BlinkingValue");
        uiManager = FindObjectOfType<UIManager>();
        isMagnet = false;
       

    }
    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * speed;
        uiManager.UpdateScore((int)score);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }
        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if (ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if (ratio >= 1f)
            {
                sliding = false;
                anim.SetBool("Sliding", false);
                boxCollider.size = boxColliderSize;
            }
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime*slideLength);

        if (currentLife==1 && GameOverMenu.isDeadGM) 
        {
            speed = tempSpeed;
            anim.SetBool("Dead", false);
            anim.Play("runStart");
            GameOverMenu.isDeadGM = false;
            asilar -= 30;
            uiManager.UpdateAsi(asilar);
        }
    }

    private float tempSpeed;
    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }


    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }
    void Slide()
    {
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / slideLength);
            anim.SetBool("Sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            sliding = true;

        }
    }
    void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / jumpLength);
            anim.SetBool("Jumping", true);
            jumping = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            //PlayServices.IncrementAchievment(EndlessRunnerServices.achievement_colete_100_peixes, 1);
            coins++;
            uiManager.UpdateCoins(coins);
            other.transform.gameObject.SetActive(false);
        }
        if (other.CompareTag("Magnet")) 
        { 
            isMagnet = true;
            other.transform.gameObject.SetActive(false);
        }
        if (other.CompareTag("SpeedUp"))
        {
            if(isSpeedUp==false)
            {
                isSpeedUp = true;
                other.transform.gameObject.SetActive(false);
                StartCoroutine(speedUp());
            }
        }
        if (other.CompareTag("Asi"))
        {
            asilar++;
            uiManager.UpdateAsi(asilar);
            other.transform.gameObject.SetActive(false);

        }

        if (invincible)
           return;

        if (other.CompareTag("Obstacle"))
        {
            if(!isSpeedUp)
            {
                //canMove = false;
                currentLife--;
                uiManager.UpdateLives(currentLife);
                anim.SetTrigger("Hit");
                if (currentLife <= 0)
                {
                    tempSpeed = speed;
                    speed = 0;
                    anim.Play("Death");
                    anim.SetBool("Dead", true); //ölünce dead animasyonunu aktif et           
                    uiManager.gameOverPanel.SetActive(true); //gameover paneli aktif et.
                    
                    //Invoke("CallMenu", 10f);              
                }
                else
                {
                    //Invoke("CanMove", 0.75f);
                    StartCoroutine(Blinking(invincibleTime));
                }
            }
        }
    }
    IEnumerator speedUp()
    {
        speed *= 10f;
        yield return new WaitForSeconds(5f);
        speed /= 10f;
        yield return new WaitForSeconds(1f);
        isSpeedUp = false;
    }

    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float currentBlink = 1f;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;
        //bool enabled = false;
        
        yield return new WaitForSeconds(1f);
        speed = minSpeed;
        while (timer < time && invincible)
        {
            //model.SetActive(enabled);
            Shader.SetGlobalFloat(blinkingValue, currentBlink);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                //enabled = !enabled;
            }
        }
       // model.SetActive(true);
        Shader.SetGlobalFloat(blinkingValue, 0);
        invincible = false;
    }

    void CallMenu()
    {
        GameManager.gm.EndRun(); 
        
    }

}