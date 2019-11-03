using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField]
	private AudioClip menu;
	[SerializeField]
	private AudioClip[] _songs;
	[SerializeField]
	private AudioClip[] _sounds;
	[SerializeField][SongList]
	private string[] song_names;
	[SerializeField][SongList]
	private string[] fx_names;

	private AudioSource soundPlayer;
	private AudioSource songPlayer;

	private static SoundManager _soundManager;
	private static ResourceRequest request;
	private static ResourceRequest autorequest;
	private static float weight;
	private static float volumeSNG = 1;
	private static float volumeSFX = 1;
	private static int loadId=1,songId=1;
	delegate void Del();
	Del update;
	private void Update()
	{
		if(weight>0)weight-=Time.deltaTime;
		update?.Invoke();
		//PRECISA DE SISTEMA DE PAUSE
		if(false)
		{
			songPlayer.volume -= Time.deltaTime;
		}
		else if(_soundManager.songPlayer.volume<volumeSNG)
		{
			_soundManager.songPlayer.volume+=Time.deltaTime/3f;
			if(_soundManager.songPlayer.volume>volumeSNG) _soundManager.songPlayer.volume=volumeSNG;
		}
	}
	void LoadSongs(){
		if(songId<_songs.Length && request.isDone)
		{
			_songs[songId]=request.asset as AudioClip;
			if(++songId<_songs.Length) request=Resources.LoadAsync<AudioClip>("Audio/"+ song_names[songId]);
			else update-=LoadSongs;
		}
	}
	void LoadSounds(){
		if(loadId<_sounds.Length && autorequest.isDone)
		{
			_sounds[loadId]=autorequest.asset as AudioClip;
			if(++loadId<_sounds.Length) autorequest=Resources.LoadAsync<AudioClip>("Audio/"+ fx_names[loadId]);
			else update-=LoadSounds;
		}
	}
	void Awake() 
	{
		if(_soundManager)
		{
			Destroy(gameObject);
			return;
		}
		update=LoadSounds;
		update+=LoadSongs;
		songPlayer = gameObject.AddComponent<AudioSource>();
		soundPlayer = gameObject.AddComponent<AudioSource>();
		songPlayer.loop = true;
		DontDestroyOnLoad(gameObject);
		_soundManager = this;
		request=Resources.LoadAsync<AudioClip>("Audio/"+ song_names[loadId]);
		autorequest=Resources.LoadAsync<AudioClip>("Audio/"+ fx_names[loadId]);
	}

	public static void Play (int i)
	{
		if(_soundManager == null)
		{
			Debug.LogWarning("SoundManager nao inicializado");
			return;
		}
		_soundManager.songPlayer.clip =_soundManager._songs[i];
		_soundManager.songPlayer.volume=0f;
		_soundManager.songPlayer.Play();
	}

	public static void PlayEffects (int i)
	{
		PlayEffects(i,0,0);
	}
	public static void PlayEffects (int i,float w, float l)
	{
		if(_soundManager == null)
		{
			Debug.LogWarning("SoundEffects nao inicializado");
			return;
		}
		if(l==0 || weight<l){
			_soundManager.soundPlayer.PlayOneShot(_soundManager._sounds[i],volumeSFX);
			weight+=w;
		}

	}

	public static void VolumeMusic(float i)
	{
		volumeSNG = i;
		_soundManager.songPlayer.volume=i;
	}

	public static void VolumeSFX(float i)
	{

		volumeSFX = i;
	}
	public static float GetVolumeSFX()
	{
		return volumeSFX;
	}

	public static float GetVolumeSNG()
	{
		return volumeSNG;
	}
	public static void Save()
	{
		PlayerPrefs.SetFloat("volumeSFX",volumeSFX);
		PlayerPrefs.SetFloat("volumeSNG",volumeSNG);
	}
	public static void Load()
	{
		if(PlayerPrefs.HasKey("volumeSFX"))volumeSFX =PlayerPrefs.GetFloat("volumeSFX");
		if(PlayerPrefs.HasKey("volumeSNG")) volumeSNG=PlayerPrefs.GetFloat("volumeSNG");
	}
}
