using System;
using System.Collections.Generic;
using System.Drawing;
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
        public AnimateCircle(double a, double b, double r, int w, int h) : base(a,b,r)       //전체 정의하는 선언.
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
        public double x, y;
        public Point()
        {
            x = 0; y = 0;
        }
        public Point(double a, double b)
        {
            x = a; y = b;
        }

        public double distance(Point C)
        {
            return Math.Sqrt((C.x-x)*(C.x-x) + (C.y-y)*(C.y-y));
        }
    }

    public class Circle : Point
    {
        
        public double radius;
        public Circle():base()      //상속시 base는 자바의 super에 해당.
        {
            radius = 0;
        }
        public Circle(double a, double b, double r) : base(a, b)         //전체 정의하는 선언
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
        public double length, width, direct;        //direct는 정 동쪽이 0도, 반시계방향 증가. deg값. 그래픽 관련.

        //아래는 피탄 관련
        public double gradient, n;     // y= mx + n 에 쓸 값. m은 기울기 gradient
        public double pos;      //두께에 따라서, 레이저 영역이 y=mx+n+pos ~ y=mx+n-pos 영역이 될때, direct 바뀔때마다 계산되는 값.
        public double pos2, n2;     //레이저 길이방향의 범위 - y=(1/m)x+n2 ~ y=(1/m)x+n2+pos2
        public Laser() : base()
        {
            length = 0; width = 0; direct = 0;
        }
        public Laser(double x, double y, double l, double w, double d) : base(x, y)
        {
            length = l; width = w; direct = d;
        }

        public bool touch(Point C)  //레이저와 점의 피탄 여부
        {
            return false;
        }

        public bool touch(Circle C) //레이저와 원의 피탄 여부         (주로 사용)
        {
            return false;
        }

        public void rotate(double d)
        {
            rotateSet(direct + d);
        }

        public void rotateSet(double d)
        {
            direct = d;
            gradient = Math.Tan(direct * Math.PI / 180d);
            n = y - gradient * x;
            n2 = y - (1 / gradient) * x;
            sort();
        }
        
        public void sort()
        {
            pos = Math.Abs(width * Math.Sqrt(1 + gradient * gradient));
            pos2 = length * Math.Sqrt(1 + (1d / gradient / gradient));
        }
    }
}
