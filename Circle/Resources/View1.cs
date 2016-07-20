using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace MyCircle.Resources
{
    public class View1 : View
    {
        private float distanceX;
        private float distanceY;
        private float Previous_X;
        private float Previous_Y;
        private List<Circle> circles;
        private Paint line = new Paint() { Color = Color.Black, AntiAlias = true, StrokeWidth=5 };
        private Paint backgroud = new Paint() { Color = Color.White };
        private Rect r;
        private Activity1 a1;

        public View1(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
            a1 = context as Activity1;
            this.circles = a1.circles;
            r = new Rect(0, 0, 1080,1920);
        }

        public View1(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            canvas.DrawRect(r, backgroud); //±³¾°ÖÃ°× 
            foreach (Circle circle in circles)
            {
                circle.Draw(canvas);
                foreach (Circle circle1 in circles)
                {
                    if (!(circle1.Current_X == circle.Current_X && circle1.Current_Y == circle.Current_Y))
                        canvas.DrawLine(circle.Current_X, circle.Current_Y, circle1.Current_X, circle1.Current_Y, line);
                }
            }
            base.OnDraw(canvas);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            int Action = (int)e.Action;
            switch (Action)
            {
                case (int)MotionEventActions.Down:
                    Previous_X = e.GetX();
                    Previous_Y = e.GetY();
                    foreach (Circle circle in circles)
                    {
                        if (circle.SetState(Previous_X, Previous_Y, MotionEventActions.Down))
                        {
                            return true;
                        }
                    }
                    break;
                case (int)MotionEventActions.Move:
                    distanceX= e.GetX() - Previous_X;
                    distanceY= e.GetY() - Previous_Y;
                    Previous_X = e.GetX();
                    Previous_Y = e.GetY();
                    foreach (Circle circle in circles)
                    {
                        if (circle.Move(distanceX, distanceY))
                        {
                            break;
                        }
                    }
                    Invalidate();
                    break;
                case (int)MotionEventActions.Up:
                    foreach (Circle circle in circles)
                    {
                        if (circle.SetState(Previous_X, Previous_Y, MotionEventActions.Up))
                        {
                            return true;
                        }
                    }
                    break;
                case (int)MotionEventActions.Pointer2Down:
                    foreach (Circle circle in circles)
                    {
                        if (circle.SetState(e.GetX(1), e.GetY(1), MotionEventActions.Pointer2Down))
                        {
                            circles.Remove(circle);
                            break;
                        }
                    }
                    Invalidate();
                    break;
                case (int)MotionEventActions.Pointer3Down:
                    {
                        circles.Add(new Circle() { Current_X = e.GetX(2), Current_Y = e.GetY(2) });
                        Invalidate();
                        break;
                    }
            }
            return true;
        }

    }
}