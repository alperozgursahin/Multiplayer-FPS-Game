using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings")]

    [SerializeField]
    private float jointSpring = 20f;

    [SerializeField]
    private float jointMaxForce = 40f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }

    void Update()
    {
        // Calculate movement velocity as a 3D Vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Final Movement Vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Apply Movement
        motor.Move(_velocity);

        // Calculate rotation as a 3D Vector (Turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        // Apply rotation 
        motor.Rotate(_rotation);

        // Calculate Camera Rotation as a 3D Vector
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        // Apply rotation 
        motor.RotateCamera(_cameraRotationX);

        // Calculate the thruster force based on player input

        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        // Apply thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
