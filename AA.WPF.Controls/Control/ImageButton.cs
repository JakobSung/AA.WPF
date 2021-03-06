﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AA.WPF.Controls
{
    [TemplatePart(Name = "PART_TXT", Type = typeof(TextBlock))]
    /// <summary>
    /// Image가 들어간 커스텀 버튼
    /// </summary>
    public class ImageButton : Button
    {
        #region constructor
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }
        #endregion

        #region Dependency property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ImageButton), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 기본이미지소스
        /// </summary>
        public static readonly DependencyProperty ImageSourceDefaultProperty = DependencyProperty.Register("ImageSourceDefault", typeof(ImageSource), typeof(ImageButton));
        /// <summary>
        /// 마우스오버 이미지소스
        /// </summary>
        public static readonly DependencyProperty ImageSourceOverProperty = DependencyProperty.Register("ImageSourceOver", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(new PropertyChangedCallback(OnChangedOver)));

        private static void OnChangedOver(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageButton control = d as ImageButton;
            var s = e.NewValue as ImageSource;
            Debug.WriteLine(s);
        }

        /// <summary>
        /// 비활성화 이미지소스
        /// </summary>
        public static readonly DependencyProperty ImageSourceDisabledProperty = DependencyProperty.Register("ImageSourceDisabled", typeof(ImageSource), typeof(ImageButton));
        /// <summary>
        /// pressed 이미지소스
        /// </summary>
        public static readonly DependencyProperty ImageSourcePressedProperty = DependencyProperty.Register("ImageSourcePressed", typeof(ImageSource), typeof(ImageButton));
        /// <summary>
        /// 마우스 캡쳐 릴리즈 command
        /// </summary>
        public static readonly DependencyProperty MouseCaptureReleaseCommandProperty = DependencyProperty.Register("MouseCaptureReleaseCommand", typeof(ICommand), typeof(ImageButton));
        /// <summary>
        /// 마우스 캡쳐 command
        /// </summary>
        public static readonly DependencyProperty MouseCaptureComamndProperty = DependencyProperty.Register("MouseCaptureComamnd", typeof(ICommand), typeof(ImageButton));

        public static readonly DependencyProperty IsMouseCapturingProperty = DependencyProperty.Register("IsMouseCapturing", typeof(bool), typeof(ImageButton));

        public static readonly DependencyProperty TextLocationProperty = DependencyProperty.Register("TextLocation", typeof(ImageButtonTextLocation), typeof(ImageButton), new PropertyMetadata(ImageButtonTextLocation.Right));

        public static readonly DependencyProperty ForegroundDisableProperty = DependencyProperty.Register("ForegroundDisable", typeof(Brush), typeof(ImageButton), new PropertyMetadata((SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3d434d"))));

        public static readonly DependencyProperty ForegroundMouseOverProperty = DependencyProperty.Register("ForegroundMouseOver", typeof(Brush), typeof(ImageButton), new PropertyMetadata((SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"))));

        public static readonly DependencyProperty ForegroundPressedProperty = DependencyProperty.Register("ForegroundPressed", typeof(Brush), typeof(ImageButton), new PropertyMetadata((SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFFFF"))));

        public static readonly DependencyProperty TextMarginProperty = DependencyProperty.Register("TextMargin", typeof(double), typeof(ImageButton), new PropertyMetadata((double)6));

        #endregion

        #region private func

        #endregion

        #region property

        public double TextMargin
        {
            get { return (double)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        public Brush ForegroundDisable
        {
            get { return GetValue(ForegroundDisableProperty) as Brush; }
            set { SetValue(ForegroundDisableProperty, value); }
        }

        public Brush ForegroundMouseOver
        {
            get { return GetValue(ForegroundMouseOverProperty) as Brush; }
            set { SetValue(ForegroundMouseOverProperty, value); }
        }

        public Brush ForegroundPressed
        {
            get { return GetValue(ForegroundPressedProperty) as Brush; }
            set { SetValue(ForegroundPressedProperty, value); }
        }

        public ImageButtonTextLocation TextLocation
        {
            get { return (ImageButtonTextLocation)GetValue(TextLocationProperty); }
            set { SetValue(TextLocationProperty, value); }
        }

        public string Text
        {
            get { return GetValue(TextProperty) as string; }
            set { SetValue(TextProperty, value); }
        }
        public ImageSource ImageSourceDefault
        {
            get { return GetValue(ImageSourceDefaultProperty) as ImageSource; }
            set { SetValue(ImageSourceDefaultProperty, value); }
        }

        public ImageSource ImageSourceOver
        {
            get { return GetValue(ImageSourceOverProperty) as ImageSource; }
            set { SetValue(ImageSourceOverProperty, value); }
        }
        public ImageSource ImageSourceDisabled
        {
            get { return GetValue(ImageSourceDisabledProperty) as ImageSource; }
            set { SetValue(ImageSourceDisabledProperty, value); }
        }
        public ImageSource ImageSourcePressed
        {
            get { return GetValue(ImageSourcePressedProperty) as ImageSource; }
            set { SetValue(ImageSourcePressedProperty, value); }
        }
        public ICommand MouseCaptureReleaseCommand
        {
            get { return GetValue(MouseCaptureReleaseCommandProperty) as ICommand; }
            set { SetValue(MouseCaptureReleaseCommandProperty, value); }
        }
        public ICommand MouseCaptureComamnd
        {
            get { return GetValue(MouseCaptureComamndProperty) as ICommand; }
            set { SetValue(MouseCaptureComamndProperty, value); }

        }

        public bool IsMouseCapturing
        {
            get { return (bool)GetValue(IsMouseCapturingProperty); }
            set
            {
                SetValue(IsMouseCapturingProperty, value);
            }
        }

        #endregion

        #region override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (ImageSourceDefault != null)
            {
                if (ImageSourceOver == null)
                {
                    ImageSourceOver = ImageSourceDefault;
                }
                if (ImageSourceDisabled == null)
                {
                    ImageSourceDisabled = ImageSourceDefault;
                }
                if (ImageSourcePressed == null)
                {
                    ImageSourcePressed = ImageSourceDefault;
                }
            }


            var txt = GetTemplateChild("PART_TXT") as TextBlock;
            if (txt != null)
            {
                txt.Margin = new Thickness(TextMargin);
            }
            else
            {
               
            }
            
        }

        protected override void OnGotMouseCapture(MouseEventArgs e)
        {
            base.OnGotMouseCapture(e);
            IsMouseCapturing = true;

            if (MouseCaptureComamnd != null)
            {
                MouseCaptureComamnd.Execute(null);
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (IsMouseCapturing)
            {
                IsMouseCapturing = false;
                //마우스 캡처 끝나면 커맨더 발생시킴.
                if (MouseCaptureReleaseCommand != null)
                {
                    MouseCaptureReleaseCommand.Execute(null);
                }
            }
        }
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            
        }
        #endregion

    }

    public enum ImageButtonTextLocation
    {
        Right,
        Left,
        Bottom,
        Top
    }

}
