using UnityEngine;
using System.Collections;

public class CalumsSuperSecretTestingScript : MonoBehaviour {
	
	//[SerializeField]	Shader	ShaderToRenderWith;
	//Camera CachedCameraComponent;
	[SerializeField]	GameObject	Rectangle;
	public Material Shite; 
	
	// Use this for initialization
	void Start () 
	{
		Shite = Rectangle.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//CachedCameraComponent.SetReplacementShader(ShaderToRenderWith, "ForwardBase");
		Texture RenderTex = GetComponent<Camera> ().targetTexture;
		GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
		Rectangle.GetComponent<Renderer>().material.SetTexture("_MainTex", RenderTex);
		
		if(Input.GetKey(KeyCode.Z))
		{
			Shite.SetFloat("_Intensity", Shite.GetFloat("_Intensity") + 0.01f);
		}
		if(Input.GetKey(KeyCode.X))
		{
			Shite.SetFloat("_Intensity", Shite.GetFloat("_Intensity") - 0.01f);
		}
	}
}
