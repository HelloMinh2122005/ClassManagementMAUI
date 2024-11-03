using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;
using System.ComponentModel;

namespace ClassStudentManagement.Pages.HocSinhPages;

public partial class ThemHSPage : ContentPage
{
    public string idLop;
    public ThemHSPage(LopHoc lop)
	{
		InitializeComponent();
        idLop = lop.IDLop;
        BindingContext = lop;
	}

    private void Savebtn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryIDHocSinh.Text) || string.IsNullOrEmpty(EntryTenHocSinh.Text) || string.IsNullOrWhiteSpace(EntryNgSinh.Text))
        {
            DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin học sinh mới", "OK");
            return;
        }

        Database.ThemHocSinh(new HocSinh
        {
            IDHocSinh = EntryIDHocSinh.Text,
            TenHocSinh = EntryTenHocSinh.Text,
            NgSinh = EntryNgSinh.Text,
            IDLop = idLop
        });

        Navigation.PopAsync();
    }

    private void Cancelbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}