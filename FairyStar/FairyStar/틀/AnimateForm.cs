using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyStar
{
    public class AnimateCircle : Circle     //디버그에만 그대로 쓰고, 실제 구현시엔 유닛이나 탄에 상속시켜야함
    {
        public int width, height;       //그래픽 그리기를 위한 크기
        public double speedX, speedY, accelX, accelY;
        public AnimateCircle() : base()
        {
        }
        public AnimateCircle(float a, float b, float r, int w, int h) : base(a,b,r)       //전체 정의하는 선언.
        {
            width = w; height = h;
        }

        public virtual void paintDo(Graphics g, Brush b)
        {
            Res.FillEllipse(g, b, this);
        }
    }

    public class Point
    {
        public float x, y;
        public Point()
        {
            x = 0; y = 0;
        }
        public Point(float a, float b)
        {
            x = a; y = b;
        }

        public virtual float distance(Point C)
        {
            return (float)Math.Sqrt((C.x-x)*(C.x-x) + (C.y-y)*(C.y-y));
        }
    }

    public class Circle : Point
    {
        
        public float radius;
        public Circle():base()      //상속시 base는 자바의 super에 해당.
        {
            radius = 0;
        }
        public Circle(float a, float b, float r) : base(a, b)         //전체 정의하는 선언
        {
            radius = r;
        }

        public bool touch(Point C)      //점과 원 사이의 피탄 측정
        {
            return (distance(C) <= radius) ? true : false;
        }

        public bool touch(Circle C)     //원과 원 사이의 피탄 측정        (주로 사용)
        {
            return (distance(C) <= radius + C.radius) ? true : false;
        }
    }

    public class Laser : Point
    {
        public float length, width, direct;
        public double radian;        //direct는 정 동쪽이 0도, 시계방향 증가. deg값. 그래픽 관련, 우하방향 +x, +y 축에 대해 직선방정식 그대로 사용 가능

        //아래는 피탄 관련
        public double gradient, n;     // y= mx + n 에 쓸 값. m은 기울기 gradient

        public Point end = new Point();

        public Laser() : base()
        {
            length = 0; width = 0; direct = 0;
        }
        public Laser(float x, float y, float l, float w, float d) : base(x, y)
        {
            length = l; width = w; direct = d;
            rotate(0);
        }

        public bool touch(Point C)  //레이저와 점의 피탄 여부
        {
            return false;
        }

        public bool touch(Circle C) //레이저와 원의 피탄 여부         (주로 사용)
        {
            if (distance(C) <= C.radius + width && (PointInLaser(C) || base.distance(C) <= C.radius + width || end.distance(C) <= C.radius + width))
                return true;
            return false;
        }

        public override float distance(Point C)
        {
            return (float)(Math.Abs(-gradient * C.x + C.y - n) / Math.Sqrt(gradient * gradient + 1));
        }

        public bool PointInLaser(Point C)       //내적이다..
        {
            return (x - C.x) * (x - end.x) + (y - C.y) * (y - end.y) > 0 && (end.x - C.x) * (end.x - x) + (end.y - C.y) * (end.y - y) > 0;
        }

        public void rotate(float d)
        {
            rotateSet(direct + d);
        }

        public void rotateSet(float d)
        {
            direct = d;
            radian = direct * Math.PI / 180d;
            gradient = Math.Tan(radian);
            n = y - gradient * x;
            end.x = x + (float)(length * Math.Cos(radian));
            end.y = y + (float)(length * Math.Sin(radian));
            sort();
        }

        public Matrix m;
        public void sort()
        {
            Res.LaserGraphicSort(this);
        }
        public void paintDo(Graphics g, Brush b)
        {
            Res.DrawLaser(g, b, this);
        }
    }
}
