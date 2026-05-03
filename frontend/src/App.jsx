import { useState, useEffect } from 'react'

function App() {
  // Estado para controlar qual "visão" mostrar 
  const [visao, setVisao] = useState('profissionais'); // 'profissionais' ou 'agendamentos'
  
  // Estados para Recursos
  const [recursos, setRecursos] = useState([]);
  const [nomeRecurso, setNomeRecurso] = useState('');
  const [especialidade, setEspecialidade] = useState('');

  // Estados para Agendamentos
  const [agendamentos, setAgendamentos] = useState([]);
  const [clienteNome, setClienteNome] = useState('');
  const [recursoSelecionado, setRecursoSelecionado] = useState('');
  const [dataHora, setDataHora] = useState('');

  const API_BASE = 'http://localhost:5158/api';

  // Carregamento de dados
  const carregarDados = async () => {
    try {
      const resRec = await fetch(`${API_BASE}/Recursos`);
      setRecursos(await resRec.json());
      
      const resAge = await fetch(`${API_BASE}/Agendamentos`);
      setAgendamentos(await resAge.json());
    } catch (e) { console.error("Erro na API", e); }
  };

  useEffect(() => { carregarDados(); }, []);

  // Handlers
  const salvarRecurso = async (e) => {
    e.preventDefault();
    await fetch(`${API_BASE}/Recursos`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ nome: nomeRecurso, especialidade })
    });
    setNomeRecurso(''); setEspecialidade('');
    carregarDados();
  };

  const salvarAgendamento = async (e) => {
    e.preventDefault();
    await fetch(`${API_BASE}/Agendamentos`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ resourceId: recursoSelecionado, clienteNome, dataHora, status: "Confirmado" })
    });
    setClienteNome(''); setDataHora('');
    carregarDados();
    setVisao('agendamentos'); // Navega para a lista após salvar
  };

  return (
    <div style={{ maxWidth: '800px', margin: 'auto', fontFamily: 'Arial' }}>
      <nav style={{ padding: '20px 0', borderBottom: '2px solid #eee', marginBottom: '20px' }}>
        <button onClick={() => setVisao('profissionais')} style={btnStyle(visao === 'profissionais')}>Profissionais</button>
        <button onClick={() => setVisao('agendamentos')} style={btnStyle(visao === 'agendamentos')}>Agendamentos</button>
        <button onClick={() => setVisao('novo-agendamento')} style={btnStyle(visao === 'novo-agendamento')}>Novo Agendamento</button>
      </nav>

      {/* VISÃO: LISTA DE PROFISSIONAIS */}
      {visao === 'profissionais' && (
        <section>
          <h2>Profissionais</h2>
          <form onSubmit={salvarRecurso} style={formStyle}>
            <input placeholder="Nome" value={nomeRecurso} onChange={e => setNomeRecurso(e.target.value)} required />
            <input placeholder="Especialidade" value={especialidade} onChange={e => setEspecialidade(e.target.value)} required />
            <button type="submit">Adicionar Barbeiro</button>
          </form>
          <ul>
            {recursos.map(r => <li key={r.id}><b>{r.nome}</b> - {r.especialidade}</li>)}
          </ul>
        </section>
      )}

      {/* VISÃO: LISTA DE AGENDAMENTOS */}
      {visao === 'agendamentos' && (
        <section>
          <h2>Agendamentos Realizados</h2>
          <table border="1" cellPadding="10" style={{ width: '100%', borderCollapse: 'collapse' }}>
            <thead>
              <tr>
                <th>Cliente</th>
                <th>Barbeiro</th>
                <th>Data/Hora</th>
              </tr>
            </thead>
            <tbody>
              {agendamentos.map(a => (
                <tr key={a.id}>
                  <td>{a.clienteNome}</td>
                  <td>{recursos.find(r => r.id === a.resourceId)?.nome || 'N/A'}</td>
                  <td>{new Date(a.dataHora).toLocaleString()}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>
      )}

      {/* VISÃO: FORMULÁRIO DE AGENDAMENTO */}
      {visao === 'novo-agendamento' && (
        <section>
          <h2>Marcar Horário</h2>
          <form onSubmit={salvarAgendamento} style={formStyle}>
            <select value={recursoSelecionado} onChange={e => setRecursoSelecionado(e.target.value)} required>
              <option value="">Selecione o Barbeiro</option>
              {recursos.map(r => <option key={r.id} value={r.id}>{r.nome}</option>)}
            </select>
            <input placeholder="Seu Nome" value={clienteNome} onChange={e => setClienteNome(e.target.value)} required />
            <input type="datetime-local" value={dataHora} onChange={e => setDataHora(e.target.value)} required />
            <button type="submit">Confirmar Reserva</button>
          </form>
        </section>
      )}
    </div>
  )
}


const btnStyle = (active) => ({
  padding: '10px 20px',
  marginRight: '10px',
  backgroundColor: active ? '#007bff' : '#f8f9fa',
  color: active ? 'white' : 'black',
  border: '1px solid #ddd',
  cursor: 'pointer'
});

const formStyle = { display: 'flex', gap: '10px', marginBottom: '20px' };

export default App