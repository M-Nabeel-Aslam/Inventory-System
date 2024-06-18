using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    [Header("Bag")]
    public int _percentageInt;
    public Image _border;
    public TextMeshProUGUI _percentageText;
    public GameObject _inventoryPanel;

    public Transform _spawnPosition;

    public TextMeshProUGUI _infoText;

    //[Header("Pick-UP")]

    public static FirstPersonController _instance;
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //Set_Bag();
    }

    void Update()
    {
        Movement();
    }

    #region Movement

    void Movement()
    {
        // Player and Camera Rotation
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * lookSpeed;
        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        // Player Movement
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            float curSpeedX = speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    #endregion

    public void Set_Bag(int precentage)
    {
        _percentageInt = precentage;
        _percentageText.text = (Mathf.Clamp(_percentageInt, 0, 100) + "%").ToString();
        _border.fillAmount = ((float)(Mathf.Clamp(_percentageInt, 0, 100)) / 100f);
    }


    public void Show_Hide_Inventory(bool state)
    {
        if (state)
            _inventoryPanel.SetActive(true);
        else if (!state)
            _inventoryPanel.SetActive(false);
    }



   
    void OnTriggerEnter(Collider collision)
    {
        Debug.LogError("OnTriggerEnter");
        if (collision.transform.tag == "Items")
        {
            int weight = collision.gameObject.GetComponent<Item_PickUP>()._weight;
            if ((weight + Inventory_UI._instance._totalWeight) > 100)
            {
                StartCoroutine(ShowInfo("Not Enough Space In Your Bag"));
            }
            else if ((weight + Inventory_UI._instance._totalWeight) <= 100)
            {
                Inventory_UI._instance.PickUP(collision.gameObject.GetComponent<Item_PickUP>());
                collision.gameObject.SetActive(false);
            }
            
            Debug.LogError("OnTriggerEnter Items");
        }

    }


    /*public void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.LogError("OnCollisionEnter");
        if (collision.transform.tag == "Items")
        {
            Inventory_UI._instance.PickUP(collision.gameObject.GetComponent<Item_PickUP>());
            collision.gameObject.SetActive(false);
            Debug.LogError("OnCollisionEnter Items");
 
        }
    }*/




    public void Throw_PickAble(GameObject _throw)
    {
        GameObject holder = Instantiate(_throw, _spawnPosition.position, _spawnPosition.rotation);
        holder.GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime*6000);
    }



    IEnumerator ShowInfo(string info) {
        _infoText.text = info;
        _infoText.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        _infoText.transform.parent.gameObject.SetActive(false);
        _infoText.text = "";

    }

}

