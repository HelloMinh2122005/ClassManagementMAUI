using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;

namespace ClassStudentManagement.Pages.LopHocPages;

public partial class ThemLopPage : ContentPage
{
	public ThemLopPage()
	{
		InitializeComponent();
	}

    private void Savebtn_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(EntryIDLop.Text) || string.IsNullOrEmpty(EntryTenLop.Text))
        {
            DisplayAlert("Thông báo", "Vui lòng điền đầy đủ thông tin lớp học mới", "OK");
            return; 
        }

        Database.ThemLopHoc(new LopHoc
        {
            IDLop = EntryIDLop.Text,
            TenLop = EntryTenLop.Text,
        });

        Navigation.PopAsync();
    }

    private void Cancelbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}