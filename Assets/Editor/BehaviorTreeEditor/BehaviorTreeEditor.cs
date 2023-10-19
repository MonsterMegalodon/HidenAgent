using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviorTreeEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private BTNodeGraph m_BTNodeGraph = null;
    private BTInspector m_BTInspector = null;

    [MenuItem("BehaviorTree/BehaviorTreeEditor")]
    public static void ShowEditor()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("Behavior Tree Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        m_VisualTreeAsset.CloneTree(root);
        m_BTNodeGraph = root.Q<BTNodeGraph>();
        m_BTNodeGraph.onNodeSelected += NodeSelected;
        m_BTInspector = root.Q<BTInspector>();

        EditorApplication.playModeStateChanged += PlayModeChanged;
    }

    private void PlayModeChanged(PlayModeStateChange change)
    {
        if(change == PlayModeStateChange.EnteredEditMode || change == PlayModeStateChange.EnteredPlayMode)
        {
            OnSelectionChange();
        }
    }

    private void NodeSelected(BTNode node)
    {
        m_BTInspector.ShowInspectorGUI(node);
    }

    //this is called when the selection is changed in the editor
    private void OnSelectionChange()
    {
        BehaviorTree selectedAsTree = Selection.activeObject as BehaviorTree;

        if(selectedAsTree == null)
        {
            if(Selection.activeGameObject)
            {
                selectedAsTree = Selection.activeGameObject.GetComponent<AIController>()?.GetBehaviorTree();
            }
        }

        if (selectedAsTree != null)
        {
            m_BTNodeGraph.PopulateTree(selectedAsTree);
        }
    }

    private void OnDestroy()
    {
        m_BTNodeGraph.SaveTree();
    }
}
