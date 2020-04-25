using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private GameObject obj;
    
    public float moveRange;
    private Vector2 oldPosition;
    // Start is called before the first frame update
    void Awake()
    {
         if(GameController.IsMusicOff==false)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

    void Start()
    {
        obj = gameObject;
        oldPosition = obj.transform.position;
        moveRange = 20.3f;
      
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -0.13f*GameController.speed);
        if (Vector3.Distance(oldPosition, obj.transform.position) > moveRange)
        {
            obj.transform.position = oldPosition;
        }
    }
    public void MusicOn()
    {
        this.GetComponent<AudioSource>().Play();       
    }
    public void MusicOff()
    {
        this.GetComponent<AudioSource>().Pause();
    }
}
