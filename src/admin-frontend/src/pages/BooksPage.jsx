import React, { useState } from 'react';

export default function BooksPage() {
  const [action, setAction] = useState('getAll');
  const [moduleId, setModuleId] = useState('');
  const [courseId, setCourseId] = useState('');
  const [bookId, setBookId] = useState('');
  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [contentFile, setContentFile] = useState(null);
  const [coverFile, setCoverFile] = useState(null);
  const [books, setBooks] = useState([]);
  const [selectedBook, setSelectedBook] = useState(null);
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);

  const handleGetAllBooks = async () => {
    if (!moduleId) return setMessage('Введите ID модуля');
    setLoading(true);
    try {
      const res = await fetch(`http://localhost:5000/api/admin/modules/${moduleId}/books`);
      const data = await res.json();
      setBooks(data);
      setMessage(`Найдено ${data.length} книг`);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleGetBook = async () => {
    if (!bookId) return setMessage('Введите ID книги');
    setLoading(true);
    try {
      const res = await fetch(`http://localhost:5000/api/admin/books/${bookId}`);
      const data = await res.json();
      setSelectedBook(data);
      setMessage('Книга получена');
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleCreateBook = async () => {
    if (!courseId || !moduleId || !title || !author || !contentFile) {
      return setMessage('Заполните все поля');
    }
    
    setLoading(true);
    const formData = new FormData();
    formData.append('Title', title);
    formData.append('Author', author);
    formData.append('ContentFile', contentFile);
    if (coverFile) formData.append('CoverFile', coverFile);

    try {
      const res = await fetch(`http://localhost:5000/api/admin/course/${courseId}/modules/${moduleId}/books`, {
        method: 'POST',
        body: formData,
      });
      const data = await res.json();
      setMessage(`Книга создана! ID: ${data.id}`);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleUpdateBook = async () => {
    if (!courseId || !moduleId || !bookId || !title || !author) {
      return setMessage('Заполните все поля');
    }
    
    setLoading(true);
    const formData = new FormData();
    formData.append('Title', title);
    formData.append('Author', author);
    if (contentFile) formData.append('ContentFile', contentFile);
    if (coverFile) formData.append('CoverFile', coverFile);

    try {
      await fetch(`http://localhost:5000/api/admin/courses/${courseId}/modules/${moduleId}/books/${bookId}`, {
        method: 'PUT',
        body: formData,
      });
      setMessage('Книга обновлена');
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteBook = async () => {
    if (!bookId) return setMessage('Введите ID книги');
    setLoading(true);
    try {
      await fetch(`http://localhost:5000/api/admin/books/${bookId}`, { method: 'DELETE' });
      setMessage('Книга удалена');
      setSelectedBook(null);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Управление книгами</h1>
      
      <select 
        value={action} 
        onChange={(e) => setAction(e.target.value)}
        className="border p-2 mb-4"
      >
        <option value="getAll">Получить книги модуля</option>
        <option value="get">Получить книгу по ID</option>
        <option value="create">Создать книгу</option>
        <option value="update">Обновить книгу</option>
        <option value="delete">Удалить книгу</option>
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
            onClick={handleGetAllBooks} 
            disabled={loading}
            className="bg-blue-500 text-white p-2"
          >
            {loading ? 'Загрузка...' : 'Получить'}
          </button>
          {books.length > 0 && (
            <ul className="mt-2">
              {books.map(book => (
                <li key={book.id}>{book.title} (ID: {book.id})</li>
              ))}
            </ul>
          )}
        </div>
      )}

      {action === 'get' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID книги"
            value={bookId}
            onChange={(e) => setBookId(e.target.value)}
            className="border p-2 mr-2"
          />
          <button 
            onClick={handleGetBook} 
            disabled={loading}
            className="bg-blue-500 text-white p-2"
          >
            {loading ? 'Загрузка...' : 'Получить'}
          </button>
          {selectedBook && (
            <div className="mt-2 p-2 border">
              <p>Название: {selectedBook.title}</p>
              <p>Автор: {selectedBook.author}</p>
            </div>
          )}
        </div>
      )}

      {action === 'create' && (
        <div className="mb-4">
          <input
            type="text"
            placeholder="ID курса"
            value={courseId}
            onChange={(e) => setCourseId(e.target.value)}
            className="border p-2 mb-2 block"
          />
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
            placeholder="Автор"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <p>книга:</p>
          <input
            type="file"
            onChange={(e) => setContentFile(e.target.files[0])}
            className="mb-2 block"
          />
          <p>обложка:</p>
          <input
            type="file"
            onChange={(e) => setCoverFile(e.target.files[0])}
            className="mb-2 block"
          />
          <button 
            onClick={handleCreateBook} 
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
            placeholder="ID курса"
            value={courseId}
            onChange={(e) => setCourseId(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="ID модуля"
            value={moduleId}
            onChange={(e) => setModuleId(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <input
            type="text"
            placeholder="ID книги"
            value={bookId}
            onChange={(e) => setBookId(e.target.value)}
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
            placeholder="Новый автор"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
            className="border p-2 mb-2 block"
          />
          <p>книга:</p>
          <input
            type="file"
            onChange={(e) => setContentFile(e.target.files[0])}
            className="mb-2 block"
          />
          <input
            type="file"
            onChange={(e) => setCoverFile(e.target.files[0])}
            className="mb-2 block"
          />
          <button 
            onClick={handleUpdateBook} 
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
            placeholder="ID книги"
            value={bookId}
            onChange={(e) => setBookId(e.target.value)}
            className="border p-2 mr-2"
          />
          <button 
            onClick={handleDeleteBook} 
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