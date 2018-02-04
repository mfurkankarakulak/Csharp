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
    public partial class arama : Form
    {
        public arama()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
        public static int Filmid = 0;
        public static int Sanatid = 0;

        private void buton_bul(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            Sanatid = int.Parse(btn.Tag.ToString());
            Form1.santciId = Sanatid;
            Form sanat = new Sanatcilar();
            sanat.Show();
        }
        private void label_bul(object sender, EventArgs e)
        {
            
            Label lbl = sender as Label;
            Sanatid = int.Parse(lbl.Tag.ToString());
            Form1.santciId = Sanatid;
            Form sanat = new Sanatcilar();
            sanat.Text = Sanatid.ToString();
            sanat.Show();

        }
        private void buton_bul2(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            Filmid = int.Parse(btn.Tag.ToString());
            Form1.filmId = Filmid;
            Form film = new film();
            film.Show();
        }

        private void label_bul2(object sender, EventArgs e)
        {
            
            Label lbl = sender as Label;
            Filmid = int.Parse(lbl.Tag.ToString());
            Form1.filmId = Filmid;
            Form film = new film();
            film.Text = Filmid.ToString();
            film.Show();

        }

        private void getir()
        {
            int y_konum = 0;
            int x_konum = 0;

            try
            {
                baglanti.Open();

                string sanatci = "Select * from Sanatcilar WHERE isim like '%" + Form1.text + "%' OR soyad like '%" + Form1.text + "%'";
                SqlCommand kmtSanatci = new SqlCommand(sanatci, baglanti);
                SqlDataReader drSanatci = kmtSanatci.ExecuteReader();
                while (drSanatci.Read())
                {

                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(150, 170);
                    pnl.Location = new Point(x_konum, 0);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Label lblSanatci = new Label();
                    lblSanatci.Location = new Point(x_konum, 175);
                    lblSanatci.Text = ((drSanatci["isim"]).ToString().ToUpper() + " " + (drSanatci["soyad"]).ToString().ToUpper());
                    //  lblSanatci.AutoSize = true;
                    lblSanatci.Width = 160;
                    lblSanatci.Height = 36;
                    lblSanatci.Font = new Font("Calibri", 11, FontStyle.Bold);
                    lblSanatci.Tag = (drSanatci["id"]).ToString();
                    lblSanatci.Click += new EventHandler(this.label_bul);
                    panel2.Controls.Add(lblSanatci);


                    Button btn = new Button();
                    btn.Tag = (drSanatci["id"]).ToString();
                    btn.Location = new Point(0, 0);
                    btn.Size = new Size(150, 170);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul);
                    pnl.Controls.Add(btn);



                    Sanatid = int.Parse((drSanatci["id"]).ToString());

                    SqlConnection foto = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from SanatciFoto where sid='" + Sanatid + "'";
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

                string film = "Select * from Filmler WHERE isim like '%" + Form1.text + "%'";
                SqlCommand kmtFilm = new SqlCommand(film, baglanti);
                SqlDataReader drFilm = kmtFilm.ExecuteReader();

                while (drFilm.Read())
                {
                    Panel pnl = new Panel();

                    pnl.Size = new System.Drawing.Size(835, 189);
                    pnl.Location = new Point(5, y_konum);
                    pnl.BorderStyle = BorderStyle.FixedSingle;

                    Button btn = new Button();
                    btn.Tag = (drFilm["id"]).ToString();
                    btn.Location = new Point(5, 5);
                    btn.Size = new Size(160, 180);
                    btn.BackColor = Color.Green;
                    btn.Click += new EventHandler(this.buton_bul2);

                    pnl.Controls.Add(btn);


                    Label lblFilm = new Label();
                    lblFilm.Tag = (drFilm["id"]).ToString();
                    lblFilm.Location = new Point(200, 5);
                    lblFilm.Text = (drFilm["isim"]).ToString();
                    lblFilm.AutoSize = true;
                    lblFilm.Font = new Font("Calibri", 18, FontStyle.Bold);
                    lblFilm.Click += new EventHandler(this.label_bul2);
                    pnl.Controls.Add(lblFilm);

                    Label lblKategori = new Label();
                    lblKategori.Location = new Point(210, 35);
                    lblKategori.Text = "> " + (drFilm["katagori"]).ToString().ToUpper();
                    lblKategori.AutoSize = true;
                    lblKategori.Font = new Font("Calibri", 12, FontStyle.Regular);
                    pnl.Controls.Add(lblKategori);


              


                    Filmid = int.Parse((drFilm["id"]).ToString());
                    SqlConnection foto = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                    foto.Open();
                    string img = "Select foto from FilmFoto where fid='" + Filmid + "'";
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

                    SqlConnection imdb = new SqlConnection("Server=.;Database=ımdb;Trusted_Connection=True");
                    imdb.Open();
                    string oy = "SELECT AVG(cast(oy as float) ) oy FROM FilmYorum WHERE fid=@fid";
                    SqlCommand kmtoy = new SqlCommand(oy, imdb);
                    kmtoy.Parameters.AddWithValue("@fid", Filmid);
                    SqlDataReader drOy = kmtoy.ExecuteReader();
                    if (drOy.Read())
                    {
                        Label lblImdb = new Label();
                        lblImdb.Location = new Point(210, 55);
                        lblImdb.Text = "> " + ((drOy["oy"]).ToString()).ToUpper() + " PUAN";
                        if (lblImdb.Text == ">  PUAN")
                            lblImdb.Text = "> PUAN VERİLMEMİŞ";
                        lblImdb.AutoSize = true;
                        lblImdb.Font = new Font("Calibri", 11, FontStyle.Regular);
                        pnl.Controls.Add(lblImdb);
                    }

                    drOy.Close();
                    
                    imdb.Close();



                    panel1.Controls.Add(pnl);


                    y_konum += 190;

                }
                if(x_konum == 0 || y_konum == 0)
                {
                    MessageBox.Show("BULUNAMADI !");
                    this.Close();
                }
                
            }
            catch (Exception hata)
            {

                MessageBox.Show("Hata Oluştu " + hata.Message);
            }

        }
        private void arama_Load(object sender, EventArgs e)
        {

            

        }

        private void arama_Load_1(object sender, EventArgs e)
        {
            getir();
        }
    }
}
