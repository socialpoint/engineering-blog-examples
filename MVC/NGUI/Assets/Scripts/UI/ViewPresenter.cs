using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SocialPoint.Examples.MVC
{
    [ExecuteInEditMode]
    public class ViewPresenter : MonoBehaviour
    {

        protected virtual UIRect ViewRoot { get; set; }    

        public event EventHandler ViewDidHide;
        public event EventHandler ViewDidShow;


        #region Unity3D messages
        void Awake()
        {

            // This will allow to set the view in the inspector if we want to
            ViewRoot = ViewRoot ?? GetComponent<UIRect>();

            // Set the alpha to cero so the item is created
            // invisible. When the show method is called
            // the view will be made visible using a transition.
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
                UpdateAlpha(0);
#else
            ViewRoot.alpha = 0f;
#endif

            AwakeUnityMsg();
        }

        void Start()
        {
            StartUnityMsg();
        }

        void OnValidate()
        {
            AutoPopulateDeclaredWidgets();

            OnValidateUnityMsg();
        }

        void OnDestroy()
        {
            OnDestroyUnityMsg();
        }
        #endregion

        #region Unity3D Messages propagation
        protected virtual void AwakeUnityMsg()
        {
        }

        protected virtual void StartUnityMsg()
        {
        }

        protected virtual void OnValidateUnityMsg()
        {
        }

        protected virtual void OnDestroyUnityMsg()
        {
        }
        #endregion

        #region Autopopulate widgets
        [ContextMenu("Auto populate widgets")]
        public void AutoPopulateDeclaredWidgetsContextMenu()
        {
            AutoPopulateDeclaredWidgets();
        }

        /// <summary>
        ///     This matches the widgets found in the prefab this MB is attached to
        ///     with the properties defined in this file, so we can keep a reference
        ///     to them.
        ///     We use the following rules to do the matching:
        ///     GameObject's name == Property Name
        ///     GameObjects widget component type == Property type
        /// </summary>
        void AutoPopulateDeclaredWidgets()
        {
            foreach(var nguiWidget in GetDeclaredUIWidgets())
            {
                var childTransform = GetChildRecursive(transform, nguiWidget.Name);
                if(childTransform == null)
                {
                    continue;
                }
                
                if(nguiWidget.GetValue(this) == null)
                {
                    nguiWidget.SetValue(this, childTransform.GetComponent(nguiWidget.FieldType));
                }
            }
        }

        /// <summary>
        ///    Finds all properties that derive from UIWidgets or UIWidgetContainers
        ///    in this object
        /// </summary>
        /// <returns>The declared user interface widgets.</returns>
        IEnumerable<FieldInfo> GetDeclaredUIWidgets()
        {
            return this.GetType().GetFields(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance).Where(
                m => typeof(UIWidget).IsAssignableFrom(m.FieldType)
                || typeof(UIWidgetContainer).IsAssignableFrom(m.FieldType));
        }


        #endregion

        public virtual void Enable()
        {
            ViewRoot.enabled = true;
        }

        public virtual void Disable()
        {
            ViewRoot.enabled = false;
        }

 

        public virtual void Show()
        {
            UpdateAlpha(1f);

            if(ViewDidShow != null)
            {
                ViewDidShow(this, EventArgs.Empty);
            }
        }

        public virtual void Hide()
        {
            UpdateAlpha(0);

            if(ViewDidHide != null)
            {
                ViewDidHide(this, EventArgs.Empty);
            }
        }


        public static Transform GetChildRecursive(Transform trans, string name)
        {
            Component[] transforms = trans.GetComponentsInChildren( typeof( Transform ), true );
            
            foreach( Transform atrans in transforms )
            {
                if( atrans.name == name )
                {
                    return atrans;
                }
            }
            return null;
        }

        protected void UpdateAlpha(float value)
        {
            ViewRoot.alpha = value;
        }

    }

}
