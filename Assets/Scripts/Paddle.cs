using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoPlay =false;
	private Ball ball;
	
	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!autoPlay){
			MoveWithMouse();
		}
		else{
			AutoPlay();
		}
	}	
	
	void MoveWithMouse(){
		float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		transform.position = new Vector3(
				Mathf.Clamp( mousePosInBlocks,0.5f, 15.5f ),
				this.transform.position.y,0
		);
	}
	
	void AutoPlay(){
		Vector2 ballPos = ball.transform.position;
		transform.position = new Vector3(
			Mathf.Clamp( ballPos.x,0.5f, 15.5f ),
			this.transform.position.y,0
		);
	}
}
