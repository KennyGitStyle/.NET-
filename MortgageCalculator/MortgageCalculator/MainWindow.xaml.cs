using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MortgageCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static public double amountBarrowed { get; set; }
        static public double interestRate { get; set; }
        static public int mortgagePeriod { get; set; }

        static public List<string> currencyList;


        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                amountBarrowed = (double)Int32.Parse(txtMortgageAmount.Text);

                decimal result;

                if (decimal.TryParse(txtInterest.Text, out result))
                    interestRate = (double)result;

                mortgagePeriod = Int32.Parse(txtDuration.Text);

                txtMonthlyPayment.Text = CalcMortgage(amountBarrowed, interestRate, mortgagePeriod);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            
        }

        private string CalcMortgage(double amountBarrowed, double interestRate, int mortgagePeriod)
        {

            double p = amountBarrowed;
            double r = ConvertToMonthlyInterest(interestRate);
            double n = YearsToMonths(mortgagePeriod);
            var c = (decimal)(((r * p) * Math.Pow((1 + r), n)) / (Math.Pow((1 + r), n) - 1));

            return CurrencyType(c);
        }

        private string CurrencyType(decimal c)
        {
            
            if (cboCurrency.SelectedItem.Equals("£"))
                return ($"{Math.Round(c, MidpointRounding.AwayFromZero).ToString("C", CultureInfo.GetCultureInfo("en-GB"))}");

            else if (cboCurrency.SelectedItem.Equals("$"))
                return ($"{Math.Round(c, MidpointRounding.AwayFromZero).ToString("C", CultureInfo.GetCultureInfo("en-US"))}");

            else if (cboCurrency.SelectedItem.Equals("€"))
                return ($"{Math.Round(c, MidpointRounding.AwayFromZero).ToString("C", CultureInfo.GetCultureInfo("en-EURO"))}");

            else
            return ($"{Math.Round(c, MidpointRounding.AwayFromZero)}");
        }

        private int YearsToMonths(int years)
        {
            return (12 * years);
        }
        
        private double ConvertToMonthlyInterest(double percent) 
        {
            return  (percent / 12) / 100;
        }
        
        private void txtMortgageAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HasContent)
            {
               
            }
                
        }

        private void cboCurrency_Loaded(object sender, RoutedEventArgs e)
        {
            CurrencyValue(sender);
        }

        private static void CurrencyValue(object sender)
        {
            currencyList = new List<string>();

            currencyList.Add("£");
            currencyList.Add("$");
            currencyList.Add("€");

            var cbo = sender as ComboBox;
            cbo.ItemsSource = currencyList;
            cbo.SelectedItem = "£";
        }

        private void cboCurrency_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
