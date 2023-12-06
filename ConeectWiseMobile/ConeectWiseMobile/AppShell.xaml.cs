using ConeectWiseMobile.ViewModels;
using ConeectWiseMobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ConeectWiseMobile
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
