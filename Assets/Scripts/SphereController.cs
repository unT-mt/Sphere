using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereController : MonoBehaviour
{
    public float force = 10f;           // 移動時の力
    public float maxSpeed = 5f;         // 通常速度上限
    public float topSpeed = 10f;        // トップスピード
    public float topSpeedMultiplier = 0.01f;  // トップスピード時の力減衰
    public float jumpForce = 5f;        // ジャンプの力
    public Transform cameraTransform;   // 追従させるカメラのTransform
    public Vector3 cameraOffset = new Vector3(0, 5, -10); // カメラの位置オフセット

    private Rigidbody rb;
    private Vector3 moveDirection;
    private float adjustedForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // カメラを追従させる
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, transform.position + cameraOffset, 0.1f);

        // 前後・左右の入力処理
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        moveDirection = (forward * vertical + right * horizontal).normalized;

        float angle = Vector3.Angle(rb.velocity, moveDirection);
        adjustedForce = force;

        if (angle > 90f)
        {
            adjustedForce *= 2f;
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            if (rb.velocity.magnitude < topSpeed)
            {
                adjustedForce *= topSpeedMultiplier;
            }
            else
            {
                adjustedForce = 0;
            }
        }
        // // スペースキー離上時にジャンプ
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        // }
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * adjustedForce, ForceMode.Force);
    }
}
