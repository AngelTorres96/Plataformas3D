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
    public float jump_force=12f;
    public float groundCheckDistance=0.15f;
    public bool is_Grounded=false;
    private Rigidbody body;
    private float h, v;
    // Start is called before the first frame update
    void Start()
    {
        //obtenemos la referencia al componente rigidbody del jugador para
        //aplicar velocidad o fuerza
        body=GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //almacenamos la entrada del teclado/mando/telefono
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //Leemos la tacla espacio por si el jugador quiere saltar
        if (Input.GetKeyDown(KeyCode.Space) && is_Grounded)
        {
            //body.velocity= new Vector3(body.velocity.x,jump_force,body.velocity.z);
            body.AddForce(body.velocity.x, body.velocity.y + jump_force, body.velocity.z);
        }
    }

    void FixedUpdate(){
       

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
        
        
        //aplicamos la velocidad en X y en Z pero conservamos su velocidad en Y para la gravedad
        body.velocity = new Vector3(move.x,body.velocity.y,move.z);
        /*comprobamos si el personaje esta moviendose 
        en alguna direccion evitando que regrese al angulo 
        original de la camara en caso de estar inactivo*/
        if(move.magnitude>0){
            //rotamos el personaje en la direccion del movimiento empleando interpolacion esferica
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(move),Time.deltaTime*speedRotation);
        }
        CheckGroundStatus();
        
    }

    //Nos ayuda a validar si el jugador está en contacto con el piso
    void CheckGroundStatus(){
        //creamos un raycast para saber si nuestro personaje esta en contacto con el suelo
        RaycastHit hitInfo;
        #if UNITY_EDITOR
            //Debug.DrawLine(transform.position + (Vector3.up*0.1f), transform.position + (Vector3.up*0.1f)+ (Vector3.down*groundCheckDistance));
            Debug.DrawLine(transform.position + (Vector3.up*0.1f), transform.position+Vector3.down +(Vector3.down*0.1f));
        
        #endif
        if(Physics.Raycast(transform.position + Vector3.down +(Vector3.up*0.1f), Vector3.down, out hitInfo, groundCheckDistance) ){
            
            is_Grounded = true;

        }else{
            is_Grounded=false;
        }
        
    }
}
