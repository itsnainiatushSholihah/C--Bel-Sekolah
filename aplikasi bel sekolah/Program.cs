using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Component;
using Database;
using System.Threading;

namespace aplikasi_bel_sekolah
{
    class Program
    {
         public static AccessDatabaseHelper DB = new AccessDatabaseHelper("./bel_sekolah.accdb");

        static void Main(string[] args)
        {

            Console.Title = "APLIKASI BEL SEKOLAH";
            Console.CursorVisible = false;

            Kotak Atas = new Kotak();
            Atas.SetXY(0, 0);
            Atas.SetWidthAndHeight(110,5);
            Atas.Tampil();

            Kotak Bawah = new Kotak();
            Bawah.SetXY(27, 25);
            Bawah.SetWidthAndHeight(55, 3);
            Bawah.Tampil();

            Kotak Bawah4 = new Kotak();
            Bawah4.SetXY(0, 25);
            Bawah4.SetWidthAndHeight(26, 3);
            Bawah4.SetForeColor(ConsoleColor.White);
            Bawah4.Tampil();

            Kotak Bawah5 = new Kotak();
            Bawah5.SetXY(83, 25);
            Bawah5.SetWidthAndHeight(27, 3);
            Bawah5.Tampil();

            Kotak Kiri = new Kotak();
            Kiri.SetXY(0, 6);
            Kiri.SetWidthAndHeight(26, 18);
            Kiri.Tampil();

            Kotak Kanan = new Kotak();
    
            Kanan.SetXY(27,6);
            Kanan.SetWidthAndHeight(83,18);
            Kanan.Tampil();

            Tulisan NamaAplikasi = new Tulisan();
            NamaAplikasi.Text = "APLIKASI BEL SEKOLAH";
            NamaAplikasi.SetXY(0, 1).SetLength(110);
            NamaAplikasi.SetForeColor(ConsoleColor.White);
            NamaAplikasi.TampilTengah();

            Tulisan Sekolah = new Tulisan();
            Sekolah.Text = "WEARNES EDUCATION CENTER MADIUN";
            Sekolah.SetXY(0, 2).SetLength(110);
            Sekolah.SetForeColor(ConsoleColor.White);
            Sekolah.TampilTengah();

            Tulisan AlamatSekolah = new Tulisan();
            AlamatSekolah.Text = "JL THAMRIN 35 MADIUN";
            AlamatSekolah.SetXY(0, 3).SetLength(110);
            AlamatSekolah.TampilTengah();

            Tulisan Bawah2 = new Tulisan();
            Bawah2.Text = "ITSNAINIATUSH SHOLIHAH";
            Bawah2.SetXY(0, 26).SetLength(110);
            Bawah2.SetForeColor(ConsoleColor.White);
            Bawah2.TampilTengah();

            Tulisan Bawah3 = new Tulisan();
            Bawah3.Text = "INFORMATIKA II - BESTFALT";
            Bawah3.SetXY(0, 27).SetLength(110);
            Bawah3.SetForeColor(ConsoleColor.White);
            Bawah3.TampilTengah();

            Menu menu = new Menu("JALANKAN", "LIHAT DATA", "TAMBAHKAN DATA", "EDIT DATA", "HAPUS DATA", "KELUAR");
            menu.SetXY(7, 10);
            menu.SetForeColor(ConsoleColor.White);
            menu.SelectedForeColor = ConsoleColor.Black;
            menu.Tampil();

            bool IsiProgram = true;

            while (IsiProgram)
            {
                ConsoleKeyInfo Tombol = Console.ReadKey(true);

                if (Tombol.Key == ConsoleKey.DownArrow)
                {
                    menu.Next();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.UpArrow)
                {
                    menu.Prev();
                    menu.Tampil();
                }
                else if (Tombol.Key == ConsoleKey.Enter)
                {
                    int MenuDipilih = menu.SelectedIndex;

                    if (MenuDipilih==0)
                    {
                        Jalankan();
                    }
                    else if (MenuDipilih == 1)
                    {
                        LihatData();
                    }
                    else if (MenuDipilih == 2)
                    {
                        TambahData();
                    }
                    else if(MenuDipilih == 3)
                    {
                        EditData();
                    }
                    else if(MenuDipilih == 4)
                    {
                        HapusData();
                    }
                    else if(MenuDipilih == 5)
                    {
                        IsiProgram = false;

                    }

                }
            }

            

            Console.ReadKey();
        }

       static void Jalankan()
        {
            new Clear(32, 7, 78, 16).Tampil();

            Tulisan judul = new Tulisan();
            judul.SetXY(31, 7).SetText(".:BEL SEKOLAH SEDANG BERJALAN:.").SetLength(79);
            judul.TampilTengah();

            Tulisan HariSekarang = new Tulisan().SetXY(33, 9);
            Tulisan JamSekarang = new Tulisan().SetXY(33, 10);

            string Qselect = "SELECT * FROM jadwal_bel WHERE hari=@hari AND jam=@jam;";

            DB.Connect();

            bool play = true;

            while (play)
            {
                DateTime Sekarang = DateTime.Now;

                HariSekarang.SetText("HARI SEKARANG :" + Sekarang.ToString("dddd"));
                HariSekarang.Tampil();

                JamSekarang.SetText("JAM SEKARANG :" + Sekarang.ToString("HH:mm:ss"));
                JamSekarang.Tampil();

                DataTable DT = DB.RunQuery(Qselect,
                    new OleDbParameter("@hari", Sekarang.ToString("dddd")),
                    new OleDbParameter("@jam", Sekarang.ToString("HH:mm")));

                if(DT.Rows.Count > 0)
                {
                    Audio HAAA = new Audio();
                    HAAA.File = "./Suara/" + DT.Rows[0]["sound"];
                    HAAA.Play();

                    new Tulisan().SetXY(31, 14).SetText("BEL TELAH BERBUNYI!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
                    new Tulisan().SetXY(31, 15).SetText(DT.Rows[0]["ket"].ToString()).SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();

                    play = false;

                }
                
                

                if(Console.KeyAvailable == true)
                {
                    ConsoleKeyInfo Tombol = Console.ReadKey(true);
                    if (Tombol.Key == ConsoleKey.Escape)
                    {
                        play = false;
                        new Clear(32, 7, 78, 16).Tampil();

                    }
                }
                

                Thread.Sleep(1000);


            }

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: LIHAT DATA JADWAL :.").SetLength(31).TampilTengah();

        }

        static void LihatData()
        {
            new Clear(32, 7, 78, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: LIHAT DATA JADWAL :.").SetLength(79).TampilTengah();

            DB.Connect();
            DataTable DT = DB.RunQuery("SELECT * FROM jadwal_bel;");
            DB.Disconnect();

            new Tulisan("┌───┬──────────┬─────────┬─────────────────────┐").SetXY(34, 10).Tampil();
            new Tulisan("│NO │ HARI     │ JAM     │   KETERANGAN        │").SetXY(34, 11).Tampil();
            new Tulisan("├───┼──────────┼─────────┼─────────────────────┤").SetXY(34, 12).Tampil();

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string ID = DT.Rows[i]["id"].ToString();
                string Hari = DT.Rows[i]["hari"].ToString();
                string Jam = DT.Rows[i]["jam"].ToString();
                string keterangan = DT.Rows[i]["ket"].ToString();

                string isi = string.Format("|{0, -3}|{1,-10}|{2,-9}|{3,-21}|", ID, Hari, Jam, keterangan);
                new Tulisan(isi).SetXY(34, 13 + i).Tampil();
            }

            new Tulisan("└───┴──────────┴─────────┴─────────────────────┘").SetXY(34, 13 + DT.Rows.Count).Tampil();

        }




        static void TambahData()
        {
            new Clear(32, 7, 78, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: LIHAT DATA JADWAL :.").SetLength(75).TampilTengah();

            Inputan HariInput = new Inputan();
            HariInput.Text = "masukkan hari :";
            HariInput.SetXY(33, 9);

            Inputan JamInput = new Inputan();
            JamInput.Text = "masukkan Jam :";
            JamInput.SetXY(33, 10);

            Inputan KetInputan = new Inputan();
            KetInputan.Text = "Masukkan Keterangan :";
            KetInputan.SetXY(33, 11);

            //Inputan Soundinputan = new Inputan();
            //Soundinputan.Text = "Masukkan Sound :";
            //Soundinputan.SetXY(33, 12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "pembuka.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(33, 12);
            
                
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInputan.Read();
            string Sound = SoundInput.Read();

            DB.Connect();
            DB.RunNonQuery("INSERT INTO  jadwal_bel ( hari, jam, ket, sound) VALUES  (@hari, @jam, @ket, @sound);",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound)
                );
          
            DB.Disconnect();

            new Tulisan().SetXY(31, 14).SetText("Data Berhasil Di Simpan!!!! ").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();
        }
        static void EditData()
        {
            new Clear(32, 7, 78, 16).Tampil();

            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: EDIT DATA JADWAL :.").SetLength(88).TampilTengah();

            Inputan IDInputDirubah = new Inputan();
            IDInputDirubah.Text = "Edit Data ID yang diinginkan :";
            IDInputDirubah.SetXY(33, 9);
            IDInputDirubah.Tampil();

            Inputan HariInput = new Inputan();
            HariInput.Text = "masukkan hari :";
            HariInput.SetXY(33, 10);

            Inputan JamInput = new Inputan();
            JamInput.Text = "masukkan Jam :";
            JamInput.SetXY(33, 11);

            Inputan KetInputan = new Inputan();
            KetInputan.Text = "Masukkan Keterangan :";
            KetInputan.SetXY(33, 12);

            //Inputan Soundinputan = new Inputan();
            //Soundinputan.Text = "Masukkan Sound :";
            //Soundinputan.SetXY(33, 12);

            Pilihan SoundInput = new Pilihan();
            SoundInput.SetPilihans(
                "5 Menit Akhir Istirahat I.wav",
                "5 Menit Akhir Istirahat II.wav",
                "5 Menit Akhir Kegiatan Keagamaan.wav",
                "5 Menit Awal Kegiatan Keagamaan.wav",
                "5 Menit Awal Upacara",
                "Akhir Pekan 1.wav",
                "Akhir Pekan 2.wav",
                "Akhir Pelajaran A.wav",
                "Akhir Pelajaran B.wav",
                "awal jam ke 1.wav",
                "Istirahat I.wav",
                "Istirahat II.wav",
                "Pelajaran ke 1.wav",
                "Pelajaran ke 2.wav",
                "Pelajaran ke 3.wav",
                "Pelajaran ke 4.wav",
                "Pelajaran ke 5.wav",
                "Pelajaran ke 6.wav",
                "Pelajaran ke 7.wav",
                "Pelajaran ke 8.wav",
                "Pelajaran ke 9.wav",
                "pembuka.wav");

            SoundInput.Text = "Masukkan Audio :";
            SoundInput.SetXY(33, 14);

            string IDRubah = IDInputDirubah.Read();
            string Hari = HariInput.Read();
            string Jam = JamInput.Read();
            string Ket = KetInputan.Read();
            string Sound = SoundInput.Read();


            DB.Connect();
            DB.RunNonQuery("UPDATE jadwal_bel SET hari=@hari, jam=@jam, ket=@ket, sound=@sound WHERE id=@id;",
                new OleDbParameter("@hari", Hari),
                new OleDbParameter("@jam", Jam),
                new OleDbParameter("@ket", Ket),
                new OleDbParameter("@sound", Sound),
                new OleDbParameter("@id", IDRubah)
                );

            DB.Disconnect();

            new Tulisan().SetXY(31, 16).SetText("Data Berhasil Di UPDATE!!!").SetBackColor(ConsoleColor.Red).SetLength(79).TampilTengah();

        }

        static void HapusData()
        {
            new Clear(32, 7, 78, 16).Tampil();
                                                                                                     
            Tulisan Judul = new Tulisan();
            Judul.SetXY(31, 7).SetText(".: HAPUS DATA JADWAL :.").SetLength(79).TampilTengah();

            Inputan IDinput = new Inputan();
            IDinput.Text = "masukkan id yang akan dihapus:";
            IDinput.SetXY(33, 9);
            string ID = IDinput.Read();

            DB.Connect();
            DB.RunNonQuery("DELETE FROM jadwal_bel WHERE id=@id",
                new OleDbParameter("@id", ID));
            DB.Disconnect();

            new Tulisan().SetXY(31, 12).SetText("Data berhasil dihapus !!!").SetBackColor(ConsoleColor.Red).SetLength(75).TampilTengah();

        }
    }
}
