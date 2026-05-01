using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float enemySpeed = 4f;
    private Rigidbody2D rb;
    private Collider2D cc;
    public GroundDetect GDL;
    public GroundDetect GDR;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        print("GDR: " + GDR.groundOk);
        print("GDL:" + GDL.groundOk);
        if (GDR.groundOk == false)
        {
            Flip();
            GDR.groundOk = true;
        }
        if(GDL.groundOk == false)
        { Flip();
            GDL.groundOk = true;
        }
        transform.Translate(Vector2.right * enemySpeed * Time.deltaTime);
        
      
    }
   void Flip()
    {
        enemySpeed = enemySpeed * (-1);
    }
}
