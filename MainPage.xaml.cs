//using System.Windows.Input;
using System.Collections.ObjectModel;
using ClassStudentManagement.Data;
using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;
//using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClassStudentManagement
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<LopHoc> DSLopHoc { get; set; }
        public LopHoc LopDangChon { get; set; }
        public MainPage()
        {
            InitializeComponent();
            DSLopHoc = new ObservableCollection<LopHoc>();
            LopDangChon = new LopHoc();
            BindingContext = this;
        }
        private async Task LoadData()
        {
            await Database.pickDatabasePathIfNotExist();
            DSLopHoc.Clear();
            LopDangChon = new LopHoc();
            foreach (var lop in await Database.GetLopHocListAsync())
            {
                DSLopHoc.Add(lop);
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }
        private void addBtn_Clicked(object sender, EventArgs e)
        {
            
        }

        private void editBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void deleteBtn_Clicked(object sender, EventArgs e)
        {
            if (LopDangChon.IDLop != "")
                Database.XoaLopHoc(LopDangChon);
        }

        private void TapGestureRecognizer_Tapped_Single(object sender, TappedEventArgs e)
        {
             var lop = (sender as Element)?.BindingContext as LopHoc;
            if (lop != null)
                LopDangChon = lop;  
        }

        private void TapGestureRecognizer_Tapped_Double(object sender, TappedEventArgs e)
        {

        }
    }
}
