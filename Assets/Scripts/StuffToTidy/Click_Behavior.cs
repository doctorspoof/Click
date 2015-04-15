using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class Click_Behavior : MonoBehaviour {

	
	//Wave Origin
	[SerializeField] Vector3 _origin = Vector3.zero;
	public Vector3 origin { get { return _origin; } set { _origin = value; } }
	
	[SerializeField] Color _baseColor = new Color( 0.2f, 0.2f, 0.2f, 0.0f);
	public Color baseColor { get { return _baseColor; } set { _baseColor = value; } }
	
	[SerializeField] Color _waveColor = new Color( 1.0f, 0.2f, 0.2f, 0.0f);
	public Color waveColor { get { return _waveColor; } set { _waveColor = value; } }
	
	[SerializeField] float _waveAmplitude = 2.0f;
	public float waveAmplitude { get { return _waveAmplitude; } set { _waveAmplitude = value; } }

	[SerializeField] float _waveExponent = 22.0f;
	public float waveExponent { get { return _waveExponent; } set { _waveExponent = value; } }
	
	[SerializeField] float _waveInterval = 20.0f;
	public float waveInterval { get { return _waveInterval; } set { _waveInterval = value; } }
	
	[SerializeField] float _waveSpeed = 10.0f;
	public float waveSpeed { get { return _waveSpeed; } set { _waveSpeed = value; } }
	
	[SerializeField] Color _addColor = Color.black;
	public Color addColor { get { return _addColor; } set { _addColor = value; } }
	
	[SerializeField] Shader shader;
	
	[SerializeField]	Material material;
	//[SerializeField]	GameObject	Player;
	
	private float turnSpeed;
	int baseColorID;
	int waveColorID;
	int waveParamsID;
	int waveVectorID;
	int addColorID;
	// Use this for initialization
	void Awake()
	{
		baseColorID = Shader.PropertyToID("_BaseColor");
		waveColorID = Shader.PropertyToID("_WaveColor");
		waveParamsID = Shader.PropertyToID("_WaveParams");
		addColorID = Shader.PropertyToID("_AddColor");
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Start()
	{
		//Executed in Start so that the parent 'turnSpeed' variable is set first
		turnSpeed = GetComponentInParent<PlayerMovement>().turnSpeed;
	}
	
	void OnEnable()
	{
		//GetComponent<Camera>().SetReplacementShader(shader, null);
		Update();
	}
	
	void OnDisable()
	{
		GetComponent<Camera>().ResetReplacementShader();
	}
	
	void Update()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (Time.deltaTime * Input.GetAxis("Mouse Y") * -turnSpeed),
		                                      transform.rotation.eulerAngles.y,
		                                      transform.rotation.eulerAngles.z);
		Shader.SetGlobalColor(baseColorID, _baseColor);
		Shader.SetGlobalColor(waveColorID, _waveColor);
		Shader.SetGlobalColor(addColorID, _addColor);
		
		var param = new Vector4(_waveAmplitude, _waveExponent, _waveInterval, _waveSpeed);
		Shader.SetGlobalVector(waveParamsID, param);
		Shader.EnableKeyword("SONAR_SPHERICAL");
		//Debug.Log(Shader.ToString());
		Shader.SetGlobalVector("_WaveVector", transform.position);
		//material.SetVector("_WaveVector", transform.position);
		
	}
}
