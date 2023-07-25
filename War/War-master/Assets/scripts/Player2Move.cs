using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player2Move : MonoBehaviour
{

	float player2Speed = 0.1f;
	bool isJump;
	float jump = 20f;
	private bool playerIsAttack;
	Animator anim;
	public Rigidbody2D rigidbody2D;
	public Transform playerAttackPoint;
	public float playerAttackRange = 0.5f;
	public LayerMask playerLayers;
	public int attackDamage2=20;
	public int maxHealth = 100;
	private int currentHealth;
	public static float current=0.05f;
	public Text winText2;
	[SerializeField] private AudioSource jumpingSound;
	[SerializeField] private AudioSource AttackSound;
	[SerializeField] private AudioSource DeathSound;
	//private float delay = 4f;
	//private float timeElapsed;
	//bool isDead;
	// Start is called before the first frame update
	void Start()
    {
        playerIsAttack = false;
		anim = GetComponent<Animator>();
		isJump = false;
		rigidbody2D = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		//OnDrawGizmosSelected();
		//attackDamage = 20;
		current = 0.05f;
		//isDead = false;
		winText2.text = "";
	}
	//0.05782764

	private void PlayerMovement()
	{
		SpriteRenderer sR = GetComponent<SpriteRenderer>();
		bool player2MoveToRight = Input.GetKey(KeyCode.D);
		bool player2MoveToLeft = Input.GetKey(KeyCode.A);
		float horizontalAxis = Input.GetAxis("Horizontal2") * player2Speed;
		
		anim.SetFloat("PlayerSpeed", Mathf.Abs(horizontalAxis));
		if (player2MoveToRight)
		{
			GetComponent<Transform>().Translate(new Vector3(player2Speed, 0));
			sR.flipX = false;
		}
		if (player2MoveToLeft)
		{
			GetComponent<Transform>().Translate(new Vector3(-1 * player2Speed, 0));
			sR.flipX = true;
		}

	}
	
	private void PlayerAttack()
	{
		bool attack = Input.GetKey(KeyCode.W);
		if (attack && !playerIsAttack)
		{
			AttackSound.Play();
			anim.SetBool("PlayerIsAttack", true);
			playerIsAttack = true;
            //try { 
			Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(playerAttackPoint.position, playerAttackRange, playerLayers);
			//Debug.Log("null");
			try
			{

				foreach (Collider2D player in hitPlayers)
				{
					player.GetComponent<player1Move>().TakeDamage(20);
				}
				//Collider2D  pp= Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);
				/*if(pp)
				{
					Debug.Log("Hiihhhh");
				    pp.GetComponent<Player2Move>().TakeDamage(20);
			}*/
			}
			catch (NullReferenceException ex)
			{
				Debug.Log("Null");
			}
			//SceneManager.LoadScene(0);

		}
	//}
		if (!attack)
		{
			anim.SetBool("PlayerIsAttack", false);
			playerIsAttack = false;
		}
	}
	void OnDrawGizmosSelected()
	{
		if (playerAttackPoint == null)
			return;
		Gizmos.DrawWireSphere(playerAttackPoint.position, playerAttackRange);
	}

	private void PlayerJump()
	{
		bool jumping = Input.GetKey(KeyCode.Q);
		if (jumping && isJump == false)
		{
			jumpingSound.Play();
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jump);
			anim.SetBool("IsJump", true);
			isJump = true;
		}
		if (Mathf.Abs(rigidbody2D.velocity.y) < 0.01f)
		{
			anim.SetBool("IsJump", false);
			isJump = false;
		}
	}

	public void TakeDamage(int damage)
    {
		Debug.Log(damage);
		currentHealth = currentHealth - damage;
		current = current - 0.01f;
		anim.SetTrigger("Hurt");
        if (currentHealth <=0)
        {
			//Die(currentHealth);
			Die();
			//anim.SetBool("IsDead", false);
			//Respawn();
			winText2.text = "Player1 Win!!!";
			this.enabled = false;
			//Destroy(gameObject);
			//awaitTask.Delay(5000);
			//Respawn();
			//const time = setTimeout(Respawn,5000);
			//yield return new WaitForSeconds(5);
			//SceneManager.LoadScene("Menu");
		}
		//anim.SetBool("IsDead", false);
	}
	private void Respawn()
    {
		//Debug.Log("timeElapsed");
		//timeElapsed += Time.deltaTime;
		//yield return new WaitForSeconds(2f);

		/*while (timeElapsed != delay)
		{
			timeElapsed += Time.deltaTime;
		}*/
			//var timer = new Timer(Respawn, null, 0, 500);
			SceneManager.LoadScene("Menu");
		    //Destroy(gameObject);

	}
	void Die()
    {
		//timeElapsed = 0;
		//isDead = true;
		Debug.Log("here die");
		//Debug.Log("en die"+currentHealth);
		anim.SetBool("IsDead", true);
		DeathSound.Play();
		//Destroy(gameObject);
		//GetComponent<Collider2D>().enabled = false;
		//anim.SetBool("IsDead", false);
		//this.enabled = false;
		//this.enabled = false;
		//isDead = true;
		//anim.SetBool("IsDead", false);
	}

	// Update is called once per frame
	void Update()
    {
        if (CompareTag("player2"))
        {
			PlayerMovement();

			PlayerJump();

			PlayerAttack();
			//timeElapsed += Time.deltaTime;


		}

        
    }
}
