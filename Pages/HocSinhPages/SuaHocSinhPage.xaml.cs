using ClassStudentManagement.Mid;
using ClassStudentManagement.Models;

namespace ClassStudentManagement.Pages.HocSinhPages;

public partial class SuaHocSinhPage : ContentPage
{
    public string idhs;
    public string idlop;
	public SuaHocSinhPage(HocSinh hs)
	{
		InitializeComponent();
        idhs = hs.IDHocSinh;
        idlop = hs.IDLop;
        BindingContext = hs;
	}

    private void Savebtn_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrWhiteSpace(EntryNgSinh.Text) || 
            string.IsNullOrWhiteSpace(EntryTenHocSinh.Text))
        {
            DisplayAlert("Thông báo", "Điền đầy đủ thông tin mới của học sinh", "OK");
            return;
        }

        Database.ThemHocSinh(new HocSinh
        {
            IDHocSinh = idhs,
            TenHocSinh = EntryTenHocSinh.Text,
            NgSinh = EntryNgSinh.Text,
            IDLop = idlop
        });

        Navigation.PopAsync();
    }

    private void Cancelbtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}