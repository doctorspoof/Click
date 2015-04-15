using UnityEngine;
using System.Collections;

public class CalumsSuperSecretTestingScript : MonoBehaviour {
	
	//[SerializeField]	Shader	ShaderToRenderWith;
	//Camera CachedCameraComponent;
	[SerializeField]	GameObject	Rectangle;
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CachedCameraComponent.SetReplacementShader(ShaderToRenderWith, "ForwardBase");
		Texture RenderTex = GetComponent<Camera> ().targetTexture;
		Rectangle.GetComponent<Renderer>().material.SetTexture("_MainTex", RenderTex);
	}
}
