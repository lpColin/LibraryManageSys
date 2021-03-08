using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Common
{
    /// <summary>
    /// create Security verification
    /// </summary>
    /// create time：2014.8.22
   public class Security
    {
        /// <summary>
        /// create verification chars
        /// </summary>
        /// <param name="length">chars length</param>
        /// <return> verification chars</return>
       public static string createVerificationText(int length){
           char[] _varification = new char[length];
           char[] _dictionary = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','S','Y','Z',
                                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','s','y','z'};
           Random _random = new Random();
           for (int i = 0; i < length; i++) {
               _varification[i] = _dictionary[_random.Next(_dictionary.Length-1)]; 
           }
               return new string(_varification);       
       }

        /// <summary>
        /// create verification image
        /// </summary>
        /// <param name="verificationText">verification chars</param>
        /// <param name="width">image width</param>
        /// <param name="height">image height</param>
        /// <return>image</return>
       public static Bitmap CreateVerificationImage(string verificationText, int width, int height) {
           Pen _pen = new Pen(Color.Black);
           Font _font = new Font("Arial", 14,FontStyle.Bold);
           Brush _brush = null;
           Bitmap _bitmap = new Bitmap(width, height);
           Graphics _g = Graphics.FromImage(_bitmap);
           SizeF _totalSizeF = _g.MeasureString(verificationText,_font);
           SizeF _curCharSizeF;
           PointF _startPonitF = new PointF((width-_totalSizeF.Width)/2,(height-_totalSizeF.Height)/2);
           Random _random = new Random();
           _g.Clear(Color.White);
           for (int i = 0; i < verificationText.Length; i++) { 
                _brush = new LinearGradientBrush(new Point(0,0),new Point(1,1),Color.FromArgb(_random.Next(255),_random.Next(255),_random.Next(255)),
                    Color.FromArgb(_random.Next(255),_random.Next(255),_random.Next(255)));
                _g.DrawString(verificationText[i].ToString(),_font,_brush,_startPonitF);
                _curCharSizeF = _g.MeasureString(verificationText[i].ToString(), _font);
                _startPonitF.X += _curCharSizeF.Width;
           }
           _g.Dispose();
               return _bitmap;
       }
        
    }
}
