using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyStar
{
    public class AnimateCircle : Circle
    {
        public int width, height;       //그래픽 그리기를 위한 크기
        public double oriX, oriY, speedX, speedY, accelX, accelY;
        public AnimateCircle() : base()
        {
        }
        public AnimateCircle(int a, int b, int r, int w, int h) : base(a,b,r)       //전체 정의하는 선언.
        {
            width = w; height = h;
        }
    }

    public class Point
    {
        public int x, y;
        public Point()
        {
            x = 0; y = 0;
        }
        public Point(int a, int b)
        {
            x = a; y = b;
        }

        public double distance(Point C)
        {
            return Math.Sqrt(Math.Pow(C.x - x, 2) + Math.Pow(C.y - y, 2));
        }
    }

    public class Circle : Point
    {
        
        public int radius;
        public Circle():base()      //상속시 base는 자바의 super에 해당.
        {
            radius = 0;
        }
        public Circle(int a, int b, int r) : base(a, b)         //전체 정의하는 선언
        {
            radius = r;
        }

        public bool touch(Point C)      //점과 원 사이의 피탄 측정
        {
            return (distance(C) <= radius) ? true : false;
        }

        public bool touch(Circle C)     //원과 원 사이의 피탄 측정
        {
            return (distance(C) <= radius + C.radius) ? true : false;
        }
    }
}
