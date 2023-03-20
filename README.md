# RollaballDAM21

# Unity rollaball

**Lo desglosaremos en dos partes el codigo y el proyecto de unity**


# Codigo

* Camara controller

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
```

**De este script cabria destacar sobre todo que los metodos estan hechos de tal manera que la main camara del proyecto va seguir al jugador**

* Player

```

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count ;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText ();

     
        winTextObject.SetActive(false);
        
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) 
    {
       
        if (other.gameObject.CompareTag ("PickUp"))
        {
            other.gameObject.SetActive (false);

         
            count = count + 1;

            SetCountText ();
        }
    }
    
    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12) 
        {
            
            winTextObject.SetActive(true);
        }
    }
}
```

**Desglosando este metodo tendriamos unos cuantos metodos, el primero de ellos para iniciar el movimiento y ademas que salga el texto del score, el segundo seria para el movimiento con las variables del eje x y y, el tercero es un metodo para la velocidad del player, el cuarto es un trigger que le indica al player que cuando toca y absorba un collider, se sume uno en la puntuacion, y el ultimo es un metodo que le indica mediante un condicional que cuando llege a x puntuacion te indique que has ganado el juego**

* Rotator

```
using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    // Before rendering each frame..
    void Update () 
    {
        // Rotate the game object that this script is attached to by 15 in the X axis,
        // 30 in the Y axis and 45 in the Z axis, multiplied by deltaTime in order to make it per second
        // rather than per frame.
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}
```

**Script simple que indica que los collider se mantengan en rotacion en el aire**

* Enemy

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private NavMeshAgent pathfinder;
    private Transform target;
    
    void Start()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        pathfinder.SetDestination(target.position);
    }
}
```
**Script para el enemigo que lo que hace es señalizar al player como un target, y mediante los dos metodos le indica al enemy que tiene que seguirlo**

* Puerta

```

namespace DefaultNamespace;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {
    public Animator laPuerta ;

    private void OnTriggerEnter(Collider other)
    {

        lapuerta.play("abrir");
    }
    private void OnTriggerexit(Collider other)
    {

        lapuerta.play("salir");
    }
   
}
```
**Script que me diante dos triggers le indicamos que en caso de colision, con la puerta esta se mueva acorde al movimiento del player**

# Interfaz de unity
