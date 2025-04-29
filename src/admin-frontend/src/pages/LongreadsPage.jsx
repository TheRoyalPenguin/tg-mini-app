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
            <div className="mt-4 space-y-4">
              {longreads.map(longread => (
                <div key={longread.id} className="p-4 border rounded">
                  <h3 className="font-bold">{longread.title}</h3>
                  <p className="text-gray-600">{longread.description}</p>
                  <div className="mt-2 text-sm">
                    <p><span className="font-semibold">ID:</span> {longread.id}</p>
                    <p><span className="font-semibold">HTML контент:</span> {longread.htmlContentKey}</p>
                    <p><span className="font-semibold">DOCX файл:</span> {longread.originalDocxKey}</p>
                    {longread.audioContentKey && (
                      <p><span className="font-semibold">Аудио файл:</span> {longread.audioContentKey}</p>
                    )}
                    {longread.imageKeys.length > 0 && (
                      <p><span className="font-semibold">Изображения:</span> {longread.imageKeys.join(', ')}</p>
                    )}
                  </div>
                </div>
              ))}
            </div>
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
            <div className="mt-4 p-4 border rounded">
              <h3 className="text-xl font-bold mb-2">{selectedLongread.title}</h3>
              <p className="mb-4">{selectedLongread.description}</p>
              <div className="grid grid-cols-2 gap-4 text-sm">
                <div>
                  <p><span className="font-semibold">ID:</span> {selectedLongread.id}</p>
                  <p><span className="font-semibold">ID модуля:</span> {selectedLongread.moduleId}</p>
                </div>
                <div>
                  <p><span className="font-semibold">HTML контент:</span> {selectedLongread.htmlContentKey}</p>
                  <p><span className="font-semibold">DOCX файл:</span> {selectedLongread.originalDocxKey}</p>
                </div>
                {selectedLongread.audioContentKey && (
                  <p className="col-span-2">
                    <span className="font-semibold">Аудио файл:</span> {selectedLongread.audioContentKey}
                  </p>
                )}
                {selectedLongread.imageKeys.length > 0 && (
                  <div className="col-span-2">
                    <p className="font-semibold">Изображения:</p>
                    <ul className="list-disc pl-5">
                      {selectedLongread.imageKeys.map((img, index) => (
                        <li key={index}>{img}</li>
                      ))}
                    </ul>
                  </div>
                )}
              </div>
            </div>
          )}
        </div>
      )}

      {/* Остальные формы (create, update, delete) остаются без изменений */}
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