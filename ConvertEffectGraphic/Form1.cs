/*
*参考URL：http://dobon.net/vb/dotnet/file/getfiles.html
*/
using System;
using System.Windows.Forms;
using System.IO;

namespace ConvertEffectGraphic
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 各種ボタン処理
        /// </summary>
        /// 

        private readonly string sErrorFolderPath = "入力フォルダパスが正しくありません。";
        private readonly string sSuccess = "success!";

        //インスタンス定義
        static private ConvertGraphic mConvertGraphic = new ConvertGraphic();

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button_open_input_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //画像変換の実施
        private void button_Implement_Click(object sender, EventArgs e)
        {
            string sOutputMessage = null;

            if (textBox1.Text == "")
            {
                sOutputMessage = sErrorFolderPath;
            }

            if (sOutputMessage != sErrorFolderPath) {

                DirectoryInfo directoryInfo = new DirectoryInfo(textBox1.Text);
                FileInfo[] files
                    = directoryInfo.GetFiles("*", SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in files)
                {
                    sOutputMessage = mConvertGraphic.ReadImage(file.FullName);

                    //エラーが発生したら中断する
                    if (sOutputMessage != sSuccess)
                    {
                        break;
                    }
                }
            }

            //出力結果をダイアログ表示
            if (sOutputMessage == sSuccess) {

                MessageBox.Show(
                    "変換完了",
                    "出力結果",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                   sOutputMessage,
                   "出力結果",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
            }
        }

    }
}
