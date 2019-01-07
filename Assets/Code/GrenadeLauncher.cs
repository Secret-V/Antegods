using UnityEngine;
using System.Collections;

public enum ControlMode
{
    Mouse,
    Gamepad
}

public class GrenadeLauncher : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform nozzle;

    public ControlMode controlMode;

    public float minimumShootForce = 50.0f;
    public float maximumShootForce = 250.0f;

    public float maxChargeTime = 3.0f;

    private float chargeTimer;
    private bool isCharging;

    void Start()
    {
        
    }
    
    void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 targetPos;
        if (controlMode == ControlMode.Mouse) targetPos = Input.mousePosition;
        else targetPos = screenPos + new Vector2(Input.GetAxis("ShootX"), -Input.GetAxis("ShootY"));

        float dx = targetPos.x - screenPos.x;
        float dy = targetPos.y - screenPos.y;
        float a = Mathf.Atan2(dy, dx);
        float angle = a * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(-angle, .0f, .0f);

        if(isCharging)
        {
            chargeTimer += Time.deltaTime;

            if(ShouldShoot())
            {
                float shootForce = Mathf.Lerp(minimumShootForce, maximumShootForce, Mathf.Min(chargeTimer, maxChargeTime) / maxChargeTime);

                var grenadeObject = Instantiate(grenadePrefab, nozzle.position, Quaternion.identity);
                grenadeObject.GetComponent<Rigidbody>().AddForce(new Vector3(Mathf.Cos(a), Mathf.Sin(a), .0f) * shootForce, ForceMode.Impulse);

                isCharging = false;
            }
        }
        else
        {
            chargeTimer -= Time.deltaTime;

            if (ShouldStartCharging())
            {
                isCharging = true;
                chargeTimer = .0f;
            }
        }
    }

    private bool ShouldStartCharging()
    {
        if (chargeTimer > .0f) return false;

        if (controlMode == ControlMode.Mouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
        }
        else
        {

        }

        return false;
    }

    private bool ShouldShoot()
    {
        if (!isCharging) return false;

        if(controlMode == ControlMode.Mouse)
        {
            if(Input.GetMouseButtonUp(0))
            {
                return true;
            }
        }
        else
        {

        }

        return false;
    }
}
