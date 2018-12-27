using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;



public class UnicodeFontFactory : FontFactoryImp
{

    private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
        "arialuni.ttf");//arial unicode MS是完整的unicode字型。
    private static readonly string 标楷体Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
      "KAIU.TTF");//标楷体
    //private static readonly string 雅黑Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
    //  "微软雅黑.ttf");//雅黑   （本地）
    private static readonly string 雅黑Path = AppDomain.CurrentDomain.BaseDirectory + "fonts/微软雅黑.ttf";
    


    public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
        bool cached)
    {
        //可用Arial或标楷体，自己选一个
        //BaseFont baseFont = BaseFont.createFont("STSong-Light", "UniGB-UCS2-H",
        //    BaseFont.NOT_EMBEDDED);  
        BaseFont baseFont = BaseFont.CreateFont(雅黑Path, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        return new Font(baseFont, size, style, color);
    }


}
