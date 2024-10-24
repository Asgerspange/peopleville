using PeopleVilleEngine;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace VillageView
{
    public partial class MainWindow : Window
    {
        private Village village = new Village();

        public MainWindow()
        {
            InitializeComponent();
            village.Time.NewDayStarted += UpdateDayDisplay;

            foreach (var location in village.Locations)
            {
                // Create a StackPanel for each housing group
                StackPanel housingPanel = new StackPanel
                {
                    Margin = new Thickness(10),
                    Orientation = Orientation.Vertical
                };

                // Get the distinct last names of all villagers in this location
                var lastNames = location.Villagers()
                    .Select(v => v.LastName)
                    .Distinct()
                    .ToList();

                // Concatenate the last names and add "House" to form the location label
                string locationLabelText = string.Join(", ", lastNames) + "s Hus";

                Label locationLabel = new Label
                {
                    Content = $"Lokation: {locationLabelText}",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                housingPanel.Children.Add(locationLabel);

                Grid villagerGrid = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10)
                };

                int col = 0;
                foreach (var villager in location.Villagers().OrderByDescending(v => v.Age))
                {
                    villagerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    Grid itemGrid = new Grid();
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    Image image = new Image();
                    if (villager.IsMale)
                    {
                        image.Source = villager.IsWhite
                            ? new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/hatman.jpg"))
                            : new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/MaleBlackVillager.jpg"));
                    }
                    else
                    {
                        image.Source = villager.IsWhite
                            ? new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/FemaleWhiteVillager.jpg"))
                            : new BitmapImage(new Uri("pack://application:,,,/VillageView;component/Images/FemaleBlackVillager.jpg"));
                    }
                    image.Stretch = Stretch.Uniform;
                    Grid.SetRow(image, 0);
                    itemGrid.Children.Add(image);

                    Label label = new Label
                    {
                        Content = $"{villager.FirstName} {villager.LastName}",
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(label, 1);
                    itemGrid.Children.Add(label);

                    Button button = new Button
                    {
                        Content = itemGrid,
                        Margin = new Thickness(5)
                    };
                    button.Click += (sender, e) =>
                    {
                        infoPanel.Children.Clear();
                        infoPanel.Children.Add(new Label { Content = "Info Panel", FontSize = 32 });
                        infoPanel.Children.Add(new Label { Content = $"Dag: {village.Time.ToString()}", FontSize = 16 });
                        infoPanel.Children.Add(new Label { Content = $"Navn: {villager.FirstName} {villager.LastName}", FontSize = 16 });
                        infoPanel.Children.Add(new Label { Content = $"Alder: {villager.Age}", FontSize = 16 });
                        infoPanel.Children.Add(new Label { Content = $"Penge: {villager.PersonalWallet.Money}", FontSize = 16 });
                        infoPanel.Children.Add(new Label { Content = $"Job: {villager.Role}", FontSize=16 });
                    };

                    Grid.SetColumn(button, col);
                    villagerGrid.Children.Add(button);

                    col++;
                }

                housingPanel.Children.Add(villagerGrid);

                Border housingBorder = new Border
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = Brushes.Gray,
                    CornerRadius = new CornerRadius(5),
                    Padding = new Thickness(10),
                    Child = housingPanel
                };

                mainPanel.Children.Add(housingBorder);
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
