namespace SocialPoint.Examples.MVC
{

    public class LabelViewPresenter : ViewPresenter
    {
        public UILabel TextLabel;

        public string Text
        {
            get { return TextLabel.text; }
            set { TextLabel.text = value; }
        }

        public UnityEngine.Color TextColor
        {
            get{ return TextLabel.color; }
            set { TextLabel.color = value; }
        }
    }
}
