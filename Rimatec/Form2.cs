using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rimatec
{
    public partial class Form2 : Form
    {
        public Form2(int userid)
        {
            InitializeComponent();
            curser(true);
            user.UserID = userid;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            readUserData();
        }

        #region tasarım

        #region değişkenler
        SqlConnection con = new SqlConnection("Server=SARUS\\SQLEXPRESS;Database=Rimatec;User Id = adminuser; Password=123;");
        int x = 0, y = 0;
        string[] language = { "Gün:", "Ay:", "Yıl:", "Ürün Adı", "Miktar", "Hoşgeldin" };
        bool windowMove = false;
        bool moveupdown = false; // tam hali 182 kısa hali 43
        bool curser2 = false;
        bool maxi = false;
        int thissize = 0, thissize2 = 0, mousesize = 0, mousesize2 = 0;
        byte dilno = 0;
        UserData user = new UserData();
        //string connectionStrings = "Server=SARUS\\SQLEXPRESS;Database=Rimatec;User Id = adminuser; Password=123;";
        string search = "", totalnum = "", avaible = "";
        #endregion

        void readUserData()
        {
            SqlCommand cmd = new SqlCommand($"select * from userData where id = {user.UserID}", con);
            con.Close();
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            user.realName = reader.GetString(3);
            user.realLastName = reader.GetString(4);
            user.title = reader.GetString(5);
            user.admins = reader.GetBoolean(6);
            user.stockDisplay = reader.GetBoolean(7);
            user.stockChange = reader.GetBoolean(8);
            user.moveDisplay = reader.GetBoolean(9);
            user.moveChange = reader.GetBoolean(10);
            user.notificationEdit = reader.GetBoolean(11);
            reader.Close();
            con.Close();
            label1.Text = language[5] + " " + user.realName + " " + user.realLastName;
            userPermission();
            //MessageBox.Show(user.UserID.ToString() + "\n" + user.realName.ToString() + "\n" + user.realLastName.ToString() + "\n" + user.title.ToString() + "\n" + user.admins.ToString() + "\n" + user.stockDisplay.ToString() + "\n" + user.stockChange.ToString() + "\n" + user.moveDisplay.ToString() + "\n" + user.moveChange.ToString() + "\n" + user.notificationEdit.ToString());
        }

        void userPermission()
        {
            if (!user.admins)
            {
                button6.Visible = false;
                if (!user.stockChange) button13.Visible = false;
                if (!user.stockDisplay) button3.Visible = false;
                if (!user.moveChange)
                {
                    button5.Visible = false;
                    button10.Visible = false;
                    button11.Top -= 45;
                }
                if (!user.moveDisplay)
                {
                    button4.Visible = false;
                    button9.Visible = false;
                    button11.Top -= 45;
                    button10.Top -= 45;
                }
                if (!user.notificationEdit)
                {
                    panel64.Visible = false;
                }
            }
        }

        void curser(bool Durum)
        {
            if (Durum)
            {
                panel5.Cursor = Cursors.SizeWE;
                panel6.Cursor = Cursors.SizeNS;
                panel9.Cursor = Cursors.SizeNWSE;
            }
            else
            {
                panel5.Cursor = Cursors.Default;
                panel6.Cursor = Cursors.Default;
                panel9.Cursor = Cursors.Default;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Application.OpenForms["Form1"].Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!maxi)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
                maxi = true;
                curser(false);
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                maxi = false;
                curser(true);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            windowMove = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (windowMove)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                    x = panel1.Width / 2;
                    y = panel1.Height / 2;
                    maxi = false;
                    curser(true);
                }
                this.Location = new Point(MousePosition.X - x, MousePosition.Y - y);
            }
        }

        void sekme(Button a)
        {
            button3.BackColor = Color.FromArgb(58, 68, 77);
            button4.BackColor = Color.FromArgb(58, 68, 77);
            button5.BackColor = Color.FromArgb(58, 68, 77);
            button6.BackColor = Color.FromArgb(58, 68, 77);
            button7.BackColor = Color.FromArgb(58, 68, 77);
            panel8.Visible = true;
            panel8.Location = a.Location;
            a.BackColor = Color.FromArgb(65, 76, 86);
            label1.Text = a.Text;
        }

        void visibles()
        {
            pnlStok.Visible = false;
            pnlMoves.Visible = false;
            pnladdmoves.Visible = false;
            pnlayar.Visible = false;
            pnladmin2.Visible = false;
            pnlbildirim.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sekme(button3);
            visibles();
            pnlStok.Dock = DockStyle.Fill;
            pnlStok.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sekme(button4);
            visibles();
            pnlMoves.Dock = DockStyle.Fill;
            pnlMoves.Visible = true;
            movesSetLocation();
            button36.PerformClick();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sekme(button5);
            visibles();
            pnladdmoves.Dock = DockStyle.Fill;
            pnladdmoves.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sekme(button6);
            visibles();
            pnladmin2.Dock = DockStyle.Fill;
            pnladmin2.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            sekme(button7);
            visibles();
            pnlayar.Dock = DockStyle.Fill;
            pnlayar.Visible = true;
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            curser2 = true;
            thissize = this.Size.Width;
            mousesize = MousePosition.X;
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (curser2)
            {
                this.Size = new Size(thissize + (MousePosition.X - mousesize), this.Size.Height);
            }
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            curser2 = true;
            thissize = this.Size.Height;
            mousesize = MousePosition.Y;
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            if (curser2)
            {
                this.Size = new Size(this.Size.Width, thissize + (MousePosition.Y - mousesize));
            }
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            curser2 = false;
        }

        private void panel9_MouseDown(object sender, MouseEventArgs e)
        {
            curser2 = true;
            thissize = this.Size.Height;
            thissize2 = this.Size.Width;
            mousesize = MousePosition.Y;
            mousesize2 = MousePosition.X;
        }

        private void pnlStok_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Width = pnlStok.Width - panel10.Width - 20;
            dataGridView1.Height = pnlStok.Height - 20;
            int a, b, c, d;
            d = dataGridView1.Width;
            a = (int)((d * 10) / 100);
            b = (int)((d * 15) / 100);
            c = (int)((d * 20) / 100);
            dataGridView1.Columns[0].Width = a;
            dataGridView1.Columns[2].Width = b;
            dataGridView1.Columns[3].Width = b;
            dataGridView1.Columns[4].Width = c;
            dataGridView1.Columns[1].Width = d - (a + (2 * b) + c + 1);
            dataGridView1.Width = pnlStok.Width - panel10.Width / 2 - 20;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel65.Visible = !panel65.Visible;
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlMoves_SizeChanged(object sender, EventArgs e)
        {
            movesSetLocation();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel12.Height = 182;
            moveupdown = true;
            button19.Visible = false;
            button20.Visible = true;
            movesSetLocation();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel12.Height = 42;
            moveupdown = false;
            button19.Visible = true;
            button20.Visible = false;
            movesSetLocation();
        }

        void filteroff()
        {
            pnlislem.Visible = false;
            pnldetay.Visible = false;
            pnltarih.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pnlislem.Dock = DockStyle.Fill;
            filteroff();
            pnlislem.Visible = true;
            if (button19.Visible) button19.PerformClick();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            pnltarih.Dock = DockStyle.Fill;
            filteroff();
            pnltarih.Visible = true;
            if (button19.Visible) button19.PerformClick();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            pnldetay.Dock = DockStyle.Fill;
            filteroff();
            pnldetay.Visible = true;
            if (button19.Visible) button19.PerformClick();
        }

        void dateoff()
        {
            button37.Visible = false;
            button28.Visible = false;
            label10.Visible = false;
            textBox8.Visible = false;
            textBox7.Visible = false;
            textBox6.Visible = false;
            panel23.Visible = false;
            panel22.Visible = false;
            panel21.Visible = false;
            button30.Visible = false;
            button29.Visible = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            dateoff();
            button37.Visible = true;
            label10.Visible = true;
            textBox8.Visible = true;
            textBox7.Visible = true;
            textBox6.Visible = true;
            panel23.Visible = true;
            panel22.Visible = true;
            panel21.Visible = true;
            textBox8.Focus();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            dateoff();
            button30.Visible = true;
            button29.Visible = true;
            button28.Visible = true;
        }

        void textNumericControl(ref TextBox btn)
        {
            foreach (char item in btn.Text)
            {
                if (!char.IsNumber(item))
                {
                    btn.Clear();
                    break;
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox4);
            if (int.Parse(textBox4.Text) > 31) textBox4.Text = "30";
            else if (int.Parse(textBox4.Text) < 0) textBox4.Text = "0";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox3);
            if (int.Parse(textBox3.Text) > 12) textBox3.Text = "12";
            else if (int.Parse(textBox3.Text) < 0) textBox3.Text = "0";
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox5);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox8);
            if (int.Parse(textBox8.Text) > 31) textBox8.Text = "30";
            else if (int.Parse(textBox8.Text) < 0) textBox8.Text = "0";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox7);
            if (int.Parse(textBox7.Text) > 12) textBox7.Text = "12";
            else if (int.Parse(textBox7.Text) < 0) textBox7.Text = "0";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox6);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.FromArgb(58, 68, 77);
            button30.BackColor = Color.FromArgb(72, 84, 96);
            button32.BackColor = Color.FromArgb(72, 84, 96);
            button31.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.FromArgb(72, 84, 96);
            button30.BackColor = Color.FromArgb(58, 68, 77);
            button32.BackColor = Color.FromArgb(72, 84, 96);
            button31.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.FromArgb(72, 84, 96);
            button30.BackColor = Color.FromArgb(72, 84, 96);
            button32.BackColor = Color.FromArgb(58, 68, 77);
            button31.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            label11.Text = language[0];
            button33.BackColor = Color.FromArgb(58, 68, 77);
            button34.BackColor = Color.FromArgb(72, 84, 96);
            button35.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            label11.Text = language[1];
            button34.BackColor = Color.FromArgb(58, 68, 77);
            button33.BackColor = Color.FromArgb(72, 84, 96);
            button35.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            label11.Text = language[2];
            button35.BackColor = Color.FromArgb(58, 68, 77);
            button34.BackColor = Color.FromArgb(72, 84, 96);
            button33.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            button29.BackColor = Color.FromArgb(72, 84, 96);
            button30.BackColor = Color.FromArgb(72, 84, 96);
            button32.BackColor = Color.FromArgb(72, 84, 96);
            button31.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void pnltarih_SizeChanged(object sender, EventArgs e)
        {
            button33.Location = new Point(pnltarih.Width - button33.Width - 20, button33.Location.Y);
            button34.Location = new Point(pnltarih.Width - button34.Width - 20, button34.Location.Y);
            button35.Location = new Point(pnltarih.Width - button35.Width - 20, button35.Location.Y);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == language[3] || string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Clear();
            textBox1.ForeColor = Color.White;
            panel18.BackColor = Color.White;
            label6.Visible = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) textBox1.Text = language[3];
            textBox1.ForeColor = Color.Silver;
            panel18.BackColor = Color.Silver;
            label6.Visible = false;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == language[4] || string.IsNullOrWhiteSpace(textBox2.Text)) textBox2.Clear();
            textBox2.ForeColor = Color.White;
            panel15.BackColor = Color.White;
            label7.Visible = true;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text)) textBox2.Text = language[4];
            textBox2.ForeColor = Color.Silver;
            panel15.BackColor = Color.Silver;
            label7.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button21.Visible = false;
            button38.Visible = true;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            button38.Visible = false;
            button21.Visible = true;
        }

        private void panel9_MouseMove(object sender, MouseEventArgs e)
        {
            if (curser2)
            {
                this.Size = new Size(thissize2 + (MousePosition.X - mousesize2), thissize + (MousePosition.Y - mousesize));
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand($"update moves set moveStatus=1 where id = {dataGridView2.SelectedRows[0].Cells[0].Value.ToString()}", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("İşlem onaylandı!", "Onay");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Hata oluştu lütfen düzgün bir seçim yapın", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void addmoveselect()
        {
            button39.BackColor = Color.FromArgb(83, 92, 104);
            button40.BackColor = Color.FromArgb(83, 92, 104);
            button41.BackColor = Color.FromArgb(83, 92, 104);
            button42.BackColor = Color.FromArgb(83, 92, 104);
            button43.BackColor = Color.FromArgb(83, 92, 104);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            addmoveselect();
            button43.BackColor = Color.FromArgb(70, 81, 93);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            addmoveselect();
            button42.BackColor = Color.FromArgb(70, 81, 93);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            addmoveselect();
            button41.BackColor = Color.FromArgb(70, 81, 93);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            addmoveselect();
            button39.BackColor = Color.FromArgb(70, 81, 93);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            addmoveselect();
            button40.BackColor = Color.FromArgb(70, 81, 93);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            textBox12.Visible = true;
            button46.Visible = true;
            panel26.Visible = true;
            label17.Visible = true;
            button45.Visible = false;
            button47.Visible = true;
            textBox12.Focus();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            textBox12.Visible = false;
            button46.Visible = false;
            panel26.Visible = false;
            label17.Visible = false;
            button45.Visible = true;
            button47.Visible = false;
        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            panel26.BackColor = Color.White;
            textBox12.ForeColor = Color.White;
            label18.Visible = true;
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            panel26.BackColor = Color.Silver;
            textBox12.ForeColor = Color.Silver;
            label18.Visible = false;
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            if (textBox10.Text == language[3]) textBox10.Clear();
            label13.Visible = true;
            textBox10.ForeColor = Color.White;
            panel28.BackColor = Color.White;
            if (button44.Enabled == false) textBox10.Text += " ";
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text)) textBox10.Text = language[3];
            label13.Visible = false;
            textBox10.ForeColor = Color.Silver;
            panel28.BackColor = Color.Silver;
            if (!visiblecontrol) button99.Visible = false;
        }

        private void textBox11_Enter(object sender, EventArgs e)
        {
            if (textBox11.Text == language[4]) textBox11.Clear();
            label14.Visible = true;
            textBox11.ForeColor = Color.White;
            panel29.BackColor = Color.White;
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox11.Text)) textBox11.Text = language[4];
            label14.Visible = false;
            textBox11.ForeColor = Color.Silver;
            panel29.BackColor = Color.Silver;
        }

        private void textBox13_Enter(object sender, EventArgs e)
        {
            textBox13.ForeColor = Color.White;
            panel31.BackColor = Color.White;
            label19.Visible = true;
            if (textBox13.Text == "Hareket ID" || textBox13.Text == "Ürün ID") textBox13.Clear();
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            textBox13.ForeColor = Color.Silver;
            panel31.BackColor = Color.Silver;
            label19.Visible = false;
            if (string.IsNullOrWhiteSpace(textBox13.Text)) textBox13.Text = label19.Text == "Ürün ID:" ? "Ürün ID" : "Hareket ID";
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (label19.Text == "Hareket ID:" && textBox13.Text != "Hareket ID")
            {
                foreach (var item in textBox13.Text)
                {
                    if (!char.IsNumber(item))
                    {
                        textBox13.Clear();
                        break;
                    }
                }
            }
            else if (label19.Text == "Ürün ID:" && textBox13.Text != "Ürün ID")
            {
                foreach (var item in textBox13.Text)
                {
                    if (!char.IsNumber(item))
                    {
                        textBox13.Clear();
                        break;
                    }
                }
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            panel33.Location = new Point(2, 101);
            button51.BackColor = Color.FromArgb(72, 84, 96);
            button50.BackColor = Color.FromArgb(58, 68, 77);
            label19.Visible = false;
            textBox13.Visible = false;
            panel31.Visible = false;
            pictureBox7.Visible = false;
            pictureBox6.Visible = false;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            panel33.Location = new Point(2, 171);
            button50.BackColor = Color.FromArgb(72, 84, 96);
            button51.BackColor = Color.FromArgb(58, 68, 77);
            textBox13.Visible = true;
            panel31.Visible = true;
            if (label19.Text == "Hareket ID:") pictureBox7.Visible = true;
            else pictureBox6.Visible = true;
        }

        void bildirimozellikackapa(bool x)
        {
            button52.Enabled = x;
            button54.Enabled = x;
            button56.Enabled = x;
            button53.Enabled = x;
            button55.Enabled = !x;
            textBox14.Enabled = x;
            textBox15.Enabled = x;
            textBox16.Enabled = !x;
            if (!x)
            {
                textBox14.Clear();
                textBox15.Clear();
            }
            else textBox16.Clear();
        }

        private void button49_Click_1(object sender, EventArgs e)
        {
            button48.BackColor = Color.FromArgb(72, 84, 96);
            button49.BackColor = Color.FromArgb(58, 68, 77);
            label19.Text = "Ürün ID:";
            textBox13.Text = "Ürün ID";
            if (button50.BackColor == Color.FromArgb(72, 84, 96))
            {
                pictureBox6.Visible = true;
                pictureBox7.Visible = false;
            }
            bildirimozellikackapa(true);
        }

        private void button48_Click_1(object sender, EventArgs e)
        {
            button49.BackColor = Color.FromArgb(72, 84, 96);
            button48.BackColor = Color.FromArgb(58, 68, 77);
            label19.Text = "Hareket ID:";
            textBox13.Text = "Hareket ID";
            if (button50.BackColor == Color.FromArgb(72, 84, 96))
            {
                pictureBox7.Visible = true;
                pictureBox6.Visible = false;
            }
            bildirimozellikackapa(false);
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (button52.BackColor == Color.FromArgb(72, 84, 96)) button52.BackColor = Color.FromArgb(58, 68, 77);
            else button52.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (button54.BackColor == Color.FromArgb(72, 84, 96)) button54.BackColor = Color.FromArgb(58, 68, 77);
            else button54.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (button56.BackColor == Color.FromArgb(72, 84, 96)) button56.BackColor = Color.FromArgb(58, 68, 77);
            else button56.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (button53.BackColor == Color.FromArgb(72, 84, 96)) button53.BackColor = Color.FromArgb(58, 68, 77);
            else button53.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (button55.BackColor == Color.FromArgb(72, 84, 96)) button55.BackColor = Color.FromArgb(58, 68, 77);
            else button55.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (dilno == 0)
            {
                dilno++;
                button58.Text = "  Türkçe";
            }
            else if (dilno == 1)
            {
                dilno = 0;
                button58.Text = "  Deutsche";
            }
        }

        void adminkarartma()
        {
            button60.BackColor = Color.FromArgb(83, 92, 104);
            button61.BackColor = Color.FromArgb(83, 92, 104);
            button62.BackColor = Color.FromArgb(83, 92, 104);
            button63.BackColor = Color.FromArgb(83, 92, 104);
            button64.BackColor = Color.FromArgb(83, 92, 104);
            button65.BackColor = Color.FromArgb(83, 92, 104);
            button67.BackColor = Color.FromArgb(83, 92, 104);
            admin1.Visible = false;
            admin2_3.Visible = false;
            admin5.Visible = false;
            admin6.Visible = false;
            admin7.Visible = false;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button64.BackColor = Color.FromArgb(58, 68, 77);
            admin1.Dock = DockStyle.Fill;
            admin1.Visible = true;
        }

        private void admin2_3_SizeChanged(object sender, EventArgs e)
        {
            button102.Location = new Point(admin2_3.Width - button102.Width - 3, button102.Location.Y);
            textBox22.Location = new Point(button102.Location.X - textBox22.Width - 3, textBox22.Location.Y);
            label27.Location = new Point(textBox22.Location.X - label27.Width - 3, label27.Location.Y);
        }

        private void button63_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button63.BackColor = Color.FromArgb(58, 68, 77);
            admin2_3.Dock = DockStyle.Fill;
            admin2_3.Visible = true;
            label27.Visible = false;
            textBox22.Visible = false;
            button102.Visible = false;
            button103.Location = new Point(admin2_3.Width - button103.Width - 3, button102.Location.Y);
            button86.Visible = true;
            button104.Visible = false;
        }

        private void button67_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button67.BackColor = Color.FromArgb(58, 68, 77);
            admin2_3.Dock = DockStyle.Fill;
            admin2_3.Visible = true;
            label27.Visible = true;
            textBox22.Visible = true;
            button102.Visible = true;
            button103.Location = new Point(admin2_3.Width - button103.Width - 3, 43);
            button86.Visible = false;
            button104.Visible = true;
        }

        private void button62_Click(object sender, EventArgs e)
        {
            try
            {
                string a = dataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand("select realName,realLastName from userData where id=" + a + "", con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                if (MessageBox.Show(reader.GetString(0) + " " + reader.GetString(1) + "\n" + "Kullanıcısını silmek istediğinize emin misiniz?\nKullanıcı ile birlikte kullanıcının bağlı olduğu bütün verilerde silinecektir!", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    reader.Close();
                    cmd.CommandText = "delete from notifications where userID=" + a + "";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from moves where WorkerID=" + a + "";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from userData where id=" + a + "";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı silindi!");
                    cmd.CommandText = $"select * from userData where id = {user.UserID}";
                    try
                    {
                        reader = cmd.ExecuteReader();
                        reader.Read();
                        reader.GetString(0);
                    }
                    catch
                    {
                        Application.Exit();
                    }
                }
                else reader.Close();

                con.Close();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Herhangi bir Seçim yapılmadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button60_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button60.BackColor = Color.FromArgb(58, 68, 77);
            admin5.Dock = DockStyle.Fill;
            admin5.Visible = true;
        }

        private void button65_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button65.BackColor = Color.FromArgb(58, 68, 77);
            admin6.Dock = DockStyle.Fill;
            admin6.Visible = true;
        }

        private void admin6_SizeChanged(object sender, EventArgs e)
        {
            panel53.Width = admin6.Width - 20 - panel37.Width;
            panel57.Width = admin6.Width - 20 - panel37.Width;

            richTextBox4.Width = admin6.Width - 20 - panel37.Width;
            button89.Width = richTextBox4.Width;
            button89.Location = new Point(richTextBox4.Location.X, admin6.Height - button89.Height - 20);
            richTextBox4.Height = admin6.Height - richTextBox4.Location.Y - 30 - button89.Height;
            button79.Location = new Point(richTextBox4.Location.X + richTextBox4.Width - button79.Width, button79.Location.Y);
            button78.Location = new Point(button79.Location.X - button78.Width - 5, button79.Location.Y);
            label40.Location = new Point(button78.Location.X - label40.Width - 5, label40.Location.Y);
        }

        private void admin1_SizeChanged(object sender, EventArgs e)
        {
            dataGridView3.Size = new Size(admin1.Width + 20, admin1.Height - 109);
            int x = (dataGridView3.Width - 65) * 25 / 100;
            dataGridView3.Columns[1].Width = x;
            dataGridView3.Columns[2].Width = x;
            dataGridView3.Columns[3].Width = (dataGridView3.Width - 65) - 3 * x;
            dataGridView3.Columns[4].Width = x;
        }

        private void button61_Click(object sender, EventArgs e)
        {
            adminkarartma();
            button61.BackColor = Color.FromArgb(58, 68, 77);
            admin7.Dock = DockStyle.Fill;
            admin7.Visible = true;
        }

        private void textBox37_Enter(object sender, EventArgs e)
        {
            text(ref label43, ref panel59, ref textBox37, true, "Ürün ID");
        }

        void text(ref Label a, ref Panel b, ref TextBox c, bool d, string text)
        {
            a.Visible = d;
            if (d)
            {
                b.BackColor = Color.White;
                c.ForeColor = Color.White;
                if (c.Text == text) c.Clear();
            }
            else
            {
                b.BackColor = Color.Silver;
                c.ForeColor = Color.Silver;
                if (string.IsNullOrWhiteSpace(c.Text)) c.Text = text;
            }
        }

        private void textBox37_Leave(object sender, EventArgs e)
        {
            text(ref label43, ref panel59, ref textBox37, false, "Ürün ID");
        }

        void noNumber(ref TextBox a, string text)
        {
            if (a.Text != text)
            {
                foreach (var item in a.Text)
                {
                    if (!char.IsNumber(item))
                    {
                        a.Clear();
                        break;
                    }
                }
            }
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox37, "Ürün ID");
            button82.Enabled = false;
        }

        private void textBox38_Enter(object sender, EventArgs e)
        {
            text(ref label46, ref panel60, ref textBox38, true, "Hareket ID");
        }

        private void textBox38_Leave(object sender, EventArgs e)
        {
            text(ref label46, ref panel60, ref textBox38, false, "Hareket ID");
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox38, "Hareket ID");
            button83.Enabled = false;
        }

        private void textBox17_Enter(object sender, EventArgs e)
        {
            text(ref label21, ref panel38, ref textBox17, true, "Kullanıcı Adı");
        }

        private void textBox17_Leave(object sender, EventArgs e)
        {
            text(ref label21, ref panel38, ref textBox17, false, "Kullanıcı Adı");
        }

        private void textBox18_Enter(object sender, EventArgs e)
        {
            text(ref label22, ref panel39, ref textBox18, true, "Şifre");
        }

        private void textBox18_Leave(object sender, EventArgs e)
        {
            text(ref label22, ref panel39, ref textBox18, false, "Şifre");
        }

        private void textBox19_Enter(object sender, EventArgs e)
        {
            text(ref label23, ref panel40, ref textBox19, true, "Ad");
        }

        private void textBox19_Leave(object sender, EventArgs e)
        {
            text(ref label23, ref panel40, ref textBox19, false, "Ad");
        }

        private void textBox20_Enter(object sender, EventArgs e)
        {
            text(ref label24, ref panel41, ref textBox20, true, "Soyad");
        }

        private void textBox20_Leave(object sender, EventArgs e)
        {
            text(ref label24, ref panel41, ref textBox20, false, "Soyad");
        }

        private void textBox21_Enter(object sender, EventArgs e)
        {
            text(ref label25, ref panel42, ref textBox21, true, "Ünvan");
        }

        private void textBox21_Leave(object sender, EventArgs e)
        {
            text(ref label25, ref panel42, ref textBox21, false, "Ünvan");
        }

        void usercreatebuttons(ref Button a)
        {
            if (a.BackColor == Color.FromArgb(58, 68, 77)) a.BackColor = Color.FromArgb(72, 84, 96);
            else a.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button68);
        }

        private void button69_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button69);
        }

        private void button70_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button70);
        }

        private void button71_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button71);
        }

        private void button72_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button72);
        }

        private void button73_Click(object sender, EventArgs e)
        {
            usercreatebuttons(ref button73);
        }

        private void textBox23_Enter(object sender, EventArgs e)
        {
            text(ref label28, ref panel43, ref textBox23, true, "Ürün ID");
        }

        private void textBox23_Leave(object sender, EventArgs e)
        {
            text(ref label28, ref panel43, ref textBox23, false, "Ürün ID");
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox23, "Ürün ID");
            button88.Enabled = false;
        }

        private void textBox24_Enter(object sender, EventArgs e)
        {
            text(ref label29, ref panel44, ref textBox24, true, "Ürün Adı");
        }

        private void textBox24_Leave(object sender, EventArgs e)
        {
            text(ref label29, ref panel44, ref textBox24, false, "Ürün Adı");
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox28, "Kategori");
        }

        private void textBox28_Enter(object sender, EventArgs e)
        {
            text(ref label33, ref panel48, ref textBox28, true, "Kategori");
        }

        private void textBox28_Leave(object sender, EventArgs e)
        {
            text(ref label33, ref panel48, ref textBox28, false, "Kategori");
        }

        private void textBox25_Enter(object sender, EventArgs e)
        {
            text(ref label30, ref panel45, ref textBox25, true, "Toplam Miktar");
        }

        private void textBox25_Leave(object sender, EventArgs e)
        {
            text(ref label30, ref panel45, ref textBox25, false, "Toplam Miktar");
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox25, "Toplam Miktar");
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox26, "Mevcut Miktar");
        }

        private void textBox26_Enter(object sender, EventArgs e)
        {
            text(ref label31, ref panel46, ref textBox26, true, "Mevcut Miktar");
        }

        private void textBox26_Leave(object sender, EventArgs e)
        {
            text(ref label31, ref panel46, ref textBox26, false, "Mevcut Miktar");
        }

        private void textBox27_Enter(object sender, EventArgs e)
        {
            text(ref label32, ref panel47, ref textBox27, true, "Miktar Türü");
        }

        private void textBox27_Leave(object sender, EventArgs e)
        {
            text(ref label32, ref panel47, ref textBox27, false, "Miktar Türü");
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox29, "Haraket ID");
            button89.Enabled = false;
        }

        private void textBox29_Enter(object sender, EventArgs e)
        {
            text(ref label35, ref panel49, ref textBox29, true, "Haraket ID");
        }

        private void textBox29_Leave(object sender, EventArgs e)
        {
            text(ref label35, ref panel49, ref textBox29, false, "Haraket ID");
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox30, "Ürün ID");
        }

        private void textBox30_Enter(object sender, EventArgs e)
        {
            text(ref label36, ref panel50, ref textBox30, true, "Ürün ID");
        }

        private void textBox30_Leave(object sender, EventArgs e)
        {
            text(ref label36, ref panel50, ref textBox30, false, "Ürün ID");
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox31, "Ürün Miktarı");
        }

        private void textBox31_Enter(object sender, EventArgs e)
        {
            text(ref label37, ref panel51, ref textBox31, true, "Ürün Miktarı");
        }

        private void textBox31_Leave(object sender, EventArgs e)
        {
            text(ref label37, ref panel51, ref textBox31, false, "Ürün Miktarı");
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox32, "Görevli ID");
        }

        private void textBox32_Enter(object sender, EventArgs e)
        {
            text(ref label38, ref panel52, ref textBox32, true, "Görevli ID");
        }

        private void textBox32_Leave(object sender, EventArgs e)
        {
            text(ref label38, ref panel52, ref textBox32, false, "Görevli ID");
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox34, "qweasd");
            if (textBox34.Text.Length == 2) textBox35.Focus();
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox35, "qweasd");
            if (textBox35.Text.Length == 2) textBox36.Focus();
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox36, "qweasd");
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox33, "qweasdasdasdasdlkjljkl");
        }

        void createmovebutton1()
        {
            //72, 84, 96  /  58, 68, 77
            button78.BackColor = Color.FromArgb(72, 84, 96);
            button79.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            createmovebutton1();
            button78.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button79_Click(object sender, EventArgs e)
        {
            createmovebutton1();
            button79.BackColor = Color.FromArgb(58, 68, 77);
        }

        void createmovebutton2()
        {
            //72, 84, 96  /  58, 68, 77
            button74.BackColor = Color.FromArgb(72, 84, 96);
            button75.BackColor = Color.FromArgb(72, 84, 96);
            button76.BackColor = Color.FromArgb(72, 84, 96);
            button77.BackColor = Color.FromArgb(72, 84, 96);
            button66.BackColor = Color.FromArgb(72, 84, 96);
        }

        private void button74_Click(object sender, EventArgs e)
        {
            createmovebutton2();
            button74.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button76_Click(object sender, EventArgs e)
        {
            createmovebutton2();
            button76.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            createmovebutton2();
            button66.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button77_Click(object sender, EventArgs e)
        {
            createmovebutton2();
            button77.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button75_Click(object sender, EventArgs e)
        {
            createmovebutton2();
            button75.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void button80_Click(object sender, EventArgs e)
        {
            if (button80.BackColor == Color.FromArgb(58, 68, 77)) button80.BackColor = Color.FromArgb(72, 84, 96);
            else button80.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            visibles();
            pnlbildirim.Dock = DockStyle.Fill;
            pnlbildirim.Visible = true;
            label1.Text = "Bildirimler";
        }

        private void pnlbildirim_SizeChanged(object sender, EventArgs e)
        {
            dataGridView4.Width = pnlbildirim.Width + 30;
            dataGridView4.Height = pnlbildirim.Height - 20 - button90.Height;
            dataGridView4.Columns[1].Width = pnlbildirim.Width - 20 - dataGridView4.Columns[0].Width;
        }

        private void button90_Click(object sender, EventArgs e)
        {
            dataGridView4.Rows.Add("Taş kesme makinası", "İşlem yapıldığından beri 15 gün geçti ve geri gelmedi!");
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BorderStyle = BorderStyle.None;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(30, 39, 46);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(58, 68, 77);
        }

        private void panel58_SizeChanged(object sender, EventArgs e)
        {
            button92.Location = new Point(3, button92.Location.Y);
            textBox40.Location = new Point(button92.Location.X + button92.Width + 10, textBox40.Location.Y);
            panel63.Location = new Point(textBox40.Location.X, panel63.Location.Y);
            button93.Location = new Point(textBox40.Location.X + textBox40.Width + 10, button92.Location.Y);
            button91.Location = new Point(panel58.Width - 10 - button91.Width, button91.Location.Y);
            textBox39.Location = new Point(button93.Location.X + button93.Width + 20, textBox39.Location.Y);
            textBox39.Width = button91.Location.X - textBox39.Location.X - 18;
            panel62.Width = textBox39.Width;
            panel62.Location = new Point(textBox39.Location.X, panel62.Location.Y);
            button94.Location = button92.Location;
            button95.Location = button93.Location;
        }

        private void panel58_MouseDown(object sender, MouseEventArgs e)
        {

        }

        void stokbuttonkarartma()
        {
            button92.BackColor = Color.FromArgb(83, 92, 104);
            button93.BackColor = Color.FromArgb(83, 92, 104);
            button94.BackColor = Color.FromArgb(83, 92, 104);
            button95.BackColor = Color.FromArgb(83, 92, 104);
        }

        private void button92_Click(object sender, EventArgs e)
        {
            if (button92.BackColor == Color.FromArgb(83, 92, 104))
            {
                stokbuttonkarartma();
                button92.BackColor = Color.FromArgb(58, 68, 77);
            }
            else
            {
                stokbuttonkarartma();
                button94.BackColor = Color.FromArgb(58, 68, 77);
                button92.Visible = false;
                button94.Visible = true;
            }
        }

        private void button93_Click(object sender, EventArgs e)
        {
            if (button93.BackColor == Color.FromArgb(83, 92, 104))
            {
                stokbuttonkarartma();
                button93.BackColor = Color.FromArgb(58, 68, 77);
            }
            else
            {
                stokbuttonkarartma();
                button95.BackColor = Color.FromArgb(58, 68, 77);
                button93.Visible = false;
                button95.Visible = true;
            }
        }

        private void button94_Click(object sender, EventArgs e)
        {
            if (button94.BackColor == Color.FromArgb(83, 92, 104))
            {
                stokbuttonkarartma();
                button94.BackColor = Color.FromArgb(58, 68, 77);
            }
            else
            {
                stokbuttonkarartma();
                button92.BackColor = Color.FromArgb(58, 68, 77);
                button94.Visible = false;
                button92.Visible = true;
            }
        }

        private void button95_Click(object sender, EventArgs e)
        {
            if (button95.BackColor == Color.FromArgb(83, 92, 104))
            {
                stokbuttonkarartma();
                button95.BackColor = Color.FromArgb(58, 68, 77);
            }
            else
            {
                stokbuttonkarartma();
                button93.BackColor = Color.FromArgb(58, 68, 77);
                button95.Visible = false;
                button93.Visible = true;
            }
        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox40, "lşsdakfgpkgjılkdjflhajfdkhfaljaksd");
        }

        private void button91_Click(object sender, EventArgs e)
        {
            search = textBox39.Text.Trim();
            if (button95.BackColor == Color.FromArgb(58, 68, 77))
            {
                avaible = $"and availableNumber > {textBox40.Text.Trim()}";
                totalnum = "";
            }
            else if (button93.BackColor == Color.FromArgb(58, 68, 77))
            {
                avaible = $"and availableNumber < {textBox40.Text.Trim()}";
                totalnum = "";
            }
            else if (button94.BackColor == Color.FromArgb(58, 68, 77))
            {
                totalnum = $"and totalNumber > {textBox40.Text.Trim()}";
                avaible = "";
            }
            else if (button92.BackColor == Color.FromArgb(58, 68, 77))
            {
                totalnum = $"and totalNumber < {textBox40.Text.Trim()}";
                avaible = "";
            }
            readStockData();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = specialStockData(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), "productProperty");
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        string specialStockData(int productid, string columns)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand($"select {columns} from stock where id={productid}", con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string specialData = reader.GetString(0);
            con.Close();
            reader.Close();
            return specialData;
        }

        private void textBox41_Enter(object sender, EventArgs e)
        {
            label47.Visible = true;
            textBox41.ForeColor = Color.White;
            panel66.BackColor = Color.White;
            if (textBox41.Text == "Ürün Adı") textBox41.Clear();
        }

        private void textBox41_Leave(object sender, EventArgs e)
        {
            label47.Visible = false;
            textBox41.ForeColor = Color.Silver;
            panel66.BackColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox41.Text)) textBox41.Text = "Ürün Adı";
        }

        private void textBox42_Enter(object sender, EventArgs e)
        {
            label48.Visible = true;
            textBox42.ForeColor = Color.White;
            panel67.BackColor = Color.White;
            if (textBox42.Text == "Ürün Miktar Çeşidi") textBox42.Clear();
        }

        private void textBox42_Leave(object sender, EventArgs e)
        {
            label48.Visible = false;
            textBox42.ForeColor = Color.Silver;
            panel67.BackColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox42.Text)) textBox42.Text = "Ürün Miktar Çeşidi";
        }

        private void textBox43_Leave(object sender, EventArgs e)
        {
            label49.Visible = false;
            textBox43.ForeColor = Color.Silver;
            panel68.BackColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox43.Text)) textBox43.Text = "Toplam sayı";
        }

        private void textBox43_Enter(object sender, EventArgs e)
        {
            label49.Visible = true;
            textBox43.ForeColor = Color.White;
            panel68.BackColor = Color.White;
            if (textBox43.Text == "Toplam sayı") textBox43.Clear();
        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox43, "Toplam sayı");
        }

        private void textBox44_Enter(object sender, EventArgs e)
        {
            label50.Visible = true;
            textBox44.ForeColor = Color.White;
            panel69.BackColor = Color.White;
            if (textBox44.Text == "Mevcut Sayı") textBox44.Clear();
        }

        private void textBox44_Leave(object sender, EventArgs e)
        {
            label50.Visible = false;
            textBox44.ForeColor = Color.Silver;
            panel69.BackColor = Color.Silver;
            if (string.IsNullOrWhiteSpace(textBox44.Text)) textBox44.Text = "Mevcut Sayı";
        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            noNumber(ref textBox44, "Mevcut Sayı");
        }

        private void richTextBox5_Enter(object sender, EventArgs e)
        {
            label51.ForeColor = Color.White;
            richTextBox5.ForeColor = Color.White;
        }

        private void richTextBox5_Leave(object sender, EventArgs e)
        {
            label51.ForeColor = Color.Silver;
            richTextBox5.ForeColor = Color.Silver;
        }

        private void button97_Click(object sender, EventArgs e)
        {
            panel65.Visible = false;
        }

        private void button98_Click(object sender, EventArgs e)
        {
            textBox42.Focus();
            textBox42.Clear();
            textBox43.Focus();
            textBox43.Clear();
            textBox44.Focus();
            textBox44.Clear();
            richTextBox5.Focus();
            richTextBox5.Clear();
            textBox41.Focus();
            textBox41.Clear();
        }

        private void button96_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox41.Text) || textBox41.Text == "Ürün Adı")
            {
                MessageBox.Show("\"Ürün Adı\" boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox42.Text) || textBox42.Text == "Ürün Miktar Çeşidi")
            {
                MessageBox.Show("\"Miktar Çeşidi\" boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox43.Text) || textBox43.Text == "Toplam sayı")
            {
                MessageBox.Show("\"Toplam Sayı\" boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox44.Text) || textBox44.Text == "Mevcut Sayı")
            {
                MessageBox.Show("\"Mevcut Sayı\" boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"insert into stock values ('{textBox41.Text.Trim()}','{richTextBox5.Text.Trim()}',{textBox43.Text.Trim()},{textBox44.Text.Trim()},'{textBox42.Text.Trim()}',1)", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Kayıt işlemi Gerçekleşti", "Bildirim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel65.Visible = false;
                    button98.PerformClick();
                    con.Close();
                }
                catch (SqlException asd)
                {
                    if (asd.ToString().Contains("UNIQUE")) MessageBox.Show("Bu ürün zaten mevcut!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Veri Girişi Kabul Edilmedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            string search = "";
            if (textBox1.Text != "Ürün Adı")
            {
                search = textBox1.Text.Trim();
            }
            string adpText = $"select moves.id,stock.productName,moves.productNumber,userData.realName+' '+userData.realLastName as worker,moves.workType,moves.workDate,moves.moveStatus from moves inner join stock on productID = stock.id inner join userData on workerID = userData.id where productName like '%{search}%' ";
            if (textBox2.Text != "Miktar")
            {
                if (button38.Visible)
                {
                    adpText += $" and productNumber < {textBox2.Text.Trim()}";
                }
                else
                {
                    adpText += $" and productNumber > {textBox2.Text.Trim()}";
                }
            }
            if ((!string.IsNullOrWhiteSpace(textBox9.Text) && int.Parse(textBox9.Text) > 0) && (button32.BackColor == Color.FromArgb(58, 68, 77) || button31.BackColor == Color.FromArgb(58, 68, 77)))
            {//geçen gün sayısına göre yapılan arama
                if (button32.BackColor == Color.FromArgb(58, 68, 77)) //önce
                {
                    string a = button33.BackColor == Color.FromArgb(58, 68, 77) ? "Day" : button34.BackColor == Color.FromArgb(58, 68, 77) ? "month" : "Year";
                    adpText += $" and workDate >= DATEADD({a},{textBox9.Text.Trim()},GETDATE())";
                }
                else // içinde
                {
                    string a = button33.BackColor == Color.FromArgb(58, 68, 77) ? "Day" : button34.BackColor == Color.FromArgb(58, 68, 77) ? "month" : "Year";
                    adpText += $" and workDate <= DATEADD({a},{textBox9.Text.Trim()},GETDATE())";
                }
            }
            else if (!string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                if (button37.Visible == true)//iki tarih arası arama
                {
                    if (!string.IsNullOrWhiteSpace(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
                    {
                        adpText += $" and workDate > '{textBox5.Text + "-" + textBox3.Text + "-" + textBox4.Text}' and workDate <  '{textBox6.Text + "-" + textBox7.Text + "-" + textBox8.Text}'";
                    }
                    else if (button29.BackColor == Color.FromArgb(58, 68, 77))//belirli tarihten öncesini arama
                    {
                        adpText += $" and workDate < '{textBox5.Text + "-" + textBox3.Text + "-" + textBox4.Text}'";
                    }
                    else // sonrasını arama
                    {
                        adpText += $" and workDate > '{textBox5.Text + "-" + textBox3.Text + "-" + textBox4.Text}'";
                    }
                }
            }
            con.Close();
            con.Open();
            adpText += " order by workDate desc";
            SqlDataAdapter adp = new SqlDataAdapter(adpText, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            textNumericControl(ref textBox9);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string a = textBox2.Text.Trim();
            for (int i = 0; i < a.Length; i++)
            {
                if (!(i == 0 && a[i] == '-'))
                {
                    if (!char.IsNumber(a[i]))
                    {
                        textBox2.Clear();
                        break;
                    }
                }
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open) con.Open();
                SqlCommand cmd = new SqlCommand($"select workDate,workDetail,connectedWorkID from moves where id={dataGridView2.SelectedRows[0].Cells[0].Value.ToString()}", con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                label12.Text = reader.GetValue(0).ToString() + "\n" + (reader.GetInt32(2) == -1 ? "" : ("Bağlantılı hareket id: " + reader.GetInt32(2).ToString() + "\n")) + reader.GetString(1);
                reader.Close();
                con.Close();
            }
            catch (ArgumentOutOfRangeException)
            {
                con.Close();
            }
            con.Close();
        }

        private void pnldetay_SizeChanged(object sender, EventArgs e)
        {
            label12.MaximumSize = new Size(pnldetay.Width - 35, 0);
        }
        int moveID = -1;
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            button44.Enabled = false;
            moveID = -1;
            if (textBox10.Text != "Ürün Adı" && !string.IsNullOrWhiteSpace(textBox10.Text))
            {
                if (con.State != ConnectionState.Open) con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand($"select top(1) productName,id from stock where productName Like '%{textBox10.Text.Trim()}%'", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    button99.Text = reader.GetString(0);
                    moveID = reader.GetInt32(1);
                    reader.Close();
                    button99.Visible = true;
                }
                catch
                {
                    button99.Visible = false;
                }
                con.Close();
            }
            else
            {
                button99.Visible = false;
            }
        }

        private void button99_Click(object sender, EventArgs e)
        {
            textBox10.Text = button99.Text;
            button44.Enabled = true;
            button99.Visible = false;
        }
        bool addmoveid = false;
        private void button46_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox12.Text))
            {
                try
                {
                    con.Close();
                    if (con.State != ConnectionState.Open) con.Open();
                    SqlCommand cmd = new SqlCommand($"select stock.productName,moves.productNumber,moves.workType from moves inner join stock on stock.id = moves.productID where moves.id = {textBox12.Text.Trim()}", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    label17.Text = reader.GetString(0) + " " + reader.GetInt32(1).ToString() + " " + reader.GetValue(2).ToString();
                    reader.Close();
                    con.Close();
                    addmoveid = true;
                }
                catch
                {
                    label17.Text = "Böyle bir veri bulunamadı!";
                    addmoveid = false;
                }
            }
            else
            {
                label17.Text = "Geçerli bir ID giriniz!";
                addmoveid = false;
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            label17.Text = "";
        }
        bool visiblecontrol = false;
        private void button99_MouseHover(object sender, EventArgs e)
        {
            visiblecontrol = true;
        }

        private void button99_MouseLeave(object sender, EventArgs e)
        {
            visiblecontrol = false;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox11.Text))
            {

                int deger = -1, durum = 1;
                if (button43.BackColor == Color.FromArgb(70, 81, 93)) deger = 1;
                else if (button42.BackColor == Color.FromArgb(70, 81, 93)) deger = 2;
                else if (button41.BackColor == Color.FromArgb(70, 81, 93)) { deger = 3; durum = 0; }
                else if (button40.BackColor == Color.FromArgb(70, 81, 93)) { deger = 4; durum = 0; }
                else if (button39.BackColor == Color.FromArgb(70, 81, 93)) deger = 5;
                if (deger != -1)
                {
                    if (button47.Visible)
                    {
                        if (addmoveid) //bağlantılı id girildiği zaman
                        {
                            if (con.State != ConnectionState.Open) con.Open();
                            SqlCommand cmd = new SqlCommand($"insert into moves values ({moveID},{textBox11.Text.Trim()},{user.UserID.ToString()},'{richTextBox2.Text}',{deger},{durum},{textBox12.Text.Trim()},GetDate())", con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("Kayıt Tamamlandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("Geçerli bir hareket ID'si girilmedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else // bağlantılı id girilmediği zaman
                    {
                        if (con.State != ConnectionState.Open) con.Open();
                        SqlCommand cmd = new SqlCommand($"insert into moves values ({moveID},{textBox11.Text.Trim()},{user.UserID.ToString()},'{richTextBox2.Text}',{deger},{durum},-1,GetDate())", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Kayıt Tamamlandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else MessageBox.Show("İşlem türü seçilmemiş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Miktar kısmı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button100_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Temizlensin mi ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();
                if (button47.Visible) button47.PerformClick();
                richTextBox2.Clear();
                addmoveid = false;
                textBox11.Focus();
                textBox10.Focus();
                addmoveselect();
            }
        }
        bool kont101 = false;
        private void button101_Click(object sender, EventArgs e)
        {
            kont101 = false;
            con.Close();
            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select id,realName,realLastName,systemName,systemPass from userData", con);
            /*try
            {*/
            DataTable dt = new DataTable();
            adp.Fill(dt);
            dataGridView3.DataSource = dt;
            /*}
            catch
            {
                MessageBox.Show("Beklenmedik bir hata oluştu!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }*/
            con.Close();
            kont101 = true;
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (kont101)
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand($"select title,admins,stockDisplay,stockChange,moveDisplay,moveChange,notificationEdit from userData where id = {dataGridView3.SelectedRows[0].Cells[0].Value.ToString()}", con);
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    reader.Read();
                    label52.Text = reader.GetString(0) + "\nAdmin: " + reader.GetValue(1).ToString() + " \\ Stok Görüntüleme: " + reader.GetValue(2).ToString() + " \\ Stok Düzenleme: " + reader.GetValue(3).ToString() + "\nHaraket Görüntüleme: " + reader.GetValue(4).ToString() + " \\ Haraket Ekleme: " + reader.GetValue(5).ToString() + " \\ Bildirim Ayarlama: " + reader.GetValue(6).ToString();
                    reader.Close();
                }
                catch
                {
                }
                con.Close();
            }
        }

        private void button86_Click(object sender, EventArgs e)
        {
            useradd();
        }

        bool usercontrol(string text)
        {
            con.Close();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand($"select * from userData where systemName = '{text}'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string axb = reader.GetString(1);
                con.Close();
                reader.Close();
                return true;
            }
            catch (InvalidOperationException)
            {
                con.Close();
                return false;
            }
        }

        void useradd()
        {
            if (!usercontrol(textBox17.Text.Trim()))
            {
                //string errors = "";
                try
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"insert into userData values " +
                        $"('{textBox17.Text.Trim()}','{textBox18.Text}','{textBox19.Text.Trim()}','{textBox20.Text.Trim()}','{textBox21.Text.Trim()}'," +
                        $"{(button68.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"{(button69.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"{(button70.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"{(button71.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"{(button72.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"{(button73.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")})", con);
                    //errors = cmd.CommandText;
                    cmd.ExecuteNonQuery();
                    button103.PerformClick();
                    MessageBox.Show("Kayıt Başarılı!");
                    con.Close();
                }
                catch /*(Exception zbv)*/
                {
                    MessageBox.Show("Beklenmeyen bir hata meydana geldi!"/* +"\n\n"+zbv.Message+"\n\n"+errors */, "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
            else MessageBox.Show("Bu kullanıcı ismi zaten Bulunuyor!", "Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void userupdate()
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand($"update userData set " +
                $"systemName = '{textBox17.Text}'," +
                $"systemPass = '{textBox18.Text}'," +
                $"realName = '{textBox19.Text}'," +
                $"realLastName = '{textBox20.Text}'," +
                $"title = '{textBox21.Text}'," +
                $"admins = {(button68.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                $"stockDisplay = {(button69.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                $"stockChange = {(button70.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                $"moveDisplay= {(button71.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                $"moveChange = {(button72.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                $"notificationEdit = {(button73.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}" +
                $" where id = {textBox22.Text.Trim()}", con);
            cmd.ExecuteNonQuery();
            con.Close();
            button103.PerformClick();
            MessageBox.Show("Kayıt Başarılı!");
        }

        bool kontroling = false;

        private void button103_Click(object sender, EventArgs e)
        {
            if (!kontroling) textBox22.Clear();
            else
            {
                kontroling = false;
            }
            button68.BackColor = Color.FromArgb(72, 84, 96);
            button69.BackColor = Color.FromArgb(72, 84, 96);
            button70.BackColor = Color.FromArgb(72, 84, 96);
            button71.BackColor = Color.FromArgb(72, 84, 96);
            button72.BackColor = Color.FromArgb(72, 84, 96);
            button73.BackColor = Color.FromArgb(72, 84, 96);
            textBox18.Focus();
            textBox18.Clear();
            textBox19.Focus();
            textBox19.Clear();
            textBox20.Focus();
            textBox20.Clear();
            textBox21.Focus();
            textBox21.Clear();
            textBox17.Focus();
            textBox17.Clear();
        }

        private void button102_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox22.Text))
            {
                int updateID = 0;
                if (int.TryParse(textBox22.Text.Trim(), out updateID))//doğru veri girildi
                {
                    try
                    {
                        con.Close();
                        con.Open();
                        SqlCommand cmd = new SqlCommand($"select * from userData where id = {updateID}", con);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        kontroling = true;
                        button103.PerformClick();
                        textBox17.Text = reader.GetString(1);
                        textBox18.Text = reader.GetString(2);
                        textBox19.Text = reader.GetString(3);
                        textBox20.Text = reader.GetString(4);
                        textBox21.Text = reader.GetString(5);
                        if (reader.GetBoolean(6)) usercreatebuttons(ref button68);
                        if (reader.GetBoolean(7)) usercreatebuttons(ref button69);
                        if (reader.GetBoolean(8)) usercreatebuttons(ref button70);
                        if (reader.GetBoolean(9)) usercreatebuttons(ref button71);
                        if (reader.GetBoolean(10)) usercreatebuttons(ref button72);
                        if (reader.GetBoolean(11)) usercreatebuttons(ref button73);
                        reader.Close();
                        con.Close();
                        button104.Enabled = true;
                    }
                    catch (InvalidOperationException)
                    {
                        con.Close();
                        MessageBox.Show("Bu id ye sahip bir kullanıcı bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ID kısmına sayı dışında bir veri girilemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox22.Clear();
                    textBox22.Focus();
                }
            }
            else
            {
                MessageBox.Show("ID kısmı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox22.Clear();
                textBox22.Focus();
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            button104.Enabled = false;
        }

        private void button104_Click(object sender, EventArgs e)
        {
            userupdate();
        }

        private void button87_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand($"select * from stock where id = {textBox23.Text.Trim()}", con);
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                textBox24.Text = reader.GetString(1);
                richTextBox3.Text = reader.GetString(2);
                textBox25.Text = reader.GetInt32(3).ToString();
                textBox26.Text = reader.GetInt32(4).ToString();
                textBox27.Text = reader.GetString(5).ToString();
                textBox28.Text = reader.GetValue(6).ToString();
                reader.Close();
                con.Close();
                button88.Enabled = true;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Böyle bir ID numarasına sahip ürün bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button88.Enabled = false;
            }
        }

        private void button88_Click(object sender, EventArgs e)
        {
            try
            {
                con.Close();
                con.Open();
                SqlCommand cmd = new SqlCommand($"update stock set " +
                    $"productName = '{textBox24.Text.Trim()}'," +
                    $"productProperty = '{richTextBox3.Text.Trim()}'," +
                    $"totalNumber = {textBox25.Text.Trim()}," +
                    $"availableNumber = {textBox26.Text.Trim()}," +
                    $"typeName = '{textBox27.Text.Trim()}'," +
                    $"category = {textBox28.Text.Trim()} " +
                    $"where id={textBox23.Text.Trim()}", con);
                cmd.ExecuteNonQuery();
                con.Close();
                textBox24.Focus();
                textBox24.Clear();
                textBox25.Focus();
                textBox25.Clear();
                textBox26.Focus();
                textBox26.Clear();
                textBox27.Focus();
                textBox27.Clear();
                textBox28.Focus();
                textBox28.Clear();
                richTextBox3.Focus();
                richTextBox3.Clear();
                textBox23.Focus();
                textBox23.Clear();
                MessageBox.Show("Stock Verisi Güncellendi!");
            }
            catch
            {
                con.Close();
                MessageBox.Show("Beklenmeyen bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button81_Click(object sender, EventArgs e)
        {
            long test = 0;
            if (long.TryParse(textBox29.Text, out test))
            {
                con.Close();
                con.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand($"Select * from moves where id={textBox29.Text.Trim()}", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    textBox30.Text = reader.GetInt32(1).ToString();
                    textBox31.Text = reader.GetInt32(2).ToString();
                    textBox32.Text = reader.GetInt32(3).ToString();
                    richTextBox4.Text = reader.GetString(4);
                    if (reader.GetBoolean(6)) button78.PerformClick();
                    else button79.PerformClick();
                    if (reader.GetValue(7).ToString() != "-1")
                    {
                        textBox33.Text = reader.GetValue(7).ToString();
                        if (button80.BackColor == Color.FromArgb(72, 84, 96)) button80.PerformClick();
                    }
                    else
                    {
                        if (button80.BackColor != Color.FromArgb(72, 84, 96)) button80.PerformClick();
                    }
                    textBox34.Text = reader.GetDateTimeOffset(8).Day.ToString();
                    textBox35.Text = reader.GetDateTimeOffset(8).Month.ToString();
                    textBox36.Text = reader.GetDateTimeOffset(8).Year.ToString();
                    switch (int.Parse(reader.GetValue(5).ToString()))
                    {
                        case 1:
                            button74.PerformClick();
                            break;
                        case 2:
                            button76.PerformClick();
                            break;
                        case 3:
                            button66.PerformClick();
                            break;
                        case 4:
                            button77.PerformClick();
                            break;
                        case 5:
                            button75.PerformClick();
                            break;
                        default:
                            break;
                    }
                    button89.Enabled = true;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Belirtilen ID numarasına ait bir veri bulunamdı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("ID numarası boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button89_Click(object sender, EventArgs e)
        {
            string test = "";
            int movetype = 0;
            if (button74.BackColor != Color.FromArgb(72, 84, 96)) movetype = 1;
            else if (button76.BackColor != Color.FromArgb(72, 84, 96)) movetype = 2;
            else if (button66.BackColor != Color.FromArgb(72, 84, 96)) movetype = 3;
            else if (button77.BackColor != Color.FromArgb(72, 84, 96)) movetype = 4;
            else if (button75.BackColor != Color.FromArgb(72, 84, 96)) movetype = 5;
            else
            {
                MessageBox.Show("Herhangi bir işlem türü seçilmemiş!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (string.IsNullOrWhiteSpace(textBox30.Text))
            {
                MessageBox.Show("Ürün ID'si Boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox31.Text))
            {
                MessageBox.Show("Ürün miktarı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox32.Text))
            {
                MessageBox.Show("Görevli ID'si boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrWhiteSpace(textBox34.Text) || string.IsNullOrWhiteSpace(textBox35.Text) || string.IsNullOrWhiteSpace(textBox36.Text))
            {
                MessageBox.Show("Tarih bilgileri boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (button78.BackColor == Color.FromArgb(72, 84, 96) && button79.BackColor == Color.FromArgb(72, 84, 96))
            {
                MessageBox.Show("Durum bilgisi boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (movetype != 0)
            {
                con.Close();
                try
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update moves set " +
                        $"productID = {textBox30.Text.Trim()}," +
                        $"productNumber = {textBox31.Text.Trim()}," +
                        $"workerID = {textBox32.Text.Trim()}," +
                        $"workDetail = '{richTextBox4.Text.Trim()}'," +
                        $"moveStatus = {(button78.BackColor == Color.FromArgb(72, 84, 96) ? "0" : "1")}," +
                        $"connectedWorkID = {(button80.BackColor == Color.FromArgb(72, 84, 96) ? "-1" : textBox33.Text.Trim())}," +
                        $"workType = {movetype.ToString()}," +
                        $"workDate = '{textBox36.Text.Trim() + "-" + textBox35.Text.Trim() + "-" + textBox34.Text.Trim()}' " +
                        $"where id = {textBox29.Text.Trim()}", con);
                    test = cmd.CommandText;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarılı!");
                    textBox29.Focus();
                    textBox29.Clear();
                }
                catch (Exception v)
                {
                    MessageBox.Show("Beklenmeyen bir hata meydana geldi!\n"+v.Message+"\n"+test, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            con.Close();
            
        }

        private void button84_Click(object sender, EventArgs e)
        {
            long test = 0;
            if (long.TryParse(textBox37.Text, out test))
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"Select productName,productProperty from stock where id = {textBox37.Text.Trim()}", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    label44.Text = reader.GetString(0) + ":\n" + reader.GetString(1);
                    reader.Close();
                    con.Close();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Belitilen ID' bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Beklenmeyen bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
            else MessageBox.Show("ID kısmı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            con.Close();
            button82.Enabled = true;
        }

        private void button85_Click(object sender, EventArgs e)
        {
            long test = 0;
            if (long.TryParse(textBox38.Text, out test))
            {
                try
                {
                    con.Close();
                    con.Open();
                    SqlCommand cmd = new SqlCommand($"select userData.realName,userData.realLastName,stock.productName,workDetail from moves inner join userData on moves.workerID = userData.id inner join stock on moves.productID = stock.id where moves.id={textBox38.Text.Trim()}", con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    label45.Text = reader.GetString(0) + " " + reader.GetString(1) + "\n" + reader.GetString(2) + "\n" + reader.GetString(3);
                    reader.Close();
                    con.Close();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Belitilen ID' bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Beklenmeyen bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else MessageBox.Show("ID kısmı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            con.Close();
            button83.Enabled = true;
        }

        private void button82_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            button82.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand($"delete from stock where id = {textBox37.Text.Trim()}",con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi tamamlandı!");
            }
            catch 
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu!\nİnternet bağlantısında sıkıntı olabilir veya bu ürün ile bağlantılı veriler mevcut olabilir.\nBu ürün ile bağlantılı verileri sildikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            button82.Enabled = false;
        }

        private void button83_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            button82.Enabled = false;
            try
            {
                SqlCommand cmd = new SqlCommand($"delete from moves where id = {textBox38.Text.Trim()}", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi tamamlandı!");
            }
            catch
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu!\nİnternet bağlantısında sıkıntı olabilir veya bu haraket ile bağlantılı veriler mevcut olabilir.\nBu haraket ile bağlantılı verileri sildikten sonra tekrar deneyiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
            button83.Enabled = false;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            SqlCommand cmd = new SqlCommand($"insert into notifications values (" +
                $"notificationType = {1}," +
                $"userID = {user.UserID}," +
                $"productID = {1}," +
                $"");
            con.Close();
        }

        private void pnlStok_VisibleChanged(object sender, EventArgs e)
        {
            if (pnlStok.Visible == true)
            {
                readStockData();
            }
        }

        void readStockData()
        {
            con.Close();
            con.Open();
            //SqlDataAdapter adapter = new SqlDataAdapter("select id as urunid,productName as productName,totalNumber as total,availableNumber as mevcut,typeName as tip from stock", con);
            SqlDataAdapter adapter = new SqlDataAdapter($"select id,productName,totalNumber,availableNumber,typeName from stock where productName like '%{search}%' {totalnum} {avaible}", con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void panel9_MouseUp(object sender, MouseEventArgs e)
        {
            curser2 = false;
        }

        private void panel5_MouseUp(object sender, MouseEventArgs e)
        {
            curser2 = false;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            windowMove = false;
        }

        void movesSetLocation()
        {
            if (moveupdown)
            {
                dataGridView2.Height = pnlMoves.Height - panel12.Height - 20;
                dataGridView2.Width = pnlMoves.Width + 100;
                button14.Visible = false;
                button15.Visible = false;
            }
            else
            {
                dataGridView2.Width = pnlMoves.Width + 100;
                dataGridView2.Height = pnlMoves.Height - 90;
                button14.Location = new Point(dataGridView2.Location.X, dataGridView2.Height + 5);
                button15.Location = new Point(dataGridView2.Location.X + pnlMoves.Width - button15.Width - 3, dataGridView2.Height + 5);
                button14.Visible = true;
                button15.Visible = true;
            }
            int a, b, c, d, e;
            d = pnlMoves.Width - 300;
            a = (int)((d * 8) / 100);
            b = (int)((d * 22) / 100);
            c = (int)((d * 23) / 100);
            e = (int)((d * 20) / 100);
            dataGridView2.Columns[0].Width = a;
            dataGridView2.Columns[2].Width = b;
            dataGridView2.Columns[3].Width = c;
            dataGridView2.Columns[4].Width = e;
            dataGridView2.Columns[1].Width = d - (a + b + c + e + 2);
            dataGridView2.Columns[5].Width = 150;
            dataGridView2.Columns[6].Width = 150;
        }

        #endregion
    }
}
