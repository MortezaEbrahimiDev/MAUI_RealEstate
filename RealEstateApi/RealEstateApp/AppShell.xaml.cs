﻿using RealEstateApp.Pages;

namespace RealEstateApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage),typeof(RegisterPage));
        }
    }
}
