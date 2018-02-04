using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imdb
{
    public partial class Sanatcilar : Form
    {
        public Sanatcilar()
        {
            InitializeComponent();
        }

        int id = Form1.santciId;

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void yorum()
        {
            SqlConnection baglanti = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();
            string yorum = "Select sy.kid,sy.tarih,sy.yorum FROM SanatciYorum sy  where kid in (select kid from Kullanici where sid=@id) ";
            SqlCommand kmtYorum = new SqlCommand(yorum, baglanti);
            kmtYorum.Parameters.AddWithValue("@id", id);
            SqlDataReader drYorum = kmtYorum.ExecuteReader();

            int y = -20;
            int x = 0;
            while (drYorum.Read())
            {
                int kid = (int.Parse((drYorum["kid"]).ToString()));
                ///////////////
                SqlConnection diger = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                string nick = "select nick from Kullanici where id =@kid";
                diger.Open();
                SqlCommand kmtNick = new SqlCommand(nick, diger);
                kmtNick.Parameters.AddWithValue("@kid", kid);
                SqlDataReader drNick = kmtNick.ExecuteReader();
                if (drNick.Read())
                    nick = (drNick["nick"]).ToString() + "  --  " + (drYorum["tarih"]).ToString().Substring(0, 10);

                Label lblNick = new Label();
                y += lblNick.Height;
                lblNick.Location = new Point(5, y);
                lblNick.AutoSize = true;
                lblNick.Text = nick;
                lblNick.Font = new Font("Calibri", 10, FontStyle.Bold);
                panel16.Controls.Add(lblNick);
                x += lblNick.Width;
                diger.Close();
                drNick.Close();


                Label lblyrm = new Label();
                y += lblyrm.Height;
                lblyrm.AutoSize = true;
                lblyrm.Location = new Point(5, y);
                lblyrm.Text = (drYorum["yorum"]).ToString();
                lblyrm.Font = new Font("Calibri", 10, FontStyle.Regular);
                panel16.Controls.Add(lblyrm);

                diger.Open();
                string cvp = "select * from SanatciCevap where sid=@id and kid=@kid";
                SqlCommand kmtCvp = new SqlCommand(cvp, diger);
                kmtCvp.Parameters.AddWithValue("@id", id);
                kmtCvp.Parameters.AddWithValue("@kid", kid);
                SqlDataReader drCvp = kmtCvp.ExecuteReader();
                while (drCvp.Read())
                {
                    SqlConnection kc = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                    kc.Open();
                    cvp = "select nick from Kullanici where id=@kcid";
                    SqlCommand kmtKc = new SqlCommand(cvp, kc);
                    cvp = ((drCvp["kcid"]).ToString());
                    kmtKc.Parameters.AddWithValue("@kcid", int.Parse(cvp));
                    SqlDataReader drKc = kmtKc.ExecuteReader();

                    if (drKc.Read())
                    {
                        Label lblCvp = new Label();
                        y += lblCvp.Height;
                        lblCvp.AutoSize = true;
                        lblCvp.Location = new Point(25, y);
                        lblCvp.Text = ((drKc["nick"]).ToString() + "   ---   " + (drCvp["tarih"]).ToString().Substring(0, 10));
                        lblCvp.Font = new Font("Calibri", 10, FontStyle.Bold);
                        panel16.Controls.Add(lblCvp);
                    }
                    drKc.Close();

                    Label lblCt = new Label();
                    y += lblCt.Height;
                    lblCt.AutoSize = true;
                    lblCt.Location = new Point(25, y);
                    lblCt.Text = (drCvp["cevap"]).ToString();
                    lblCt.Font = new Font("Calibri", 10, FontStyle.Regular);
                    panel16.Controls.Add(lblCt);

                    
                }
                drCvp.Close();
                diger.Close();

                Button btn = new Button();
                btn.Tag = kid;
                y += lblyrm.Height + 5;
                btn.Location = new Point(240, y);
                btn.Size = new Size(85, 25);
                btn.Click += new EventHandler(this.cevap_ver);
                btn.Font = new Font("Calibri", 10, FontStyle.Regular);
                btn.Text = "Cevap Ver";
                btn.BackColor = Color.GhostWhite;
                panel16.Controls.Add(btn);

            }
            ///////////////////////////////////////////////////////////////////////////////////////              
            baglanti.Close();
        }

        public static int yorumKid;
        
        private void cevap_ver(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            yorumKid = int.Parse(btn.Tag.ToString());

            bool cvp = false;
            if (!Form1.kullanıcı)
            {
                MessageBox.Show("CEVAP YAPABİLMEK İÇİN LÜTFEN GİRİŞ YAPINIZ");
            }
            else
            {
                cvp = true;
                            
                if (Form1.kid == yorumKid)
                {
                    MessageBox.Show("KENDİ YORUMUNUZA CEVAP VEREMEZSİNİZ");
                    cvp = false;
                    return;
                }


                SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                baglanti.Open();
                string yorumSorgu = "Select * from SanatciCevap where sid=@sid and kid=@kid and kcid=@kcid";
                SqlCommand kmtSorgu = new SqlCommand(yorumSorgu, baglanti);
                kmtSorgu.Parameters.AddWithValue("@sid", Form1.santciId);
                kmtSorgu.Parameters.AddWithValue("@kid", yorumKid);
                kmtSorgu.Parameters.AddWithValue("@kcid", Form1.kid);
                SqlDataReader dr = kmtSorgu.ExecuteReader();


                if (dr.Read())
                {
                    MessageBox.Show("BU YORUMA ZATEN CEVAP VERMİŞSİNİZ\n\nTARİH : " + dr["tarih"].ToString().Substring(0, 10) + "\n\nYORUM : " + dr["cevap"].ToString());
                    cvp = false;
                }


                dr.Close();
                baglanti.Close();

            }

            if (cvp)
            {
                Form1.fsyc = 3;
                Form yrm = new Yorum_Yap();
                yrm.Show();

            }


        }

        private void Sanatcilar_Load(object sender, EventArgs e)
        {
            int id = Form1.santciId;
            yorum();
            SqlConnection baglanti = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");

            panel4.HorizontalScroll.Visible = false;
            panel4.HorizontalScroll.Enabled = false;
            panel4.VerticalScroll.Visible = false;
            baglanti.Open();

            string img = "Select foto from SanatciFoto where sid='" + id + "'";
            SqlCommand kmtimg = new SqlCommand(img, baglanti);
            SqlDataReader drFoto = kmtimg.ExecuteReader();

            Image sanatciFoto = null;
            byte[] resim;
            int yf = 0;
            while (drFoto.Read())
            {
                resim = (byte[])drFoto[0];
                MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                ms.Write(resim, 0, resim.Length);
                sanatciFoto = Image.FromStream(ms, true);
                Image icon = sanatciFoto;
                Bitmap bmp = new Bitmap(icon, new Size(166, 235));

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.Location = new System.Drawing.Point(0, yf);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(166, 235);
                pictureBox1.Image = bmp;
                panel4.Controls.Add(pictureBox1);
                yf += 235;
            }
            drFoto.Close();
            baglanti.Close();

            baglanti.Open();
            string sanatci = "Select * from Sanatcilar where id=@id";
            SqlCommand kmtHky = new SqlCommand(sanatci, baglanti);
            kmtHky.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = kmtHky.ExecuteReader();
            if (dr.Read())
            {
                this.Text = dr["isim"].ToString().ToUpper();

                Label lblisim = new Label();
                lblisim.Tag = (dr["id"]).ToString();
                lblisim.Location = new Point(0, 0);
                lblisim.Text = (dr["isim"]).ToString().ToUpper();
                lblisim.AutoSize = true;
                lblisim.Font = new Font("Calibri", 11, FontStyle.Regular);
                isimpnl.Controls.Add(lblisim);
                
                Label lblbaslik = new Label();               
                lblbaslik.Location = new Point(0, 0);
                lblbaslik.Text = (dr["isim"]).ToString().ToUpper();
                lblbaslik.AutoSize = true;            
                lblbaslik.Font = new Font("Calibri", 18, FontStyle.Bold);
                lblbaslik.Text += " " + (dr["soyad"]).ToString().ToUpper();
                panel1.Controls.Add(lblbaslik);


                Label lblSoyad = new Label();
                lblSoyad.Location = new Point(0, 0);
                lblSoyad.Text = (dr["soyad"]).ToString().ToUpper();
                lblSoyad.AutoSize = true;
                lblSoyad.Font = new Font("Calibri", 11, FontStyle.Regular);
                soyadpnl.Controls.Add(lblSoyad);

                Label lblDT = new Label();
                lblDT.Location = new Point(0, 0);
                lblDT.Text = ((dr["dt"]).ToString()).Substring(0, 10).ToUpper();
                lblDT.AutoSize = true;
                lblDT.Font = new Font("Calibri", 11, FontStyle.Regular);
                dtpnl.Controls.Add(lblDT);

                Label lblBoy = new Label();
                lblBoy.Location = new Point(0, 0);
                lblBoy.Text = ((dr["boy"]).ToString()).ToUpper();
                lblBoy.AutoSize = true;
                lblBoy.Font = new Font("Calibri", 11, FontStyle.Regular);
                boypnl.Controls.Add(lblBoy);

                Label lblrol = new Label();
                lblrol.Location = new Point(0, 0);
                if ((dr["boyuncu"]).ToString().Equals("1"))
                {
                    lblrol.Text += " OYUNCU ";
                }
                if ((dr["byazar"]).ToString().Equals("1"))
                {
                    lblrol.Text += " YAZAR ";
                }
                if ((dr["byönetmen"]).ToString().Equals("1"))
                {
                    lblrol.Text += " YÖNETMEN ";
                }
                lblrol.AutoSize = true;
                lblrol.Font = new Font("Calibri", 11, FontStyle.Regular);
                rolüpnl.Controls.Add(lblrol);


                Label lblOfilm = new Label();
                lblOfilm.Location = new Point(0, 0);
                lblOfilm.Text = ((dr["oyadet"]).ToString()).ToUpper();
                lblOfilm.AutoSize = true;
                lblOfilm.Font = new Font("Calibri", 11, FontStyle.Regular);
                ofilmpnl.Controls.Add(lblOfilm);

                Label lblyaz = new Label();
                lblyaz.Location = new Point(0, 0);
                lblyaz.Text = ((dr["yazadet"]).ToString()).ToUpper();
                lblyaz.AutoSize = true;
                lblyaz.Font = new Font("Calibri", 11, FontStyle.Regular);
                yazfilmpnl.Controls.Add(lblyaz);

                Label lblyön = new Label();
                lblyön.Location = new Point(0, 0);
                lblyön.Text = ((dr["yönadet"]).ToString());
                lblyön.AutoSize = true;
                lblyön.Font = new Font("Calibri", 11, FontStyle.Regular);
                yönfilmpnl.Controls.Add(lblyön);

                Label lblHky = new Label();
                lblHky.Width = 490;
                lblHky.Height = 1000;
                lblHky.Location = new Point(5, 0);
                lblHky.Text = ((dr["cv"]).ToString()).ToUpper();
                //  lblHky.AutoSize = true;
                lblHky.Font = new Font("Calibri", 10, FontStyle.Regular);
                cvpnl.Controls.Add(lblHky);


            }
            dr.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            string filmsanat = "SELECT isim FROM Filmler Where id in  (select fid from filmSanatci where sid =@sid)";
            SqlCommand kmtFs = new SqlCommand(filmsanat, baglanti);
            kmtFs.Parameters.AddWithValue("@sid", Form1.santciId);
            SqlDataReader drFs = kmtFs.ExecuteReader();
            Label fs = new Label();            
            fs.Location = new Point(5, 0);
            while(drFs.Read())
            {               
                fs.Text += ((drFs["isim"]).ToString()).ToUpper()+" - " ;
                fs.AutoSize = true;
                fs.Font = new Font("Calibri", 10, FontStyle.Regular);
                pnlfilm.Controls.Add(fs);
            }
            drFs.Close();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            string odul = "";
            string odulSel = "Select isim,tarih from SanatciÖdül where sid=@id";
            SqlCommand kmtOdul = new SqlCommand(odulSel, baglanti);
            kmtOdul.Parameters.AddWithValue("@id", id);
            SqlDataReader drOdul = kmtOdul.ExecuteReader();

            if (drOdul.Read())
            {
                odul += (drOdul["isim"]).ToString().ToUpper();
                odul += " - " + (drOdul["tarih"]).ToString().ToUpper() + "";
            }
            drOdul.Close();
            Label lblOdul = new Label();
            lblOdul.Location = new Point(0, 0);
            lblOdul.AutoSize = true;
            lblOdul.Text = odul;
            lblOdul.Font = new Font("Calibri", 10, FontStyle.Regular);

            ödülpnl.Controls.Add(lblOdul);


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool yorm = false;
            if (!Form1.kullanıcı)
            {
                MessageBox.Show("YORUM YAPABİLMEK İÇİN LÜTFEN GİRİŞ YAPINIZ");
            }
            else
            {
                yorm = true;
                SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                baglanti.Open();
                string yorumSorgu = "Select * from SanatciYorum where sid=@sid and kid=@kid";
                SqlCommand kmtSorgu = new SqlCommand(yorumSorgu, baglanti);
                kmtSorgu.Parameters.AddWithValue("@sid", Form1.santciId);
                kmtSorgu.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader dr = kmtSorgu.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("BU FİLME ZATEN YORUM YAPMIŞSINIZ\n\nTARİH : " + dr["tarih"].ToString().Substring(0, 10) + "\n\nYORUM : " + dr["yorum"].ToString());
                    yorm = false;
                }



                dr.Close();
                baglanti.Close();

            }

            if (yorm)
            {
                Form1.fsyc = 4;
                Form yrm = new Yorum_Yap();
                yrm.Show();

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
