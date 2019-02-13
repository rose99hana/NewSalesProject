using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NewSalesProject.Enum;
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
    /// Interaction logic for CRUDStateCard.xaml
    /// </summary>
    public partial class CRUDStateCard : UserControl
    {
        public CRUDStateCard()
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
            DependencyProperty.Register("ScaleRadius", typeof(double), typeof(CRUDStateCard), new PropertyMetadata(1d, OnScaleRadiusChanged));

        private static void OnScaleRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var card = d as CRUDStateCard;
            card.RenderTransform =
                new ScaleTransform((double)e.NewValue, (double)e.NewValue, 0, 0);
        }


        public CRUDCardState CardState
        {
            get { return (CRUDCardState)GetValue(CardStateProperty); }
            set { SetValue(CardStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CardState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CardStateProperty =
            DependencyProperty.Register("CardState", typeof(CRUDCardState), typeof(CRUDStateCard), new FrameworkPropertyMetadata(CRUDCardState.Default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public CRUDType CRUDType
        {
            get { return (CRUDType)GetValue(CRUDTypeProperty); }
            set { SetValue(CRUDTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CRUDType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CRUDTypeProperty =
            DependencyProperty.Register("CRUDType", typeof(CRUDType), typeof(CRUDStateCard), new FrameworkPropertyMetadata(CRUDType.Saving, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




    }
}
