namespace ClassStudentManagement.Pages.LopHocPages;
using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;

public partial class SuaLopPage : ContentPage
{
	public SuaLopPage(LopHoc lop)
	{
		InitializeComponent();
        BindingContext = lop;
	}

    private void Savebtn_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(EntryTenLop.Text))
        {
            DisplayAlert("Thông báo", "Vui lòng điền tên mới của lớp", "OK");
            return;
        }

        Database.CapNhatLopHoc(new LopHoc
        {
            IDLop = TextIDLop.Text,
            TenLop = EntryTenLop.Text,
        });

        Navigation.PopAsync();
    }

    private void Cancelbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}