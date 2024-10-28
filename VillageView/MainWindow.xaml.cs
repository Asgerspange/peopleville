using PeopleVilleEngine;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace VillageView
{
    public partial class MainWindow : Window
    {
        private static Village village = new Village();
        private PeopleVilleEngine.EventManager eventManager = new PeopleVilleEngine.EventManager(village);

        Random random = new Random();

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
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(Color.FromRgb(45, 48, 71))
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
                    Foreground = new SolidColorBrush(Colors.White),
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                housingPanel.Children.Add(locationLabel);

                Grid villagerGrid = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10),
                };

                int col = 0;
                foreach (var villager in location.Villagers().OrderByDescending(v => v.Age))
                {
                    string genderPath = villager.IsMale ? "Male" : "Female";
                    string colorPrefix = villager.IsWhite ? "White" : "Black";
                    int randomNumber = random.Next(1, villager.IsMale ? 5 : 4);

                    string imagePath = $"/Images/{genderPath}/{colorPrefix}{randomNumber}.png";

                    villagerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    Grid itemGrid = new Grid();
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var primaryForegroundColor = new SolidColorBrush(Colors.White);
                    var primaryFontFamily = new FontFamily("Segoe UI");

                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri($"pack://application:,,,/VillageView;component{imagePath}"));
                    image.Stretch = Stretch.Uniform;
                    Grid.SetRow(image, 0);
                    itemGrid.Children.Add(image);

                    Label label = new Label
                    {
                        Content = $"{villager.FirstName} {villager.LastName}",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Foreground = primaryForegroundColor,
                        FontSize = 16
                    };
                    Grid.SetRow(label, 1);
                    itemGrid.Children.Add(label);

                    Button button = new Button
                    {
                        Content = itemGrid,
                        Margin = new Thickness(5),
                        BorderThickness = new Thickness(0),
                        Style = (Style)FindResource("VillagerButtonStyle")
                    };
                    button.Click += (sender, e) =>
                    {
                        infoPanel.Children.Clear();
                        infoPanel.Children.Add(new Label { Content = "Person Info", FontSize = 32, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, FontWeight = FontWeights.Medium, Margin = new Thickness(10, 7, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Dag: {village.Time.ToString()}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Navn: {villager.FirstName} {villager.LastName}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Alder: {villager.Age}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Penge: {villager.PersonalWallet.Money}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Job: {villager.Role}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                    };

                    Grid.SetColumn(button, col);
                    villagerGrid.Children.Add(button);

                    col++;
                }

                housingPanel.Children.Add(villagerGrid);

                Border housingBorder = new Border
                {
                    BorderThickness = new Thickness(5),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(69, 70, 92)),
                    CornerRadius = new CornerRadius(5),
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

            var gameEvents = eventManager.ExecuteEvents();
            foreach (var gameEvent in gameEvents)
            {
                eventLabel.Content = gameEvent.Description;
                EventPopup popup = new EventPopup(gameEvent.Title, gameEvent.Description);
                popup.ShowDialog();
            }
        }
    }
}
