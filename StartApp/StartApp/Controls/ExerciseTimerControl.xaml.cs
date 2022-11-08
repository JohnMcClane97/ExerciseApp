using SkiaSharp;
using SkiaSharp.Views.Forms;
using StartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StartApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseTimerControl : ContentView
    {

        public static readonly BindableProperty SecondsProperty =
            BindableProperty.Create("Seconds", typeof(int), typeof(ExerciseTimerControl), propertyChanged: OnSecondsPropertyChanged);

        private float radius = 450;
        private int SecondsLeft = 0;
        private static void OnSecondsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ExerciseTimerControl control = (ExerciseTimerControl)bindable;
            SKCanvasView view = control.TimerCanvas;
            view.PaintSurface += control.OnCanvasViewPaintSurface;
            control.SecondsLeft = (int)newValue;
            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                
                view.InvalidateSurface();

                control.SecondsLeft -= 1;

                if (control.SecondsLeft == 0)
                    return false;
                else
                    return true;
            });
        }

        public int Seconds
        {
            get => (int)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
        }
        public ExerciseTimerControl()
        {
            InitializeComponent();
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
                Size = new SKSize(radius*2, radius*2),
                Location = new SKPoint( (float)((info.Width-(radius*2))/2),(float)((info.Height - (radius * 2))/2)),

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

            canvas.DrawText(SecondsLeft.ToString(), info.Width / 2f, info.Height/2f+ (Textpaint.TextSize/4), Textpaint);
            path.AddArc(arc, -90,(360/Seconds)*(Seconds-SecondsLeft));
            pathBackground.AddArc(arc, -90, 360);

            canvas.DrawPath(pathBackground, paintBackground);
            canvas.DrawPath(path, paint);
            
        }
    }
}