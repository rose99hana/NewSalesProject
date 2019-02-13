using NewSalesProject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewSalesProject.Controls
{
    /// <summary>
    /// Interaction logic for MySpinner.xaml
    /// </summary>
    public partial class MySpinner : UserControl
    {
        public MySpinner()
        {
            InitializeComponent();
        }

        public double ScaleRadius
        {
            get { return (double)GetValue(ScaleRadiusProperty); }
            set { SetValue(ScaleRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ScaleRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleRadiusProperty =
            DependencyProperty.Register("ScaleRadius", typeof(double), typeof(MySpinner), new PropertyMetadata(1d, OnScaleRadiusChanged));

        private static void OnScaleRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var spin = d as MySpinner;
            (d as MySpinner).SpinnerGrid.RenderTransform =
                new ScaleTransform((double)e.NewValue, (double)e.NewValue, 15, 15);
        }




        public SpinnerState SpinnerState
        {
            get { return (SpinnerState)GetValue(SpinnerStateProperty); }
            set { SetValue(SpinnerStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpinnerState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerStateProperty =
            DependencyProperty.Register("SpinnerState", typeof(SpinnerState), typeof(MySpinner), new FrameworkPropertyMetadata(SpinnerState.Loading, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public string SpinnerBackground
        {
            get { return (string)GetValue(SpinnerBackgroundProperty); }
            set { SetValue(SpinnerBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpinnerBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpinnerBackgroundProperty =
            DependencyProperty.Register("SpinnerBackground", typeof(string), typeof(MySpinner), new PropertyMetadata("Transparent"));



        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(MySpinner), new PropertyMetadata(1d));




        public string Fill
        {
            get { return (string)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(string), typeof(MySpinner), new PropertyMetadata("#FFF4F4F5"));





        public ViewModeType StateMode
        {
            get { return (ViewModeType)GetValue(StateModeProperty); }
            set { SetValue(StateModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StateMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateModeProperty =
            DependencyProperty.Register("StateMode", typeof(ViewModeType), typeof(MySpinner), new FrameworkPropertyMetadata(ViewModeType.Default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    }
}
