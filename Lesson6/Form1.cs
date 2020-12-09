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

        public Form1()
        {
            InitializeComponent();

        }


        void tBar_Button_Click(object sender, ToolBarButtonClickEventArgs e)
        {
            OpenFileDialog of;
            SaveFileDialog sf;

            switch (e.Button.ImageIndex)
            {
                case 0:
                    of = new OpenFileDialog();
                    of.Filter = "txt files (*.txt)|*.txt";
                    if (of.ShowDialog() == DialogResult.OK)
                    {
                        StreamReader sr = File.OpenText(of.FileName);
                        txbx_TexField.Text = sr.ReadToEnd();
                    }
                    break;

                case 1:
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
                    break;
                case 2:
                    this.Close();
                    break;

                case 3:
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
                            Form1_Load(sender, e);
                            txbx_TexField.Text = "";
                        }
                    }
                   
                    else
                    {
                        txbx_TexField.Text = "";
                    }
                    break;

                case 4:
                    copy = txbx_TexField.SelectedText;
                    break;

                    case 5:
                    var SelectionIndex = txbx_TexField.SelectionStart;
                    txbx_TexField.Text = txbx_TexField.Text.Insert(SelectionIndex, copy);
                    break;

                case 6:
                    txbx_TexField.Undo();           
                    break;

                case 7:
                    txbx_TexField.Redo();
                    break;

                case 8:
                    ColorDialog cd = new ColorDialog();
                    cd.Color = txbx_TexField.SelectionColor;
                    if(cd.ShowDialog() == DialogResult.OK && cd.Color != txbx_TexField.SelectionColor)
                    {
                        txbx_TexField.SelectionColor = cd.Color;
                    }                 
                    break;

                case 9:
                    ColorDialog cd1 = new ColorDialog();
                    cd1.Color = txbx_TexField.BackColor;
                    if (cd1.ShowDialog() == DialogResult.OK && cd1.Color != txbx_TexField.BackColor)
                    {
                        txbx_TexField.BackColor = cd1.Color;
                    }
                    break;

                case 10:
                    FontDialog fd = new FontDialog();
                    fd.Font = txbx_TexField.SelectionFont;
                    if(fd.ShowDialog() == DialogResult.OK && fd.Font != txbx_TexField.SelectionFont)
                    {
                        txbx_TexField.SelectionFont = fd.Font;
                    }
                    break;

                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = new ImageList();
            list.ImageSize = new Size(15, 15);
            list.Images.Add(new Bitmap("open.bmp"));
            list.Images.Add(new Bitmap("save.bmp"));
            list.Images.Add(new Bitmap("exit.bmp"));
            list.Images.Add(new Bitmap("newfile.png"));
            list.Images.Add(new Bitmap("copy.png"));
            list.Images.Add(new Bitmap("paste.png"));
            list.Images.Add(new Bitmap("undo.png"));
            list.Images.Add(new Bitmap("redo.png"));
            list.Images.Add(new Bitmap("color.png"));
            list.Images.Add(new Bitmap("background.png"));
            list.Images.Add(new Bitmap("font.png"));
            tBar = new ToolBar();
            tBar.ImageList = list;
            ToolBarButton tb_Open = new ToolBarButton("Open");
            ToolBarButton tb_Save = new ToolBarButton("Save");
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
            tb_Save.ImageIndex = 1;
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
            tBar.Buttons.Add(tb_Save);
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

       

       
    }
}
