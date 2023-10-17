using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplikasiInputDataMahasiswa
{
    public partial class Form1 : Form
    {
        private List<Mahasiswa> list = new List<Mahasiswa> ();
        public Form1()
        {
            InitializeComponent();
            InisialisasiListView();
        }

        private void InisialisasiListView()
        {
            lvwMahasiswa.View = View.Details;
            lvwMahasiswa.FullRowSelect = true;
            lvwMahasiswa.GridLines = true;
            lvwMahasiswa.Columns.Add("No.", 30, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nim", 91, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nama", 200, HorizontalAlignment.Left);
            lvwMahasiswa.Columns.Add("Kelas", 70, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai", 50, HorizontalAlignment.Center);
            lvwMahasiswa.Columns.Add("Nilai Huruf", 100, HorizontalAlignment.Center);
        }

        private void ResetForm()
        {
            textNim.Clear();
            textNama.Clear();
            textKelas.Clear();
            textNilai.Text = "0";
            textNim.Focus();
        }

        private bool NumericOnly(KeyPressEventArgs e)
        {
            var strValid = "0123456789";
            if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                // inputan selain angka
                if (strValid.IndexOf(e.KeyChar) < 0)
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private void TampilkanData()
        {
            // kosongkan data listview
            lvwMahasiswa.Items.Clear();
            // lakukan perulangan untuk menampilkan data mahasiswa ke listview
            foreach (var mhs in list)
            {
                var nilaiHuruf = ' ';

                if (mhs.Nilai <= 20)
                {
                    nilaiHuruf = 'E';
                } else if (mhs.Nilai > 20 && mhs.Nilai <= 40)
                {
                    nilaiHuruf = 'D';
                } else if (mhs.Nilai > 40 && mhs.Nilai <= 60)
                {
                    nilaiHuruf = 'C';
                } else if (mhs.Nilai > 60 && mhs.Nilai <= 80)
                {
                    nilaiHuruf = 'B';
                } else if (mhs.Nilai > 80 && mhs.Nilai <= 100)
                {
                    nilaiHuruf = 'A';
                }


                var noUrut = lvwMahasiswa.Items.Count + 1;
                var item = new ListViewItem(noUrut.ToString());
                item.SubItems.Add(mhs.Nim);
                item.SubItems.Add(mhs.Nama);
                item.SubItems.Add(mhs.Kelas);
                item.SubItems.Add(mhs.Nilai.ToString());
                item.SubItems.Add(nilaiHuruf.ToString());
                lvwMahasiswa.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // membuat objek mahasiswa
            Mahasiswa mhs = new Mahasiswa();
            // set nilai masing-masing propertynya
            // berdasarkan inputan yang ada di form
            mhs.Nim = textNim.Text;
            mhs.Nama = textNama.Text;
            mhs.Kelas = textKelas.Text;
            mhs.Nilai = int.Parse(textNilai.Text);
            // tambahkan objek mahasiswa ke dalam collection
            list.Add(mhs);
            var msg = "Data mahasiswa berhasil disimpan.";
            // tampilkan dialog informasi
            MessageBox.Show(msg, "Informasi", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            // reset form input
            ResetForm();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void textNilai_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = NumericOnly(e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TampilkanData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // cek apakah data mahasiswa sudah dipilih
            if (lvwMahasiswa.SelectedItems.Count > 0)
            {
                // tampilkan konfirmasi
                var konfirmasi = MessageBox.Show("Apakah data mahasiswa ingin dihapus ? ", "Konfirmasi",
               
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (konfirmasi == DialogResult.Yes)
                {
                    // ambil index list yang di pilih
                    var index = lvwMahasiswa.SelectedIndices[0];
                    // hapus objek mahasiswa dari list
                    list.RemoveAt(index);
                    // refresh tampilan listivew
                    TampilkanData();
                }
            }
            else // data belum dipilih
            {
                MessageBox.Show("Data mahasiswa belum dipilih !!!", "Peringatan",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
