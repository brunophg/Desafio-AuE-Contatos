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
        private int idSelecionado = 0;

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

        private void dgvContatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow linha = dgvContatos.Rows[e.RowIndex];

                    idSelecionado = Convert.ToInt32(linha.Cells["CodContato"].Value);

                    txtNome.Text = linha.Cells["Nome"].Value.ToString();
                    cbSexo.Text = linha.Cells["Sexo"].Value.ToString();
                    txtCidade.Text = linha.Cells["Cidade"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao selecionar contato: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSelecionado == 0)
                {
                    MessageBox.Show("Por favor, Selecione um contato na tabela para alterar.", "Aviso", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNome.Text))
                {
                    MessageBox.Show("O campo Nome é obrigatório", "Aviso", MessageBoxButtons.OK);
                    return;
                }

                Contato contatoAtualizado = new Contato()
                {
                    CodContato = idSelecionado,
                    Nome = txtNome.Text,
                    Cidade = txtCidade.Text,
                    Sexo = cbSexo.Text
                };

                repositorio.Alterar(contatoAtualizado);
                MessageBox.Show("Contato alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarTabela();

                txtNome.Clear();
                txtCidade.Clear();
                cbSexo.SelectedIndex = -1;

                idSelecionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSelecionado == 0)
                {
                    MessageBox.Show("Por favor, selecione um contato na tabela para excluir", "Aviso", MessageBoxButtons.OK);
                    return;
                } 

                repositorio.Excluir(idSelecionado);

                MessageBox.Show("Contato excluído", "Sucesso", MessageBoxButtons.OK);

                AtualizarTabela();

                txtNome.Clear();
                txtCidade.Clear();
                cbSexo.SelectedIndex = -1;

                
                idSelecionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK);
            }
        }
    }
}