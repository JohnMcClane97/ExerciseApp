using StartApp.Models;
using StartApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StartApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabataExerciseControl : ContentView
    {
        public static readonly BindableProperty ExerciseProperty =
            BindableProperty.Create("Exercise", typeof(TabataExercise), typeof(TabataExerciseControl), propertyChanged: OnExercisePropertyChanged);

        public static readonly BindableProperty EditButtonPressedProperty =
            BindableProperty.Create("EditButtonPressedCommand", typeof(Command), typeof(TabataExerciseControl));

        public static readonly BindableProperty PlayButtonPressedProperty =
           BindableProperty.Create("PlayButtonPressedCommand", typeof(Command), typeof(TabataExerciseControl));

        private static void OnExercisePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var tec = (TabataExerciseControl)bindable;
            if(newValue is TabataExercise exercise)
            {
                tec.Name.Text = exercise.Name;
                tec.Sets.Text = string.Format("Sets: {0}", exercise.Sets.ToString());
                tec.Cycles.Text = string.Format("Cycles: {0}", exercise.Cycles.ToString());
                tec.RestBetweenSets.Text = string.Format("Rest Between Sets: {0}", exercise?.RestBetweenSets.ToString());
            }
            tec.playImage.ReplaceStringMap.Clear();
            tec.playImage.ReplaceStringMap.Add("Stroke", "Black");
            tec.playImage.ReplaceStringMap.Add("fill", "Black");
            tec.playImage.ReloadImage();

            tec.moreImage.ReplaceStringMap.Clear();
            tec.moreImage.ReplaceStringMap.Add("Stroke", "Black");
            tec.moreImage.ReplaceStringMap.Add("fill", "Black");
            
            tec.moreImage.ReloadImage();

            tec.EditImage.ReplaceStringMap.Clear();
            tec.EditImage.ReplaceStringMap.Add("Stroke", "Black");
            tec.EditImage.ReloadImage();

            tec.deleteImage.ReplaceStringMap.Clear();
            tec.deleteImage.ReplaceStringMap.Add("Stroke", "Black");

            tec.deleteImage.ReloadImage();
        }

        public TabataExercise Exercise 
        { 
            get => (TabataExercise)GetValue(ExerciseProperty); 
            set => SetValue(ExerciseProperty, value); 
        }

        public Command EditButtonPressedCommand 
        {
            get => (Command)GetValue(EditButtonPressedProperty);
            set => SetValue(EditButtonPressedProperty, value);
        }
        public Command PlayButtonPressedCommand 
        {
            get => (Command)GetValue(PlayButtonPressedProperty);
            set => SetValue(PlayButtonPressedProperty, value);
        }

        public TabataExerciseControl()
        {
            InitializeComponent();


            EditButtonPressedCommand = new Command( async () =>
            {
                More.IsVisible = !More.IsVisible;
                //if(!More.IsVisible)
                //{
                //    More.HeightRequest = 0;
                //    var heightAnimation = new Animation(x => { More.HeightRequest = x; });
                //    heightAnimation.Commit(More, "heightAnimation", 1000);
                //}
                //else
                //{
                //    var heightAnimation = new Animation(x => { More.HeightRequest = x; },100,0);
                //    heightAnimation.Commit(More, "heightAnimation", 1000, finished: (d, b) => 
                //    {
                //        More.IsVisible = false;
                //    });
                //}
                    
            });
            PlayButtonPressedCommand = new Command(async () =>
            {
                await Navigation.PushAsync(new WorkoutView());
            });
            
        }
       
            
        
    }
}