using StartApp.Models;
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

        private static void OnExercisePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var tec = (TabataExerciseControl)bindable;
            if(newValue is TabataExercise exercise)
            {
                tec.Name.Text =            string.Format("Name:              {0}",exercise.Name);
                tec.Sets.Text =            string.Format("Sets:              {0}", exercise.Sets.ToString());
                tec.Cycles.Text =          string.Format("Cycles:            {0}", exercise.Cycles.ToString());
                tec.RestBetweenSets.Text = string.Format("Rest Between Sets: {0}", exercise?.RestBetweenSets.ToString());
            }
        }

        public TabataExercise Exercise 
        { 
            get => (TabataExercise)GetValue(ExerciseProperty); 
            set => SetValue(ExerciseProperty, value); 
        }

        public TabataExerciseControl()
        {
            InitializeComponent();
        }
    }
}