using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopup2 : MonoBehaviour {

	[SerializeField]
	private Button _popupBut1;
	[SerializeField]
	private Button _popupBut2;

	[SerializeField]
	private GameObject _popupCloseBut1;

	public float minSize;
	public float growFactor;
	public float waitTime;

	public RectTransform standartPos;

	[SerializeField]
	private GameObject _popup2;

	[SerializeField]
	private Image _popup2Image;

	[SerializeField]
	private GameObject _popup2Fin;

	[SerializeField]
	private GameObject _popup2Finish;

	[SerializeField]
	private Popup1 _grow;

	private bool buttset = true;

	void Start()
	{
		standartPos = _popup2.GetComponent<RectTransform>();
	}

	public void ClosePopup ()
	{
		StopAllCoroutines ();
		StartCoroutine(Spin (_popup2Finish));
		StartCoroutine(Scale());
		StartCoroutine(Move ());

		_popupCloseBut1.SetActive (false);

		_popupBut1.interactable = false;
		_popupBut2.interactable = false;

		buttset = false;
	}

	IEnumerator Spin( GameObject go)
	{
		float duration = 90f;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed -= 1;
			go.transform.Rotate(Vector3.forward, elapsed);
			yield return new WaitForEndOfFrame();
		}
		yield return null;	
	}



	IEnumerator Move()
	{
		while(_popup2.transform.localPosition.x < 500)
		{
			_popup2.transform.localPosition = Vector3.MoveTowards(_popup2.transform.localPosition, _popup2Fin.transform.localPosition, growFactor);
			yield return null;
		}
	}

	IEnumerator Scale()
	{
		while(_popup2.transform.localScale != new Vector3(0.0f, 0.0f, 0.0f)) // this could also be a condition indicating "alive or dead"
		{
			// we scale all axis, so they will have the same value, 
			// so we can work with a float instead of comparing vectors
			while(minSize < _popup2.transform.localScale.x )
			{
				_popup2.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * growFactor/10;
				_grow.grow = false;
				yield return null;
			}
			yield return new WaitForSeconds(waitTime);
		}
	}

	void Update()
	{
		if (_popup2.transform.localPosition.x == 500) 
		{
			StopAllCoroutines ();

			_popup2Image.color = new Color(_popup2Image.color.r,_popup2Image.color.g,_popup2Image.color.b,0);
			_popup2.gameObject.SetActive (false);
		}

		if (_popup2.activeSelf == false && buttset==false) 
		{
			_popup2Finish.transform.localRotation = Quaternion.Euler (Vector3.zero);

			_popupBut1.interactable = true;
			_popupBut2.interactable = true;
			buttset = true;

			StopAllCoroutines ();
		}
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
