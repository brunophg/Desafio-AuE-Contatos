using AuECadastroContatos.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuECadastroContatos.Repositories
{
    public class ContatoRepository
    {
        private readonly string stringConexao;

        public ContatoRepository()
        {
            stringConexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\auebd.mdb;";
        }

        public List<Contato> ObterTodos()
        {
            var listaContatos = new List<Contato>();
            OleDbConnection conexao = null;
            OleDbCommand comando = null;
            OleDbDataReader reader = null;

            try
            {
                conexao = new OleDbConnection(stringConexao);
                conexao.Open();

                string query = "SELECT CodContato, Nome, Sexo, Cidade FROM Contatos";
                comando = new OleDbCommand(query, conexao);

                reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    var contato = new Contato
                    {
                        CodContato = Convert.ToInt32(reader["CodContato"]),
                        Nome = reader["Nome"].ToString(),
                        Sexo = reader["Sexo"].ToString(),
                        Cidade = reader["Cidade"].ToString()
                    };
                    listaContatos.Add(contato);
                }

            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar contatos no banco: " + e.Message);
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                if (comando != null) { comando.Dispose(); }
                if (conexao != null)
                {
                    conexao.Close();
                    conexao.Dispose();
                }
            }
            return listaContatos;
        }
        public void Inserir(Contato contato)
        {
            OleDbConnection conexao = null;
            OleDbCommand comando = null;

            try
            {
                conexao = new OleDbConnection(stringConexao);
                conexao.Open();

                string query = "INSERT INTO Contatos (Nome, Sexo, Cidade) VALUES (?, ?, ?)";
                comando = new OleDbCommand(query, conexao);

                comando.Parameters.AddWithValue("@Nome", contato.Nome);
                comando.Parameters.AddWithValue("@Sexo", contato.Sexo);
                comando.Parameters.AddWithValue("@Cidade", contato.Cidade);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inserir contato no banco: " + e.Message);
            }
            finally
            {
                if (comando != null) { comando.Dispose(); }
                if (conexao != null)
                {
                    conexao.Close();
                    conexao.Dispose();
                }
            }
        }

        public void Alterar(Contato contato)
        {
            OleDbConnection conexao = null;
            OleDbCommand comando = null;

            try
            {
                conexao = new OleDbConnection(stringConexao);
                conexao.Open();

                string query = "UPDATE Contatos SET Sexo = ?, Cidade = ? WHERE CodContato = ?";
                comando = new OleDbCommand(query, conexao);

                comando.Parameters.AddWithValue("@Sexo", contato.Sexo);
                comando.Parameters.AddWithValue("@Cidade", contato.Cidade);
                comando.Parameters.AddWithValue("@Nome", contato.Nome);
                comando.Parameters.AddWithValue("@CodContato", contato.CodContato);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao alterar contato no banco: " + e.Message);
            }
            finally
            {
                if (comando != null) { comando.Dispose(); }
                if (conexao != null)
                {
                    conexao.Close();
                    conexao.Dispose();
                }
            }
        }

        public void Excluir(int codContato)
        {
            OleDbConnection conexao = null;
            OleDbCommand comando = null;

            try
            {
                conexao = new OleDbConnection(stringConexao);
                conexao.Open();

                string query = "DELETE FROM Contatos WHERE CodContato = ?";
                comando = new OleDbCommand(query, conexao);

                comando.Parameters.AddWithValue("@CodContato", codContato);

                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao excluir contato no banco: " + e.Message);
            }
            finally
            {
                if (comando != null)
                {
                    comando.Dispose();
                }
                if (conexao != null)
                {
                    conexao.Close();
                }
            }

        }
    }
}
