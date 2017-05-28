using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    //Ship variables.
    public GameObject playerShip;
    Rigidbody pS;
    public float speed;
    private float speedL;
    private float speedN;
    private float speedH;
    public float turnSpeed;
    public float mSensitivity;

    public float shipHealth;
    public float shipShields;
    private float shieldL;
    private float shieldN;
    private float shieldH;

    //Modes.
    bool onSpeed;
    bool onShield;
    bool onAttack;

    //Camera variables.
    public Camera mainCamera;
    public GameObject camPos;
    public float camLerp;
    public float camTurnSpeed;

    //Shooting variables.
    public GameObject projectile;
    public float bulletSpeed;
    public GameObject sPoint;
    public float damage;
    private float damageL;
    private float damageN;
    private float damageH;
    public float aimRange;
    public GameObject crosshair;
    //int aimX = Screen.width / 2;
    //int aimY = Screen.height / 2;

    void Start ()
    {
        Cursor.visible = false;

        pS = playerShip.GetComponent<Rigidbody>();

        speedN = speed;
        speedL = speedN / 2;
        speedH = speedN * 2;

        damageN = damage;
        damageL = damageN / 2;
        damageH = damageN * 2;

        shieldN = shipShields;
        shieldL = shieldN / 2;
        shieldH = shieldN * 2;

		onSpeed = false;
		onShield = true;
        onAttack = false;
    }
	
	void Update ()
	{

		Cursor.lockState = CursorLockMode.Locked;

        //Mode managing.
        if (onSpeed == true)
        {
            onShield = false;
            onAttack = false;

            speed = speedH;
            damage = damageL;
            shipShields = shieldL;
        }

        if (onShield == true)
        {
            onSpeed = false;
            onAttack = false;

            speed = speedN;
            damage = damageL;
            shipShields = shieldH;
        }

        if (onAttack == true)
        {
            onShield = false;
            onSpeed = false;

            speed = speedN;
            damage = damageH;
            shipShields = shieldL;
        }

        //Mode changing.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            onSpeed = true;
            onShield = false;
            onAttack = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onSpeed = false;
            onShield = true;
            onAttack = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            onSpeed = false;
            onShield = false;
            onAttack = true;
        }

        //This part takes care of the player movement.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.back * moveHorizontal* turnSpeed);
        transform.Rotate(Vector3.right * moveVertical);
        transform.Rotate(Vector3.left * mouseY * mSensitivity);
        transform.Rotate(Vector3.up * mouseX * mSensitivity);

        pS.transform.position += transform.forward * Time.deltaTime * speed;

        //Lerping the camera to its place.
        mainCamera.transform.DOMove(new Vector3(camPos.transform.position.x, camPos.transform.position.y, camPos.transform.position.z), camLerp);
        //Lerping the camera rotation.
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camPos.transform.rotation, Time.deltaTime * camTurnSpeed);

        //Positions the crosshair appropriately.
        RaycastHit aim;
        if (Physics.Raycast(sPoint.transform.position, sPoint.transform.forward, out aim, aimRange))
        {
            crosshair.SetActive(true);
            Vector3 aimPos = mainCamera.WorldToScreenPoint(aim.point);
            crosshair.transform.position = aimPos;
        }
        else
        {
            //crosshair.transform.position = mainCamera.WorldToScreenPoint(mainCamera.transform.forward);
            crosshair.SetActive(false);
        }


        //Shooting.
        if (Input.GetButton("Fire1"))
        {
            GameObject bullet = Instantiate(projectile, sPoint.transform.position, pS.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(pS.transform.forward * bulletSpeed);
        }

        if (shipHealth < 0)
        {
            SceneManager.LoadScene(0);
        }

    }

    public void healthDamage(float amount)
    {
        shipHealth -= amount;
    }

    public void shieldDamage(float amount)
    {
        shipHealth -= amount;
    }

}
