using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	private Paddle paddle;
	
	private bool started = false;
	private Vector3 paddleToBallVector;
	
	// Use this for initialization
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!started){
			//lock the ball to the paddle.
			transform.position = paddle.transform.position + paddleToBallVector;
			
			// wait for a click to launch.
			if(Input.GetMouseButtonDown(0)){
				started = true;
				rigidbody2D.velocity = new Vector2(2f,10f);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		Vector2 tweak = new Vector2(Random.Range(0f,0.2f),Random.Range(0f,0.2f));
		if(started){
			//audio.Play();
			rigidbody2D.velocity += tweak;
		}
	}
}
