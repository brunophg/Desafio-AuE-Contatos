using AuECadastroContatos.Models;
using AuECadastroContatos.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuECadastroContatos
{
    public partial class Form1 : Form
    {
        private ContatoRepository repositorio = new ContatoRepository();

        public Form1()
        {
            InitializeComponent();
        }

        private void AtualizarTabela()
        {
            try
            {
                dgvContatos.DataSource = null;
                dgvContatos.DataSource = repositorio.ObterTodos();

            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao carregar dados: " + e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarTabela();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCidade_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbSexo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("O campo Nome é obrigatório!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Contato novoContato = new Contato()
                {
                    Nome = txtNome.Text,
                    Cidade = txtCidade.Text,
                    Sexo = cbSexo.Text
                };

                repositorio.Inserir(novoContato);

                MessageBox.Show("Contato inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarTabela();

                txtNome.Clear();
                txtCidade.Clear();
                cbSexo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
