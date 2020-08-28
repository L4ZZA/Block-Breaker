using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float leftWorldEdgeStopPoint = 0.5f;
    public float rightWorldEdgeStopPoint = 15.5f;

    // Cache
    private Ball ball;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsAutoPlay())
        {
            MoveWithMouse();
        }
        else
        {
            AutoPlay();
        }
    }

    void MoveWithMouse()
    {
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        transform.position = new Vector3(
                Mathf.Clamp(mousePosInBlocks, leftWorldEdgeStopPoint, rightWorldEdgeStopPoint),
                transform.position.y, 0
        );
    }

    // let the paddle move with the ball
    void AutoPlay()
    {
        Vector2 ballPos = ball.transform.position;
        transform.position = new Vector3(
            Mathf.Clamp(ballPos.x, leftWorldEdgeStopPoint, rightWorldEdgeStopPoint),
            transform.position.y, 0
        );
    }
}
