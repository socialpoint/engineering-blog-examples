using System;
using System.Configuration;
using UnityEngine;

namespace SocialPoint.Examples.MVC
{
    public class ButtonPresenterEventArgs : System.EventArgs
    {
        public Vector3 MousePosition {get;private set;}

        public ButtonPresenterEventArgs(Vector2 mousePosition)
        {
            MousePosition = mousePosition;
        }

    }

    /// <summary>
    ///     View Presenter for a button with a label
    /// </summary>
    public class ButtonViewPresenter : ViewPresenter
    {
        Color _buttonOriginalHoverColor;
        Color _buttonOriginalDefaultColor;

        public UIButton Button;
        public UILabel ButtonLabel;
        public UITexture ButtonImage;

        public string Text
        {
            get
            {
                return ButtonLabel != null ? ButtonLabel.text : string.Empty;
            }
            set
            {
                if(ButtonLabel == null)
                {
                    return;
                }
                ButtonLabel.text = value;
            }
        }

        public Texture ImageSprite
        {
            get
            {
                return ButtonImage != null ? ButtonImage.mainTexture : null;
            }

            set
            {
                if(ButtonImage == null)
                {
                    return;
                }

                ButtonImage.mainTexture = value;
            }
        }

        /// <summary>
        ///     This will allow to keep track of the status of the button in order to disable
        ///     the events if the button is disabled
        /// </summary>
        public bool IsEnabled
        {
            get;
            private set;

        }

        public override void Enable()
        {
            IsEnabled = true;
            Button.defaultColor = _buttonOriginalDefaultColor;
            Button.hover = _buttonOriginalHoverColor;
            Button.UpdateColor(IsEnabled, false);
        }

        public override void Disable()
        {
            IsEnabled = false;
            Button.defaultColor = Button.disabledColor;
            Button.hover = Button.disabledColor;
            Button.UpdateColor(IsEnabled, false);
        }

        public event EventHandler<ButtonPresenterEventArgs> Clicked;

        protected virtual void OnButtonClicked()
        {
            // Do not propagate the click event if the button is disabled
            if(!IsEnabled)
            {
                return;
            }

            if(Clicked != null)
            {
                Clicked(this, new ButtonPresenterEventArgs(Input.mousePosition));
            }
        }

        protected override void AwakeUnityMsg()
        {
            base.AwakeUnityMsg();

            WireUIEvents();

            IsEnabled = Button.isEnabled;

            _buttonOriginalDefaultColor = Button.defaultColor;
            _buttonOriginalHoverColor = Button.hover;
        }

        protected virtual void WireUIEvents()
        {
            // Programatically add the onClick handler if it is not set
            // so the ButtonClicked event is always called (NGUI specific)
            if(Button.onClick.Count <= 0)
            {
                Button.onClick.Add(new EventDelegate(this, "OnButtonClicked"));
            }
        }
    }


}
