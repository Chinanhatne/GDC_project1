using System.Collections;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = -50f;
    private Animator animator;
    private bool flipped = false;
    private bool isReload = false;
    [SerializeField] private GameObject hook;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        ShootHandle();
    }

    private void RotateGun()
    {
        float angle = transform.eulerAngles.z;

        if(!flipped && (angle > 315 || angle < 225))
        {
            Flip();
            flipped = true;
        }

        if(flipped && (angle < 315 && angle > 225))
        {
            flipped = false;
        }

        transform.Rotate(0,0,rotateSpeed*Time.deltaTime);
    }
    void Flip()
    {
        transform.localScale = new Vector3(
            transform.localScale.x,
            -transform.localScale.y,
            transform.localScale.z
        );
        rotateSpeed = -rotateSpeed;
    }

    private void ShootHandle()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                animator.SetBool("isShoot",true);
                StartCoroutine(isReloadE());
            }
        if (Mouse.current.rightButton.wasPressedThisFrame && isReload)
        {
            animator.SetBool("isShoot",false);
            SpawnHook();
            isReload = false;
        }
    }

    private IEnumerator isReloadE()
    {
        yield return new WaitForSeconds(2f);
        isReload = true;
    }

    private void SpawnHook()
    {
        GameObject newHook = Instantiate(hook,transform);
        newHook.transform.SetParent(this.transform);
    }
}
