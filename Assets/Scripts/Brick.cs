using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    public AudioClip [] hitSounds;
    [Range(0f, 2f)]
    public float hitVolume = 0.5f;
    // static field to keep count of the total breakable bricks
    public static int breakableBlocksCount = 0;
    public Sprite[] hitSprites;
    public GameObject blockSparkleVFX;

    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    private int maxHits;
    private int hitsTaken = 0;
    private bool isBreakable;

    // Use this for initialization
    void Start()
    {
        isBreakable = tag == "Breakable";
        if (isBreakable)
        {
            breakableBlocksCount++;
        }

        maxHits = hitSprites.Length + 1;
        hitsTaken = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
        {
            HandleHits();
        }
        else
        {
            PlayHitSound(0);
        }
    }

    void HandleHits()
    {
        hitsTaken++;
        int hitSoundIndex = -1;

        if (hitsTaken < maxHits)
        {
            // change sound clip
            hitSoundIndex = 0;
            // change sprite
            LoadNextHitSprite();
        }
        else
        {
            hitSoundIndex = hitSounds.Length - 1;
            breakableBlocksCount--;
            Destroy(gameObject);
            gameManager.BrickDestroyed();
            TriggerSparkleVFX();
        }
        PlayHitSound(hitSoundIndex);
    }

    private void PlayHitSound(int hitSoundIndex)
    {
        if (hitSoundIndex < 0 || hitSoundIndex > hitSounds.Length)
        {
            Debug.LogError(gameObject.name + " - Can't play clips index out of bound: " + hitSoundIndex);
            return;
        }   

        var currentClip = hitSounds[hitSoundIndex];
        if (currentClip == null)
            Debug.LogError(gameObject.name + " - No sound file found at index " + hitSoundIndex);
        else
            AudioSource.PlayClipAtPoint(currentClip, Camera.main.transform.position, hitVolume);
    }

    void TriggerSparkleVFX()
    {
        var particles = Instantiate(blockSparkleVFX, transform.position, transform.rotation);
        Destroy(particles, 2f);
    }

    void LoadNextHitSprite()
    {
        int spriteIndex = (maxHits - 1) - hitsTaken;
        //if we forget to assign the srite in the editor
        if (hitSprites[spriteIndex])
        {
            spriteRenderer.sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError(gameObject.name + " - Sprite missing from array.");
        }
    }
}
