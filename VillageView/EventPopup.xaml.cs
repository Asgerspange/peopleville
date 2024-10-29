// EventPopup.xaml.cs
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PeopleVilleEngine;

namespace VillageView
{
    public partial class EventPopup : Window
    {
        private List<EventDetails> events; // Changed from List<GameEvent> to List<EventDetails>
        private int currentEventIndex = -1;

        public EventPopup(List<EventDetails> gameEvents, int currentDay) // Add currentDay parameter
        {
            InitializeComponent();
            events = gameEvents;

            // Show day message initially
            titleLabel.Content = $"Dag {currentDay}";
            descriptionTextBlock.Text = ""; // Clear any previous description
            eventCountLabel.Content = "";  // Clear any previous event count
            nextButton.Visibility = Visibility.Visible; // Make sure the next button is visible

            currentEventIndex++; // Increment to 0 for the first event
        }

        private void UpdateEventDisplay()
        {
            titleLabel.Content = events[currentEventIndex].Title;
            descriptionTextBlock.Text = events[currentEventIndex].Description;
            eventCountLabel.Content = $"{currentEventIndex + 1}/{events.Count}";
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentEventIndex < events.Count)
            {
                UpdateEventDisplay();
            }
            else
            {
                this.Close();
            }
            currentEventIndex++;
        }
    }
}