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

namespace VillageView
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

            var village = new Village();
            List<string> myStrings = new List<string>();

            foreach (var villager in village.Villagers)
            {
                myStrings.Add($"{villager.FirstName} {villager.LastName}");
            }

            int row = 0;
            int col = 0;

            foreach (string str in myStrings)
            {
                // Create a Grid to hold the image and label for each item
                Grid itemGrid = new Grid();

                // Add RowDefinitions to the itemGrid
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }); // For the image
                itemGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // For the label

                // Create an Image control (you'll need to add image loading logic later)
                Image image = new Image();
                image.Source = new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/Villager.jpg")); // Set the image source here
                image.Stretch = Stretch.Uniform;
                Grid.SetRow(image, 0);
                itemGrid.Children.Add(image);

                // Create a Label for the string
                Label label = new Label();
                label.Content = str;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(label, 1);
                itemGrid.Children.Add(label);

                // Create a Button and set its content to the itemGrid
                Button button = new Button();
                button.Content = itemGrid;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                buttonPanel.Children.Add(button); // Add the *button* to the panel

                // Update column and row counters
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
        }
    }
}