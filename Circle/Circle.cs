using Android.Graphics;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCircle
{
    
    public class Circle
    {
        public float Current_X;
        public float Current_Y;
        public float Previous_X;
        public float Previous_Y;
        private static Random rnd = new Random();
        private int diameter;
        enum State { In, Out }
        State state = State.Out;
        private Paint paint;
        Paint line;

        public Circle()
        {
            this.diameter = rnd.Next(60,150);
            Current_X = rnd.Next(50, 800);
            Current_Y = rnd.Next(50, 1000);
            paint = new Paint() { Color = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), AntiAlias = true };
            line = new Paint() { Color = Color.Black, StrokeWidth = 5, AntiAlias = true };
            line.SetStyle(Paint.Style.Stroke);
        }

        public void Draw(Android.Graphics.Canvas canvas)
        {
            canvas.DrawCircle(Current_X, Current_Y, diameter, line);
            canvas.DrawCircle(Current_X, Current_Y, diameter, paint);
        }

        public bool Move(float x, float y)
        {
            if (state == State.In)
            {
                Current_X += x;
                Current_Y += y;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetState(float x,float y,MotionEventActions mea)
        {
            if (Math.Sqrt((Current_X - x) * (Current_X - x) + (Current_Y - y) * (Current_Y - y)) < diameter)
            {
                if (MotionEventActions.Pointer2Down == mea)
                {
                    return true;
                }

                if (mea == MotionEventActions.Down)
                    state = State.In;
                else
                {
                    state = State.Out;
                }
                return true;
            }
            else
            {
                state = State.Out;
                return false;
            }
        }

    }
}
