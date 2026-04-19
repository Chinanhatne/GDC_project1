using UnityEngine;
using UnityEngine.InputSystem; // For handling input actions
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour
{
    public GameObject mainCamera;
    public Camera viewCamera;
    public GameObject boat;
    public GameObject hooker;
    public Vector2 initialVelocity;
    public LineRenderer lineRenderer;
    public GameObject fishButton;
    public GameObject Fish;
    Vector2 mousePosition;
    bool hooked = false;
    bool fish = false;
    void Start()
    {
    }

    public void OnReady()
    {
        mainCamera.GetComponent<Animator>().SetBool("Fish?",false);
        fishButton.SetActive(true);
        fish = false;
    }

    public void OnFish()
    {
        mainCamera.GetComponent<Animator>().SetBool("Fish?",true);
        fishButton.SetActive(false);
        hooker.transform.position = boat.transform.position;
        fish = true;
    }

    public void OnHooked(GameObject fish)
    {
        mainCamera.GetComponent<Animator>().SetBool("Fish?",false);
        hooked = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        SceneManager.LoadScene("TugOfwar");
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
        Vector2 UImiddle = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseRelativePosition = mousePosition - UImiddle;
        if(fish)
        {
            Debug.Log("mouse position" + mouseRelativePosition);
            Vector3 cursor = viewCamera.ScreenToWorldPoint(mouseRelativePosition);
            cursor.z = 0f;
            hooker.transform.localPosition = cursor;
        }

    }

    private bool preventMouseRelease = false; // Flag to prevent multiple triggers on mouse release

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if(!preventMouseRelease)
        {
            preventMouseRelease = true;
            Debug.Log("Mouse clicked");

        }
        else
        {
            preventMouseRelease = false; // Reset flag on mouse release
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
