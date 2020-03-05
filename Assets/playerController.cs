using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    
    public Transform cam;
    //este vector almacenara la
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    public float speedRotation=3f;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        //obtenemos la referencia al componente rigidbody del jugador para
        //aplicar velocidad o fuerza
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //almacenamos la entrada del teclado/mando/telefono
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //verificamos si la camara esta asociada al componente
        if(cam != null){
            //multiplicamos el vector frontal de la camara
            m_CamForward = Vector3.Scale(cam.forward, new Vector3(1,0,1)).normalized;
            m_Move = v * m_CamForward + h * cam.right;
        }else{
            //si no hay una camara vinculada al personaje usaremos los ejes del mundo
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
        //enviamos el vector de movimiento a la funcion move
        Move(m_Move);
    }

    void Move(Vector3 move){
        //aplicamos el movimiento sobre el jugador
        body.velocity = move;
        //rotamos el personaje en la direccion del movimiento empleando interpolacion esferica
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(move),Time.deltaTime*speedRotation);
    }
}
