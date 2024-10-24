using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using PeopleVilleEngine;
using System.Windows.Threading;

namespace VillageView
{
    public partial class MainWindow : Window
    {
        private Village village = new Village();
        public MainWindow()
        {
            InitializeComponent();


            village.Time.NewDayStarted += UpdateDayDisplay;

            int row = 0;
            int col = 0;


            foreach (var villager in village.Villagers)
            {
                // Create a Grid to hold the image and label for each item
                Grid itemGrid = new Grid();

                // Add RowDefinitions to the itemGrid
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // For the image
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // For the label

                // Create an Image control (you'll need to add image loading logic later)
                Image image = new Image();

                if (villager.IsMale)
                {
                    if (!villager.IsWhite)
                    {
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/MaleBlackVillager.jpg")); // Set the image to male black villager
                    }
                    else
                    {
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/MaleWhiteVillager.jpg")); // Set the image to male white villager
                    }
                }
                else
                {
                    if (!villager.IsWhite)
                    {
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/FemaleBlackVillager.jpg")); // Set the image to female black villager
                    }
                    else
                    {
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/FemaleWhiteVillager.jpg")); // Set the image to female white villager
                    }
                }
                image.Stretch = Stretch.Uniform;
                Grid.SetRow(image, 0);
                itemGrid.Children.Add(image);

                // Create a Label for the string
                Label label = new Label();
                label.Content = $"{villager.FirstName} {villager.LastName}";
                label.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(label, 1);
                itemGrid.Children.Add(label);

                // Create a Button and set its content to the itemGrid
                Button button = new Button();
                button.Content = itemGrid;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                button.Click += (sender, e) => {
                    infoPanel.Children.Clear();
                    infoPanel.Children.Add(new Label { Content = "Info Panel", FontSize = 32 });
                    infoPanel.Children.Add(new Label { Content = $"Dag: {village.Time.ToString()}", FontSize = 16 });
                    infoPanel.Children.Add(new Label { Content = $"Navn: {villager.FirstName} {villager.LastName}", FontSize = 16 });
                    infoPanel.Children.Add(new Label { Content = $"Alder: {villager.Age}", FontSize = 16});
                };

                buttonPanel.Children.Add(button);

                col++;
                if (col >= 7)
                {
                    col = 0;
                    row++;
                }

                // Add a new row to buttonPanel if needed
                if (row >= buttonPanel.RowDefinitions.Count)
                {
                    buttonPanel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
            }

            this.PreviewKeyUp += MainWindow_PreviewKeyUp;
        }

        private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                village.Time.UpdateDay();
            }
        }

        private void UpdateDayDisplay()
        {
            dayLabel.Content = village.Time.ToString();
        }
    }
}