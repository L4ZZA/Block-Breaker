using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {
	
	public AudioClip crack;
	[Range(0f,2f)]
	public float crakVolume = 0.5f;
	public static int breakableCount=0;
	public Sprite[] hitSprites;
	public GameObject blockSparkleVFX;
	
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
		levelManager = FindObjectOfType<LevelManager>();
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
			Destroy(gameObject);
			AudioSource.PlayClipAtPoint(crack, Camera.main.transform.position, crakVolume);
			levelManager.BrickDestroyed();
			TriggerSparkleVFX();
		}
		else{
			LoadSprites();
		}
	}

	void TriggerSparkleVFX()
    {
		var particles = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
		Destroy(particles, 2f);
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
