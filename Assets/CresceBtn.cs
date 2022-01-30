using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CresceBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool On = false;
    [SerializeField] private Vector3 Max;
    [SerializeField] private Vector3 Min;
    public GameObject tx;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Min;
        On = false;
    }

    void OnEnable()
    {
        transform.localScale = Min;
        On = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            On = false;
        }
        if (On == true && gameObject.transform.localScale.x < Max.x)
        {
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (On == true && gameObject.transform.localScale.x >= Max.x)
        {
            transform.localScale = Max;
            if (tx != null)
            {
                tx.SetActive(true);
            }
            
        }

        if (On == false && gameObject.transform.localScale.x > Min.x)
        {
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        }
        else if (On == false && gameObject.transform.localScale.x <= Min.x)
        {
            transform.localScale = Min;
            if (tx != null)
            {
                tx.SetActive(false);
            }
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        On = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        On = false;
    }
}
