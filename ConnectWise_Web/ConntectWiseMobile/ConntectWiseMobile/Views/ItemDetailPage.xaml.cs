using ConntectWiseMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ConntectWiseMobile.Views
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