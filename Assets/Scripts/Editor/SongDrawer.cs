using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(SongListAttribute))]
public class SongDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType!=SerializedPropertyType.String){
            EditorGUI.LabelField(position,"SongList attribute should only be used in String Arrays");
            return;
        }
        if(string.IsNullOrWhiteSpace(property.stringValue)){
            AudioClip ac=null;
            ac=EditorGUI.ObjectField(position,label,ac,typeof(AudioClip),false)as AudioClip;
            if(ac)property.stringValue=ac.name;
        }else{
            position.width-=position.height;
            EditorGUI.LabelField(position,property.stringValue);
            position.x+=position.width;
            position.width=position.height;
            if(GUI.Button(position,"X"))property.stringValue="";
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
        return EditorGUI.GetPropertyHeight(property);
    }
}
