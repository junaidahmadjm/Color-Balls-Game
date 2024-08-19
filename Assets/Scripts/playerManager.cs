using UnityEngine;
using UnityEngine.SceneManagement;
public class playerManager : MonoBehaviour
{
    private Transform ball;
    private Vector3 startMousePos, startBallPos;
    private bool moveTheBall;
    [Range(0f,1f)]public float maxSpeed;
    [Range(0f, 1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    private float velocity,camVelocity_x, camVelocity_y;
    private Camera mainCamera;
    public Transform path;
    private Rigidbody rb;
    private Collider _collider;
    private Renderer BallRenderer;
    public ParticleSystem colliderEffect;
    public ParticleSystem airParticleEffect;
    public GameObject endPanel;

    private bool isJumping = false;
    private void Start()
    {
        ball = transform;
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        BallRenderer = GetComponent<Renderer>();
        
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&&MainMenuManager.Instance.gameState)
        {
            moveTheBall = true;

            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(newPlan.Raycast(ray, out var distance))
            {
                startMousePos = ray.GetPoint(distance);
                startBallPos = ball.position;
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            moveTheBall = false;
        }
        if(moveTheBall)
        {
            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray, out var distance))
            {
                Vector3 mouseNewPos = ray.GetPoint(distance);
                Vector3 MouseNewPos = mouseNewPos - startMousePos;
                Vector3 DesireBallPos = MouseNewPos + startBallPos;

                DesireBallPos.x = Mathf.Clamp(DesireBallPos.x, -1.5f, 1.5f);

                ball.position = new Vector3(Mathf.SmoothDamp(ball.position.x, DesireBallPos.x, ref velocity,
                    maxSpeed), ball.position.y, ball.position.z);
            }
        }
        if (MainMenuManager.Instance.gameState)
        {
            var pathNewPos = path.position;

            path.position = new Vector3(pathNewPos.x, pathNewPos.y, Mathf.MoveTowards(pathNewPos.z, -1000f, pathSpeed * Time.deltaTime));

        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.isKinematic = _collider.isTrigger = false;
            rb.velocity = new Vector3(0f, 8f, 0f); 

            var airEffect = airParticleEffect.main;
            airEffect.simulationSpeed = 10f; 
        }
    }
    private void LateUpdate()
    {
        var CameraNewPos = mainCamera.transform.position;
        if (rb.isKinematic)
        {
            mainCamera.transform.position = new Vector3(Mathf.SmoothDamp(CameraNewPos.x, ball.transform.position.x, ref camVelocity_x, camSpeed)
                , Mathf.SmoothDamp(CameraNewPos.y, ball.transform.position.y+3f, ref camVelocity_y, camSpeed), CameraNewPos.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            MainMenuManager.Instance.gameState = false;
            endPanel.SetActive(true);
        }

        switch(other.tag)
        {
            case "Red":
                other.gameObject.SetActive(false);
                BallRenderer.material = other.GetComponent<Renderer>().material;
                var NewParticle = Instantiate(colliderEffect, transform.position, Quaternion.identity);
                NewParticle.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                break;
            case "Green":
                other.gameObject.SetActive(false);
                BallRenderer.material = other.GetComponent<Renderer>().material;
                var NewParticle1 = Instantiate(colliderEffect, transform.position, Quaternion.identity);
                NewParticle1.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                break;
            case "Yellow":
                other.gameObject.SetActive(false);
                BallRenderer.material = other.GetComponent<Renderer>().material;
                var NewParticle2 = Instantiate(colliderEffect, transform.position, Quaternion.identity);
                NewParticle2.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                break;
            case "Blue":
                other.gameObject.SetActive(false);
                BallRenderer.material = other.GetComponent<Renderer>().material;
                var NewParticle3 = Instantiate(colliderEffect, transform.position, Quaternion.identity);
                NewParticle3.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                break;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        //if(other.CompareTag("Path"))
        //{
           
        //    rb.isKinematic = _collider.isTrigger = false;
        //    rb.velocity = new Vector3(0f, 8f, 0f);
        //    pathSpeed = pathSpeed * 2;
        //}
        //var airEffect = airParticleEffect.main;
        //airEffect.simulationSpeed = 10f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Path") && isJumping)
        {
            isJumping = false;
            rb.isKinematic = _collider.isTrigger = true;

            pathSpeed = 30f;

            var airEffect = airParticleEffect.main;
            airEffect.simulationSpeed = 4f; 
        }
    }
    public void retryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
