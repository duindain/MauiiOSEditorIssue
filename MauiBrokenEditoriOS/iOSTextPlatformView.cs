using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using UIKit;

namespace MauiBrokenEditoriOS
{
    public class iOSTextPlatformView : Microsoft.Maui.Platform.MauiTextView
    {
        internal static readonly UIColor SeventyPercentGrey = new UIColor(0.7f, 0.7f, 0.7f, 1);

        internal static UIColor PlaceholderColor
        {
            get
            {
                if (OperatingSystem.IsIOSVersionAtLeast(13) || OperatingSystem.IsTvOSVersionAtLeast(13))
                    return UIColor.PlaceholderText;

                return SeventyPercentGrey;
            }
        }

        readonly UILabel _placeholderLabel;

        public iOSTextPlatformView()
        {
            _placeholderLabel = InitPlaceholderLabel();
            Changed += OnChanged;
        }

        public iOSTextPlatformView(CGRect frame)
            : base(frame)
        {
            _placeholderLabel = InitPlaceholderLabel();
            Changed += OnChanged;
        }

        // Native Changed doesn't fire when the Text Property is set in code
        // We use this event as a way to fire changes whenever the Text changes
        // via code or user interaction.
        public event EventHandler? TextSetOrChanged;

        public string? PlaceholderText
        {
            get => _placeholderLabel.Text;
            set
            {
                _placeholderLabel.Text = value;
                _placeholderLabel.SizeToFit();
            }
        }

        public NSAttributedString? AttributedPlaceholderText
        {
            get => _placeholderLabel.AttributedText;
            set
            {
                _placeholderLabel.AttributedText = value;
                _placeholderLabel.SizeToFit();
            }
        }

        public UIColor? PlaceholderTextColor
        {
            get => _placeholderLabel.TextColor;
            set => _placeholderLabel.TextColor = value;
        }

        public TextAlignment VerticalTextAlignment { get; set; }

        public override string? Text
        {
            get => base.Text;
            set
            {
                var old = base.Text;

                base.Text = value;

                if (old != value)
                {
                    HidePlaceholderIfTextIsPresent(value);
                    TextSetOrChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override NSAttributedString AttributedText
        {
            get => base.AttributedText;
            set
            {
                var old = base.AttributedText;

                base.AttributedText = value;

                if (old?.Value != value?.Value)
                {
                    HidePlaceholderIfTextIsPresent(value?.Value);
                    TextSetOrChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ShouldCenterVertically();
        }

        UILabel InitPlaceholderLabel()
        {
            var placeholderLabel = new Microsoft.Maui.Platform.MauiLabel
            {
                BackgroundColor = UIColor.Clear,
                TextColor = PlaceholderColor,
                Lines = 0
            };

            AddSubview(placeholderLabel);

            var edgeInsets = TextContainerInset;
            var lineFragmentPadding = TextContainer.LineFragmentPadding;

            placeholderLabel.TextInsets = new UIEdgeInsets(edgeInsets.Top, edgeInsets.Left + lineFragmentPadding,
                edgeInsets.Bottom, edgeInsets.Right + lineFragmentPadding);
            return placeholderLabel;
        }

        void HidePlaceholderIfTextIsPresent(string? value)
        {
            _placeholderLabel.Hidden = !string.IsNullOrEmpty(value);
        }

        void OnChanged(object? sender, EventArgs e)
        {
            HidePlaceholderIfTextIsPresent(Text);
            TextSetOrChanged?.Invoke(this, EventArgs.Empty);
        }

        void ShouldCenterVertically()
        {
            var fittingSize = new CGSize(Bounds.Width, NFloat.MaxValue);
            var sizeThatFits = SizeThatFits(fittingSize);
            var availableSpace = Bounds.Height - sizeThatFits.Height * ZoomScale;
            if (availableSpace <= 0)
                return;

            ContentOffset = VerticalTextAlignment switch
            {
                Microsoft.Maui.TextAlignment.Center => new CGPoint(0, -Math.Max(1, availableSpace / 2)),
                Microsoft.Maui.TextAlignment.End => new CGPoint(0, -Math.Max(1, availableSpace)),
                _ => new CGPoint(0, 0),
            };
        }
    }
}

