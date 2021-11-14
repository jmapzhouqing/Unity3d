using UnityEngine;
using UnityEngine.EventSystems;
using System.Timers;

enum MouseControl {
    None,
    LeftClick,
    LeftDbclick,
    LeftDrag,
    RightClick,
    RightDbClick,
    RightDrag,
    WheelClick,
    WheelDbClick,
    WheelDrag,
    WheelScroll
}
public class CameraControl : MonoBehaviour
{
    public float distance = 100;

    public float pitch_min = 0;

    public float pitch_max = 80;

    public float distance_min = 10;

    public float distance_max = 200;

    MouseControl control_state = MouseControl.None;

    public Transform target;

    private float speed = 60;
    

    private Matrix4x4 matrix;

    private Vector3 origin;

    public Vector2 rotation;

    private Vector2 pre_rotation;

    private float pre_distance = 100;

    public Vector3 target_position;

    private bool sleep = true;

    private Timer timer;

    private Vector3 origin_rotation;
    private Vector3 origin_position;
    private float origin_distance;

    private bool Sleep {
        get { return this.sleep; }
        set {
            this.sleep = value;
            if(!value) {
                timer.Stop();
                timer.Elapsed += delegate{
                    this.Sleep = true;
                };
                timer.Start();
            }
        }
    }

    private void Awake()
    {
        timer = new Timer();
        timer.Interval = 60000;

        this.origin_position = target_position;
        this.origin_rotation = rotation;
        this.origin_distance = distance;

    }
    // Start is called before the first frame update
    void Start()
    {
        pre_distance = distance;

        //target_position = target.position;

        //rotation = Vector2.zero;

        matrix = new Matrix4x4();

        origin = this.distance * Vector3.forward;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Screen.fullScreen = false;
        }

        if (Input.GetKeyDown(KeyCode.F11)){
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }


        if (control_state.Equals(MouseControl.LeftDrag)){
            this.Sleep = false;
            rotation += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 2;
            rotation.y = Mathf.Clamp(rotation.y, pitch_min, pitch_max);
        }else if (control_state.Equals(MouseControl.WheelScroll)){
            this.Sleep = false;
            distance += -1 * Input.GetAxis("Mouse ScrollWheel") * speed;
            distance = Mathf.Clamp(distance, distance_min, distance_max);
        }
        else if (control_state.Equals(MouseControl.WheelDrag)) {
            this.Sleep = false;
            //this.target.position += new Vector3(10,0,10);
            Vector3 direction = Vector3.Cross(this.transform.right, Vector3.up).normalized;
            target_position += (-1 * this.transform.right * Input.GetAxis("Mouse X") - direction * Input.GetAxis("Mouse Y"))*4;
        }

        if(this.Sleep){
            rotation.x += 0.05f;
        }
    }

    private void LateUpdate(){
        pre_rotation = Vector2.Lerp(pre_rotation, rotation, Time.deltaTime * 5);
        pre_distance = Mathf.Lerp(pre_distance, distance, Time.deltaTime * 5);

        target.position = Vector3.Lerp(target.position,target_position, Time.deltaTime * 5);

        origin = this.pre_distance * Vector3.forward;

        Quaternion quaternion = Quaternion.Euler(-1 * pre_rotation.y, pre_rotation.x, 0);

        matrix.SetTRS(target.position, quaternion, new Vector3(1, 1, 1));

        this.transform.position = matrix.MultiplyPoint(origin);
        this.transform.rotation = quaternion;

        this.transform.LookAt(target);
    }

    public void Location(Transform target) {
        target_position = target.position;
        distance = 60.0f;
        //rotation.y = pitch_max;
    }

    public void ReWind() {
        this.rotation = this.origin_rotation;
        this.distance = this.origin_distance;
        this.target_position = this.origin_position;
        this.Sleep = true;
    }

    void OnGUI(){
        Event mouse_event = Event.current;
        if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 1) {
            control_state = MouseControl.LeftClick;
        } else if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 2) {
            control_state = MouseControl.LeftDbclick;
        } else if (mouse_event.isMouse && mouse_event.button == 0 && mouse_event.type == EventType.MouseDrag) {
            control_state = MouseControl.LeftDrag;
        } else if (mouse_event.isMouse && mouse_event.button == 1 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 1) {
            control_state = MouseControl.RightClick;
        } else if (mouse_event.isMouse && mouse_event.button == 1 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 2) {
            control_state = MouseControl.RightDbClick;
        } else if (mouse_event.isMouse && mouse_event.button == 1 && mouse_event.type == EventType.MouseDrag) {
            control_state = MouseControl.RightDrag;
        } else if (mouse_event.isMouse && mouse_event.button == 2 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 1) {
            control_state = MouseControl.WheelClick;
        } else if (mouse_event.isMouse && mouse_event.button == 2 && mouse_event.type == EventType.MouseDown && mouse_event.clickCount == 2) {
            control_state = MouseControl.WheelDbClick;
        } else if (mouse_event.isMouse && mouse_event.button == 2 && mouse_event.type == EventType.MouseDrag) {
            control_state = MouseControl.WheelDrag;
        } else if (mouse_event.isScrollWheel && mouse_event.type == EventType.ScrollWheel) {
            control_state = MouseControl.WheelScroll;
        }
        else {
            control_state = MouseControl.None;
        }
    }
}
