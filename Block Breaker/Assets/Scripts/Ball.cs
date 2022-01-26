using UnityEngine;
public class Ball : MonoBehaviour
{
    private float speedCurrent;
    [SerializeField] private float collisionSpeedIncreasePercentage = 1.01f;
    [SerializeField] private float speedMinimum = 20f;
    [SerializeField] private float speedLimit = 24f;
    [SerializeField] private float randomFactor = 0.2f;
    [HideInInspector] public bool started = false;
    public bool tutorialBlock = false;
    private Rigidbody2D body;
    private GameObject paddle;
    private GameObject text;
    private Arrow arrow;

    void Start()
    {
        speedCurrent = speedMinimum;
        body = GetComponent<Rigidbody2D>();
        paddle = GameObject.FindGameObjectWithTag("Paddle");
        text = GameObject.Find("Launch ball text");
        arrow = GetComponentInChildren<Arrow>();
    }
    
    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.G) && !tutorialBlock)
        {
            started = true;
            Launch();
        }
        else if (!started)
        {
            PaddlePosition();
        }
    }

    void Launch()
    {
        Destroy(text);
        arrow.Hide();
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        body.velocity = new Vector2(pos.x - transform.position.x, pos.y - transform.position.y).normalized * speedCurrent;
    }

    void PaddlePosition()
    {
        var ppos = paddle.transform.position;
        transform.position = new Vector2(ppos.x, ppos.y + 1f);
    }

    public void Stop()
    {
        started = false;
        arrow.Show();
        body.velocity = Vector2.zero;
        PaddlePosition();
        paddle.GetComponent<Paddle>().Stop();

    }

    public float speedRatio
    {
        get
        {
            return speedCurrent / speedMinimum;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (body.velocity.magnitude < speedMinimum)
            speedCurrent = speedMinimum;
        else if (body.velocity.magnitude <= speedLimit)
            speedCurrent += speedMinimum * collisionSpeedIncreasePercentage;
        

        var randomVector = new Vector2(Random.Range(0, randomFactor) * Mathf.Sign(body.velocity.x), 
            Random.Range(0, randomFactor) * Mathf.Sign(body.velocity.y));
        body.velocity = (body.velocity + randomVector).normalized * speedCurrent;
    }
}
