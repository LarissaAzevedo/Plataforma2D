using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {


    private Camera cam;
    public Transform playerTransform;

    public float speedCam;

    public Transform LimiteCamEsc, LimiteCamDir, LimiteCamCima, LimiteCamBaixo;

    [Header("Audio")]

    public AudioSource sfxsource;
    public AudioSource musicSource;

    public AudioClip sfxJump;
    public AudioClip sfxAttack;
    public AudioClip sfxCoin;
    public AudioClip sfxEnemyDeath;
    public AudioClip[] sfxStep;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate(){ 
    
        float posCamX = playerTransform.position.x;
        float posCamY = playerTransform.position.y;

        
        if(cam.transform.position.x < LimiteCamEsc.position.x && playerTransform.position.x < LimiteCamEsc.position.x)
        {
            posCamX = LimiteCamEsc.position.x;//limite pra ir pra esquerda
        }
        else if (cam.transform.position.x > LimiteCamDir.position.x && playerTransform.position.x > LimiteCamDir.position.x)
        {
            posCamX = LimiteCamDir.position.x; //limite pra ir pra direita
        }

        if(cam.transform.position.y < LimiteCamBaixo.position.y && playerTransform.position.y < LimiteCamBaixo.position.y)
        {
            posCamY = LimiteCamBaixo.position.y; //limite pra ir pra baixo
        }else if (cam.transform.position.y > LimiteCamCima.position.y && playerTransform.position.y > LimiteCamCima.position.y)
        {
            posCamY = LimiteCamCima.position.y; //limite pra ir pra baixo
        }

        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);

    }

    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxsource.PlayOneShot(sfxClip, volume);
    }
}
  