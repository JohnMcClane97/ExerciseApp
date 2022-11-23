using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartApp.Viewmodels
{
    public class WorkoutViewModel : BaseViewModel
    {
        private int _restWorkTime;

        public int RestWorkTime
        {
            get => _restWorkTime; 
            set => SetProperty(ref _restWorkTime, value);
        }
        private bool _workoutIsPaused = false;

        public bool WorkoutIsPaused 
        { 
            get => _workoutIsPaused;  
            set => SetProperty(ref _workoutIsPaused, value);
        }

        private Action _onTimerFinished;
        public Action OnTimerFinished
        {
            get => _onTimerFinished;
            set => SetProperty(ref _onTimerFinished, value);
        }

        private Command _nextCommand;
        public Command NextCommand 
        { 
            get => _nextCommand; 
            set => SetProperty(ref _nextCommand, value); 
        }

        public Command PlayPauseCommand { get; set; }

        private Command _previousCommand;

        public Command PreviousCommand
        {
            get => _previousCommand; 
            set => SetProperty(ref _previousCommand, value); 
        }
        public WorkoutViewModel()
        {
            OnTimerFinished = () =>
            {
                RestWorkTime += 1;
            };
            RestWorkTime = 5;
            PlayPauseCommand = new Command(togglePause);
        }

        void togglePause()
        {
            WorkoutIsPaused = !WorkoutIsPaused;
        }

    }
}
