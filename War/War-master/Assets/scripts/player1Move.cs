using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class player1Move : MonoBehaviour
{
    float speed = 0.1f;
    bool isAttack;
    bool isJump;
    float jump=20f;
	public Animator animator;
	public Rigidbody2D rigidbody2D;
	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;
	public int attackDamage = 20;
	public int maxHealth = 100;
	private int currentHealth;
	public static float current = 0.05f;
	public Text winText;
	[SerializeField]private AudioSource jumpingSound;
	[SerializeField] private AudioSource AttackSound;
	[SerializeField] private AudioSource DeathSound;
	// Start is called before the first frame update
	void Start()
    {
        isAttack = false;
        isJump = false;
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
		currentHealth = maxHealth;
		winText.text ="";
		current = 0.05f;
	}

	private void PlayerMovement()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		bool moveToRight = Input.GetKey(KeyCode.RightArrow);
		bool moveToLeft = Input.GetKey(KeyCode.LeftArrow);
		float horizontal = Input.GetAxis("Horizontal") * speed;


		animator.SetFloat("Speed", Mathf.Abs(horizontal));
		if (moveToRight)
		{
			GetComponent<Transform>().Translate(new Vector3(speed, 0));
			spriteRenderer.flipX = false;
		}
		if (moveToLeft)
		{
			GetComponent<Transform>().Translate(new Vector3(-1 * speed, 0));
			spriteRenderer.flipX = true;
		}
	}


	private void PlayerAttack()
	{
	     bool up = Input.GetKey(KeyCode.UpArrow);
		if (up && !isAttack)
		{   //Debug.Log("null");
			AttackSound.Play();
			animator.SetBool("IsAttack", true);
			isAttack = true;
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
			try
			{

				foreach (Collider2D enemy in hitEnemies)
				{
					//if (enemy == null)
					//{
					//throw new Exception("Null");
					//}
					//Debug.Log("Hit");
					enemy.GetComponent<Player2Move>().TakeDamage(20);
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
			
		if (!up)
		{
			animator.SetBool("IsAttack", false);
			isAttack = false;
		}
	}
	void OnDrawGizmosSelected()
    {
		if (attackPoint == null)
			return;
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
	private void PlayerJump()
	{
		bool jumping = Input.GetKey(KeyCode.Space);
		if (jumping && isJump == false)
		{
			jumpingSound.Play();
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jump);
			animator.SetBool("IsJump", true);
			isJump = true;
		}
		if (Mathf.Abs(rigidbody2D.velocity.y) < 0.01f)
		{
			animator.SetBool("IsJump", false);
			isJump = false;
		}
	}
	public void TakeDamage(int damage)
	{
		Debug.Log(damage);
		currentHealth = currentHealth - damage;
		current = current - 0.01f;
		animator.SetTrigger("Hurt");
		if (currentHealth <= 0)
		{
			//Die(currentHealth);
			Die();
			winText.text = "Player2 Win!!!";
			//Respawn();
			//yield return new WaitForSeconds(5);
			//SceneManager.LoadScene("Menu");
			//anim.SetBool("IsDead", false);
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
		//Debug.Log("en die"+currentHealth);
		animator.SetBool("IsDead", true);
		//GetComponent<Collider2D>().enabled = false;
		//anim.SetBool("IsDead", false);
		DeathSound.Play();
		this.enabled = false;
		//anim.SetBool("IsDead", false);
	}

	// Update is called once per frame
	void Update()
    {
        if (CompareTag("player1"))
        {
			PlayerMovement();

			PlayerJump();
			
			PlayerAttack();

			//Debug.Log("null");
		}
        
    }
}
