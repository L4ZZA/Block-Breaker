using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	
	public AudioClip crack;
	public static int breakableCount=0;
	public Sprite[] hitSprites;
	
	private LevelManager levelManager;
	private int timesHit;
	private bool isBreakable;
	
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		if(isBreakable){
			breakableCount++;
		}
		
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		GetComponent<AudioSource>().Play();
		if(isBreakable){
			HandleHits();
		}
	}
	
	void HandleHits(){
		timesHit++;
		int maxHits = hitSprites.Length +1;
		if(timesHit >= maxHits){
			breakableCount--;
			AudioSource.PlayClipAtPoint(crack, transform.position, 0.009f);
			levelManager.BrickDestroyed();
			Destroy(gameObject);
		}
		else{
			LoadSprites();
		}
	}
	
	void LoadSprites(){
		int spriteIndex = timesHit-1;
		//if we forget to put the sprite
		if(hitSprites[spriteIndex]){
			GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
	
	void SimulateWin(){
		levelManager.LoadNextLevel();
	}
}
