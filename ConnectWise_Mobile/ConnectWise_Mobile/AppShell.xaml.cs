using ConnectWise_Mobile.ViewModels;
using ConnectWise_Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ConnectWise_Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
