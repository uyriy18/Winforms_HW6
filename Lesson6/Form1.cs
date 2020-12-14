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

namespace Lesson6
{
    public partial class Form1 : Form
    {
        ImageList list;
        ToolBar tBar;
        string copy = "";
        string docName = "";
        SaveFileDialog sf = null;

        public Form1()
        {
            InitializeComponent();

        }


        void tBar_Button_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            

            switch (e.Button.ImageIndex)
            {
                case 0:
                    btn_Open_Click();
                    break;

                case 1:
                    btn_SaveAs_Click();
                    break;
                case 2:
                    btn_Exit_Click();
                    break;

                case 3:
                    btn_NewFile_Click();
                    break;

                case 4:
                    btn_Copy_Click();
                    break;

                    case 5:
                    btn_Paste_Click();
                    break;

                case 6:
                    btn_Ubdo_Click();          
                    break;

                case 7:
                    btn_Redo_Click();
                    break;

                case 8:
                    btn_FontColor_Click();            
                    break;

                case 9:
                    btn_Backcolor_Click();
                    break;

                case 10:
                    btn_FontFormat_Click();
                    break;

                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = new ImageList();
            list.ImageSize = new Size(15, 15);
            list.Images.Add(new Bitmap("open.png"));
            list.Images.Add(new Bitmap("save.png"));
            list.Images.Add(new Bitmap("exit.png"));
            list.Images.Add(new Bitmap("newfile.png"));
            list.Images.Add(new Bitmap("copy.png"));
            list.Images.Add(new Bitmap("paste.png"));
            list.Images.Add(new Bitmap("undo.png"));
            list.Images.Add(new Bitmap("redo.png"));
            list.Images.Add(new Bitmap("font_color.png"));
            list.Images.Add(new Bitmap("backcolor.png"));
            list.Images.Add(new Bitmap("font_format.png"));
            tBar = new ToolBar();
            tBar.ImageList = list;
            ToolBarButton tb_Open = new ToolBarButton("Open");
            ToolBarButton tb_SaveAs = new ToolBarButton("Save As");
            ToolBarButton tb_Exit = new ToolBarButton("Exit");
            ToolBarButton tb_NewFile = new ToolBarButton("New File");
            ToolBarButton tb_Copy = new ToolBarButton("Copy");
            ToolBarButton tb_Paste = new ToolBarButton("Paste");
            ToolBarButton tb_Undo = new ToolBarButton("Undo");
            ToolBarButton tb_Redo = new ToolBarButton("Redo");
            ToolBarButton tb_Color = new ToolBarButton("Text Color");
            ToolBarButton tb_BackColor = new ToolBarButton("Back Color");
            ToolBarButton tb_Font = new ToolBarButton("Font Format");
            ToolBarButton tb_Separator = new ToolBarButton();
            tb_Separator.Style = ToolBarButtonStyle.Separator;
            tb_Open.ImageIndex = 0;
            tb_SaveAs.ImageIndex = 1;
            tb_Exit.ImageIndex = 2;
            tb_NewFile.ImageIndex = 3;
            tb_Copy.ImageIndex = 4;
            tb_Paste.ImageIndex = 5;
            tb_Undo.ImageIndex = 6;
            tb_Redo.ImageIndex = 7;
            tb_Color.ImageIndex = 8;
            tb_BackColor.ImageIndex = 9;
            tb_Font.ImageIndex = 10;
            tBar.Buttons.Add(tb_Open);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_SaveAs);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Exit);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_NewFile);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Copy);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Paste);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Undo);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Redo);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Color);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_BackColor);
            tBar.Buttons.Add(tb_Separator);
            tBar.Buttons.Add(tb_Font);


            tBar.Appearance = ToolBarAppearance.Flat;
            tBar.BorderStyle = BorderStyle.Fixed3D;
            tBar.ButtonClick += new ToolBarButtonClickEventHandler(tBar_Button_Click);
            this.Controls.Add(tBar);

           
        }

        void btn_Open_Click()
        {
            OpenFileDialog of;
            of = new OpenFileDialog();
            of.Filter = "txt files (*.txt)|*.txt";
            if (of.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = File.OpenText(of.FileName);
                txbx_TexField.Text = sr.ReadToEnd();
                docName = of.FileName;
            }
        }
        void btn_SaveAs_Click()
        {
            sf = new SaveFileDialog();
            sf.Filter = "Text Files | *.txt";
            sf.DefaultExt = "txt";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sf.FileName))
                {
                    sw.WriteLine(txbx_TexField.Text);
                }
            }
        }

        void btn_Exit_Click()
        {
            this.Close();
        }

        void btn_NewFile_Click()
        {
            object sender = new object();
            EventArgs e = new EventArgs();
            if (!String.IsNullOrEmpty(txbx_TexField.Text))
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Do you want to save the file?", "", buttons);

                if (result == DialogResult.Yes)
                {
                    sf = new SaveFileDialog();
                    sf.Filter = "Text Files | *.txt";
                    sf.DefaultExt = "txt";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter sw = new StreamWriter(sf.FileName))
                        {
                            sw.WriteLine(txbx_TexField.Text);
                            txbx_TexField.Text = "";
                        }
                    }
                }
                else
                {
                    Form1_Load(sender,e);
                    this.Controls.Remove(tBar);
                    txbx_TexField.Text = "";
                }
            }

            else
            {
                txbx_TexField.Text = "";
            }
        }

        void btn_Copy_Click()
        {
            copy = txbx_TexField.SelectedText;               
        }

        void btn_Paste_Click()
        {
            var SelectionIndex = txbx_TexField.SelectionStart;
            txbx_TexField.Text = txbx_TexField.Text.Insert(SelectionIndex, copy);
        }

        void btn_Ubdo_Click()
        {
            txbx_TexField.Undo();           
        }

        void btn_Redo_Click()
        {
            txbx_TexField.Redo();
        }

        void btn_FontColor_Click()
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = txbx_TexField.SelectionColor;
            if (cd.ShowDialog() == DialogResult.OK && cd.Color != txbx_TexField.SelectionColor)
            {
                txbx_TexField.SelectionColor = cd.Color;
            }
        }

        void btn_Backcolor_Click()
        {
            ColorDialog cd1 = new ColorDialog();
            cd1.Color = txbx_TexField.BackColor;
            if (cd1.ShowDialog() == DialogResult.OK && cd1.Color != txbx_TexField.BackColor)
            {
                txbx_TexField.BackColor = cd1.Color;
            }
        }

        void btn_FontFormat_Click()
        {
            FontDialog fd = new FontDialog();
            fd.Font = txbx_TexField.SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK && fd.Font != txbx_TexField.SelectionFont)
            {
                txbx_TexField.SelectionFont = fd.Font;
            }
        }

        void btn_SelectAll_Click()
        {
            txbx_TexField.SelectAll();
        }

        void btn_Save_Click()
        {
            if (!String.IsNullOrEmpty(docName))
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, docName)))
                {
                    sw.WriteLine(txbx_TexField.Text);
                }
                MessageBox.Show($"File was save to {docPath}");
            }
            else
            {
                btn_SaveAs_Click();
            }
      
        }

        void btn_Exsize_Click()
        {
            copy = txbx_TexField.SelectedText;
            txbx_TexField.SelectedText = "";
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_SaveAs_Click();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Open_Click();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Exit_Click();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btn_Copy_Click();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            btn_Paste_Click();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Ubdo_Click();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Redo_Click();
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_FontColor_Click();
        }

        private void fontFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_FontFormat_Click();
        }

        private void backcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Backcolor_Click();
        }

        private void sm_Copy_Click(object sender, EventArgs e)
        {
            btn_Copy_Click();
        }

        private void sm_Paste_Click(object sender, EventArgs e)
        {
            btn_Paste_Click();
        }

        private void sm_Undo_Click(object sender, EventArgs e)
        {
            btn_Ubdo_Click();
        }

        private void sm_Redo_Click(object sender, EventArgs e)
        {
            btn_Redo_Click();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_SelectAll_Click();
        }

        private void tb_Save_Click(object sender, EventArgs e)
        {
            btn_Save_Click();
        }

        private void sm_Exsize_Click(object sender, EventArgs e)
        {
            btn_Exsize_Click();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            btn_Exsize_Click();
        }
    }
}
