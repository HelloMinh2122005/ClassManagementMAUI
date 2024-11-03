//using System.Windows.Input;
using System.Collections.ObjectModel;
using ClassStudentManagement.Data;
using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;
using ClassStudentManagement.Pages.LopHocPages;
using Microsoft.Maui.Controls;
//using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ClassStudentManagement
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<LopHoc> DSLopHoc { get; set; }
        public LopHoc LopDangChon { get; set; }
        private VisualElement previousSelectedElement;
        public MainPage()
        {
            InitializeComponent();
            DSLopHoc = new ObservableCollection<LopHoc>();
            LopDangChon = new LopHoc{
                IDLop = "XXX",
                TenLop = "XXX"
            };
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
            if (previousSelectedElement != null)
            {
                previousSelectedElement.BackgroundColor = Colors.White;
                previousSelectedElement = null;
            }
            Navigation.PushAsync(new ThemLopPage());
        }

        private void editBtn_Clicked(object sender, EventArgs e)
        {
            if (LopDangChon.IDLop == "XXX")
            {
                DisplayAlert("Thông báo", "Vui lòng chọn lớp để sửa", "Ok");
                return;
            }

            if (previousSelectedElement != null)
            {
                previousSelectedElement.BackgroundColor = Colors.White;
                previousSelectedElement = null;
            }

            Navigation.PushAsync(new SuaLopPage(LopDangChon));
        }

        private void deleteBtn_Clicked(object sender, EventArgs e)
        {
            if (LopDangChon.IDLop == "XXX")
            {
                DisplayAlert("Thông báo", "Vui lòng chọn lớp để xóa", "Ok");
                return;
            }
            Database.XoaLopHoc(LopDangChon);
            DSLopHoc.Remove(LopDangChon);
            LopDangChon.IDLop = "XXX";
            if (previousSelectedElement != null)
            {
                previousSelectedElement.BackgroundColor = Colors.White;
                previousSelectedElement = null;
            }
        }

        private void TapGestureRecognizer_Tapped_Single(object sender, TappedEventArgs e)
        {
            var currentElement = sender as VisualElement;

            if (currentElement == null) 
                return;

            if (previousSelectedElement != null)
            {
                previousSelectedElement.BackgroundColor = Colors.White;
            }
            currentElement.BackgroundColor = Colors.LightBlue;
            previousSelectedElement = currentElement;

            LopDangChon = (sender as VisualElement)?.BindingContext as LopHoc ?? new LopHoc
            {
                IDLop = "XXX",
                TenLop = "XXX"
            };
        }

        private void TapGestureRecognizer_Tapped_Double(object sender, TappedEventArgs e)
        {
            if (previousSelectedElement != null)
            {
                previousSelectedElement.BackgroundColor = Colors.White;
                previousSelectedElement = null;
            }

            var currentElement = (sender as VisualElement)?.BindingContext as LopHoc;
            Navigation.PushAsync(new DSHocSinhPage(currentElement.IDLop));
        }
    }
}