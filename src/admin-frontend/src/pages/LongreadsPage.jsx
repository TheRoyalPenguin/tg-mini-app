import React, { useState } from 'react';

export default function LongreadsPage() {
  const [action, setAction] = useState('getAll');
  const [moduleId, setModuleId] = useState('');
  const [longreadId, setLongreadId] = useState('');
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [docxFile, setDocxFile] = useState(null);
  const [audioFile, setAudioFile] = useState(null);
  const [longreads, setLongreads] = useState([]);
  const [selectedLongread, setSelectedLongread] = useState(null);
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);

  const handleGetAllLongreads = async () => {
    if (!moduleId) return setMessage('Введите ID модуля');
    setLoading(true);
    try {
      const res = await fetch(`http://localhost:5000/api/admin/modules/${moduleId}/longreads`);
      const data = await res.json();
      setLongreads(data);
      setMessage(`Найдено ${data.length} лонгридов`);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleGetLongread = async () => {
    if (!longreadId) return setMessage('Введите ID лонгрида');
    setLoading(true);
    try {
      const res = await fetch(`http://localhost:5000/api/admin/longreads/${longreadId}`);
      const data = await res.json();
      setSelectedLongread(data);
      setMessage('Лонгрид получен');
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleCreateLongread = async () => {
    if (!moduleId || !title || !description || !docxFile) {
      return setMessage('Заполните все поля');
    }
    
    setLoading(true);
    const formData = new FormData();
    formData.append('Title', title);
    formData.append('Description', description);
    formData.append('DocxFile', docxFile);
    if (audioFile) formData.append('AudioFile', audioFile);

    try {
      const res = await fetch(`http://localhost:5000/api/admin/modules/${moduleId}/longreads`, {
        method: 'POST',
        body: formData,
      });
      const data = await res.json();
      setMessage(`Лонгрид создан! ID: ${data.id}`);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleUpdateLongread = async () => {
    if (!longreadId || !title || !description || !docxFile) {
      return setMessage('Заполните все поля');
    }
    
    setLoading(true);
    const formData = new FormData();
    formData.append('Id', longreadId);
    formData.append('Title', title);
    formData.append('Description', description);
    formData.append('DocxFile', docxFile);

    try {
      await fetch(`http://localhost:5000/api/admin/longreads`, {
        method: 'PUT',
        body: formData,
      });
      setMessage('Лонгрид обновлен');
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteLongread = async () => {
    if (!longreadId) return setMessage('Введите ID лонгрида');
    setLoading(true);
    try {
      await fetch(`http://localhost:5000/api/admin/longreads/${longreadId}`, { method: 'DELETE' });
      setMessage('Лонгрид удален');
      setSelectedLongread(null);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Управление лонгридами</h1>
      
      <select 
        value={action} 
        onChange={(e) => setAction(e.target.value)}
        className="border p-2 mb-4"
      >
        <option value="getAll">Получить лонгриды модуля</option>
        <option value="get">Получить лонгрид по ID</option>
        <option value="create">Создать лонгрид</option>
        <option value="update">Обновить лонгрид</option>
        <option value="delete">Удалить лонгрид</option>
      </select>

      {action === 'getAll' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID модуля"
            value={moduleId}
            onChange={(e) => setModuleId(e.target.value)}
            className="border p-2 mr-2"
          />
          <button 
            onClick={handleGetAllLongreads} 
            disabled={loading}
            className="bg-blue-500 text-white p-2"
          >
            {loading ? 'Загрузка...' : 'Получить'}
          </button>
          {longreads.length > 0 && (
            <ul className="mt-2">
              {longreads.map(longread => (
                <li key={longread.id}>{longread.title} (ID: {longread.id})</li>
              ))}
            </ul>
          )}
        </div>
      )}

      {action === 'get' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID лонгрида"
            value={longreadId}
            onChange={(e) => setLongreadId(e.target.value)}
            className="border p-2 mr-2"
          />
          <button 
            onClick={handleGetLongread} 
            disabled={loading}
            className="bg-blue-500 text-white p-2"
          >
            {loading ? 'Загрузка...' : 'Получить'}
          </button>
          {selectedLongread && (
            <div className="mt-2 p-2 border">
              <p>Название: {selectedLongread.title}</p>
              <p>Описание: {selectedLongread.description}</p>
            </div>
          )}
        </div>
      )}

      {action === 'create' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID модуля"
            value={moduleId}
            onChange={(e) => setModuleId(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="Название"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="Описание"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <p>DOCX файл:</p>
          <input
            type="file"
            onChange={(e) => setDocxFile(e.target.files[0])}
            className="mb-2 block"
          />
          <p>Аудио файл (необязательно):</p>
          <input
            type="file"
            onChange={(e) => setAudioFile(e.target.files[0])}
            className="mb-2 block"
          />
          <button 
            onClick={handleCreateLongread} 
            disabled={loading}
            className="bg-green-500 text-white p-2"
          >
            {loading ? 'Создание...' : 'Создать'}
          </button>
        </div>
      )}

      {action === 'update' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID лонгрида"
            value={longreadId}
            onChange={(e) => setLongreadId(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="Новое название"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="Новое описание"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <p>DOCX файл:</p>
          <input
            type="file"
            onChange={(e) => setDocxFile(e.target.files[0])}
            className="mb-2 block"
          />
          <button 
            onClick={handleUpdateLongread} 
            disabled={loading}
            className="bg-yellow-500 text-white p-2"
          >
            {loading ? 'Обновление...' : 'Обновить'}
          </button>
        </div>
      )}

      {action === 'delete' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID лонгрида"
            value={longreadId}
            onChange={(e) => setLongreadId(e.target.value)}
            className="border p-2 mr-2"
          />
          <button 
            onClick={handleDeleteLongread} 
            disabled={loading}
            className="bg-red-500 text-white p-2"
          >
            {loading ? 'Удаление...' : 'Удалить'}
          </button>
        </div>
      )}

      {message && (
        <div className={`p-2 mt-2 ${message.includes('Ошибка') ? 'bg-red-100' : 'bg-green-100'}`}>
          {message}
        </div>
      )}
    </div>
  );
}