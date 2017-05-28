
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{

    //Ship variables.
    public GameObject playerShip;
    Rigidbody pS;
    public float speed;
    private float speedL;
    private float speedN;
    private float speedH;
    public float turnSpeed;
    public float mSensitivity;

    private float shipHealth;
    public float shipMaxHealth;
    private float shipShield;
    public float shipMaxShield;
    private float lastHit;
    private float shieldL;
    private float shieldN;
    private float shieldH;
    public GameObject lifeBar, shieldBar;

    //Ship parts.
    public GameObject leftFin;
    public GameObject rightFin;
    public GameObject tlFlap;
    public GameObject trFlap;
    public GameObject blFlap;
    public GameObject brFlap;
    public GameObject lExhaust;
    public GameObject rExhaust;
    public GameObject shield;
    public GameObject shieldDoors;

    //UI parts.
    public GameObject attackUI;
    public GameObject shieldUI;
    public GameObject speedUI;

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
    public GameObject projectileH;
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
    public float enemyLife,
                 enemyMaxLife;
    private bool targeting;

    public GameObject _base,
                      explosion,
                      outOfBoundsText,
                      enemyLifeBar;
    public float mapRadius,
                 outOfBoundsTimer;
    private bool outOfBounds;
    private float _outOfBoundsTimer;

    void Start()
    {
        Cursor.visible = false;

        lastHit = 0;
        shipHealth = shipMaxHealth;
        shipShield = shipMaxShield;

        pS = playerShip.GetComponent<Rigidbody>();

        speedN = speed;
        speedL = speedN / 2;
        speedH = speedN * 2;

        damageN = damage;
        damageL = damageN / 2;
        damageH = damageN * 2;

        shieldN = shipMaxShield;
        shieldL = shieldN / 2;
        shieldH = shieldN * 2;

        onSpeed = false;
        onShield = true;
        onAttack = false;

        outOfBoundsText.GetComponent<Text>().enabled = false;
        _outOfBoundsTimer = outOfBoundsTimer;

        enemyLife = 0;
        enemyMaxLife = 0;
        enemyLifeBar.SetActive(false);
        targeting = false;
    }

    void Update()
    {
        #region ModeManagement
        //Mode managing.
        if (onSpeed == true)
        {
            bool maxShield = false;
            if (shipShield == shipMaxShield)
                maxShield = true;

            speed = speedH;
            damage = damageL;
            shipMaxShield = shieldL;
            if (shipShield > shipMaxShield || maxShield)
            {
                shipShield = shipMaxShield;
            }
        }

        if (onShield == true)
        {
            bool maxShield = false;
            if (shipShield == shipMaxShield)
                maxShield = true;

            speed = speedN;
            damage = damageL;
            shipMaxShield = shieldH;
            if (shipShield > shipMaxShield || maxShield)
            {
                shipShield = shipMaxShield;
            }
        }

        if (onAttack == true)
        {
            bool maxShield = false;
            if (shipShield == shipMaxShield)
                maxShield = true;

            speed = speedN;
            damage = damageH;
            shipMaxShield = shieldL;
            if (shipShield > shipMaxShield || maxShield)
            {
                shipShield = shipMaxShield;
            }
        }

        //Mode changing.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            onSpeed = true;
            onShield = false;
            onAttack = false;
            leftFin.transform.DOLocalRotate(new Vector3(0, 0, -80), 1);
            rightFin.transform.DOLocalRotate(new Vector3(0, 0, 80), 1);

            speedUI.SetActive(true);
            attackUI.SetActive(false);
            shieldUI.SetActive(false);

            shield.SetActive(false);
            shieldDoors.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            onSpeed = false;
            onShield = true;
            onAttack = false;
            leftFin.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
            rightFin.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);

            speedUI.SetActive(false);
            attackUI.SetActive(false);
            shieldUI.SetActive(true);

            shield.SetActive(true);
            shieldDoors.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            onSpeed = false;
            onShield = false;
            onAttack = true;
            leftFin.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
            rightFin.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);

            speedUI.SetActive(false);
            attackUI.SetActive(true);
            shieldUI.SetActive(false);

            shield.SetActive(false);
            shieldDoors.SetActive(true);
        }

        #endregion

        #region Movement
        //This part takes care of the player movement.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.back * moveHorizontal * turnSpeed);
        transform.Rotate(Vector3.right * moveVertical);
        transform.Rotate(Vector3.left * mouseY * mSensitivity);
        transform.Rotate(Vector3.up * mouseX * mSensitivity);

        pS.transform.position += transform.forward * Time.deltaTime * speed;

        //Moving the flaps.
        tlFlap.transform.DOLocalRotate(new Vector3((-30 * moveVertical), 0, 0), 0.5f);
        trFlap.transform.DOLocalRotate(new Vector3((-30 * moveVertical), 0, 0), 0.5f);
        blFlap.transform.DOLocalRotate(new Vector3((-30 * moveVertical), 0, 0), 0.5f);
        brFlap.transform.DOLocalRotate(new Vector3((-30 * moveVertical), 0, 0), 0.5f);

        tlFlap.transform.DOLocalRotate(new Vector3((-30 * moveHorizontal), 0, 0), 0.5f);
        trFlap.transform.DOLocalRotate(new Vector3((30 * moveHorizontal), 0, 0), 0.5f);
        blFlap.transform.DOLocalRotate(new Vector3((-30 * moveHorizontal), 0, 0), 0.5f);
        brFlap.transform.DOLocalRotate(new Vector3((30 * moveHorizontal), 0, 0), 0.5f);

        tlFlap.transform.DOLocalRotate(new Vector3((-60 * mouseY), 0, 0), 0.5f);
        trFlap.transform.DOLocalRotate(new Vector3((-60 * mouseY), 0, 0), 0.5f);
        blFlap.transform.DOLocalRotate(new Vector3((-60 * mouseY), 0, 0), 0.5f);
        brFlap.transform.DOLocalRotate(new Vector3((-60 * mouseY), 0, 0), 0.5f);

        //Moving the exhaust.
        lExhaust.transform.DOLocalRotate(new Vector3((30 * moveVertical), -180, 0), 0.5f);
        rExhaust.transform.DOLocalRotate(new Vector3((30 * moveVertical), -180, 0), 0.5f);

        lExhaust.transform.DOLocalRotate(new Vector3((30 * moveHorizontal), -180, 0), 0.5f);
        rExhaust.transform.DOLocalRotate(new Vector3((-30 * moveHorizontal), -180, 0), 0.5f);

        lExhaust.transform.DOLocalRotate(new Vector3((60 * mouseY), -180, 0), 0.5f);
        rExhaust.transform.DOLocalRotate(new Vector3((60 * mouseY), -180, 0), 0.5f);

        //Lerping the camera to its place.
        mainCamera.transform.DOMove(new Vector3(camPos.transform.position.x, camPos.transform.position.y, camPos.transform.position.z), camLerp);
        //Lerping the camera rotation.
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camPos.transform.rotation, Time.deltaTime * camTurnSpeed);
        #endregion

        #region Shooting
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

        FindEnemy();

        //Shooting.
        if (Input.GetButton("Fire1"))
        {
            if (onAttack == true)
            {
                GameObject bullet = Instantiate(projectileH, sPoint.transform.position, pS.transform.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(pS.transform.forward * bulletSpeed);

            }
            else
            {
                GameObject bullet = Instantiate(projectile, sPoint.transform.position, pS.transform.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(pS.transform.forward * bulletSpeed);
            }

        }
                
        #endregion

        UpdatePlayerLife();
        RegenerateShield();

        #region BoundChecks
        CheckBaseDistance();

        if (outOfBounds)
        {
            outOfBoundsText.GetComponent<Text>().enabled = true;
            WarnPlayer();
        }
        else
        {
            outOfBoundsText.GetComponent<Text>().enabled = false;
        }
        #endregion
    }

    private void UpdatePlayerLife()
    {
        lifeBar.GetComponent<Image>().fillAmount = shipHealth / shipMaxHealth;
        shieldBar.GetComponent<Image>().fillAmount = shipShield / shipMaxShield;
    }

    private void FindEnemy()
    {
        RaycastHit aim;
        if (Physics.Raycast(sPoint.transform.position, sPoint.transform.forward, out aim, aimRange))
        {
            if (aim.transform.gameObject.name == "Disc" && aim.transform != null)
            {
                targeting = true;
                StopCoroutine(WaitWithLifeBar());
                enemyLifeBar.SetActive(true);
                //enemyLife = aim.transform.GetComponent<baseEnemy>().health;
                enemyMaxLife = aim.transform.GetComponent<AIDisc>().maxLife;
                enemyLifeBar.GetComponent<Image>().fillAmount = enemyLife / enemyMaxLife;
            }
        }
        else
        {
            if (enemyLifeBar.activeSelf && targeting)
            {
                targeting = false;
                //enemyLifeBar.SetActive(false);
                StartCoroutine(WaitWithLifeBar());
            }
        }
    }

    private IEnumerator WaitWithLifeBar()
    {
        yield return new WaitForSeconds(1f);
        enemyLifeBar.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Enemy")
        {
            KillPlayer();
        }
    }

    public void CheckBaseDistance()
    {
        if (Mathf.Abs(Vector3.Distance(transform.position, _base.transform.position)) >= mapRadius)
        {
            outOfBounds = true;
        }
        else
        {
            _outOfBoundsTimer = outOfBoundsTimer;
            outOfBounds = false;
        }
    }

    private void WarnPlayer()
    {
        _outOfBoundsTimer -= Time.deltaTime;                                                  //Converte o float timer para um string com duas casas decimais.
        outOfBoundsText.GetComponent<Text>().text = "WARNING!!!\nYOU ARE LEAVING THE AREA!!!\n" + _outOfBoundsTimer.ToString("F2");

        if (_outOfBoundsTimer <= 0)
        {
            outOfBoundsText.GetComponent<Text>().text = "You were out of bounds for too long.";
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameObject e = Instantiate(explosion);
        e.transform.position = transform.position;
        shipHealth = 0;

        Transform[] children = this.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.gameObject != this.gameObject)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        GameObject.Destroy(this.gameObject);
    }

    public void TakeDamage(float amount)
    {
        lastHit = Time.time;
        if (shipShield > 0)
        {
            ShieldDamage(amount);
        }
        else
        {
            HealthDamage(amount);
        }
    }

    private void HealthDamage(float amount)
    {
        shipHealth -= amount;
        if (shipHealth <= 0)
        {
            KillPlayer();
        }
    }

    private void ShieldDamage(float amount)
    {
        shipShield -= amount;
    }

    private void RegenerateShield()
    {
        if ((lastHit - Time.time) >= 5 && shipShield != shipMaxShield)
        {
            shipShield = shipMaxShield;
        }
    }

}