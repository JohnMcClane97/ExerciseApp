using StartApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StartApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutView : ContentPage
    {
        public WorkoutView()
        {
            InitializeComponent();
            BindingContext = new WorkoutViewModel();

            playImage.ReplaceStringMap.Clear();
            playImage.ReplaceStringMap.Add("Stroke", "Black");
            playImage.ReplaceStringMap.Add("fill", "Black");
            playImage.ReloadImage();

            pauseImage.ReplaceStringMap.Clear();
            pauseImage.ReplaceStringMap.Add("Stroke", "Black");
            pauseImage.ReplaceStringMap.Add("fill", "Black");
            pauseImage.ReloadImage();

            backImage.ReplaceStringMap.Clear();
            backImage.ReplaceStringMap.Add("Stroke", "Black");
            backImage.ReplaceStringMap.Add("fill", "Black");
            backImage.ReloadImage();

            forwardImage.ReplaceStringMap.Clear();
            forwardImage.ReplaceStringMap.Add("Stroke", "Black");
            forwardImage.ReplaceStringMap.Add("fill", "Black");
            forwardImage.ReloadImage();
        }
    }
}