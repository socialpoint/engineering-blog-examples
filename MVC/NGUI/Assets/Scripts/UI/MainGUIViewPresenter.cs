namespace SocialPoint.Examples.MVC
{

    public class MainGUIViewPresenter : ViewPresenter
    {
        public ButtonViewPresenter AddXpButton;
        public ButtonViewPresenter TakeDamageButton;

        public LabelViewPresenter HitPointsLabel;
        public LabelViewPresenter XpLabel;
        public LabelViewPresenter LevelLabel;

        public override void Show()
        {
            AddXpButton.Show();
            XpLabel.Show();
            LevelLabel.Show();
            HitPointsLabel.Show();
            TakeDamageButton.Show();

            base.Show();
        }

        public override void Hide()
        {
            AddXpButton.Hide();
            XpLabel.Hide();
            LevelLabel.Hide();
            HitPointsLabel.Hide();
            TakeDamageButton.Hide();

            base.Hide();
        }
    }
}
