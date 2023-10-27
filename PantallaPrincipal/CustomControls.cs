using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

public class CustomControls
{
    private Panel plLateral;
    private Button currentButton;

    public CustomControls(Panel lateralPanel)
    {
        plLateral = lateralPanel;
    }

    public void ActivateButton(object btnSender)
    {
        if (btnSender != null)
        {
            if (currentButton != (Button)btnSender)
            {
                DisableButton();
                Color color = Color.FromArgb(178, 8, 55);
                currentButton = (Button)btnSender;
                currentButton.BackColor = color;
                currentButton.ForeColor = Color.White;
                currentButton.Font = new Font("MADE INFINITY PERSONAL USE", 22F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }
    }

    public void DisableButton()
    {
        foreach (Control previousBtn in plLateral.Controls)
        {
            if (previousBtn.GetType() == typeof(Button))
            {
                previousBtn.BackColor = Color.Gainsboro;
                previousBtn.ForeColor = Color.Black;
                previousBtn.Font = new Font("MADE INFINITY PERSONAL USE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
        }
    }

    public class CustomRadioButton : RadioButton
    {
        private Color checkedColor = Color.FromArgb(178, 8, 55);
        private Color unCheckedColor = Color.Gray;

        public Color CheckedColor
        {
            get { return checkedColor; }
            set
            {
                checkedColor = value;
                Invalidate();
            }
        }
        public Color UnCheckedColor
        {
            get { return unCheckedColor; }
            set
            {
                unCheckedColor = value;
                Invalidate();
            }
        }

        public CustomRadioButton()
        {
            MinimumSize = new Size(0, 50);
            Padding = new Padding(10, 0, 0, 0);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics graphics = pevent.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float rbBorderSize = 18F;
            float rbCheckSize = 12F;
            RectangleF rectRbBorder = new RectangleF()
            {
                X = 0.5F,
                Y = (Height - rbBorderSize) / 2,
                Width = rbBorderSize,
                Height = rbBorderSize
            };
            RectangleF rectRbCheck = new RectangleF()
            {
                X = rectRbBorder.X + ((rectRbBorder.Width - rbCheckSize) / 2),
                Y = (Height - rbCheckSize) / 2,
                Width = rbCheckSize,
                Height = rbCheckSize
            };

            using (Pen penBorder = new Pen(checkedColor, 1.6F))
            using (SolidBrush brushRbCheck = new SolidBrush(checkedColor))
            using (SolidBrush brushText = new SolidBrush(ForeColor))
            {

                graphics.Clear(BackColor);

                if (Checked)
                {
                    graphics.DrawEllipse(penBorder, rectRbBorder);
                    graphics.FillEllipse(brushRbCheck, rectRbCheck);
                }
                else
                {
                    penBorder.Color = unCheckedColor;
                    graphics.DrawEllipse(penBorder, rectRbBorder);
                }

                graphics.DrawString(Text, Font, brushText,
                    rbBorderSize + 8, (Height - TextRenderer.MeasureText(Text, Font).Height) / 2);

            }
        }
    }
}
