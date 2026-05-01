using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    public bool groundOk = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
            groundOk = false;
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundOk = true;
    }
}
