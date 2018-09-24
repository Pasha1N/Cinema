using Cinema.Presentation.Wpf.ViewModel;
using System;
using System.Windows;

namespace Cinema.Presentation.Wpf.View
{
    public partial class MakingFilm : Window
    {
        public MakingFilm(ViewModelMakingFilm viewModelMakingFilm)
        {
            InitializeComponent();

            DataContext = viewModelMakingFilm;
            viewModelMakingFilm.ClickingButtonCreateFilm += PressedButton_CreateFilm;
        }

        public void PressedButton_CreateFilm(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}