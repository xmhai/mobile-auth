using IdentityPass.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace IdentityPass.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}