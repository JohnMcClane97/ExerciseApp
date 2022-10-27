using MvvmHelpers;
using StartApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StartApp.Viewmodels
{
    public class MainViewModel : BaseViewModel
    {
        private List<TabataExercise> _exerciseList;
        public List<TabataExercise> ExerciseList 
        {
            get => _exerciseList; 
            set => SetProperty(ref _exerciseList, value); 
        }
        public MainViewModel()
        {
            ExerciseList = new List<TabataExercise> { new TabataExercise
            {
                Name = "krikpass 15/1",
                Sets = 8,
                Cycles = 6,
                RestBetweenSets = 30,

            }, new TabataExercise
            {
                Name = "krikpass 30/1",
                Sets = 4,
                Cycles = 6,
                RestBetweenSets = 45,
            }, new TabataExercise
            {
                Name = "krikpass 15/2",
                Sets = 10,
                Cycles = 6,
                RestBetweenSets = 60,
            } };
                
        }

        
    }
}
