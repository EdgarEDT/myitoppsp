using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.TLPSP.DEVICE
{
    public class complex
    {
        //复数中的实部 
        private double complex_real;
        //复数中的虚部 
        private double complex_imagin;

        //构造函数 
        public complex(double r, double i)
        {
            complex_real = r;
            complex_imagin = i;
        }

        public double Real
        {
            get { return complex_real; }
            set { complex_real = value; }
        }
        public double Image
        {
            get { return complex_imagin; }
            set { complex_imagin = value; }
        }
        //重写ToString()方法 
        public override string ToString()
        {
            return this.complex_real + "+" + this.complex_imagin + "i";
        }

        //复数加法运算 
        public complex complex_add(complex c)
        {
            //取得加法运算后的实部 
            double complex_real = this.complex_real + c.complex_real;

            //取得加法运算后的虚部 
            double complex_imagin = this.complex_imagin + c.complex_imagin;

            //返回一个复数类 
            return new complex(complex_real, complex_imagin);
        }

        //复数减法运算 
        public complex complex_minus(complex c)
        {
            //取得减法运算后的实部 
            double complex_real = this.complex_real - c.complex_real;

            //取得减法运算后的虚部 
            double complex_imagin = this.complex_imagin - c.complex_imagin;

            //返回一个复数类 
            return new complex(complex_real, complex_imagin);
        }

        //乘法运算 
        public complex complex_multi(complex c)
        {
            //取得乘法运算后的实部 
            double complex_real = this.complex_real * c.complex_real - this.complex_imagin * c.complex_imagin;

            //取得乘法运算后的虚部 
            double complex_imagin = this.complex_real * c.complex_imagin + this.complex_imagin * c.complex_real;

            //返回一个复数类 
            return new complex(complex_real, complex_imagin);
        }

        //除法运算结果 (a+bi)/(c+di)=(a+bi)(c-di)/(c+di)(c-di) 
        public complex complex_divide(complex c)
        {
            //取得(c+di)(c-di)的值 
            double d = c.complex_real * c.complex_real + c.complex_imagin * c.complex_imagin;

            //取得除法运算后的实部 
            double complex_real = (this.complex_real * c.complex_real + this.complex_imagin * c.complex_imagin) / d;

            //取得除法运算后的虚部 
            double complex_imagin = (this.complex_real * (-c.complex_imagin) + this.complex_imagin * c.complex_real) / d;

            //返回一个复数类 
            return new complex(complex_real, complex_imagin);
        }
    }

}
