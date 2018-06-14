using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RiMonitoring//saRoj.Class
{
    public class FormFader
    {
        private double _i = 0.0;
        private int _MinOpacity = 0;
        private int _MaxOpacity = 1;

        public void FadeIn(Form TargetForm, double FadeStep)
        {
            if (TargetForm.Visible == false)
            {
                TargetForm.Opacity = 0;
                TargetForm.Visible = true;
            }

            for (_i = 0; _i <= _MaxOpacity; _i += FadeStep)
            {
                TargetForm.Opacity = _i;
                TargetForm.Refresh();
            }
        }

        public void FadeOut(Form TargetForm, double FadeStep)
        {
            if (TargetForm.Visible == false)
            {
                return;
            }

            for (_i = 1; _i >= _MinOpacity; _i -= FadeStep)
            {
                TargetForm.Opacity = _i;
                TargetForm.Refresh();
            }
        }
    }
}