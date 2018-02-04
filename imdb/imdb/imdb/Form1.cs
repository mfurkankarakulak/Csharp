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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");

        public static int filmId = 0;
        public static int santciId = 0;
        public static bool kullanıcı = false;
        public static string text;
        
        public static string isim;//--
        public static string nick;//--
        public static string mail;//--
        public static int kid;
        public static int fsyc=0;

        private void önerigetir()
        {
            int yKonum = 0;
            int xKonum = 0;

            try
            {
                SqlConnection kat = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
             
                string kt = "select katagori from Filmler where id = (select min(fid) from favori )";
                kat.Open();
                SqlCommand kmtkt = new SqlCommand(kt, kat);                
                SqlDataReader drkt = kmtkt.ExecuteReader();
                if(drkt.Read())
                {
                    kt = drkt["katagori"].ToString();
                }
                drkt.Close();
                kat.Close();                
                string [] ktg = kt.Split(',');

                //   MessageBox.Show("Veri Tabanına Baglantı Başarılı");
                SqlConnection baglanti1 = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                string öneri = "select * from Filmler f where f.katagori like '%"+ktg[0]+"%'";
                baglanti1.Open();
                SqlCommand kmtöneri = new SqlCommand(öneri, baglanti1);
               // kmtöneri.Parameters.AddWithValue("@kid", Form1.kid);
                SqlDataReader dröneri = kmtöneri.ExecuteReader();
                while (dröneri.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(245, 60);
                    pnl.Location = new Point(0, yKonum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblfavori = new Label();
                    lblfavori.Location = new Point(60, 20);
                    lblfavori.Text = ((dröneri["isim"]).ToString().ToUpper());
                    lblfavori.AutoSize = true;
                    //  lblfavori.Width = 160;
                    //  lblfavori.Height = 36;
                    lblfavori.Font = new Font("Calibri", 7, FontStyle.Bold);
                    pnl.Controls.Add(lblfavori);
                    lblfavori.Tag = (dröneri["id"]).ToString();
                    lblfavori.Click += new EventHandler(this.label_bul);


                    Button btn = new Button();
                    btn.Tag = (dröneri["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(60, 60);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    int flimId = int.Parse((dröneri["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + flimId + "'";
                    SqlCommand kmtimg = new SqlCommand(img, foto);
                    SqlDataReader drFoto = kmtimg.ExecuteReader();
                    Image filmFoto = null;
                    byte[] resim;
                    if (drFoto.Read())
                    {
                        resim = (byte[])drFoto[0];
                        MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                        ms.Write(resim, 0, resim.Length);
                        filmFoto = Image.FromStream(ms, true);
                        Image icon = filmFoto;
                        Bitmap bmp = new Bitmap(icon, new Size(60, 60));
                        btn.Image = bmp;
                    }
                    drFoto.Close();
                    foto.Close();



                    yKonum += 65;
                    panel3.Controls.Add(pnl);
                }
                dröneri.Close();


                baglanti1.Close();

            }


            catch (Exception hata)
            {
               // baglanti1.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }
        }
        private void buton_bul2(object sender, EventArgs e)
        {
            Button btn2 = sender as Button;
            santciId = int.Parse(btn2.Tag.ToString());
            Form Sanatcilar = new Sanatcilar();
            Sanatcilar.Show();
        }
        private void label_bul2(object sender, EventArgs e)
        {
            Label lbl2 = sender as Label;
            santciId = int.Parse(lbl2.Tag.ToString());
            Form Sanatcilar = new Sanatcilar();
            Sanatcilar.Text = santciId.ToString();
            Sanatcilar.Show();

        }

        private void buton_bul(object sender, EventArgs e)
        {
          //  baglanti.Open();
            Button btn = sender as Button;
         /*   string hky = "Select * from Filmler where id=@id";
            SqlCommand kmtHky = new SqlCommand(hky, baglanti);
            kmtHky.Parameters.AddWithValue("@id", int.Parse(btn.Tag.ToString()));
            
            SqlDataReader dr = kmtHky.ExecuteReader();
            if(dr.Read())
                MessageBox.Show((dr["hikaye"]).ToString());
            baglanti.Close();*/
            filmId = int.Parse(btn.Tag.ToString());
            Form film = new film();
            film.Show();
        }

        private void label_bul(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            filmId = int.Parse(lbl.Tag.ToString());
            Form film = new film();
            film.Text = filmId.ToString();
            film.Show();
           
        }
        

        private void getir()
        {
            int y_konum = 0;
            int x_konum = 0;

            try
            {
                baglanti.Open();
             //   MessageBox.Show("Veri Tabanına Baglantı Başarılı");
                
                string sanatci = "Select * from Sanatcilar";
                SqlCommand kmtSanatci = new SqlCommand(sanatci, baglanti);                
                SqlDataReader drSanatci = kmtSanatci.ExecuteReader();
                while(drSanatci.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(150, 170);
                    pnl.Location = new Point(x_konum,0);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblSanatci = new Label();
                    lblSanatci.Location = new Point(x_konum, 175);
                    lblSanatci.Text = ((drSanatci["isim"]).ToString().ToUpper() + " " + (drSanatci["soyad"]).ToString().ToUpper());
                  //  lblSanatci.AutoSize = true;
                    lblSanatci.Width = 160;
                    lblSanatci.Height = 36;
                    lblSanatci.Font = new Font("Calibri", 11, FontStyle.Bold);
                    panel2.Controls.Add(lblSanatci);
                    lblSanatci.Tag = (drSanatci["id"]).ToString();
                    lblSanatci.Click += new EventHandler(this.label_bul2);
                    

                    Button btn = new Button();
                    btn.Tag = (drSanatci["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(150, 170);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul2);
                     pnl.Controls.Add(btn);



                     santciId = int.Parse((drSanatci["id"]).ToString());
                     SqlConnection foto = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                     foto.Open();
                     string img = "Select foto from SanatciFoto where sid='" + santciId + "'";
                     SqlCommand kmtimg = new SqlCommand(img, foto);
                     SqlDataReader drFoto = kmtimg.ExecuteReader();
                     Image filmFoto = null;
                     byte[] resim;
                     if (drFoto.Read())
                     {
                         resim = (byte[])drFoto[0];
                         MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                         ms.Write(resim, 0, resim.Length);
                         filmFoto = Image.FromStream(ms, true);
                         Image icon = filmFoto;
                         Bitmap bmp = new Bitmap(icon, new Size(160, 180));
                         btn.Image = bmp;
                     }
                     drFoto.Close();
                     foto.Close();  

                   

                    x_konum += 175;
                    panel2.Controls.Add(pnl);
                }
                drSanatci.Close();

                string film = "Select * from Filmler";
                SqlCommand kmt = new SqlCommand(film, baglanti);
                SqlDataReader dr = kmt.ExecuteReader();
                while (dr.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(835, 189);
                    pnl.Location = new Point(5, y_konum);                 
                    pnl.BorderStyle = BorderStyle.FixedSingle;
                 //   pnl.AutoScroll = true;
                   

                   /* PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Location = new Point(10, 10);
                    pic.Size = new Size(170, 215);
                    pic.BackColor = Color.Green;
                    pnl.Controls.Add(pic);*/

                    Button btn = new Button();
                    btn.Tag = (dr["id"]).ToString();
                    btn.Location = new Point(5,5);
                    btn.Size = new Size(160, 180);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);

                  /*  Image icon = Image.FromFile("D:\\imdb\\imdb\\imdb\\bin\\Debug\\imdb.jpg");
                    Bitmap bmp = new Bitmap(icon, new Size(160, 180));
                    btn.Image = bmp;*/
                    
                    pnl.Controls.Add(btn);


                    

                    Label lblFilm = new Label();
                    lblFilm.Tag = (dr["id"]).ToString();
                    lblFilm.Location = new Point(200, 5);
                    lblFilm.Text = (dr["isim"]).ToString();
                    lblFilm.AutoSize = true;
                    lblFilm.Font = new Font("Calibri", 18, FontStyle.Bold);
                    lblFilm.Click += new EventHandler(this.label_bul);
                    pnl.Controls.Add(lblFilm);

                    Label lblKategori = new Label();
                    lblKategori.Location = new Point(210, 35);
                    lblKategori.Text = "> " + (dr["katagori"]).ToString().ToUpper();
                    lblKategori.AutoSize = true;
                    lblKategori.Font = new Font("Calibri", 12, FontStyle.Regular);
                    pnl.Controls.Add(lblKategori);

                 
            

                    filmId = int.Parse((dr["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + filmId + "'";
                    SqlCommand kmtimg = new SqlCommand(img, foto);
                    SqlDataReader drFoto = kmtimg.ExecuteReader();
                    Image filmFoto = null;
                    byte[] resim;
                    if(drFoto.Read())
                    {
                        resim = (byte[])drFoto[0];
                        MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                        ms.Write(resim, 0, resim.Length);
                        filmFoto = Image.FromStream(ms, true);
                        Image icon = filmFoto;
                        Bitmap bmp = new Bitmap(icon, new Size(160, 180));
                        btn.Image = bmp;
                    }
                    
                    drFoto.Close();
                    foto.Close();                  



                
                    panel1.Controls.Add(pnl);


                    y_konum += 190;

                }
                baglanti.Close();
                dr.Close();
            }

               
            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("Hata Oluştu !\n" + hata.Message);
                // throw;
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("D:\\imdb\\imdb\\imdb\\bin\\Debug\\imdb.jpg");
            txtAra.Text = "Film yada Sanatçı Adı Giriniz";
            txtKullanici.Text = "yusufelosman";
            txtSifre.Text = "123456";
            getir();
            panel3.Hide();

        }



        private void button2_Click(object sender, EventArgs e)
        {
           if(!kullanıcı)
           { 
                Form uye = new uye_giris();
                uye.Show();
           }
           else
           {
               Form ayrinti = new ayrinti();
               ayrinti.Show();
           }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
                baglanti.Open();

            string checkcommand = "SELECT nick,şifre,isim,mail,soyad,id FROM Kullanici WHERE nick=@nick AND şifre=@şifre";
            SqlCommand checkcmd = new SqlCommand(checkcommand, baglanti);
            checkcmd.Parameters.AddWithValue("@nick", txtKullanici.Text);
            checkcmd.Parameters.AddWithValue("@şifre", txtSifre.Text);
            SqlDataReader dr = checkcmd.ExecuteReader();

            //--//
            if(kullanıcı)
            {
                MessageBox.Show("ÇIKIŞ İŞLEMİ BAŞARI İLE GERÇEKLEŞTİ");
                button3.Text = "GİRİŞ";
                lblAktf.Visible = false;
                lblisim.Text = "";
                lblNick.Text = "";
                lblMail.Text = "";
                kullanıcı = false;
                button2.Text = "ÜYE OL";
                dr.Close();
                panel3.Hide();
                lbloneri.Visible = false;
            }

            else if (dr.Read())//--//
            {
                MessageBox.Show("GİRİŞ İŞLEMİ BAŞARI İLE GERÇEKLEŞTİ");
                kullanıcı = true;
                isim = ((dr["isim"]).ToString().ToUpper() + " " + (dr["soyad"]).ToString().ToUpper());//--
                nick = (dr["nick"]).ToString();//--
                mail = (dr["mail"]).ToString();//--
                kid = int.Parse((dr["id"]).ToString());
                button3.Text = "ÇIKIŞ";//--
                lblAktf.Visible = true;//--
                lblisim.Text = isim;//--
                lblNick.Text = nick;//--
                lblMail.Text = mail;//-
                önerigetir();
                panel3.Show();
                button2.Text = "AYRINTILAR";
                lbloneri.Visible = true;
                dr.Close();
            }
            else
            {
                dr.Close();
                MessageBox.Show("KULLANICI ADI YADA ŞİFRE YANLIŞ!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = txtAra.Text;
            Form ara = new arama();
            ara.Show();
        }

        private void txtAra_ClientSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAra_Click(object sender, EventArgs e)
        {
            txtAra.Text = "";
        }

        private void txtAra_Leave(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form top = new top();
            top.Show();
        }
    }
}
