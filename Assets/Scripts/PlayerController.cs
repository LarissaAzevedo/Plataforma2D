using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    private GameController _GameController;

    private Rigidbody2D playerRb;

    public float speed;

    public float jumpForce;

    public Transform groundCheck;

    private bool isGrounded;

    private bool isAttack;

    private Animator playerAnimator;

    public bool isLookLeft;

    public Transform mao;

    public GameObject hitboxPrefab;
   



	// Use this for initialization
	void Start () {

        //dá acesso ao componente rigidBody do player
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;

    }
	
	// Update is called once per frame
	void Update () {

        // pegar a entrada do teclado e atribuir valor pra variável
        float h = Input.GetAxisRaw("Horizontal");

        if (isAttack && isGrounded)
        {
            h = 0;
        }

        //chamando a função que vira o personagem pra direção que ele está olhando
        if (h > 0 && isLookLeft) //se ta olhando pra esquerda
        {
            Flip();
        }else if (h < 0 && !isLookLeft) //se ta olhando pra direita
        {
            Flip();
        }

        //velocidade vertical
        float speedY = playerRb.velocity.y;

        //se o botão de pulo for pressionado
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            _GameController.playSFX(_GameController.sfxJump, 0.5f);
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if(Input.GetButtonDown("Fire1") && isAttack == false)
        {
            _GameController.playSFX(_GameController.sfxAttack, 0.5f);
            isAttack = true;
            playerAnimator.SetTrigger("attack");        
        }

        //anda pra frente e pra trás
        playerRb.velocity = new Vector2(h * speed, speedY);

        playerAnimator.SetInteger("h", (int) h);
        playerAnimator.SetBool("isGrounded", isGrounded);
        playerAnimator.SetFloat("speedY", speedY);
        playerAnimator.SetBool("isAttack", isAttack);
		
	}

    //função que atualiza de 0.2 segundos para físicas
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "coletavel")
        {
            _GameController.playSFX(_GameController.sfxCoin, 0.5f);
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.tag == "damage")
        {
            print("Dano");
        }
    }

    void Flip() //inverte o valor do scale x, o que faz o personagem olhar para a esquerda e direita
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        //devolve o valor que foi alterado
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void EndAttack()
    {
        isAttack = false;
    }

    void hitBoxAttack()
    {
        GameObject hitboxTempo = Instantiate(hitboxPrefab, mao.position, transform.localRotation);
        Destroy(hitboxTempo, 0.2f);
            
    }

    void footStep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 1f);
    }
}
