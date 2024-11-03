using System.Collections.ObjectModel;
using ClassStudentManagement.Models;
using ClassStudentManagement.Mid;
using ClassStudentManagement.Pages.HocSinhPages;

namespace ClassStudentManagement.Pages.LopHocPages;

public partial class DSHocSinhPage : ContentPage
{
    public ObservableCollection<HocSinh> HocSinhCollection { get; set; }
    public HocSinh HSDangChon { get; set; }
    private VisualElement previousSelectedElement;
    public string idLop;
    public DSHocSinhPage(string id)
	{
		InitializeComponent();
        HocSinhCollection = new ObservableCollection<HocSinh>();
        HSDangChon = new HocSinh
        {
            IDHocSinh = "XXX",
            TenHocSinh = "XXX",
            NgSinh = ""
        };
        idLop = id;
        BindingContext = this;
    }

    private async Task LoadData()
    {
        HocSinhCollection.Clear();
        foreach (var hs in await Database.GetHocSinhListAsync(idLop))
        {
            HocSinhCollection.Add(hs);
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
        Navigation.PushAsync(new ThemHSPage(new LopHoc
        {
            IDLop = idLop,
            TenLop = ""
        }));
    }

    private void editBtn_Clicked(object sender, EventArgs e)
    {
        if (HSDangChon.IDHocSinh == "XXX")
        {
            DisplayAlert("Thông báo", "Vui lòng chọn học sinh để sửa", "Ok");
            return;
        }

        Navigation.PushAsync(new SuaHocSinhPage(HSDangChon));

        if (previousSelectedElement != null)
        {
            previousSelectedElement.BackgroundColor = Colors.White;
            previousSelectedElement = null;
        }
    }

    private void deleteBtn_Clicked(object sender, EventArgs e)
    {
        if (HSDangChon.IDHocSinh == "XXX")
        {
            DisplayAlert("Thông báo", "Vui lòng chọn học sinh để xóa", "Ok");
            return;
        }
        Database.XoaHocSinh(HSDangChon);
        HocSinhCollection.Remove(HSDangChon);
        HSDangChon.IDHocSinh = "XXX";
        if (previousSelectedElement != null)
        {
            previousSelectedElement.BackgroundColor = Colors.White;
            previousSelectedElement = null;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
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

        HSDangChon = (sender as VisualElement)?.BindingContext as HocSinh ?? new HocSinh
        {
            IDHocSinh = "XXX",
            TenHocSinh = "XXX",
            NgSinh = "XXX"
        };
    }
}