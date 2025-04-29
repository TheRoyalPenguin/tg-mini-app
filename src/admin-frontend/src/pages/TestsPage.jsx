import React, { useState } from 'react';

export default function QuestionsPage() {
  const [action, setAction] = useState('getAll');
  const [courseId, setCourseId] = useState('');
  const [moduleId, setModuleId] = useState('');
  const [questionsList, setQuestionsList] = useState([
    {
      question: '',
      options: ['', '', '', ''],
      correctAnswer: 0
    }
  ]);
  const [questions, setQuestions] = useState([]);
  const [message, setMessage] = useState('');
  const [loading, setLoading] = useState(false);

  const handleGetAllQuestions = async () => {
    if (!courseId || !moduleId) return setMessage('Введите ID курса и модуля');
    setLoading(true);
    try {
      const res = await fetch(`/api/courses/${courseId}/modules/${moduleId}/questions`);
      const data = await res.json();
      setQuestions(data);
      setMessage(`Найдено ${data.length} вопросов`);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleAddQuestions = async () => {
    if (!courseId || !moduleId || 
        questionsList.some(q => !q.question || q.options.some(opt => !opt))) {
      return setMessage('Заполните все поля');
    }
    
    setLoading(true);
    try {
      const res = await fetch(`/api/courses/${courseId}/modules/${moduleId}/questions`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(questionsList.map(q => ({
          Question: q.question,
          Options: q.options,
          CorrectAnswer: q.correctAnswer
        }))),
      });
      const data = await res.json();
      setMessage(`Добавлено ${data.length} вопросов`);
      setQuestionsList([{
        question: '',
        options: ['', '', '', ''],
        correctAnswer: 0
      }]);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteQuestions = async () => {
    if (!courseId || !moduleId) return setMessage('Введите ID курса и модуля');
    setLoading(true);
    try {
      await fetch(`/api/courses/${courseId}/modules/${moduleId}/questions`, {
        method: 'DELETE',
      });
      setMessage('Все вопросы модуля удалены');
      setQuestions([]);
    } catch (err) {
      setMessage('Ошибка: ' + err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleQuestionChange = (index, field, value) => {
    const newQuestions = [...questionsList];
    newQuestions[index][field] = value;
    setQuestionsList(newQuestions);
  };

  const handleOptionChange = (qIndex, optIndex, value) => {
    const newQuestions = [...questionsList];
    newQuestions[qIndex].options[optIndex] = value;
    setQuestionsList(newQuestions);
  };

  const addQuestionForm = () => {
    setQuestionsList([...questionsList, {
      question: '',
      options: ['', '', '', ''],
      correctAnswer: 0
    }]);
  };

  const removeQuestionForm = (index) => {
    if (questionsList.length <= 1) return;
    const newQuestions = [...questionsList];
    newQuestions.splice(index, 1);
    setQuestionsList(newQuestions);
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Управление вопросами</h1>
      
      <select 
        value={action} 
        onChange={(e) => setAction(e.target.value)}
        className="border p-2 mb-4"
      >
        <option value="getAll">Получить вопросы модуля</option>
        <option value="add">Добавить вопросы</option>
        <option value="delete">Удалить все вопросы</option>
      </select>

      <div className="mb-4">
        <input
          type="text"
          placeholder="ID курса"
          value={courseId}
          onChange={(e) => setCourseId(e.target.value)}
          className="border p-2 mb-2"
        />
        <input
          type="text"
          placeholder="ID модуля"
          value={moduleId}
          onChange={(e) => setModuleId(e.target.value)}
          className="border p-2 mb-2"
        />
      </div>

      {action === 'getAll' && (
        <div>
          <button 
            onClick={handleGetAllQuestions} 
            disabled={loading}
            className="bg-blue-500 text-white p-2 mb-2"
          >
            {loading ? 'Загрузка...' : 'Получить вопросы'}
          </button>
          {questions.length > 0 && (
            <ul className="mt-2">
              {questions.map((q, index) => (
                <li key={index} className="mb-4 p-2 border">
                  <p>Вопрос: {q.question}</p>
                  <p>Варианты: {q.options.join(', ')}</p>
                  <p>Правильный ответ: {q.options[q.correctAnswer]}</p>
                </li>
              ))}
            </ul>
          )}
        </div>
      )}

      {action === 'add' && (
        <div>
          {questionsList.map((q, qIndex) => (
            <div key={qIndex} className="mb-6 p-4 border rounded">
              <div className="flex justify-between mb-2">
                <h3 className="font-bold">Вопрос {qIndex + 1}</h3>
                <button 
                  onClick={() => removeQuestionForm(qIndex)}
                  className="text-red-500"
                >
                  Удалить
                </button>
              </div>
              <input
                type="text"
                placeholder="Текст вопроса"
                value={q.question}
                onChange={(e) => handleQuestionChange(qIndex, 'question', e.target.value)}
                className="border p-2 mb-2 w-full"
              />
              {q.options.map((opt, optIndex) => (
                <div key={optIndex} className="mb-2 flex items-center">
                  <input
                    type="text"
                    placeholder={`Вариант ${optIndex + 1}`}
                    value={opt}
                    onChange={(e) => handleOptionChange(qIndex, optIndex, e.target.value)}
                    className="border p-2 flex-1"
                  />
                  <input
                    type="radio"
                    name={`correctAnswer-${qIndex}`}
                    checked={q.correctAnswer === optIndex}
                    onChange={() => handleQuestionChange(qIndex, 'correctAnswer', optIndex)}
                    className="ml-2"
                  />
                </div>
              ))}
            </div>
          ))}
          <button
            onClick={addQuestionForm}
            className="bg-gray-200 p-2 mb-2"
          >
            + Добавить еще вопрос
          </button>
          <button 
            onClick={handleAddQuestions} 
            disabled={loading}
            className="bg-green-500 text-white p-2 block w-full"
          >
            {loading ? 'Добавление...' : 'Добавить все вопросы'}
          </button>
        </div>
      )}

      {action === 'delete' && (
        <div>
          <button 
            onClick={handleDeleteQuestions} 
            disabled={loading}
            className="bg-red-500 text-white p-2"
          >
            {loading ? 'Удаление...' : 'Удалить все вопросы'}
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