using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup2 : MonoBehaviour {

	[SerializeField]
	private Button _popupBut1;
	[SerializeField]
	private Button _popupBut2;

	[SerializeField]
	private GameObject _popupCloseBut2;

	[SerializeField]
	private GameObject _popup1;
	[SerializeField]
	private ClosePopup1 _closePopup1;

	[SerializeField]
	private GameObject _popup2;

	[SerializeField]
	private Image _popup2Image;

	[SerializeField]
	private GameObject _closePopup2;

	[SerializeField]
	private GameObject _closePopup2Fin;

	[Range(1.0f,10.0f)]
	public float _moveSpeed;

	[SerializeField]
	RectTransform _startTr;

	[SerializeField]
	RectTransform _finishTr;

	private Sprite[] myTextures;

	// Use this for initialization
	void Awake () 
	{
		myTextures = Resources.LoadAll<Sprite>("300");	
	}

	public void Clik () 
	{
		StopAllCoroutines ();

		if (_popup1.activeSelf != true) 
		{
			_popup2.SetActive (true);
			_popupCloseBut2.SetActive (true);
			_popup2.transform.position = _startTr.position;
			_popup2.transform.localScale = _startTr.localScale;
			_popup2.transform.localRotation = _startTr.localRotation;
			_popup2Image.sprite = myTextures [Random.Range (0, 3)];
			_popup2Image.color = new Color (_popup2Image.color.r, _popup2Image.color.g, _popup2Image.color.b, 1);
			StartCoroutine (Move ());
			StartCoroutine (Rotate());
		} 
		else
		{
			_closePopup1.ClosePopup ();
			StartCoroutine (Wait(2));
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(_popup2.transform.localPosition.y == _finishTr.transform.localPosition.y)
		StartCoroutine (Rotate());
	}

	IEnumerator Move()
	{
		while(_popup2.transform.localPosition.y > 0)
		{
			_popup2.transform.localPosition = Vector3.MoveTowards(_popup2.transform.localPosition,  _finishTr.transform.localPosition, _moveSpeed);
			yield return null;
		}
	}

	IEnumerator Rotate()
	{
		while(_popup2.transform.localPosition.y == _finishTr.transform.localPosition.y) {
			_popup2.transform.localRotation = Quaternion.RotateTowards (_popup2.transform.localRotation, _finishTr.transform.localRotation, _moveSpeed);
			_closePopup2.transform.position = _closePopup2Fin.transform.position;
			_closePopup2.transform.localRotation = _closePopup2Fin.transform.localRotation;

			yield return null;
		}
	}
	IEnumerator Wait(float time)
	{
		print ("pause");

		yield return new WaitForSeconds (time);
		_popup2.SetActive (true);
		_popupCloseBut2.SetActive (true);
		_popup2.transform.position = _startTr.position;
		_popup2.transform.localScale = _startTr.localScale;
		_popup2.transform.localRotation = _startTr.localRotation;
		_popup2Image.sprite = myTextures [Random.Range (0, 3)];
		_popup2Image.color = new Color (_popup2Image.color.r, _popup2Image.color.g, _popup2Image.color.b, 1);
		StartCoroutine (Move ());
		StartCoroutine (Rotate());

		yield return null;
	}
}
