using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartApp.Viewmodels
{
    public class WorkoutViewModel : BaseViewModel
    {

        private Action _onTimerFinished;
        public Action OnTimerFinished
        {
            get => _onTimerFinished;
            set => SetProperty(ref _onTimerFinished, value);
        }
        public WorkoutViewModel()
        {
            OnTimerFinished = () => 
            { 
                
            };
        }

        
    }
}
