using SkiaSharp;
using SkiaSharp.Views.Forms;
using StartApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StartApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseTimerControl : ContentView
    {

        private float radius = 450;
        private int? SecondsLeft;
        private int _millieSecondsPassed = 0;
        private static CancellationTokenSource _timerCanceletion;

        public static readonly BindableProperty OnTimerFinishedProperty =
            BindableProperty.Create("OnTimerFinished", typeof(Action), typeof(ExerciseTimerControl));

        public static readonly BindableProperty SecondsProperty =
            BindableProperty.Create("Seconds", typeof(int), typeof(ExerciseTimerControl), propertyChanged: OnSecondsPropertyChanged);

        public static readonly BindableProperty PausedProperty =
            BindableProperty.Create("OnTimerFinished", typeof(bool), typeof(ExerciseTimerControl), propertyChanged: OnPausedPropertyChanged);

       

        public int Seconds
        {
            get => (int)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
        }

        public Action? OnTimerFinished
        {
            get => (Action)GetValue(OnTimerFinishedProperty);
            set => SetValue(OnTimerFinishedProperty, value);
        }

        public bool Paused
        {
            get => (bool)GetValue(PausedProperty);
            set => SetValue(PausedProperty, value);
        }

        public ExerciseTimerControl()
        {
            InitializeComponent();
            _timerCanceletion = new CancellationTokenSource();
            
        }

        private static void OnPausedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ExerciseTimerControl)bindable;

            if(newValue is bool paused) 
            { 
                if (paused)
                    control.Stop();
                else if (control.SecondsLeft != null && control.SecondsLeft < control.Seconds)
                    StartTimer(control);
            }
                
        }

        private static void OnSecondsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ExerciseTimerControl control = (ExerciseTimerControl)bindable;
            
            control.SecondsLeft = (int)newValue;
            StartTimer(control);
        }

        public void Stop()
        {
            Interlocked.Exchange(ref _timerCanceletion, new CancellationTokenSource()).Cancel();
        }
        private static void StartTimer(ExerciseTimerControl control)
        {
            SKCanvasView view = control.TimerCanvas;
            view.PaintSurface += control.OnCanvasViewPaintSurface;
            CancellationTokenSource timerCancel = _timerCanceletion;
            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                if (timerCancel.IsCancellationRequested)
                    return false;

                if (control.SecondsLeft == 0)
                {
                    Vibration.Vibrate(600);

                    control.OnTimerFinished();
                    control._millieSecondsPassed = 0;
                    return false;
                }

                view.InvalidateSurface();
                control._millieSecondsPassed += 10;
                if (control._millieSecondsPassed % 1000 == 0)
                    control.SecondsLeft -= 1;
                
                return true;
            });
        }


        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPath path = new SKPath();
            SKPath pathBackground = new SKPath();

            canvas.Clear();

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.White.ToSKColor(),
                StrokeCap = SKStrokeCap.Round,
                StrokeWidth = 55

            };

            SKPaint paintBackground = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = Color.Gray.ToSKColor(),
                StrokeCap = SKStrokeCap.Round,
                StrokeWidth = 40

            };
            SKRect arc = new SKRect
            {
                Size = new SKSize(radius * 2, radius * 2),
                Location = new SKPoint((float)((info.Width - (radius * 2)) / 2), (float)((info.Height - (radius * 2)) / 2)),

            };

            var Textpaint = new SKPaint
            {
                TextSize = 600,
                IsAntialias = true,
                Color = new SKColor(0xFF, 0xFF, 0xFF),
                IsStroke = false,
                StrokeWidth = 20,
                TextAlign = SKTextAlign.Center,
            };
            float totalArcs = (360.0f / Seconds / 100.0f);
            float arcDegree = totalArcs * (_millieSecondsPassed / 10);

            canvas.DrawText(SecondsLeft.ToString(), info.Width / 2f, info.Height / 2f + (Textpaint.TextSize / 3), Textpaint);
            path.AddArc(arc, -90, arcDegree);
            pathBackground.AddArc(arc, -90, 360);

            canvas.DrawPath(pathBackground, paintBackground);
            canvas.DrawPath(path, paint);

        }
    }
}