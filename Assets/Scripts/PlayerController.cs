using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float velocidade;

	public Transform player;
	private Animator animator;


	public bool isGrounded = true;
	public float force; 

	public float jumpTime = 0.4f;
	public float jumpDelay = 0.4f;
	public bool jumped = false;
	public Transform ground;
	public Rigidbody2D myRigidbody2D;

	//JUMP SOUND
	public AudioClip jumpSound;


	// Use this for initialization
	void Start () {

	
		animator = player.GetComponent<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	

		Movimentar();
	}

	void Movimentar()
	{

		isGrounded = Physics2D.Linecast (this.transform.position, ground.position, 1 << LayerMask.NameToLayer ("Plataforma"));

		animator.SetFloat("run",Mathf.Abs(Input.GetAxisRaw("Horizontal")));

		if (Input.GetAxisRaw ("Horizontal") > 0) {
		

			transform.Translate(Vector2.right * velocidade * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0);
		}

		if (Input.GetAxisRaw ("Horizontal") < 0) {
			
			transform.Translate(Vector2.right * velocidade * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,180);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded && !jumped) {
				
			myRigidbody2D.AddForce(transform.up * force);
			jumpTime = jumpDelay;
			animator.SetTrigger("Jump");
			jumped = true;
			PlayJumpSound ();

		
		}

		jumpTime -= Time.deltaTime;


		if (jumpTime <= 0 && isGrounded && jumped) {
				
			animator.SetTrigger("ground");
			jumped = false;
		}
	
	

	}



	void OnCollisionEnter2D(Collision2D colisor)
	{

		if (colisor.gameObject.name == "Porta") {
        /*
			if(gerenciador.IsColetado())
			{
		
				//gerenciador.ProximoLevel(gerenciador.proximoLevel);
			}else
			{
				// indicando que nao coletou a quantidade satisfatoria
				//Debug.Log ("Nao coletou a quantidade maxima");
			}
		*/
		}

	}

	public void PlayJumpSound()
	{
		GetComponent<AudioSource>().PlayOneShot(jumpSound);
	}


}

