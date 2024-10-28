using System.Windows;

namespace VillageView
{
    public partial class EventPopup : Window
    {
        public EventPopup(string title, string description)
        {
            InitializeComponent();
            titleLabel.Content = title;
            descriptionTextBlock.Text = description;
        }
    }
}
