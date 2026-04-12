using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("CameraSpeed")]
    public float camera_speed = 5f;

    [Header("追跡対象")]
    public Transform player;
    [Header("感度")]
    public float sensitivity = 200f;

    [Header("追跡対象までの距離")]
    public float distance = 4f;

    float yaw;
    float pitch;
    float start_yaw;
    float start_pitch;
    Vector3 player_position;
    void Start()
    {
        player_position = player.position + Vector3.up * 2f;
        transform.position = player_position + player.forward * 3f;
        transform.LookAt(player);

        Vector3 dir = transform.position - player_position;
        distance = dir.magnitude;

        yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        pitch = Mathf.Asin(dir.y / distance) * Mathf.Rad2Deg;

        start_yaw = yaw;
        start_pitch = pitch;
    }
    void Update()
    {
        player_position = player.position + Vector3.up * 2f;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -60f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3 (0, 0, -distance);

        Vector3 desired_position = player_position + offset;

        
        Vector3 dir = (desired_position - player_position).normalized;

        RaycastHit hit;
        if (Physics.SphereCast(player_position, 0.3f, dir, out hit, distance))
        {
            transform.position = player_position + dir * (hit.distance - 0.2f);
        }
        else
        {
            transform.position = desired_position;
        }

        transform.LookAt(player);
        if (Input.GetKeyDown(KeyCode.R))
        {
            yaw = start_yaw;
            pitch = start_pitch;
        }
    }
}
