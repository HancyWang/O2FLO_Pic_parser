using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace o2flo_pic_parser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> m_list_bmp_files = new List<string>();
        //private List<List<byte>> m_new_data_list = new List<List<byte>>();

        public struct FILENAME_DATALIST
        {
            public string filename;
            public List<List<byte>> data_list;
        };


        private List<FILENAME_DATALIST> m_file_datalist_collections = new List<FILENAME_DATALIST>();

        public struct BmpInfo
        {
            public int bitDepth;    //位数深度
            public int width;       //宽
            public int height;      //高
            public int size;        //大小
        };
        
        //将00-FF分成16份，映射
        private byte map_2_gray_byte(byte val)
        {
            byte bt = 0;

            bt=Convert.ToByte(0x0F - Convert.ToInt32(val) / 16);
            
            return bt;
        }

        private void button_load_pic_folder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if(m_list_bmp_files!=null||m_file_datalist_collections!=null)
                {
                    m_list_bmp_files.Clear();
                    m_file_datalist_collections.Clear();
                }



                string folderPath = folderBrowserDialog1.SelectedPath;
                textBox_folder_path.Text = folderPath;
                string[] files = Directory.GetFiles(folderPath);
                foreach (string str in files)
                {
                    //挑选bmp图片
                    if (str.Substring(str.LastIndexOf('.') + 1).ToLower() == "bmp")
                    {
                        m_list_bmp_files.Add(str);
                        richTextBox1.AppendText(str + "\n");
                    }
                }
                richTextBox1.AppendText("一共发现" + m_list_bmp_files.Count.ToString() + "张图片\n");
            }
        }

        private void button_start_parse_pic_Click(object sender, EventArgs e)
        {
            richTextBox_parse.Clear();

            foreach(var bmpfile in m_list_bmp_files)
            {
                FILENAME_DATALIST filename_datalist = new FILENAME_DATALIST();
                filename_datalist.data_list = new List<List<byte>>();

                FileStream fs = new FileStream(bmpfile, FileMode.Open);
                BinaryReader br = new BinaryReader(fs, Encoding.ASCII);

                const int BMP_FILE_HEAD_CNT = 14;
                const int BMP_INFO = 40;

                byte[] bmp_file_header_buffer = new byte[BMP_FILE_HEAD_CNT];  //bmp文件头
                byte[] bmp_info_buffer = new byte[BMP_INFO];         //位图信息头

                int len = br.Read(bmp_file_header_buffer, 0, BMP_FILE_HEAD_CNT);

                string str_log = "";
                filename_datalist.filename = bmpfile.Substring(bmpfile.LastIndexOf('\\') + 1);
                richTextBox_parse.AppendText("- - - - - - - - - - - - - - - - - -\n");
                richTextBox_parse.AppendText("打开文件: " + filename_datalist.filename + "\n");
                if (bmp_file_header_buffer[0] != 0x42 && bmp_file_header_buffer[0] != 0x4D)
                {
                    str_log += filename_datalist.filename + "不是bmp文件";
                    richTextBox_parse.AppendText(str_log + "\n");

                    fs.Close();
                    br.Close();

                    richTextBox_parse.AppendText("关闭文件: " + filename_datalist.filename + "\n");
                    continue;
                }

                //位图文件头 (14字节)
                #region
                //文件大小 2-5
                int fSize = bmp_file_header_buffer[2] +
                    bmp_file_header_buffer[3]*256 +
                    bmp_file_header_buffer[4]*256*256 +
                    bmp_file_header_buffer[5]*256*256*256;
                str_log += "文件大小:" + fSize.ToString()+"字节\n";
                //6-9 保留
                //从文件开始到位图数据之间的偏移量 A-D
                int offSize = bmp_file_header_buffer[0x0A] +
                    bmp_file_header_buffer[0x0B] * 256 +
                    bmp_file_header_buffer[0x0C] * 256*256 +
                    bmp_file_header_buffer[0x0D] * 256*256*256;
                str_log += "从文件开始到位图数据之间的偏移量:" + offSize.ToString() + "字节\n";
                #endregion

                if (offSize != (14 + 40))
                {
                    str_log += filename_datalist.filename + "格式不对，从文件开始到位图数据之间的偏移量 不等于 54";
                    richTextBox_parse.AppendText(str_log + "\n");

                    fs.Close();
                    br.Close();

                    richTextBox_parse.AppendText("关闭文件: " + filename_datalist.filename + "\n");
                    continue;
                }

                //位图信息头 (40字节)
                #region
                BmpInfo bmpInfo;
                len = br.Read(bmp_info_buffer, 0, BMP_INFO);
                //位图宽度  12H-15H； 位图高度  16H-19H
                bmpInfo.width = bmp_info_buffer[0x12 - BMP_FILE_HEAD_CNT] +
                    bmp_info_buffer[0x13 - BMP_FILE_HEAD_CNT] * 256 +
                    bmp_info_buffer[0x14 - BMP_FILE_HEAD_CNT] * 256*256 +
                    bmp_info_buffer[0x15 - BMP_FILE_HEAD_CNT] * 256*256*256;
                bmpInfo.height = bmp_info_buffer[0x16 - BMP_FILE_HEAD_CNT] +
                    bmp_info_buffer[0x17 - BMP_FILE_HEAD_CNT] * 256 +
                    bmp_info_buffer[0x18 - BMP_FILE_HEAD_CNT] * 256 * 256 +
                    bmp_info_buffer[0x19 - BMP_FILE_HEAD_CNT] * 256 * 256 * 256;

                str_log += "位图尺寸: " + bmpInfo.width.ToString()+"*"+ bmpInfo.height.ToString()+ "\n";


                //bit depth 1C-1D
                bmpInfo.bitDepth = bmp_info_buffer[0x1C - BMP_FILE_HEAD_CNT] +
                    bmp_info_buffer[0x1D - BMP_FILE_HEAD_CNT] * 256;
                if(bmpInfo.bitDepth!=24)
                {
                    str_log += filename_datalist.filename + "格式不对，bitdepth不是24";
                    richTextBox_parse.AppendText(str_log + "\n");

                    fs.Close();
                    br.Close();

                    richTextBox_parse.AppendText("关闭文件: " + filename_datalist.filename + "\n");
                    continue;
                }
                str_log += "bit depth: " + bmpInfo.bitDepth.ToString() + "\n";

                //位图数据大小  22H-25H
                bmpInfo.size = bmp_info_buffer[0x22 - BMP_FILE_HEAD_CNT] +
                    bmp_info_buffer[0x23 - BMP_FILE_HEAD_CNT] * 256 +
                    bmp_info_buffer[0x24 - BMP_FILE_HEAD_CNT] * 256 +
                    bmp_info_buffer[0x25 - BMP_FILE_HEAD_CNT] * 256;
                str_log += "位图数据大小:" + bmpInfo.size.ToString() + "\n";
                #endregion

                //解析
                #region
                int DATA_SIZE = bmpInfo.width * bmpInfo.height * 3;
                byte[] bmp_data = new byte[DATA_SIZE];  //默认只解析RGB(bitdepth=24)
                br.Read(bmp_data, 0, DATA_SIZE);
                //注意，bmp图片中的最后一行才是第一行的数据(数据是倒着的)
                List<List<byte>>  raw_data_list = new List<List<byte>>();
                for(int i=0;i<bmpInfo.height;i++)
                {
                    List<byte> tmp_list = new List<byte>();
                    for (int j=0;j<bmpInfo.width*3;j+=3)  //因为是RGB，所以每次跳3格
                    {
                        int val = bmp_data[i * bmpInfo.width * 3 + j + 0] +
                            bmp_data[i * bmpInfo.width * 3 + j + 1] +
                            bmp_data[i * bmpInfo.width * 3 + j + 2];
                        byte bt = Convert.ToByte(val / 3);
                        tmp_list.Add(map_2_gray_byte(bt)); //翻译成灰度值，并加入链表
                    }
                    raw_data_list.Insert(0, tmp_list);  //每次都向头部插入，让数据到过来
                }

                //得到的数据是"从左至右，从上至下"
                //目标：将数据改成"从下至上，从左至右"
                bool b_need_add_empty_byte = false;
                if (bmpInfo.height % 2 != 0)           //检查数据是否需要补缺
                {
                    b_need_add_empty_byte = true;
                }

                if(b_need_add_empty_byte)
                {
                    bmpInfo.height += 1;

                    List<byte> tmp_list = new List<byte>();
                    for (int i = 0; i < bmpInfo.width ; i++)  //因为是RGB，所以每次跳3格
                    {
                        byte bt = 0;
                        tmp_list.Add(bt); //翻译成灰度值，并加入链表
                    }
                    raw_data_list.Insert(0, tmp_list);
                }

                
                for(int col=0;col<bmpInfo.width;col++)
                {
                    List<byte> tmp_list = new List<byte>();
                    for(int row=bmpInfo.height-1;row>=0;row-=2)
                    {
                        tmp_list.Add(Convert.ToByte(raw_data_list[row][col] * 16 + raw_data_list[row - 1][col]));
                    }
                    filename_datalist.data_list.Add(tmp_list);
                    //m_new_data_list.Add(tmp_list);
                }


                m_file_datalist_collections.Add(filename_datalist);

                #endregion

                richTextBox_parse.AppendText(str_log);

                fs.Close();
                br.Close();
 
                richTextBox_parse.AppendText("关闭文件: " + filename_datalist.filename + "\n\n");
            }
        }

        private void button_save_2_file_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                #region
                FileStream fs = new FileStream(this.saveFileDialog1.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                string fn = m_file_datalist_collections[0].filename;
                List<List<byte>> list = m_file_datalist_collections[0].data_list;


                foreach (var filename_datalist in m_file_datalist_collections)
                {
                    sw.Write("char " + filename_datalist.filename+"[" + filename_datalist.data_list[0].Count.ToString() + "*" +
                        filename_datalist.data_list.Count.ToString() + "]={\n");
                    //foreach (var row_data_array in m_new_data_list)
                    for (int i = 0; i < filename_datalist.data_list.Count; i++)
                    {
                        for (int j = 0; j < filename_datalist.data_list[0].Count; j++)
                        {
                            //最后一个不用加逗号
                            if ((i == filename_datalist.data_list.Count - 1) && (j == filename_datalist.data_list[0].Count - 1))
                            {
                                sw.Write("0x" + string.Format("{0:X2}", filename_datalist.data_list[i][j]));
                            }
                            else
                            {
                                sw.Write("0x" + string.Format("{0:X2}", filename_datalist.data_list[i][j]) + ",");

                            }
                        }
                        sw.Write("\n");
                    }
                    sw.Write("};\n");
                }

                

                sw.Close();
                fs.Close();
                #endregion
            }
        }
    }
}
