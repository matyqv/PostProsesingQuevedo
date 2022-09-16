using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanqueMove : MonoBehaviour
{
    public KeyCode Derecha;
    public KeyCode Izquierda;
    float x;
   [SerializeField] float speed;
   [SerializeField] CharacterController CC;
    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Derecha))
        { x = 1; moveTanq(); }
        else if (Input.GetKey(Izquierda))
        { x = -1; moveTanq(); }
        else
        { }
    }

    void moveTanq()
    {
        CC.Move(new Vector3(x*Time.deltaTime*speed, 0, 0));
    }
}
