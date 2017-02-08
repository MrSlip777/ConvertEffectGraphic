/*
*参考URL：http://qiita.com/Toshi332/items/2749690489730f32e63d
*/

using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConvertEffectGraphic
{
    class ConvertGraphic
    {
        private readonly int const_original_size = 240;    //元のサイズ
        private readonly int const_convert_size = 192;     //変換後のサイズ
        private readonly string const_folder_name = "\\convert\\";
        private readonly double const_CalcCorrection = 0.1;

        //※画像に対して横幅のフレーム数
        private const int const_Width_framenumber = 5;     //幅フレーム数 

        public string ReadImage(string inputPath) {

            try
            {
                int width, height;
                //追記：元画像のフォーマット
                ImageFormat format;

                //元画像読み込み
                Bitmap img = new Bitmap(Image.FromFile(inputPath));
                //元画像のフォーマットを保持
                format = img.RawFormat;

                //画像サイズを取得
                width = img.Width;
                height = img.Height;

                //サイズ変換
                Bitmap resizeImg
                    = new Bitmap(img,
                    (int)((float)width * (float)const_convert_size / (float)const_original_size),
                    (int)((float)height * (float)const_convert_size / (float)const_original_size));

                //出力先画像を用意
                Bitmap convertImg
                    = new Bitmap(
                    const_convert_size * const_Width_framenumber,
                    (int)Math.Ceiling((double)resizeImg.Width
                    / (double)const_convert_size / (double)const_Width_framenumber)
                    * const_convert_size);

                //リサイズ画像から写し取る
                for (int x = 0; x < resizeImg.Width; x++)
                {

                    for (int y = 0; y < resizeImg.Height; y++)
                    {
                        int convert_x = x % (const_convert_size * const_Width_framenumber);
                        int convert_y
                            = y + ((int)Math.Ceiling((double)((double)x + const_CalcCorrection)
                            / (double)const_convert_size / (double)const_Width_framenumber)
                            - 1) * const_convert_size;

                        convertImg.SetPixel(convert_x, convert_y, resizeImg.GetPixel(x, y));
                    }
                }

                //出力先フォルダー生成
                string folder_Path = null;

                FileInfo fileInfo = new FileInfo(inputPath);
                folder_Path = fileInfo.DirectoryName + const_folder_name;

                if (!Directory.Exists(folder_Path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folder_Path);
                    directoryInfo.Create();
                }

                //ファイルパス
                string outputPath = folder_Path + fileInfo.Name;

                //同じファイルが出力先にない場合出力される
                if (!File.Exists(outputPath)) {
                    //元画像のフォーマットで保存する
                    convertImg.Save(outputPath, format);
                }

                //画像メモリを開放する
                img.Dispose();
                resizeImg.Dispose();
                convertImg.Dispose();

                return "success!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }


    }
}
