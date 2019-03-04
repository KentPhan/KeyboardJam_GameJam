using UnityEditor;

namespace Assets.Source.Editor
{
    [CustomEditor(typeof(WordEventManager))]
    public class WordEventManagerEditor : UnityEditor.Editor
    {
        public void OnEnable()
        {

        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
}
