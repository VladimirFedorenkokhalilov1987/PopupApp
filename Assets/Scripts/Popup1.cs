using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup1 : MonoBehaviour {

	[SerializeField]
	private Button _popupBut1;
	[SerializeField]
	private Button _popupBut2;

	[SerializeField]
	private GameObject _popupCloseBut1;


	[SerializeField]
	private GameObject _popup1;

	[SerializeField]
	private GameObject _popup2;
	[SerializeField]
	private ClosePopup2 _closePopup2;

	[SerializeField]
	private Image _popup1Image;

	static float t = 0.0f;

	private float minimum = 0.0f;
	private float maximum =  255.0F;

	[Range(0.1f,1.0f)]
	public float _fadeSpeed;

	[SerializeField]
	RectTransform _startTr;

	private Sprite[] myTextures;

	public float minSize;
	public float growFactor;
	public float waitTime;

	public bool grow = false;

	void Awake()
	{
		myTextures = Resources.LoadAll<Sprite>("500");	
	}

	public void Clik () {
		grow = false;
		if (_popup2.activeSelf != true) {
			_popup1.SetActive (true);
			_popupCloseBut1.SetActive (true);
			_popup1.transform.position = _startTr.position;
			_popup1.transform.localScale = _startTr.localScale;
			_popup1Image.sprite = myTextures [Random.Range (0, 3)];

			StartCoroutine (Scale());
			_popup1Image.color = new Color (_popup1Image.color.r, _popup1Image.color.g, _popup1Image.color.b, 0);
		}
		else
		{
			_closePopup2.ClosePopup ();
			StartCoroutine (Wait(3));
		}
	}

	IEnumerator Scale()
	{
		float timer = 0;

		while(_popup1.transform.localScale != new Vector3(1.0f, 1.0f, 1.0f) && grow==false) // this could also be a condition indicating "alive or dead"
		{
			// we scale all axis, so they will have the same value, 
			// so we can work with a float instead of comparing vectors
			while(minSize > _popup1.transform.localScale.x )
			{
				timer += Time.deltaTime;
				_popup1.transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
				grow = true;
				yield return null;
			}

			yield return new WaitForSeconds(waitTime);
		}
	}

	void Update () 
	{
		if (_popup1Image.color.a == 0)
			t = 0;
		
		t += _fadeSpeed * Time.deltaTime;

		if (t > 255.0f)
		{
			float temp = maximum;
			maximum = minimum;
			minimum = temp;
			t = 0.0f;
		}

		if (t < 255.0f && _popup1.activeSelf == true) 
		{
			_popup1Image.color = new Color (_popup1Image.color.r, _popup1Image.color.g, _popup1Image.color.b, t);
		}
	}

	IEnumerator Wait(float time)
	{
		print ("pause");

		yield return new WaitForSeconds (time);

		_popup1.SetActive (true);
		_popupCloseBut1.SetActive (true);
		_popup1.transform.position = _startTr.position;
		_popup1.transform.localScale = _startTr.localScale;
		_popup1Image.sprite = myTextures [Random.Range (0, 3)];
		StartCoroutine (Scale());
		_popup1Image.color = new Color (_popup1Image.color.r, _popup1Image.color.g, _popup1Image.color.b, 0);
		_popup2.SetActive (false);

		yield return null;
	}
}
