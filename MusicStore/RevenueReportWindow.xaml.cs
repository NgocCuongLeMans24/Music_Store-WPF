using MusicStore.BLL.Services;
using System;
using System.Windows;

namespace MusicStore
{
    public partial class RevenueReportWindow : Window
    {
        private readonly RevenueService _revenueService;

        public RevenueReportWindow()
        {
            InitializeComponent();
            _revenueService = new RevenueService(); 
            LoadRevenueReport();
        }

        private void LoadRevenueReport()
        {
            var report = _revenueService.GetRevenueReport();

            TotalRevenueTextBlock.Text = $"${report.totalRevenue:F2}"; 
            TotalQuantitySoldTextBlock.Text = $"{report.totalQuantitySold} items sold";
        }

        private void RefreshReportButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRevenueReport();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMainWindow a = new();
            a.Show();
            this.Close();
        }
    }
}
