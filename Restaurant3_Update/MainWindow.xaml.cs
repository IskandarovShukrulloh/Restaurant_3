using Restaurant_3.Abstract;
using Restaurant_3.Classes;
using Restaurant_3.Classes.Drinks;
using System;
using System.Windows;

namespace Restaurant_3
{
    public partial class MainWindow : Window
    {
        private readonly Server _server = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnReceiveRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Parse quantities
                uint chickenQty = ParseUInt(txtChicken.Text, "Chicken quantity");
                uint eggQty = ParseUInt(txtEgg.Text, "Egg quantity");

                // Create drink object (or null)
                Drink? drink = CreateDrinkFromComboBox();

                // Send request to server
                _server.ReceiveRequest(chickenQty, eggQty, drink);

                txtResults.Text = "Received request successfully.";
                txtEggQuality.Text = "";
            }
            catch (Exception ex)
            {
                txtResults.Text = $"Error: {ex.Message}";
            }
        }

        private void BtnSendToCook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResults.Text = _server.Send();

                // If eggs were requested, show egg quality info (simple demo output)
                if (int.TryParse(txtEgg.Text, out int eggs) && eggs > 0)
                    txtEggQuality.Text = "Egg quality " + new Egg().GetQuality();
                else
                    txtEggQuality.Text = "";
            }
            catch (Exception ex)
            {
                txtResults.Text = $"Error: {ex.Message}";
            }
        }

        private void BtnServeFood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtResults.Text = _server.Serve();
            }
            catch (Exception ex)
            {
                txtResults.Text = $"Error: {ex.Message}";
            }
        }

        // -------------------------
        // Helpers (clean UI logic)
        // -------------------------

        private static uint ParseUInt(string? text, string fieldName)
        {
            if (!uint.TryParse(text, out uint value))
                throw new Exception($"{fieldName} is not a valid number.");

            return value;
        }

        private Drink? CreateDrinkFromComboBox()
        {
            // If nothing is selected - no drink
            if (cmbDrink.SelectedValue == null)
                return null;

            string drinkName = cmbDrink.SelectedValue.ToString() ?? "";

            // Map UI string to drink class
            return drinkName switch
            {
                "Tea" => new Tea(),
                "Coffee" => new Coffee(),
                "Coca-Cola" => new Cola(),
                "Water" => new Water(),
                "Milk" => new Milk(),
                _ => null
            };
        }
    }
}
