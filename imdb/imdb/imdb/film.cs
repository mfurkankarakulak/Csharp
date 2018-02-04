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
    public partial class film : Form
    {
        public film()
        {
            InitializeComponent();
        }

        int id = Form1.filmId;

        private void yorum()
        {
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();
            string yorum = "Select fy.kid,fy.tarih,fy.yorum,fy.oy from FilmYorum fy  where kid in (select kid from Kullanici where fid=@id) ";
            SqlCommand kmtYorum = new SqlCommand(yorum, baglanti);
            kmtYorum.Parameters.AddWithValue("@id", id);
            SqlDataReader drYorum = kmtYorum.ExecuteReader();


            int y = -25;
            int x = 0;
            while (drYorum.Read())
            {
                int kid = (int.Parse((drYorum["kid"]).ToString()));
                ///////////////
                SqlConnection diger = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                string nick = "select nick from Kullanici where id =@kid";
                diger.Open();
                SqlCommand kmtNick = new SqlCommand(nick, diger);
                kmtNick.Parameters.AddWithValue("@kid", kid);
                SqlDataReader drNick = kmtNick.ExecuteReader();
                if (drNick.Read())
                    nick = (drNick["nick"]).ToString() + "  --  " + (drYorum["tarih"]).ToString().Substring(0, 10) + "  --- Puan :  " + (drYorum["oy"]).ToString();

                Label lblNick = new Label();
                y += lblNick.Height+5;
                lblNick.Location = new Point(5, y);
                lblNick.AutoSize = true;
                lblNick.Text = nick;
                lblNick.Font = new Font("Calibri", 10, FontStyle.Bold);
                panel16.Controls.Add(lblNick);
                x += lblNick.Width;
                diger.Close();
                drNick.Close();
                /////////////////

                Label lblyrm = new Label();
                y += lblyrm.Height;
                lblyrm.AutoSize = true;
                lblyrm.Location = new Point(5, y);
                lblyrm.Text = (drYorum["yorum"]).ToString();
                lblyrm.Font = new Font("Calibri", 10, FontStyle.Regular);
                panel16.Controls.Add(lblyrm);

                diger.Open();
                string cvp = "select * from FilmCevap where fid=@id and kid=@kid";
                SqlCommand kmtCvp = new SqlCommand(cvp, diger);
                kmtCvp.Parameters.AddWithValue("@id", id);
                kmtCvp.Parameters.AddWithValue("@kid", kid);
                SqlDataReader drCvp = kmtCvp.ExecuteReader();
                while (drCvp.Read())
                {
                    SqlConnection kc = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
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
                        lblCvp.Text = ((drKc["nick"]).ToString() + "   ---   " + (drCvp["fctarih"]).ToString().Substring(0, 10));
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
                y += lblyrm.Height+5;
                btn.Location = new Point(240, y);
                btn.Size = new Size(85, 25);                
                btn.Click += new EventHandler(this.cevap_ver);
                btn.Font = new Font("Calibri", 10, FontStyle.Regular);
                btn.Text = "Cevap Ver";
                btn.BackColor = Color.GhostWhite;
                panel16.Controls.Add(btn);
            }

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
                

                if(Form1.kid == yorumKid)
                {
                    MessageBox.Show("KENDİ YORUMUNUZA CEVAP VEREMEZSİNİZ");
                    cvp = false;
                    return;
                }


                SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
                baglanti.Open();
                string yorumSorgu = "Select * from FilmCevap where fid=@fid and kid=@kid and kcid=@kcid";
                SqlCommand kmtSorgu = new SqlCommand(yorumSorgu, baglanti);
                kmtSorgu.Parameters.AddWithValue("@fid", Form1.filmId);
                kmtSorgu.Parameters.AddWithValue("@kid", yorumKid);
                kmtSorgu.Parameters.AddWithValue("@kcid", Form1.kid);
                SqlDataReader dr = kmtSorgu.ExecuteReader();


                if(dr.Read())
                {                     
                    MessageBox.Show("BU YORUMA ZATEN CEVAP VERMİŞSİNİZ\n\nTARİH : " + dr["fctarih"].ToString().Substring(0, 10) + "\n\nYORUM : " + dr["cevap"].ToString());
                    cvp = false;
                }

                
                dr.Close();
                baglanti.Close();

            }

            if (cvp)
            {
                Form1.fsyc = 2;
                Form yrm = new Yorum_Yap();
                yrm.Show();

            }


        }

        private void film_Load(object sender, EventArgs e)
        {
            yorum();
            int id = Form1.filmId;
           
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");

          /*image icon = Image.FromFile("D:\\imdb\\imdb\\imdb\\bin\\Debug\\imdb.jpg");
            Bitmap bmp = new Bitmap(icon, new Size(183,235));
            panel4.BackgroundImage = bmp;*/

            panel4.HorizontalScroll.Visible = false;
            panel4.HorizontalScroll.Enabled = false;
            panel4.VerticalScroll.Visible = false;

            baglanti.Open();
            string img = "Select foto from FilmFoto where fid='" + id + "'";
            SqlCommand kmtimg = new SqlCommand(img, baglanti);
            SqlDataReader drFoto = kmtimg.ExecuteReader();
            Image filmFoto = null;
            byte[] resim;
            int  yf =0;
            while (drFoto.Read())
            {
                resim = (byte[])drFoto[0];
                MemoryStream ms = new MemoryStream(resim, 0, resim.Length);
                ms.Write(resim, 0, resim.Length);
                filmFoto = Image.FromStream(ms, true);
                Image icon = filmFoto;
                Bitmap bmp = new Bitmap(icon, new Size(166, 235));

                PictureBox pictureBox1 = new PictureBox();
                pictureBox1.Location = new System.Drawing.Point(0,yf);
                pictureBox1.Name = "pictureBox1";
                pictureBox1.Size = new System.Drawing.Size(166, 235);
                pictureBox1.Image = bmp;
                panel4.Controls.Add(pictureBox1);
                yf += 235;                
            }
            drFoto.Close();
            baglanti.Close(); 


            


            baglanti.Open();
            string film = "Select * from Filmler where id=@id";
            SqlCommand kmtHky = new SqlCommand(film, baglanti);
            kmtHky.Parameters.AddWithValue("@id", id);            
            SqlDataReader dr = kmtHky.ExecuteReader();
            if(dr.Read())
            {
                this.Text = dr["isim"].ToString().ToUpper();

                Label lblFilm = new Label();
                lblFilm.Tag = (dr["id"]).ToString();
                lblFilm.Location = new Point(0,0);
                lblFilm.Text = (dr["isim"]).ToString();
                lblFilm.AutoSize = true;
                lblFilm.Font = new Font("Calibri", 18, FontStyle.Bold);
                panel1.Controls.Add(lblFilm);

                Label lblKategori = new Label();
                lblKategori.Location = new Point(0,0);
                lblKategori.Text = (dr["katagori"]).ToString().ToUpper();
                lblKategori.AutoSize = true;
                lblKategori.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel2.Controls.Add(lblKategori);


                Label lblTarih = new Label();
                lblTarih.Location = new Point(0,0);
                lblTarih.Text = ((dr["tarih"]).ToString()).Substring(0, 10).ToUpper();
                lblTarih.AutoSize = true;
                lblTarih.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel12.Controls.Add(lblTarih);

                Label lblDil = new Label();
                lblDil.Location = new Point(0,0);
                lblDil.Text = ((dr["dil"]).ToString()).ToUpper();
                lblDil.AutoSize = true;
                lblDil.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel6.Controls.Add(lblDil);

                Label lblUlke = new Label();
                lblUlke.Location = new Point(0,0);
                lblUlke.Text = ((dr["ülke"]).ToString()).ToUpper();
                lblUlke.AutoSize = true;
                lblUlke.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel7.Controls.Add(lblUlke);

                Label lblSure = new Label();
                lblSure.Location = new Point(0,0);
                lblSure.Text = ((dr["süre"]).ToString()).ToUpper() + " DK";
                lblSure.AutoSize = true;
                lblSure.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel5.Controls.Add(lblSure);

                Label lblmal = new Label();
                lblmal.Location = new Point(0,0);
                lblmal.Text = ((dr["maliyet"]).ToString()).ToUpper();
                lblmal.AutoSize = true;
                lblmal.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel8.Controls.Add(lblmal);

                Label lbKazanc = new Label();
                lbKazanc.Location = new Point(0, 0);                
                lbKazanc.Text = ((dr["kazanc"]).ToString());
                lbKazanc.AutoSize = true;
                lbKazanc.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel9.Controls.Add(lbKazanc);

                Label lblHky = new Label();
                lblHky.Width = 490;
                lblHky.Height = 1000;
                lblHky.Location = new Point(5,0);
                lblHky.Text = ((dr["hikaye"]).ToString()).ToUpper();
              //  lblHky.AutoSize = true;
                lblHky.Font = new Font("Calibri", 10, FontStyle.Regular);
                panel14.Controls.Add(lblHky);

                
            }
            dr.Close();
            string oy = "SELECT AVG(cast(oy as float) ) oy FROM FilmYorum WHERE fid=@fid";
            SqlCommand kmtoy = new SqlCommand(oy, baglanti);
            kmtoy.Parameters.AddWithValue("@fid", id);
            SqlDataReader drOy = kmtoy.ExecuteReader();
            if(drOy.Read())
            {
                Label lblImdb = new Label();
                lblImdb.Location = new Point(0, 0);
                lblImdb.Text = ((drOy["oy"]).ToString()).ToUpper();
                if(lblImdb.Text == "")
                    lblImdb.Text = "PUAN VERİLMEMİŞ.";
                lblImdb.AutoSize = true;
                lblImdb.Font = new Font("Calibri", 11, FontStyle.Regular);
                panel3.Controls.Add(lblImdb);
            }
            

            drOy.Close();



///////////////////////////////////////////////////////////////////////////////////////////////////////
            string yntmn = "";
            string yonetmen = "Select s.isim,s.soyad from filmSanatci f ,Sanatcilar s where f.fid=@id and f.sid =  s.id and s.byönetmen = 1 ";
            SqlCommand kmtYonetmen = new SqlCommand(yonetmen, baglanti);
            kmtYonetmen.Parameters.AddWithValue("@id", id);
            SqlDataReader drYonetmen = kmtYonetmen.ExecuteReader();
            while(drYonetmen.Read())
            {
                
                yntmn += (drYonetmen["isim"]).ToString().ToUpper();
                yntmn += " " + (drYonetmen["soyad"]).ToString().ToUpper() + " ,";
                
            }
            Label lblYonetmen = new Label();
            lblYonetmen.Location = new Point(0, 0);
            lblYonetmen.AutoSize = true;
            lblYonetmen.Text = yntmn;
            lblYonetmen.Font = new Font("Calibri", 10, FontStyle.Regular);
            panel11.Controls.Add(lblYonetmen);
            drYonetmen.Close();
//////////////////////////////////////////////////////////////////////////////////////////////////////
            string yzr = "";
            string yazar = "Select s.isim,s.soyad from filmSanatci f ,Sanatcilar s where f.fid=@id and f.sid =  s.id and s.byazar = 1 ";
            SqlCommand kmtYazar = new SqlCommand(yazar, baglanti);
            kmtYazar.Parameters.AddWithValue("@id", id);
            SqlDataReader drYazar = kmtYazar.ExecuteReader();

            while (drYazar.Read())
            {                
                yzr += (drYazar["isim"]).ToString().ToUpper();
                yzr += " " + (drYazar["soyad"]).ToString().ToUpper() + " ,";                
            }
            drYazar.Close();
            Label lblYzr = new Label();
            lblYzr.Location = new Point(0, 0);
            lblYzr.AutoSize = true;
            lblYzr.Text = yzr;
            lblYzr.Font = new Font("Calibri", 10, FontStyle.Regular);
            panel13.Controls.Add(lblYzr);
//////////////////////////////////////////////////////////////////////////////////////////////////////

            string oyn = "";
            string oyuncu = "Select s.isim,s.soyad from filmSanatci f ,Sanatcilar s where f.fid=@id and f.sid =  s.id and s.boyuncu = 1 ";
            SqlCommand kmtOyuncu = new SqlCommand(oyuncu, baglanti);
            kmtOyuncu.Parameters.AddWithValue("@id", id);
            SqlDataReader drOyuncu = kmtOyuncu.ExecuteReader();

            while (drOyuncu.Read())
            {
                oyn += (drOyuncu["isim"]).ToString().ToUpper();
                oyn += " " + (drOyuncu["soyad"]).ToString().ToUpper() + " ,";                
            }
            drOyuncu.Close();
            Label lblOyn = new Label();
            lblOyn.Location = new Point(0, 0);
            lblOyn.AutoSize = true;
            lblOyn.Text = oyn;
            lblOyn.Font = new Font("Calibri", 10, FontStyle.Regular);
            
            panel15.Controls.Add(lblOyn);
////////////////////////////////////////////////////////////////////////////////////////////////////

            string odul = "";
            string odulSel = "Select isim,tarih from FilmÖdül where fid=@id";
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

            panel17.Controls.Add(lblOdul);
///////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            
            baglanti.Close();
 ///////////////////////////////////////////////////////////////////////////////////////              
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {
            
       
             
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();
            string film = "Select * from Filmler where id=@id";
            SqlCommand kmtHky = new SqlCommand(film, baglanti);
            kmtHky.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = kmtHky.ExecuteReader();
            string fragman="";
            if (dr.Read())
                fragman = (dr["link"]).ToString();
            dr.Close();
            baglanti.Close();

            System.Diagnostics.Process.Start(fragman);
        }

        //--//
        private void button1_Click(object sender, EventArgs e)
        {
            bool yorm = false;
           if(!Form1.kullanıcı)
           {
               MessageBox.Show("YORUM YAPABİLMEK İÇİN LÜTFEN GİRİŞ YAPINIZ");
           }
           else
           {
               yorm = true;
               SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
               baglanti.Open();
               string yorumSorgu = "Select * from FilmYorum where fid=@fid and kid=@kid";
               SqlCommand kmtSorgu = new SqlCommand(yorumSorgu, baglanti);
               kmtSorgu.Parameters.AddWithValue("@fid", Form1.filmId);
               kmtSorgu.Parameters.AddWithValue("@kid", Form1.kid);
               SqlDataReader dr = kmtSorgu.ExecuteReader();
               if(dr.Read())
               {
                   MessageBox.Show("BU FİLME ZATEN YORUM YAPMIŞSINIZ\n\nTARİH : " + dr["tarih"].ToString().Substring(0,10) + "\n\nYORUM : " + dr["yorum"].ToString() + "\n\nOY : " + dr["oy"].ToString());
                   yorm = false;
               }

              

               dr.Close();
               baglanti.Close();

           }

           if(yorm)
           {
               Form1.fsyc = 1;
               Form yrm = new Yorum_Yap();
               yrm.Show();
               
           }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();

            if(Form1.kullanıcı)
            {
               
                string izlenecek = "select * from izlenecekler Where kid = @kid AND fid = @fid ";
                SqlCommand kmtizlenecek = new SqlCommand(izlenecek, baglanti);
                kmtizlenecek.Parameters.AddWithValue("@kid", Form1.kid);
                kmtizlenecek.Parameters.AddWithValue("@fid", Form1.filmId);
                SqlDataReader drizlenecek = kmtizlenecek.ExecuteReader();

                if(drizlenecek.Read())
                {
                    MessageBox.Show("BU FİLMİ DAHA ÖNCE LİSTENİZE EKLEMİŞSİNİZ!");
                    drizlenecek.Close();
                    baglanti.Close();
                }
                else
                {
                    drizlenecek.Close();
                    string kayit = "insert into izlenecekler(kid,fid) values (@kid,@fid)";
                    SqlCommand kmtekle = new SqlCommand(kayit, baglanti);
                    kmtekle.Parameters.AddWithValue("@kid", Form1.kid);
                    kmtekle.Parameters.AddWithValue("@fid", Form1.filmId);
                    kmtekle.ExecuteNonQuery();
                    MessageBox.Show("İZLENECEKLERE EKLENDİ");
                    baglanti.Close();
                    drizlenecek.Close();
                }
                
             

            }
            else
            {

                MessageBox.Show("KULLANICI GİRİŞİ YAPINIZ.");
                baglanti.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();

            if (Form1.kullanıcı)
            {

                string fav = "select * from favori Where kid = @kid AND fid = @fid ";
                SqlCommand kmtfav = new SqlCommand(fav, baglanti);
                kmtfav.Parameters.AddWithValue("@kid", Form1.kid);
                kmtfav.Parameters.AddWithValue("@fid", Form1.filmId);
                SqlDataReader drfav = kmtfav.ExecuteReader();

                if (drfav.Read())
                {
                    MessageBox.Show("BU FİLMİ DAHA ÖNCE FAVORİLERİNİZE EKLEMİŞSİNİZ!");
                    drfav.Close();
                    baglanti.Close();
                }
                else
                {
                    drfav.Close();
                    string kayit = "insert into favori(kid,fid) values (@kid,@fid)";
                    SqlCommand kmtekle = new SqlCommand(kayit, baglanti);
                    kmtekle.Parameters.AddWithValue("@kid", Form1.kid);
                    kmtekle.Parameters.AddWithValue("@fid", Form1.filmId);
                    kmtekle.ExecuteNonQuery();
                    MessageBox.Show("FAVORİLERE EKLENDİ");
                    baglanti.Close();
                    drfav.Close();
                }



            }
            else
            {

                MessageBox.Show("KULLANICI GİRİŞİ YAPINIZ.");
                baglanti.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Server=desktop-bo0ddrv;Database=ımdb;Trusted_Connection=True");
            baglanti.Open();

            if (Form1.kullanıcı)
            {

                string izlenen = "select * from izlenen Where kid = @kid AND fid = @fid ";
                SqlCommand kmtizlenen = new SqlCommand(izlenen, baglanti);
                kmtizlenen.Parameters.AddWithValue("@kid", Form1.kid);
                kmtizlenen.Parameters.AddWithValue("@fid", Form1.filmId);
                SqlDataReader drizlenen = kmtizlenen.ExecuteReader();

                if (drizlenen.Read())
                {
                    MessageBox.Show("BU FİLMİ DAHA ÖNCE İZLEDİGİNİZ FİLMLERE EKLEMİŞSİNİZ!");
                    drizlenen.Close();
                    baglanti.Close();
                }
                else
                {
                    drizlenen.Close();
                    string kayit = "insert into izlenen(kid,fid) values (@kid,@fid)";
                    SqlCommand kmtekle = new SqlCommand(kayit, baglanti);
                    kmtekle.Parameters.AddWithValue("@kid", Form1.kid);
                    kmtekle.Parameters.AddWithValue("@fid", Form1.filmId);
                    kmtekle.ExecuteNonQuery();
                    MessageBox.Show("İZLENENLERE EKLENDİ");
                    baglanti.Close();
                    drizlenen.Close();
                }



            }
            else
            {

                MessageBox.Show("KULLANICI GİRİŞİ YAPINIZ.");
                baglanti.Close();
            }
        }
        //--//

    }
}
