using PeopleVilleEngine;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;

namespace VillageView
{
    public partial class MainWindow : Window
    {
        private static Village village = new Village();
        private PeopleVilleEngine.EventManager eventManager = new PeopleVilleEngine.EventManager(ref village);

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            village.Time.NewDayStarted += UpdateDayDisplay;

            RefreshVillagersUI();

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
            string dayString = village.Time.ToString().Replace("Day ", "");
            int currentDay = int.Parse(dayString);

            dayLabel.Content = village.Time.ToString();
            dagLabel.Content = ("Dag " + currentDay.ToString());

            var gameEvents = eventManager.ExecuteEvents();
            if (gameEvents.Any())
            {
                EventPopup popup = new EventPopup(gameEvents, currentDay);
                popup.ShowDialog();
            }

            RefreshVillagersUI();
        }

        private Dictionary<string, string> villagerImages = new Dictionary<string, string>();
        private List<string> knownVillagerKeys = new List<string>();

        private void RefreshVillagersUI()
        {
            mainPanel.Children.Clear();
            foreach (var location in village.Locations)
            {
                StackPanel housingPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Background = new SolidColorBrush(Color.FromRgb(45, 48, 71))
                };

                var lastNames = location.Villagers()
                    .Select(v => v.LastName)
                    .Distinct()
                    .ToList();

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
                    string villagerKey = $"{villager.FirstName} {villager.LastName}";
                    var primaryForegroundColor = new SolidColorBrush(Colors.White);
                    var primaryFontFamily = new FontFamily("Segoe UI");

                    string genderPath = villager.IsMale ? "Male" : "Female";
                    string colorPrefix = villager.IsWhite ? "White" : "Black";
                    string agePath = villager.Age < 18 ? "Baby/" : "";


                    string imagePath;
                    if (!knownVillagerKeys.Contains(villagerKey))
                    {
                        knownVillagerKeys.Add(villagerKey);
                        string uniqueId = Guid.NewGuid().ToString();
                        villagerKey = $"{villagerKey}-{uniqueId}";
                        int randomNumber = villager.Age < 18 ? 1 : random.Next(1, villager.IsMale ? 5 : 4);
                        imagePath = $"/Images/{genderPath}/{agePath}{colorPrefix}{randomNumber}.png";
                        villagerImages[villagerKey] = imagePath;
                    }
                    else
                    {
                        string fullVillagerKey = villagerImages.Keys.FirstOrDefault(k => k.StartsWith(villagerKey));
                        if (fullVillagerKey != null)
                        {
                            imagePath = villagerImages[fullVillagerKey];
                        }
                        else
                        {
                            imagePath = "/Images/Male/White1.png";
                        }
                    }

                    villagerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                    Grid itemGrid = new Grid();
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    itemGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });


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
                        infoPanel.Children.Add(new Label { Content = $"Navn: {villager.FirstName} {villager.LastName}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Alder: {villager.Age}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Penge: {villager.PersonalWallet.Money}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });
                        infoPanel.Children.Add(new Label { Content = $"Job: {villager.Role}", FontSize = 16, Foreground = primaryForegroundColor, FontFamily = primaryFontFamily, Margin = new Thickness(10, 0, 0, 0) });

                        // Create and populate the inventory grid
                        Grid inventoryGrid = new Grid
                        {
                            Margin = new Thickness(10, 10, 0, 0),
                            Background = new SolidColorBrush(Color.FromRgb(34, 34, 34)),
                        };

                        int columns = 3;
                        for (int i = 0; i < columns; i++)
                        {
                            inventoryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        }

                        int row = 0;
                        int col = 0;

                        foreach (var item in villager.Inventory)
                        {
                            if (col == columns)
                            {
                                col = 0;
                                row++;
                            }

                            inventoryGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                            string itemImagePath = $"/Images/Weapons/{item.Name}.png";

                            Image itemImage = new Image();
                            itemImage.Source = new BitmapImage(new Uri($"pack://application:,,,/VillageView;component{itemImagePath}"));
                            itemImage.Stretch = Stretch.Uniform;
                            Grid.SetColumn(itemImage, col);
                            Grid.SetRow(itemImage, row);
                            inventoryGrid.Children.Add(itemImage);

                            col++;
                        }

                        infoPanel.Children.Add(inventoryGrid);
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
                    Margin = new Thickness(0, 0, 0, 10),
                    Child = housingPanel
                };

                mainPanel.Children.Add(housingBorder);
            }
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                infoPanel.Children.Clear();
            }
        }

        private void mainPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            infoPanel.Children.Clear();
        }

        private void infoPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateDayDisplay();
        }
    }
}
