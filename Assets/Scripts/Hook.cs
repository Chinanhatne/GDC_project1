using System;
using System.Collections;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Hook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float hookFLy = 5f;
    private bool isShoot = false;
    private bool isDelete = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootHandle();
    }

    void ShootHandle()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame){
            isShoot = true;

        }
        if(isShoot)
        {
            transform.SetParent(null);
            transform.position += transform.right * hookFLy * Time.deltaTime;
            if(isDelete)
            {
                StartCoroutine(Delete());
                isDelete = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "fish")
        {
            //SceneManager.LoadScene("SceneB", LoadSceneMode.Additive);
            //Time.timeScale = 0f;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
