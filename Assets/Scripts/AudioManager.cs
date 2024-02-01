using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static bool IsSoundOff;

    private void Awake()
    {
	    AudioListener.pause = IsSoundOff;
    }

    void Start()
    {
    
		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			
		}		
		PlaySound("MainTheme");        
    }
	
	public void PlaySound(string name)
	{
		foreach(Sound s in sounds)
		{
			if(s.name == name) 
				s.source.Play();
			
		}
	}

	public void SoundOnOff()
	{
		AudioListener.pause = !AudioListener.pause;
		IsSoundOff = !IsSoundOff;
	}
	public void SoundOnOff(bool par)
	{
		AudioListener.pause = par;
		IsSoundOff = par;
	}
}
