using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private TextMeshProUGUI textMeshPro;
    Color32 originalColor;
    // Start is called before the first frame update
    void Start(){
        textMeshPro=GetComponent<TextMeshProUGUI>();
        originalColor=textMeshPro.color;
    }

    // Update is called once per frame
    void Update(){
        
    }
    
    public void OnPointerEnter(PointerEventData eventData){
        textMeshPro.color=Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData){
        textMeshPro.color=originalColor;
    }
}
